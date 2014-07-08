using Assets.Scripts.Components;
using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Nodes
{
    public class AudioNode : Node
    {
        public Audio Audio { get; set; }
        public Transform Transform { get; set; }
    }
}
