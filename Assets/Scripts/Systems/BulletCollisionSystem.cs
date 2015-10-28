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
        private readonly EntityCreator _creator;
        private INodeList<GameNode> _games;

        public BulletCollisionSystem(EntityCreator creator)
        {
            _creator = creator;
            _updateCallback = OnUpdate;
            _addedToEngineCallback = OnAddedToEngine;
        }

        private void OnAddedToEngine(Engine engine)
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

                SplitAsteroid(asteroid);

                _creator.CreateAsteroidInDeathroes(asteroid.transform);
                entity.Destroy();

                game.State.hits++;
            }

            collisions.hits.Clear();
        }

        private void SplitAsteroid(Asteroid asteroid)
        {
            if (asteroid.size != AsteroidSize.Tiny)
            {
                var newSize = AsteroidSize.Medium;
                var scale = 0.5f;
                if (asteroid.size == AsteroidSize.Medium)
                {
                    newSize = AsteroidSize.Small;
                    scale = 0.2f;
                }
                if (asteroid.size == AsteroidSize.Small)
                {
                    newSize = AsteroidSize.Tiny;
                    scale = 0.1f;
                }

                var vel = asteroid.GetComponent<Rigidbody2D>().velocity;
                var velNormal = vel.normalized;

                var perp = new Vector2(-velNormal.y, velNormal.x);
                var newVelNormal = (velNormal + perp).normalized;
                var pos = new Vector3(asteroid.transform.position.x + perp.x * scale, asteroid.transform.position.y + perp.y * scale);
                var a = _creator.CreateAsteroid(newSize, pos);
                var rigidbody = a.GetComponent<Rigidbody2D>();
                rigidbody.AddRelativeForce(new Vector2(newVelNormal.x * 100f * scale, newVelNormal.y * 100f * scale));
                rigidbody.AddTorque(UnityEngine.Random.Range(-200f, 200f) * scale);

                perp = new Vector2(velNormal.y, -velNormal.x);
                newVelNormal = (velNormal + perp).normalized;
                pos = new Vector3(asteroid.transform.position.x + perp.x * scale, asteroid.transform.position.y + perp.y * scale);
                a = _creator.CreateAsteroid(newSize, pos);
                rigidbody = a.GetComponent<Rigidbody2D>();
                rigidbody.AddRelativeForce(new Vector2(newVelNormal.x * 100f * scale, newVelNormal.y * 100f * scale));
                rigidbody.AddTorque(UnityEngine.Random.Range(-200f, 200f) * scale);
            }
        }
    }
}
