using Assets.Scripts.Nodes;
using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Systems
{
    public  class DeathThroesSystem : SystemBase
    {
        private EntityCreator creator;

        private NodeList nodes;

        public DeathThroesSystem(EntityCreator creator)
        {
            this.creator = creator;
        }

        override public void AddToGame(IGame game)
        {
            nodes = game.GetNodeList<DeathThroesNode>();
            nodes.NodeAdded += OnNodeAdded;
        }

        void OnNodeAdded(Node node)
        {
            var deathroesNode = (DeathThroesNode)node;
            if (deathroesNode.Death.deathSound != null)
                deathroesNode.Audio.Play(deathroesNode.Death.deathSound);
        }

        override public void Update(float time)
        {
            for (var node = (DeathThroesNode)nodes.Head; node != null; node = (DeathThroesNode)node.Next)
            {
                node.Death.countdown -= time;
                if (node.Death.countdown <= 0)
                {
                    creator.DestroyEntity(node.Entity);
                }
            }
        }

        override public void RemoveFromGame(IGame game)
        {
            nodes.NodeAdded -= OnNodeAdded;
            nodes = null;
        }
    }
}
