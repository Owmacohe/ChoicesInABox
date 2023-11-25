using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] MechanicManager manager;

    Vector2 mousePosition;

    void Start()
    {
        InputSystem.onAnyButtonPress.Call(callback => OnKey(callback.name));
    }

    void OnMove(InputValue value)
    {
        manager.SendEvent(MechanicEvent.Move, value.Get<Vector2>());
    }

    void OnMouse(InputValue value)
    {
        mousePosition = value.Get<Vector2>();
        manager.SendEvent(MechanicEvent.Mouse, mousePosition);
    }
    
    void OnClick(InputValue value)
    {
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.zero, new ContactFilter2D(), results);
        
        manager.SendEvent(
            value.Get() != null ? MechanicEvent.ClickDown : MechanicEvent.ClickUp,
            Camera.main.ScreenToWorldPoint(mousePosition),
            results.Count == 0 ? null : results[0].transform.gameObject);
    }

    void OnKey(string value)
    {
        if (manager == null) return;
        
        manager.SendEvent(
            Keyboard.current.anyKey.wasPressedThisFrame ? MechanicEvent.KeyUp : MechanicEvent.KeyDown,
            new(), null, value.Trim());
    }
}