using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class MechanicManager : MonoBehaviour
{
    Type[] mechanicInitializationOrder = { };
    
    void Start()
    {
        Initialize(0, 0, 0);
    }

    public void Initialize(float ratio, float playerSaturation, float systemSaturation)
    {
        // TODO: convert data into a list of mechanic patterns and add them as components
        // TODO: configure initial mechanic data member states
        // TODO: make sure they're initialized in the right order using mechanicInitializationOrder
        
        AddMechanic<CanWinLose>(true).Initialize();
        AddMechanic<Bounding>(true).Initialize(BoundingType.Bounded);
        AddMechanic<Entities>(true).Initialize();
        AddMechanic<Exploration>(true).Initialize(GameObject.FindWithTag("Stats").GetComponent<TMP_Text>());
        AddMechanic<PopulationModification>(true).Initialize();
        AddMechanic<Inventory>(true).Initialize();
        AddMechanic<PlayerUpgrades>(true).Initialize();
        AddMechanic<Items>(true).Initialize(true);
        AddMechanic<Combat>(true).Initialize(true);
    }

    public T AddMechanic<T>(bool fromManager = false) where T : MechanicPattern
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

    public void AddToControls(string key, string action)
    {
        TMP_Text controls = GameObject.FindWithTag("Controls").GetComponent<TMP_Text>();
        if (controls.text.Length > 0) controls.text += "\n";
        controls.text += "<i>[" + key + "]</i>: " + action;
    }
}