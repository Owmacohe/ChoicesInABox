using System;
using System.Collections.Generic;
using System.Linq;
using NiEngine;
using TMPro;
using UnityEngine;

public class Inventory : MechanicPattern
{
    List<string> itemNames;
    List<ItemType> itemTypes;

    bool open;
    ReactionStateMachine rsm;
    Transform listGrid;
    
    public override void Initialize(params object[] args)
    {
        itemNames = new List<string>();
        itemTypes = new List<ItemType>();

        foreach (var i in args)
        {
            var temp = (KeyValuePair<string, ItemType>) i;
            
            AddToInventory(temp.Key, temp.Value);
        }
        
        rsm = Instantiate(Resources.Load<GameObject>("InventoryCanvas")).GetComponent<ReactionStateMachine>();
        listGrid = rsm.transform.GetChild(1);

        MechanicManager.AddToControls("I", "inventory");
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
    
    public void AddToInventory(string item, ItemType type)
    {
        itemNames.Add(item);
        itemTypes.Add(type);

        Instantiate(Resources.Load<GameObject>("InventoryItem"), listGrid)
            .GetComponentInChildren<TMP_Text>().text = item;
    }

    public void RemoveFromInventory(string item)
    {
        int index = itemNames.IndexOf(item);
            
        itemNames.Remove(item); 
        itemTypes.RemoveAt(index);
        
        DestroyImmediate(listGrid.GetChild(index).gameObject);
    }
}