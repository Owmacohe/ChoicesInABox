using System;
using System.Collections.Generic;
using NiEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgrades : MechanicPattern
{
    public List<string> UpgradeList;
    public List<bool> UpgradeCompletion;

    bool open;
    ReactionStateMachine rsm;
    Transform listGrid;

    public override void Initialize(params object[] args)
    {
        UpgradeList = new List<string>();
        UpgradeCompletion = new List<bool>();
        
        for (int i = 0; i < args.Length; i++)
        {
            UpgradeList.Add((string)args[i]);
            UpgradeCompletion.Add(false);
            
            GameObject temp = Instantiate(Resources.Load<GameObject>("UpgradesItem"), listGrid); 
            temp.GetComponentInChildren<TMP_Text>().text = (string)args[i];

            var copy = i;
            temp.GetComponent<Button>().onClick.AddListener(() =>
            {
                CompleteUpgrade(copy);
            });
        }
        
        rsm = Instantiate(Resources.Load<GameObject>("UpgradesCanvas")).GetComponent<ReactionStateMachine>();
        listGrid = rsm.transform.GetChild(1);
        
        MechanicManager.AddToControls("Tab", "player upgrades");
    }

    public override void KeyDown(string keyName)
    {
        if (keyName == "tab")
        {
            open = !open;
            
            if (open) MechanicManager.PauseExcept(this);
            else MechanicManager.SendEvent(MechanicEvent.Resume);
            
            rsm.ForceActivateState(rsm.IsStateActive("Out") ? "In" : "Out");
        }
    }

    public void CompleteUpgrade(int upgradeIndex)
    {
        // TODO: add a cost for completion
        
        if (!UpgradeCompletion[upgradeIndex])
        {
            UpgradeCompletion[upgradeIndex] = true;

            Transform temp = listGrid.GetChild(upgradeIndex);
            temp.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.5f);
            temp.GetComponentInChildren<TMP_Text>().text += " [COMPLETED]";
            temp.GetComponent<Button>().enabled = false;

            // TODO: get some bonus for upgrading
        }
    }
}