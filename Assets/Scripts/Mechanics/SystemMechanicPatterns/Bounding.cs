using System;
using UnityEngine;

public enum BoundingType { Bounded, Free }

public class Bounding : SystemMechanicPattern, IInitializableMechanic
{
    public static Vector2 Bounds = new Vector2(6, 4);
    
    public BoundingType Type;

    new void Awake()
    {
        base.Awake();
        Type = BoundingType.Free;
    }

    public void Initialize(params object[] args)
    {
        Type = (BoundingType)args[0];

        if (Type.Equals(BoundingType.Bounded))
            Instantiate(Resources.Load<GameObject>("Bounds"));
    }
}