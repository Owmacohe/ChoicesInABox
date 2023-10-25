using System;
using UnityEngine;

public enum ExplorationType { Bounded, Free }

public class Exploration : PlayerMechanicPattern, IInitializableMechanic
{
    public ExplorationType Type;
    
    Entity player;
    Vector2 direction;
    Camera cam;

    void Start()
    {
        player = GetComponent<Entities>().CreateDefaultEntity(Vector2.zero, false);

        cam = Camera.main;
    }

    void FixedUpdate()
    {
        if (!Paused)
        {
            if (!direction.Equals(Vector2.zero))
            {
                player.MoveDirection(direction);
            }
        
            cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
    }
    
    public void Initialize(params object[] args)
    {
        Type = (ExplorationType)args[0];
    }

    public override void Move(Vector2 delta)
    {
        direction = delta;
    }
}