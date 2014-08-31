using Assets.Scripts.Components;
using Assets.Scripts.Nodes;
using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class CollisionSystem : SystemBase
    {
        private EntityCreator creator;

       	private NodeList games;
		private NodeList spaceships;
		private NodeList asteroids;
		private NodeList bullets;

        public CollisionSystem(EntityCreator creator)
        {
            this.creator = creator;
        }

        override public void AddToGame(IGame game)
        {
            games = game.GetNodeList<GameNode>();
            spaceships = game.GetNodeList<SpaceshipCollisionNode>();
            asteroids = game.GetNodeList<AsteroidCollisionNode>();
            bullets = game.GetNodeList<BulletCollisionNode>();
        }

        override public void Update(float time)
        {
            var cam = Camera.main;
            for (var bullet = (BulletCollisionNode)bullets.Head; bullet != null; bullet = (BulletCollisionNode)bullet.Next)
            {
                foreach(var hit in bullet.Collisions.hits)
                {
                    var asteroid = hit.gameObject.GetComponent<Asteroid>();
                    if (asteroid != null)
                    {
                        SplitAsteroid(asteroid);
                        creator.CreateAsteroidInDeathroes(asteroid.transform);
                        creator.DestroyEntity(asteroid.GetComponent<Entity>());
                        creator.DestroyEntity(bullet.Entity);
                        if (games.Head!=null) 
                           ((GameNode)games.Head).State.hits++;
                    }
                }
                bullet.Collisions.hits.Clear();
            }

            for (var spaceship = (SpaceshipCollisionNode)spaceships.Head; spaceship != null; spaceship = (SpaceshipCollisionNode)spaceship.Next)
            {
                foreach (var hit in spaceship.Collisions.hits)
                {
                    var asteroid = hit.gameObject.GetComponent<Asteroid>();
                    if (asteroid != null)
                    {
                        creator.CreateSpaceshipInDeathroes(spaceship.Transform);
                        creator.DestroyEntity(spaceship.Entity);
                        //spaceship.audio.play(ExplodeShip);
                        if (games.Head != null)
                            ((GameNode)games.Head).State.lives--;
                    }
                }
                spaceship.Collisions.hits.Clear();
            }
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

                var vel = asteroid.rigidbody2D.velocity;
                var velNormal = vel.normalized;

                var perp = new Vector2(-velNormal.y, velNormal.x);
                var newVelNormal = (velNormal + perp).normalized;
                var pos = new Vector3(asteroid.transform.position.x + perp.x * scale, asteroid.transform.position.y + perp.y * scale);
                var a = creator.CreateAsteroid(newSize, pos);
                var rigidbody = a.Get<Rigidbody2D>(typeof(Rigidbody2D));
                rigidbody.AddRelativeForce(new Vector2(newVelNormal.x * 100f * scale, newVelNormal.y * 100f * scale));
                rigidbody.AddTorque(UnityEngine.Random.Range(-200f, 200f) * scale);

                perp = new Vector2(velNormal.y, -velNormal.x);
                newVelNormal = (velNormal + perp).normalized;
                pos = new Vector3(asteroid.transform.position.x + perp.x * scale, asteroid.transform.position.y + perp.y * scale);
                a = creator.CreateAsteroid(newSize, pos);
                rigidbody = a.Get<Rigidbody2D>(typeof(Rigidbody2D));
                rigidbody.AddRelativeForce(new Vector2(newVelNormal.x * 100f * scale, newVelNormal.y * 100f * scale));
                rigidbody.AddTorque(UnityEngine.Random.Range(-200f, 200f) * scale);
            }
        }

        override public void RemoveFromGame(IGame game)
        {
            spaceships = null;
            asteroids = null;
            bullets = null;
            games = null;
        }
    }
}
