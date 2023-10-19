using System;
using UnityEngine;

public enum MechanicEvent { Move, Mouse, ClickDown, ClickUp, KeyDown, KeyUp }

public abstract class MechanicPattern : MonoBehaviour
{
    public virtual void Move() { }
    public virtual void Mouse(Vector2 position) { }
    public virtual void ClickDown(Vector2 position, GameObject clicked) { }
    public virtual void ClickUp(Vector2 position, GameObject clicked) { }
    public virtual void KeyDown(string keyName) { }
    public virtual void KeyUp(string keyName) { }
}

public class PlayerMechanicPattern : MechanicPattern
{
        
}

public class SystemMechanicPattern : MechanicPattern
{
        
}