using System;
using System.Collections.Generic;
using NiEngine;
using TMPro;
using UnityEngine;

public class Inventory : PlayerMechanicPattern
{
    public List<string> ItemList;
    
    ReactionStateMachine rsm;
    Transform listGrid;

    void Start()
    {
        rsm = Instantiate(Resources.Load<GameObject>("InventoryCanvas")).GetComponent<ReactionStateMachine>();
        listGrid = rsm.transform.GetChild(1);

        ItemList = new List<string>();
        
        // TODO: add [press I] popup
    }

    public override void KeyDown(string keyName)
    {
        if (keyName == "i") rsm.ForceActivateState(rsm.IsStateActive("Out") ? "In" : "Out");
    }
    
    public void AddToInventory(string item)
    {
        ItemList.Add(item);

        Instantiate(Resources.Load<GameObject>("UIItem"), listGrid)
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