using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entities : SystemMechanicPattern
{
    List<GameObject> entities = new List<GameObject>();
    Transform entitiesParent;

    void Awake()
    {
        entitiesParent = new GameObject("Entities").transform;
    }

    void Start()
    {
        CreateDefaultEntity(GetRandomPoint(), true);
    }

    public Entity CreateEntity(
        Vector2 position,
        string name,
        float health,
        float speed,
        float armour,
        float damage,
        bool autonomous)
    {
        GameObject entity = Instantiate(
            Resources.Load<GameObject>("Entity"),
            position,
            Quaternion.identity,
            entitiesParent);
        
        Entity temp = entity.GetComponent<Entity>();

        Exploration exploration = GetComponent<Exploration>();
        bool bounded = !exploration || (exploration && exploration.Type.Equals(ExplorationType.Bounded));
        
        temp.Initialize(
            name,
            health,
            speed,
            armour,
            damage,
            autonomous,
            bounded ? ExplorationType.Bounded : ExplorationType.Free
        );
        
        entities.Add(entity);

        return temp;
    }

    public Entity CreateDefaultEntity(Vector2 position, bool autonomous)
    {
        return CreateEntity(position, "TestEntity", 20, 1, 10, 1, autonomous);
    }

    public void RemoveEntity(GameObject entity)
    {
        entities.Remove(entity);
        entity.GetComponent<Entity>().Die();
    }

    public static Vector3 GetRandomPoint()
    {
        Camera temp = Camera.main;
        Vector3 trueOrigin = temp.transform.position;
        Vector3 origin = new Vector3(trueOrigin.x, trueOrigin.y, 0);

        return origin + (new Vector3(Random.Range(-1f, 1f) * (temp.pixelWidth / 250f), Random.Range(-1f, 1f) * (temp.pixelHeight / 250f), 0));
    }

    public static Vector3 GetBoundedPoint(Vector3 point)
    {
        Camera temp = Camera.main;
        
        float width = temp.pixelWidth / 250f;
        float height = temp.pixelHeight / 250f;
        
        float x = point.x;
        if (x > width) x = width;
        if (x < -width) x = -width;
        
        float y = point.y;
        if (y > height) y = height;
        if (y < -height) y = -height;

        return new Vector3(x, y, 0);
    }
}