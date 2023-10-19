using System;
using UnityEngine;

public class PopulationModification : PlayerMechanicPattern
{
    public override void ClickDown(Vector2 position, GameObject clicked)
    {
        EntitiesPattern manager = GetComponent<EntitiesPattern>();
        
        if (clicked != null) manager.RemoveEntity(clicked);
        else manager.CreateDefaultEntity(position);
    }
}