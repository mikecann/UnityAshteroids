using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ash.Core;
using Ash.Helpers;
using Assets.Scripts.Components;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class AsteroidSplittingSystem : NodelessSystem<Asteroid, Transform, Hitpoints, Entity, Rigidbody2D>
    {
        private readonly EntityCreator _creator;

        public AsteroidSplittingSystem(EntityCreator creator)
        {
            _creator = creator;
            _updateCallback = OnUpdate;
        }

        private void OnUpdate(float delta, Asteroid asteroid, Transform transform, Hitpoints hitpoints,
            Entity entity, Rigidbody2D rigidbody)
        {
            if (hitpoints.hp > 0)
                return;

            SplitAsteroid(asteroid, rigidbody, transform);

            _creator.CreateAsteroidInDeathroes(transform);
            entity.Destroy();
        }

        private void SplitAsteroid(Asteroid asteroid, Rigidbody2D rigidbody, Transform transform)
        {
            if (asteroid.size == AsteroidSize.Tiny)
                return;

            var newSize = GetSplitSize(asteroid.size);
            var velNormal = rigidbody.velocity.normalized;
            
            CreatePerpendicularAsteroids(transform, velNormal, newSize);
        }

        private void CreatePerpendicularAsteroids(Transform transform, Vector2 velNormal, AsteroidSize newSize)
        {
            var perp = new Vector2(-velNormal.y, velNormal.x);
            var dir = (velNormal + perp).normalized;
            CreateChildAsteroid(dir, transform.position, newSize);

            perp = new Vector2(velNormal.y, -velNormal.x);
            dir = (velNormal + perp).normalized;
            CreateChildAsteroid(dir, transform.position, newSize);
        }

        private void CreateChildAsteroid(Vector2 dir, Vector2 asteroidPos, AsteroidSize newSize)
        {
            var pos = new Vector3(asteroidPos.x + dir.x, asteroidPos.y + dir.y);
            var a = _creator.CreateAsteroid(newSize, pos);

            var multiplier = GetForceMultiplier(newSize);
            var rb = a.GetComponent<Rigidbody2D>();
            var force = new Vector2(dir.x, dir.y) * multiplier;
            rb.AddForce(force, ForceMode2D.Impulse);
            rb.AddTorque(force.x * multiplier * multiplier, ForceMode2D.Impulse);           
        }

        private AsteroidSize GetSplitSize(AsteroidSize size)
        {
            if (size == AsteroidSize.Medium)
                return AsteroidSize.Small;
            else if (size == AsteroidSize.Small)
                return AsteroidSize.Tiny;
            else
                return AsteroidSize.Medium;
        }

        private float GetForceMultiplier(AsteroidSize size)
        {
            if (size == AsteroidSize.Tiny)
                return 0.1f;
            else if (size == AsteroidSize.Small)
                return 0.2f;
            else
                return 0.5f;
        }
    }
}
