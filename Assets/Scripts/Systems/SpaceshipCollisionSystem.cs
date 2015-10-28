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
    public class SpaceshipCollisionSystem : NodelessSystem<Spaceship, Transform, Entity, Collisions>
    {
        private readonly EntityCreator _creator;
        private INodeList<GameNode> _games;

        public SpaceshipCollisionSystem(EntityCreator creator)
        {
            _creator = creator;
            _updateCallback = OnUpdate;
            _addedToEngineCallback = OnAddedToEngine;
        }

        private void OnAddedToEngine(Engine engine)
        {
            _games = engine.GetNodes<GameNode>();
        }

        private void OnUpdate(float arg1, Spaceship spaceship, Transform transform, 
            Entity entity, Collisions collisions)
        {
            var game = _games.First();
            foreach (var hit in collisions.hits)
            {
                var asteroid = hit.gameObject.GetComponent<Asteroid>();
                if (asteroid == null)
                    continue;

                _creator.CreateSpaceshipInDeathroes(spaceship.transform);
                entity.Destroy();
                game.State.lives--;
            }

            collisions.hits.Clear();
        }
    }
}
