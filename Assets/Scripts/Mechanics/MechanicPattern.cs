using System;
using UnityEngine;

public enum MechanicEvent { Move, Mouse, ClickDown, ClickUp, KeyDown, KeyUp, Pause, Resume }

public abstract class MechanicPattern : MonoBehaviour
{
    public bool Paused;
    protected MechanicManager MechanicManager;

    protected void Awake()
    {
        MechanicManager = GetComponent<MechanicManager>();
    }
    
    public virtual void Move(Vector2 delta) { }
    public virtual void Mouse(Vector2 position) { }
    public virtual void ClickDown(Vector2 position, GameObject clicked) { }
    public virtual void ClickUp(Vector2 position, GameObject clicked) { }
    public virtual void KeyDown(string keyName) { }
    public virtual void KeyUp(string keyName) { }
    
    public void Pause() { Paused = true; }
    public void Resume() { Paused = false; }
}

public class PlayerMechanicPattern : MechanicPattern
{
        
}

public class SystemMechanicPattern : MechanicPattern
{
        
}

public interface IInitializableMechanic
{
    public void Initialize(params object[] args);
}