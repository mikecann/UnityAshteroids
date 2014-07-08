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
    public class GameManagerSystem : SystemBase
    {
        private EntityCreator creator;
        private GameConfig config;

        private NodeList gameNodes;
        private NodeList spaceships;
        private NodeList asteroids;
        private NodeList bullets;

        public GameManagerSystem(EntityCreator creator, GameConfig config)
        {
            this.creator = creator;
            this.config = config;
        }

        override public void AddToGame(IGame game)
        {
            gameNodes = game.GetNodeList<GameNode>();
            spaceships = game.GetNodeList<SpaceshipNode>();
            asteroids = game.GetNodeList<AsteroidCollisionNode>();
            bullets = game.GetNodeList<BulletCollisionNode>();
        }       

        override public void Update(float time)
        {
            var node = (GameNode)gameNodes.Head;
            if (node!=null && node.State.playing)
            {
                if (spaceships.Empty)
                {
                    if (node.State.lives > 0)
                    {
                        var newSpaceshipPosition = Vector2.zero;
                        var clearToAddSpaceship = true;
                        for (var asteroid = (AsteroidCollisionNode)asteroids.Head; asteroid!=null; asteroid = (AsteroidCollisionNode)asteroid.Next)
						{
							if( Vector2.Distance( asteroid.Transform.position, newSpaceshipPosition ) <= 1f)
							{
								clearToAddSpaceship = false;
								break;
							}
						}
                        if (clearToAddSpaceship)
                        {
                            creator.CreateSpaceship();
                        }
                    }
                    else
                    {
                        Debug.Log("IN HERE");
                        node.State.playing = false;
                        creator.CreateWaitForClick();
                    }
                }
            }

            if(asteroids.Empty && bullets.Empty && !spaceships.Empty)
            {
                // next level
                var spaceship = (SpaceshipNode)spaceships.Head;
                node.State.level++;

                var asteroidCount = 2 + node.State.level;
                for (int i = 0; i < asteroidCount; i++)
                {
                    // check not on top of spaceship
                    Vector2 position;
                    do
                    {
                        position = new Vector2(UnityEngine.Random.Range(config.Bounds.min.x, config.Bounds.max.x),
                            UnityEngine.Random.Range(config.Bounds.min.y, config.Bounds.max.y));
                    }
                    while (Vector2.Distance(position, spaceship.Transform.position) <= 1);

                    var asteroid = creator.CreateAsteroid(AsteroidSize.Large, position);

                    // Give it a kick
                    var xVel = UnityEngine.Random.Range(-100f, 100f);
                    var yVel = UnityEngine.Random.Range(-100f, 100f);
                    var torque = UnityEngine.Random.Range(-100f, 100f);

                    var rigidBody = asteroid.Get<Rigidbody2D>(typeof(Rigidbody2D));
                    rigidBody.AddForce(new Vector2(xVel, yVel));
                    rigidBody.AddTorque(torque);
                }
            }
        }

        override public void RemoveFromGame(IGame game)
        {
            gameNodes = null;
            spaceships = null;
            asteroids = null;
            bullets = null;
        }
    }
}
