using System;
using UnityEngine;

public class PopulationModification : MechanicPattern
{
    Entities manager;

    public override void Initialize(params object[] args)
    {
        manager = GetComponent<Entities>();
    }

    public override void ClickDown(Vector2 position, GameObject clicked)
    {
        if (clicked != null)
        {
            Entity clickedEntity = clicked.GetComponent<Entity>();
            
            if (clickedEntity != null && clickedEntity.Autonomous)
                manager.RemoveEntity(clicked);
        }
        else
        {
            manager.CreateEntity(position, "Enemy", Color.red, 1, 1, 1, 1, true);
        }   
    }
}