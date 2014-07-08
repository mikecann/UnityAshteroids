using Assets.Scripts.Components;
using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Nodes
{
    public class GunControlNode : Node
    {
        public GunControls Control { get; set; }
        public Gun Gun { get; set; }
        public Transform Transform { get; set; }
        public Audio Audio { get; set; }
    }
}
