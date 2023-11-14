using System;
using System.Collections.Generic;
using UnityEngine;

public class Entities : SystemMechanicPattern
{
    [HideInInspector] public Bounding Bounding;
    List<GameObject> entities = new List<GameObject>();
    Transform entitiesParent;

    new void Awake()
    {
        base.Awake();
        
        entitiesParent = new GameObject("Entities").transform;
        Bounding = MechanicManager.AddMechanic<Bounding>();
    }

    Entity CreateEntity(
        Vector2 position,
        string name,
        Color colour,
        float health,
        float speed,
        float armour,
        float damage,
        bool autonomous)
    {
        if (Bounding.Type.Equals(BoundingType.Bounded) && !IsPointBounded(position))
            return null;
        
        GameObject entity = Instantiate(
            Resources.Load<GameObject>("Entity"),
            position,
            Quaternion.identity,
            entitiesParent);

        entity.name = name;
        
        Entity temp = entity.GetComponent<Entity>();
        
        temp.Initialize(
            name,
            colour,
            health,
            speed,
            armour,
            damage,
            autonomous,
            Bounding.Type,
            this
        );
        
        entities.Add(entity);

        return temp;
    }

    public Entity CreateDefaultEntity(Vector2 position, Color colour, bool autonomous)
    {
        return CreateEntity(position, "TestEntity", colour, 20, 1, 10, 1, autonomous);
    }

    public void RemoveEntity(GameObject entity)
    {
        entities.Remove(entity);
        entity.GetComponent<Entity>().Die();
    }

    public bool IsPointBounded(Vector3 point)
    {
        float x = point.x;
        if (x > Bounding.Bounds.x || x < -Bounding.Bounds.x) return false;
        
        float y = point.y;
        if (y > Bounding.Bounds.y || y < -Bounding.Bounds.y) return false;

        return true;
    }
}