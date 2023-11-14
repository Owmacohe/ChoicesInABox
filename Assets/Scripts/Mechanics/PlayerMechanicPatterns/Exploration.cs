using System;
using UnityEngine;

public class Exploration : PlayerMechanicPattern
{
    Bounding bounding;
    Entity player;
    Vector2 direction;
    Camera cam;

    void Start()
    {
        bounding = MechanicManager.AddMechanic<Bounding>();
        
        player = MechanicManager.AddMechanic<Entities>()
            .CreateDefaultEntity(Vector2.zero, Color.blue, false);

        cam = Camera.main;
    }

    void FixedUpdate()
    {
        if (!Paused)
        {
            if (!direction.Equals(Vector2.zero))
                player.MoveDirection(direction);

            if (bounding.Type.Equals(BoundingType.Free))
                cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
    }

    public override void Move(Vector2 delta)
    {
        direction = delta;
    }
}