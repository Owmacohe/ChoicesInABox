using System;
using UnityEngine;

public class MechanicManager : MonoBehaviour
{
    void Start()
    {
        AddMechanic<EntitiesPattern>();
        AddMechanic<PopulationModification>();
    }

    public void Initialize(float ratio, float playerSaturation, float systemSaturation)
    {
        // TODO: convert data into a list of mechanic patterns and add them as components
    }

    void AddMechanic<T>() where T : MechanicPattern
    {
        gameObject.AddComponent<T>();
    }

    public void SendEvent(MechanicEvent evt, Vector2 position = new(), GameObject clicked = null, string keyName = "")
    {
        foreach (var i in GetComponents<MechanicPattern>())
        {
            switch (evt)
            {
                case MechanicEvent.Move:
                    i.Move();
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
            }   
        }
    }
}