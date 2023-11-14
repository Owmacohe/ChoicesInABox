using System;
using UnityEngine;

public class PopulationModification : PlayerMechanicPattern
{
    Entities manager;
    
    void Start()
    {
        manager = MechanicManager.AddMechanic<Entities>();
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
            manager.CreateDefaultEntity(position, Color.red, true);
        }   
    }
}