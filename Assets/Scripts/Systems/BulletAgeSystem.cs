using Assets.Scripts.Nodes;
using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Systems
{
    public class BulletAgeSystem : SystemBase
    {
        private EntityCreator creator;

        private NodeList nodes;

        public BulletAgeSystem(EntityCreator creator)
        {
            this.creator = creator;
        }

        override public void AddToGame(IGame game)
        {
            nodes = game.GetNodeList<BulletAgeNode>();
        }

        override public void Update(float time)
        {
            for (var node = (BulletAgeNode)nodes.Head; node != null; node = (BulletAgeNode)node.Next)
            {
                var bullet = node.Bullet;
                bullet.lifeRemaining -= time;
                if (bullet.lifeRemaining <= 0)
                {
                    creator.DestroyEntity(node.Entity);
                }
            }
        }

        override public void RemoveFromGame(IGame game)
        {
            nodes = null;
        }
    }
}
