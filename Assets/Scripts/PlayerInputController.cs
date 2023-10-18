using Mechanics;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;
    
    [Header("Managers")]
    [SerializeField] MechanicManager manager;
    
    void OnMove()
    {
        manager.SendEvent(MechanicEvent.Move);
        
        // TODO: move player
    }
    
    void OnJump()
    {
        manager.SendEvent(MechanicEvent.Jump);
        
        // TODO: jump player
    }

    void OnMouse()
    {
        manager.SendEvent(MechanicEvent.Mouse); // TODO: send position too
    }
    
    void OnClick()
    {
        manager.SendEvent(MechanicEvent.Click); // TODO: send position and object clicked too
    }

    void OnKeyOrButton()
    {
        manager.SendEvent(MechanicEvent.KeyOrButton); // TODO: send key/button code t
    }
}