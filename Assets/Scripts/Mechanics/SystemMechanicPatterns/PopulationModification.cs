using System;
using UnityEngine;

public class PopulationModification : PlayerMechanicPattern
{
    public override void ClickDown(Vector2 position, GameObject clicked)
    {
        Entities manager = GetComponent<Entities>();

        if (clicked != null)
        {
            Entity clickedEntity = clicked.GetComponent<Entity>();
            
            if (clickedEntity != null && clickedEntity.Autonomous)
                manager.RemoveEntity(clicked);
        }
        else manager.CreateDefaultEntity(position, true);   
    }
}