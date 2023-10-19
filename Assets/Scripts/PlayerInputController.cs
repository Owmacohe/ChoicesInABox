using System;
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
        manager.SendEvent(MechanicEvent.Move);
        
        // TODO: move player
    }

    void OnMouse(InputValue value)
    {
        mousePosition = value.Get<Vector2>();
        manager.SendEvent(MechanicEvent.Mouse, mousePosition);
    }
    
    void OnClick(InputValue value)
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out var hit);
        
        manager.SendEvent(
            value.Get() != null ? MechanicEvent.ClickDown : MechanicEvent.ClickUp,
            Camera.main.ScreenToWorldPoint(mousePosition),
            hit.transform == null ? null : hit.transform.gameObject);
    }

    void OnKey(string value)
    {
        manager.SendEvent(
            Keyboard.current.anyKey.wasPressedThisFrame ? MechanicEvent.KeyUp : MechanicEvent.KeyDown,
            new(), null, value);
    }
}