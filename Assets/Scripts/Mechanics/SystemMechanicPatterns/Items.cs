using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum ItemType { Health, Speed, Armour, Damage }

[MechanicRequirements(typeof(Inventory), typeof(Bounding))]
public class Items : SystemMechanicPattern
{
    [HideInInspector] public Inventory Inventory;

    Bounding bounds;
    bool spawnRandomly;
    [HideInInspector] public List<Item> ItemsList;

    public override void Initialize(params object[] args)
    {
        spawnRandomly = (bool)args[0];
        
        Inventory = GetComponent<Inventory>();
        bounds = GetComponent<Bounding>();
        
        ItemsList = new List<Item>();
    }

    void FixedUpdate()
    {
        if (spawnRandomly && ItemsList.Count < 3 && Random.value <= 0.01f)
        {
            var itemTypes = Enum.GetValues(typeof(ItemType));
            ItemType itemType = (ItemType)itemTypes.GetValue(Random.Range(0, itemTypes.Length));
            
            SpawnItem(bounds.GetRandomWithinBounds(), itemType + " potion", itemType);
        }
    }

    void SpawnItem(Vector2 position, string name, ItemType type)
    {
        Item temp = Instantiate(
            Resources.Load<GameObject>("Item"),
            position,
            Quaternion.identity).GetComponent<Item>();

        temp.Initialize(this, name, type);
        
        ItemsList.Add(temp);
    }
    
    public override string ToString()
    {
        return base.ToString() + (spawnRandomly ? " (spawn randomly)" : "");
    }
}