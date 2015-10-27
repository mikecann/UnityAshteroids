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
        public Spaceship spaceship;
        public Transform transform;
        public Collisions collisions;
        public Entity entity;
    }
}
