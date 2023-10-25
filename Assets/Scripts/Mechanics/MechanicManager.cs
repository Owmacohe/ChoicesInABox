using System;
using UnityEngine;

public class MechanicManager : MonoBehaviour
{
    Type[] mechanicInitializationOrder = new[]
    {
        typeof(CanWinLose),
        typeof(Exploration),
        typeof(Entities),
        typeof(PopulationModification)
    };
    
    void Start()
    {
        Initialize(0, 0, 0);
    }

    public void Initialize(float ratio, float playerSaturation, float systemSaturation)
    {
        // TODO: convert data into a list of mechanic patterns and add them as components
        // TODO: configure initial mechanic data member states
        // TODO: make sure they're initialized in the right order using mechanicInitializationOrder
        
        AddMechanic<CanWinLose>().Initialize(null, null, null);
        AddMechanic<Exploration>().Initialize(ExplorationType.Bounded);
        AddMechanic<Entities>();
        AddMechanic<PopulationModification>();
    }

    T AddMechanic<T>() where T : MechanicPattern
    {
        return gameObject.AddComponent<T>();
    }

    public void SendEvent(MechanicEvent evt, Vector2 position = new(), GameObject clicked = null, string keyName = "")
    {
        foreach (var i in GetComponents<MechanicPattern>())
        {
            if (!i.Paused)
            {
                switch (evt)
                {
                    case MechanicEvent.Move:
                        i.Move(position);
                        break;
                    
                    case MechanicEvent.Mouse:
                        i.Mouse(position);
                        break;
                    
                    case MechanicEvent.ClickDown:
                        i.ClickDown(position, clicked);
                        break;
                
                    case MechanicEvent.ClickUp:
                        i.ClickUp(position, clicked);
                        break;
                    
                    case MechanicEvent.KeyDown:
                        i.KeyDown(keyName);
                        break;
                
                    case MechanicEvent.KeyUp:
                        i.KeyUp(keyName);
                        break;
                
                    case MechanicEvent.Pause:
                        i.Pause();
                        break;
                }   
            }
            else if (evt.Equals(MechanicEvent.Resume)) i.Resume();
        }
    }
}