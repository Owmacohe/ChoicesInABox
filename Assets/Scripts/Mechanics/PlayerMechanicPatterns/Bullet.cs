using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector2 direction;
    Entity source;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        Invoke(nameof(Remove), 6);
    }

    void FixedUpdate()
    {
        rb.position += direction * 0.2f;
    }
    
    public void Initialize(Vector2 d, Entity s)
    {
        direction = d;
        source = s;

        GetComponentInChildren<SpriteRenderer>().color = source.Colour;

        transform.position = source.gameObject.transform.position;
    }

    void Remove()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Entity otherEntity = other.gameObject.GetComponent<Entity>();

        if (otherEntity != null && !otherEntity.Equals(source) && otherEntity.Autonomous != source.Autonomous)
        {
            otherEntity.Health -= source.Damage / otherEntity.Armour;
            
            if (!otherEntity.Autonomous)
            {
                source.Entities.GetComponent<Exploration>().UpdateStats();
            }
            
            Remove();
        }
    }
}