using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    Items items;
    string name;
    ItemType type;

    public void Initialize(Items i, string n, ItemType t)
    {
        items = i;
        name = n;
        type = t;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Entity temp = other.GetComponent<Entity>();

        if (temp != null && !temp.Autonomous)
        {
            items.Inventory.AddToInventory(name, type);
            items.ItemsList.Remove(this);
            Destroy(gameObject);
        }
    }
}