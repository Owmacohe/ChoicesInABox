using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntitiesPattern : SystemMechanicPattern
{
    List<GameObject> entities = new List<GameObject>();
    Transform entitiesParent;

    void Start()
    {
        entitiesParent = new GameObject("Entities").transform;
        
        for (int i = 0; i < 10; i++) CreateDefaultEntity(GetRandomPoint());
    }

    public void CreateEntity(Vector2 position, string name, float health, float speed, float armour, float damage)
    {
        GameObject entity = Instantiate(
            Resources.Load<GameObject>("Entity"),
            position,
            Quaternion.identity,
            entitiesParent);
        
        entity.GetComponent<Entity>().Initialize(name, health, speed, armour, damage);
        
        entities.Add(entity);
    }

    public void CreateDefaultEntity(Vector2 position)
    {
        CreateEntity(position, "TestEntity", 20, 1, 10, 1);
    }

    public void RemoveEntity(GameObject entity)
    {
        entities.Remove(entity);
        Destroy(entity);
    }

    public static Vector3 GetRandomPoint()
    {
        Camera temp = Camera.main;
        Vector3 trueOrigin = temp.transform.position;
        Vector3 origin = new Vector3(trueOrigin.x, trueOrigin.y, 0);

        return origin + (new Vector3(Random.Range(-1f, 1f) * (temp.pixelWidth / 250f), Random.Range(-1f, 1f) * (temp.pixelHeight / 250f), 0));
    }
}