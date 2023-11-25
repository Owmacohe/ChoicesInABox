using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum ItemType { Health, Speed, Armour, Damage }

public class Items : MechanicPattern
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
            SpawnItem(bounds.GetRandomWithinBounds(), "Health potion", ItemType.Health);
    }

    public void SpawnItem(Vector2 position, string name, ItemType type)
    {
        Item temp = Instantiate(
            Resources.Load<GameObject>("Item"),
            position,
            Quaternion.identity).GetComponent<Item>();

        temp.Initialize(this, name, type);
        
        ItemsList.Add(temp);
    }
}