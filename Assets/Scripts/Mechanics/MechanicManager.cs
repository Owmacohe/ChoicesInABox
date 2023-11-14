using System;
using TMPro;
using UnityEngine;

public class MechanicManager : MonoBehaviour
{
    Type[] mechanicInitializationOrder = {
        typeof(CanWinLose),
        typeof(Bounding),
        typeof(Entities),
        typeof(Exploration),
        typeof(PopulationModification),
        typeof(Inventory),
        typeof(PlayerUpgrades)
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
        AddMechanic<Bounding>().Initialize(BoundingType.Bounded);
        AddMechanic<Entities>();
        AddMechanic<Exploration>();
        AddMechanic<PopulationModification>();
        AddMechanic<Inventory>().Initialize("Item1", "Item2", "Item3");
        AddMechanic<PlayerUpgrades>().Initialize("Upgrade1", "Upgrade2", "Upgrade3");
    }

    public T AddMechanic<T>() where T : MechanicPattern
    {
        T temp = GetComponent<T>();
        if (temp == null) temp = gameObject.AddComponent<T>();
        
        return temp;
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

    public void PauseExcept(MechanicPattern pattern)
    {
        foreach (var i in GetComponents<MechanicPattern>())
            if (!i.Equals(pattern))
                i.Pause();
    }

    public void AddToControls(string control)
    {
        TMP_Text controls = GameObject.FindWithTag("Controls").GetComponent<TMP_Text>();
        if (controls.text.Length > 0) controls.text += "\n";
        controls.text += control;
    }
}