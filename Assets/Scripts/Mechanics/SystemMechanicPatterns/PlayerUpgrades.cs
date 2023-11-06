using System.Collections.Generic;
using NiEngine;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerUpgrades : PlayerMechanicPattern, IInitializableMechanic
{
    public List<string> UpgradeList;
    public List<bool> UpgradeCompletion;
    
    ReactionStateMachine rsm;
    Transform upgradeList;

    void Start()
    {
        rsm = Instantiate(Resources.Load<GameObject>("UpgradesCanvas")).GetComponent<ReactionStateMachine>();
        upgradeList = rsm.transform.GetChild(1);

        // TODO: add [press Tab] popup
    }

    public void Initialize(params object[] args)
    {
        UpgradeList = new List<string>();
        UpgradeCompletion = new List<bool>();
        
        foreach (var i in args)
        {
            UpgradeList.Add((string)i);
            UpgradeCompletion.Add(false);
        }
    }

    public override void KeyDown(string keyName)
    {
        if (keyName == "tab") rsm.ForceActivateState(rsm.IsStateActive("Out") ? "In" : "Out");
    }

    public void CompleteUpgrade(int upgradeIndex) // TODO: call this at some point
    {
        // TODO: add a threshold for completion
        
        if (!UpgradeCompletion[upgradeIndex])
        {
            UpgradeCompletion[upgradeIndex] = true;

            Transform temp = upgradeList.GetChild(upgradeIndex);
            temp.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.5f);
            temp.GetComponentInChildren<TMP_Text>().text += " [COMPLETED]";
            
            // TODO: get some bonus for upgrading
        }
    }
}