using UnityEngine;

namespace Mechanics
{
    public abstract class MechanicPattern
    {
        protected MechanicManager Manager { get; }
        
        protected MechanicPattern(MechanicManager manager)
        {
            Manager = manager;
        }

        public void FixedUpdate() { }
        public void Move() { }
        public void Jump() { }
        public void Mouse(Vector2 position) { }
        public void Click(Vector2 position, GameObject clicked) { }
        public void KeyOrButton(KeyCode code) { }
    }
}