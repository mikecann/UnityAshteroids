using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ash.Helpers;
using Assets.Scripts.Components;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class MotionControlSystem : NodelessSystem<Rigidbody2D, MotionControl>
    {
        public MotionControlSystem()
        {
            _updateCallback = OnUpdate;
        }

        private void OnUpdate(float delta, Rigidbody2D rigidBody, MotionControl motionControl)
        {
            if (Input.GetKey(motionControl.left))
                rigidBody.AddTorque(motionControl.rotationRate * delta);

            if (Input.GetKey(motionControl.right))
                rigidBody.AddTorque(-motionControl.rotationRate * delta);

            if (Input.GetKey(motionControl.accelerate))
                rigidBody.AddRelativeForce(new Vector2(0, motionControl.accelerationRate * delta));
        }
    }
}
