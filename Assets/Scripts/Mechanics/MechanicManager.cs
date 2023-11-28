using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MechanicManager : MonoBehaviour
{
    [SerializeField] TMP_Text mechanicsText;
    
    Type[] mechanicInitializationOrder = {
	    typeof(CanWinLose),
	    typeof(Bounding),
	    typeof(Entities),
	    typeof(Exploration),
	    typeof(PopulationModification),
	    typeof(Inventory),
	    typeof(PlayerUpgrades),
	    typeof(Items),
	    typeof(Combat)
	};

    List<Type> mechanicsToInitialize = new List<Type>();

    List<MechanicPattern> mechanics = new List<MechanicPattern>();

    Slider ratioSlider, saturationSlider;

    void Start()
    {
        ratioSlider = GameObject.FindWithTag("Ratio").GetComponent<Slider>();
        saturationSlider = GameObject.FindWithTag("Saturation").GetComponent<Slider>();
        
        Initialize(
            ratioSlider.GetComponentInChildren<Toggle>().isOn ? ratioSlider.value : Random.Range(0f, 1f),
            saturationSlider.GetComponentInChildren<Toggle>().isOn ? saturationSlider.value : Random.Range(0f, 1f)
        );
    }

    public void Initialize(float ratio, float saturation) // 0..1, 0..1
    {
        ratioSlider.value = ratio;
        saturationSlider.value = saturation;
        
        Debug.Log("<b>Input:</b> ratio=" + ratio + " saturation=" + saturation);
        
        const int totalPlayerMechanics = 5;
        const int totalSystemMechanics = 4;

        int numMechanics = (int)((totalPlayerMechanics + totalSystemMechanics) * saturation);
        int numPlayerMechanics = (int)(numMechanics * ratio);
        int numSystemMechanics = numMechanics - numPlayerMechanics;

        var playerMechanics = Assembly
            .GetAssembly(typeof(PlayerMechanicPattern))
            .GetTypes()
            .Where(type => type.IsSubclassOf(typeof(PlayerMechanicPattern)))
            .ToList();
        
        var systemMechanics = Assembly
            .GetAssembly(typeof(SystemMechanicPattern))
            .GetTypes()
            .Where(type => type.IsSubclassOf(typeof(SystemMechanicPattern)))
            .ToList();
        
        for (int i = 0; i < numPlayerMechanics; i++)
        {
            var temp = GetMechanic(playerMechanics);
            
            if (MechanicsContainedEntirely(playerMechanics)) break;
            
            while (mechanicsToInitialize.Contains(temp))
                temp = GetMechanic(playerMechanics);

            mechanicsToInitialize.Add(temp);
        }
        
        for (int j = 0; j < numSystemMechanics; j++)
        {
            var temp = GetMechanic(systemMechanics);

            if (MechanicsContainedEntirely(systemMechanics)) break;
            
            while (mechanicsToInitialize.Contains(temp))
                temp = GetMechanic(systemMechanics);

            mechanicsToInitialize.Add(temp);
        }

        for (int k = 0; k < mechanicsToInitialize.Count; k++)
        {
            MechanicRequirementsAttribute requirements =
                mechanicsToInitialize[k].GetCustomAttribute<MechanicRequirementsAttribute>();
            
            if (requirements != null)
                foreach (var l in requirements.Mechanics)
                    mechanicsToInitialize.Add(l);
        }

        string log = "<b>Mechanics:</b>";

        foreach (var m in mechanicInitializationOrder)
        {
            if (mechanicsToInitialize.Contains(m))
            {
                MechanicPattern temp = (MechanicPattern) gameObject.AddComponent(m);

                switch (m.Name)
                {
                    case "Bounding":
                        temp.Initialize(ratio < 0.5f ? BoundingType.Bounded : BoundingType.Free);
                        break;
                    case "Entities":
                        temp.Initialize(ratio < 0.5f);
                        break;
                    case "Items":
                        temp.Initialize(ratio < 0.5f);
                        break;
                    case "Combat":
                        temp.Initialize(ratio < 0.5f);
                        break;
                    default:
                        temp.Initialize();
                        break;
                }
                
                log += " " + temp;
                mechanicsText.text += temp + "\n";
                
                mechanics.Add(temp);
            }
        }
        
        Debug.Log(log);
    }

    Type GetMechanic(List<Type> lst)
    {
        return lst[Random.Range(0, lst.Count)];
    }

    bool MechanicsContainedEntirely(List<Type> lst)
    {
        foreach (var i in lst)
            if (!mechanicsToInitialize.Contains(i))
                return false;

        return true;
    }

    T AddMechanic<T>() where T : MechanicPattern
    {
        T temp = GetComponent<T>();
        
        if (temp == null)
        {
            temp = gameObject.AddComponent<T>();
            mechanics.Add(temp);
        }

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