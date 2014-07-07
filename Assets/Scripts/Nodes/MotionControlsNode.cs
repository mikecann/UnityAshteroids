using Assets.Scripts.Components;
using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Nodes
{
    public class MotionControlsNode : Node
    {
        public MotionControl MotionControl { get; set; }
        public Rigidbody2D Rigidbody { get; set; }
    }
}
