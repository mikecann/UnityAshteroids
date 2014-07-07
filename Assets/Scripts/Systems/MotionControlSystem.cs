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

        override public void RemoveFromGame(IGame game)
        {
        }

        override public void Update(double time)
        {
            for (var node = nodes.Head; node!=null; node = node.Next)
            {
                var control = ((MotionControlsNode)node).MotionControl;
                var rigidBody = ((MotionControlsNode)node).Rigidbody;

                if (Input.GetKey(control.left))
                {
                    rigidBody.AddTorque(control.rotationRate * (float)time);
                }                
                if (Input.GetKey(control.right))
                {
                    rigidBody.AddTorque(-control.rotationRate * (float)time);
                }
                if (Input.GetKey(control.accelerate))
                {
                    rigidBody.AddRelativeForce(new Vector2(0, control.accelerationRate * (float)time));
                }
            }
        }
    }
}
