using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Entity : MonoBehaviour
{
    public string Name;
    public Color Colour;
    public float Health;
    public float Speed;
    public float Armour;
    public float Damage;
    public bool Autonomous;
    public BoundingType Bounding;

    [HideInInspector] public Entities Entities;
    Rigidbody2D rb;
    Vector2 target;
    Vector2 currentDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GetComponentInChildren<SpriteRenderer>().color = Colour;
        
        if (Autonomous && !Entities.Paused) MoveRandom();
    }

    void FixedUpdate()
    {
        if (!Entities.Paused)
        {
            if (Health <= 0) Die();

            if (Autonomous)
            {
                if (Random.value <= 0.01f) MoveRandom();
                
                if (Vector2.Distance(transform.position, target) > 0.1f)
                    MoveDirection((target - (Vector2)transform.position).normalized);
            }
        }
    }

    public void Initialize(
        string name,
        Color colour,
        float health,
        float speed,
        float armour,
        float damage,
        bool autonomous,
        BoundingType boundingType,
        Entities entitiesManager)
    {
        Name = name;

        Colour = colour;
        
        Health = health;
        Speed = speed;
        Armour = armour;
        Damage = damage;

        Autonomous = autonomous;
        Bounding = boundingType;

        Entities = entitiesManager;
    }

    void MoveRandom()
    {
        target = transform.position + new Vector3(
            Random.Range(-5f, 5f),
            Random.Range(-5f, 5f),
            Random.Range(-5f, 5f)
        );
    }
    
    public void MoveDirection(Vector3 delta)
    {
        currentDirection = delta;
        rb.position += currentDirection * (Speed * 0.1f);
    }

    public void Attack()
    {
        if (currentDirection.Equals(Vector2.zero)) return;
        
        Instantiate(Resources.Load<GameObject>("Bullet"))
            .GetComponent<Bullet>().Initialize(currentDirection, this);
    }

    public void Die()
    {
        if (!Autonomous) Entities.CanWinLose.End(false);
        
        Destroy(gameObject);
    }
}