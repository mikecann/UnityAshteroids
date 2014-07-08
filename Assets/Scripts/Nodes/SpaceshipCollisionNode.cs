using Assets.Scripts.Components;
using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Nodes
{
    public class SpaceshipCollisionNode : Node
    {
        public Spaceship Spaceship { get; set; }
        public Transform Transform { get; set; }
        public Collisions Collisions { get; set; }
    }
}
