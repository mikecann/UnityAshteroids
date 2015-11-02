using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ash.Core;
using Ash.Helpers;
using Assets.Scripts.Components;
using Assets.Scripts.Nodes;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class BulletCollisionSystem : NodelessSystem<Bullet, Collisions, Entity>
    {
        private INodeList<GameNode> _games;

        public BulletCollisionSystem()
        {
            _updateCallback = OnUpdate;
            _addedToEngineCallback = OnAddedToEngine;
        }

        private void OnAddedToEngine(IEngine engine)
        {
            _games = engine.GetNodes<GameNode>();
        }

        private void OnUpdate(float delta, Bullet bullet, Collisions collisions, Entity entity)
        {
            var game = _games.First();
            foreach (var hit in collisions.hits)
            {
                var asteroid = hit.gameObject.GetComponent<Asteroid>();
                if (asteroid == null)
                    continue;

                asteroid.GetComponent<Hitpoints>().hp--;
              
                entity.Destroy();

                game.State.hits++;
            }

            collisions.hits.Clear();
        }
    }
}
