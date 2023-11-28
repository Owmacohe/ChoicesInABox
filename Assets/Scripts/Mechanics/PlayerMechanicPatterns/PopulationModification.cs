using System;
using UnityEngine;


[MechanicRequirements(typeof(Entities))]
public class PopulationModification : PlayerMechanicPattern
{
    Entities entities;

    public override void Initialize(params object[] args)
    {
        entities = GetComponent<Entities>();
        
        MechanicManager.AddToControls("Click", "spawn/destroy");
    }

    public override void ClickDown(Vector2 position, GameObject clicked)
    {
        if (clicked != null)
        {
            Entity clickedEntity = clicked.GetComponent<Entity>();
            
            if (clickedEntity != null && clickedEntity.Autonomous)
                entities.RemoveEntity(clicked);
        }
        else
        {
            entities.CreateDefaultEnemy(position);
        }   
    }
}