using Assets.Scripts.Nodes;
using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class GunControlSystem : SystemBase
    {
        private EntityCreator creator;

        private NodeList nodes;

        public GunControlSystem(EntityCreator creator)
        {
            this.creator = creator;
        }

        override public void AddToGame(IGame game)
        {
            nodes = game.GetNodeList<GunControlNode>();
        }

        override public void Update(float time)
        {
            for (var node = (GunControlNode)nodes.Head; node != null; node = (GunControlNode)node.Next)
            {
                var control = node.Control;
                var transform = node.Transform;
                var gun = node.Gun;

                gun.shooting = Input.GetKey(control.trigger);
                gun.timeSinceLastShot += time;

                if(gun.shooting && gun.timeSinceLastShot >= gun.minimumShotInterval)
                {
                    creator.CreateUserBullet(gun, transform.FindChild("Gun"));
                    node.Audio.Play(node.Gun.shootSound);
                    gun.timeSinceLastShot = 0;
                }
            }
        }

        override public void RemoveFromGame(IGame game)
        {
            nodes = null;
        }
    }
}
