using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entity : MonoBehaviour
{
    public string Name;
    public float Health;
    public float Speed;
    public float Armour;
    public float Damage;
    public bool Autonomous;
    public ExplorationType Exploration;

    bool moving;
    Vector3 target;

    void Start()
    {
        if (Autonomous) MoveRandom();
    }

    void FixedUpdate()
    {
        if (Health <= 0) Die();

        if (Autonomous && Random.value <= 0.01f) MoveRandom();

        if (moving)
        {
            float convertedSpeed = Speed * 0.1f;

            if (Vector3.Distance(transform.position, target) > convertedSpeed * 2)
                transform.position += (target - transform.position).normalized * convertedSpeed;
            else moving = false;
        }
    }

    public void Initialize(
        string name,
        float health,
        float speed,
        float armour,
        float damage,
        bool autonomous,
        ExplorationType explorationType)
    {
        Name = name;
        
        Health = health;
        Speed = speed;
        Armour = armour;
        Damage = damage;

        Autonomous = autonomous;
        Exploration = explorationType;
    }

    void MoveRandom()
    {
        MoveTowards(Entities.GetRandomPoint());
    }
    
    public void MoveDirection(Vector3 delta)
    {
        MoveTowards(transform.position + delta);
    }

    void MoveTowards(Vector3 t)
    {
        target = Exploration.Equals(ExplorationType.Bounded) ? Entities.GetBoundedPoint(t) : t;
        moving = true;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}