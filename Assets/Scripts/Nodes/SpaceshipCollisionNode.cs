using Assets.Scripts.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ash.Core;
using UnityEngine;

namespace Assets.Scripts.Nodes
{
    public class SpaceshipCollisionNode
    {
        public Spaceship Spaceship { get; set; }
        public Transform Transform { get; set; }
        public Collisions Collisions { get; set; }
        public Entity Entity { get; set; }
    }
}
