using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entity : MonoBehaviour
{
    [HideInInspector] string Name;
    [HideInInspector] float Health;
    [HideInInspector] float Speed;
    [HideInInspector] float Armour;
    [HideInInspector] float Damage;
    
    Vector3 target;

    void Start()
    {
        Point();
    }

    void FixedUpdate()
    {
        if (Health <= 0) Die();

        if (Random.value <= 0.01f) Point();

        float convertedSpeed = Speed * 0.1f;

        if (Vector3.Distance(transform.position, target) > convertedSpeed * 2)
            transform.position += (target - transform.position).normalized * convertedSpeed;
    }

    public void Initialize(string name, float health, float speed, float armour, float damage)
    {
        Name = name;
        
        Health = health;
        Speed = speed;
        Armour = armour;
        Damage = damage;
    }

    void Point()
    {
        target = EntitiesPattern.GetRandomPoint();
        transform.LookAt(target);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}