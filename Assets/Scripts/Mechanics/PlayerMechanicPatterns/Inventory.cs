using System;
using System.Collections.Generic;
using NiEngine;
using TMPro;
using UnityEngine;

public class Inventory : PlayerMechanicPattern, IInitializableMechanic
{
    public List<string> ItemList;

    bool open;
    ReactionStateMachine rsm;
    Transform listGrid;
    
    public void Initialize(params object[] args)
    {
        rsm = Instantiate(Resources.Load<GameObject>("InventoryCanvas")).GetComponent<ReactionStateMachine>();
        listGrid = rsm.transform.GetChild(1);

        MechanicManager.AddToControls("<i>[I]</i>: inventory");
        
        ItemList = new List<string>();
        
        foreach (var i in args)
            AddToInventory((string)i);
    }

    public override void KeyDown(string keyName)
    {
        if (keyName == "i")
        {
            open = !open;
            
            if (open) MechanicManager.PauseExcept(this);
            else MechanicManager.SendEvent(MechanicEvent.Resume);
            
            rsm.ForceActivateState(rsm.IsStateActive("Out") ? "In" : "Out");
        }
    }
    
    public void AddToInventory(string item)
    {
        ItemList.Add(item);

        Instantiate(Resources.Load<GameObject>("InventoryItem"), listGrid)
            .GetComponentInChildren<TMP_Text>().text = item;
    }

    public void RemoveFromInventory(string item)
    {
        RemoveFromInventory(ItemList.IndexOf(item));
    }
    
    public void RemoveFromInventory(int itemIndex)
    {
        ItemList.RemoveAt(itemIndex);
        
        DestroyImmediate(listGrid.GetChild(itemIndex).gameObject);
    }
}