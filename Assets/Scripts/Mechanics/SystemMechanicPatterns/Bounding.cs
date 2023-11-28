using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum BoundingType { Free, Bounded }

public class Bounding : SystemMechanicPattern
{
    public static Vector2 Bounds = new Vector2(6, 4);
    
    public BoundingType Type = BoundingType.Free;
    
    public override void Initialize(params object[] args)
    {
        Type = (BoundingType)args[0];
        
        if (Type.Equals(BoundingType.Bounded))
            Instantiate(Resources.Load<GameObject>("Bounds"));
    }

    public Vector2 GetRandomWithinBounds()
    {
        return new Vector2(
            Random.Range(-Bounds.x, Bounds.x),
            Random.Range(-Bounds.y, Bounds.y)
        );
    }
    
    public override string ToString()
    {
        return base.ToString() + (Type.Equals(BoundingType.Free) ? " (free)" : " (bounded)");
    }
}