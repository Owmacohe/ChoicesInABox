using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public enum MechanicEvent { FixedUpdate, Move, Jump, Mouse, Click, KeyOrButton }
    
    public class MechanicManager : MonoBehaviour
    {
        public Dictionary<Type, MechanicPattern> Mechanics { get; private set; }

        public void Initialize(float ratio, float playerSaturation, float systemSaturation)
        {
            Mechanics = new Dictionary<Type, MechanicPattern>();

            // TODO: convert data into a list of mechanics
        }

        void FixedUpdate()
        {
            SendEvent(MechanicEvent.FixedUpdate);
        }

        public void SendEvent(MechanicEvent evt, Vector2 position = new(), GameObject clicked = null, KeyCode code = new())
        {
            if (Mechanics == null) return;
            
            foreach (var i in Mechanics.Values)
            {
                switch (evt)
                {
                    case MechanicEvent.FixedUpdate:
                        i.FixedUpdate();
                        break;
                    
                    case MechanicEvent.Move:
                        i.Move();
                        break;
                    
                    case MechanicEvent.Jump:
                        i.Jump();
                        break;
                    
                    case MechanicEvent.Mouse:
                        i.Mouse(position);
                        break;
                    
                    case MechanicEvent.Click:
                        i.Click(position, clicked);
                        break;
                    
                    case MechanicEvent.KeyOrButton:
                        i.KeyOrButton(code);
                        break;
                }   
            }
        }
    }
}