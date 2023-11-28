using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[MechanicRequirements(typeof(Bounding), typeof(CanWinLose))]
public class Entities : SystemMechanicPattern
{
    [HideInInspector] public Bounding Bounding;
    [HideInInspector] public List<GameObject> EntitiesList = new List<GameObject>();
    Transform entitiesParent;

    [HideInInspector] public CanWinLose CanWinLose;

    bool spawnRandomly;
    
    public override void Initialize(params object[] args)
    {
        spawnRandomly = (bool)args[0];
        
        entitiesParent = new GameObject("Entities").transform;
        
        Bounding = GetComponent<Bounding>();

        CanWinLose = GetComponent<CanWinLose>();   
    }

    void FixedUpdate()
    {
        if (spawnRandomly && EntitiesList.Count < 10 && Random.value <= 0.01f)
            CreateDefaultEnemy(Bounding.GetRandomWithinBounds());
    }

    public Entity CreateEntity(
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
        
        EntitiesList.Add(entity);

        return temp;
    }

    public void CreateDefaultEnemy(Vector2 position)
    {
        CreateEntity(position, "Enemy", Color.red, 1, 1, 1, 1, true);
    }

    public void RemoveEntity(GameObject entity)
    {
        EntitiesList.Remove(entity);
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
    
    public override string ToString()
    {
        return base.ToString() + (spawnRandomly ? " (spawn randomly)" : "");
    }
}