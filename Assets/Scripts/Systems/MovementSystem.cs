using Assets.Scripts.Nodes;
using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class MovementSystem : SystemBase
    {
        private GameConfig config;

        private NodeList nodes;

        public MovementSystem(GameConfig config)
        {
            this.config = config;
        }

        override public void AddToGame(IGame game)
        {
            nodes = game.GetNodeList<MovementNode>();
        }

        override public void Update(float time)
        {
            var cam = Camera.main;
            for (var node = (MovementNode)nodes.Head; node != null; node = (MovementNode)node.Next)
            {
                var transform = node.Transform;
			    var rigidbody = node.Rigidbody;

                if (transform.position.x < config.Bounds.min.x)
			    {
                    transform.position = new Vector3(transform.position.x + config.Bounds.size.x, transform.position.y, transform.position.z);
			    }
                if (transform.position.x > config.Bounds.max.x)
                {
                    transform.position = new Vector3(transform.position.x - config.Bounds.size.x, transform.position.y, transform.position.z);
                }
                if (transform.position.y < config.Bounds.min.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + config.Bounds.size.y, transform.position.z);
                }
                if (transform.position.y > config.Bounds.max.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - config.Bounds.size.y, transform.position.z);
                }
            }
        }

        override public void RemoveFromGame(IGame game)
        {
            nodes = null;
        }
    }
}
