using Assets.Scripts.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ash.Core;
using Assets.Scripts.Nodes;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class GameManagerSystem : ISystem
    {
        private readonly EntityCreator creator;
        private readonly GameConfig config;

        private IEnumerable<GameNode> gameNodes;
        private IEnumerable<Node<Asteroid, Transform>> asteroids;
        private IEnumerable<SpaceshipNode> spaceships;
        private IEnumerable<Node<Bullet>> bullets;

        public GameManagerSystem(EntityCreator creator, GameConfig config)
        {
            this.creator = creator;
            this.config = config;
        }

        public void AddedToEngine(Engine engine)
        {
            gameNodes = engine.GetNodes<GameNode>();
            asteroids = engine.GetNodes<Node<Asteroid, Transform>>();
            spaceships = engine.GetNodes<SpaceshipNode>();
            bullets = engine.GetNodes<Node<Bullet>>();
        }

        public void RemovedFromEngine(Engine engine)
        {
        }

        public void Update(float delta)
        {
            foreach (var game in gameNodes)
            {
                if (!game.state.playing)
                    continue;

                if (!spaceships.Any())
                    RespawnPlayer(game);

                if (IsReadyForNextLevel())
                    NextLevel(game);
            }
        }

        private void NextLevel(GameNode game)
        {
            game.state.level++;

            var asteroidCount = 2 + game.state.level;
            for (int i = 0; i < asteroidCount; i++)
                SpawnAsteroid();
        }

        private void SpawnAsteroid()
        {
            var spaceship = spaceships.First();
            var position = FindPositionForNewAsteroid(spaceship);
            var asteroid = creator.CreateAsteroid(AsteroidSize.Large, position);

            // Give it a kick
            var xVel = UnityEngine.Random.Range(-100f, 100f);
            var yVel = UnityEngine.Random.Range(-100f, 100f);
            var torque = UnityEngine.Random.Range(-100f, 100f);

            var rigidBody = asteroid.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(new Vector2(xVel, yVel));
            rigidBody.AddTorque(torque);
        }

        private Vector2 FindPositionForNewAsteroid(SpaceshipNode spaceship)
        {
            Vector2 position;
            do
            {
                position = new Vector2(UnityEngine.Random.Range(config.bounds.min.x, config.bounds.max.x),
                    UnityEngine.Random.Range(config.bounds.min.y, config.bounds.max.y));
            }
            while (Vector2.Distance(position, spaceship.transform.position) <= 1);
            return position;
        }

        private bool IsReadyForNextLevel()
        {
            return !asteroids.Any() && !bullets.Any() && spaceships.Any();
        }

        private void RespawnPlayer(GameNode game)
        {
            if (game.state.lives > 0)
            {
                if (IsClearToAddShip(Vector2.zero))
                    creator.CreateSpaceship();
            }
            else
            {
                game.state.playing = false;
                creator.CreateWaitForClick();
            }
        }

        private bool IsClearToAddShip(Vector2 newSpaceshipPosition)
        {
            foreach (var asteroid in asteroids)
                if (Vector2.Distance(asteroid.component2.position, newSpaceshipPosition) <= 1f)
                    return false;

            return true;
        }
    }
}
