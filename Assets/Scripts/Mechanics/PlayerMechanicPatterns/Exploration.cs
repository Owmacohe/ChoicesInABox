using System;
using TMPro;
using UnityEngine;

public class Exploration : MechanicPattern
{
    Bounding bounding;
    [HideInInspector] public Entity Player;
    Vector2 direction;
    Camera cam;

    TMP_Text stats;
    
    public override void Initialize(params object[] args)
    {
        stats = (TMP_Text)args[0];
        
        bounding = GetComponent<Bounding>();

        Player = GetComponent<Entities>()
            .CreateEntity(Vector2.zero, "Player", Color.blue, 5, 1, 1, 1, false);
        
        UpdateStats();
        
        MechanicManager.AddToControls("WASD", "move");

        cam = Camera.main;
    }

    void FixedUpdate()
    {
        if (!Paused)
        {
            if (!direction.Equals(Vector2.zero))
                Player.MoveDirection(direction);

            if (bounding.Type.Equals(BoundingType.Free))
                cam.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
        }
    }

    public override void Move(Vector2 delta)
    {
        direction = delta;
    }

    public void UpdateStats()
    {
        stats.text =
            "Health: " + Player.Health + "\n" +
            "Speed: " + Player.Speed + "\n" +
            "Armour: " + Player.Armour + "\n" +
            "Damage: " + Player.Damage;
    }
}