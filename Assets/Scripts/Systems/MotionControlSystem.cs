using Assets.Scripts.Nodes;
using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class MotionControlSystem : SystemBase
    {
        private NodeList nodes;

        override public void AddToGame(IGame game)
        {
            nodes = game.GetNodeList<MotionControlsNode>();
        }       

        override public void Update(float time)
        {
            for (var node = (MotionControlsNode)nodes.Head; node != null; node = (MotionControlsNode)node.Next)
            {
                var control = node.MotionControl;
                var rigidBody = node.Rigidbody;

                if (Input.GetKey(node.MotionControl.left))
                {
                    rigidBody.AddTorque(control.rotationRate * time);
                }                
                if (Input.GetKey(control.right))
                {
                    rigidBody.AddTorque(-control.rotationRate * time);
                }
                if (Input.GetKey(control.accelerate))
                {
                    rigidBody.AddRelativeForce(new Vector2(0, control.accelerationRate * time));
                }
            }
        }

        override public void RemoveFromGame(IGame game)
        {
            nodes = null;
        }
    }
}
