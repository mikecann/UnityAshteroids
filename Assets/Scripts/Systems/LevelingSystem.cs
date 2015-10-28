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
    public class LevelingSystem : NodelessSystem<GameState>
    {
        private readonly EntityCreator _creator;
        private readonly GameConfig _config;

        private INodeList<Node<Asteroid>> asteroids;
        private INodeList<SpaceshipNode> spaceships;
        private INodeList<Node<Bullet>> bullets;

        public LevelingSystem(EntityCreator creator, GameConfig config)
        {
            _creator = creator;
            _config = config;
            _updateCallback = OnUpdate;
            _addedToEngineCallback = OnAddedToEngine;
        }

        private void OnAddedToEngine(Engine engine)
        {
            asteroids = engine.GetNodes<Node<Asteroid>>();
            spaceships = engine.GetNodes<SpaceshipNode>();
            bullets = engine.GetNodes<Node<Bullet>>();
        }

        private void OnUpdate(float delta, GameState state)
        {
            if (IsReadyForNextLevel())
                NextLevel(state);
        }

        private bool IsReadyForNextLevel()
        {
            return !asteroids.Any() && !bullets.Any() && spaceships.Any();
        }

        private void NextLevel(GameState state)
        {
            state.level++;

            var asteroidCount = 2 + state.level;
            for (int i = 0; i < asteroidCount; i++)
                SpawnAsteroid();
        }

        private void SpawnAsteroid()
        {
            var spaceship = spaceships.First();
            var position = FindPositionForNewAsteroid(spaceship);
            var asteroid = _creator.CreateAsteroid(AsteroidSize.Large, position);

            // Give it a kick
            var vel = UnityEngine.Random.insideUnitCircle.normalized;
            var torque = vel.x / 2;

            var rigidBody = asteroid.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(vel, ForceMode2D.Impulse);
            rigidBody.AddTorque(torque, ForceMode2D.Impulse);
        }

        private Vector2 FindPositionForNewAsteroid(SpaceshipNode spaceship)
        {
            Vector2 position;
            do
            {
                position = new Vector2(UnityEngine.Random.Range(_config.bounds.min.x, _config.bounds.max.x),
                    UnityEngine.Random.Range(_config.bounds.min.y, _config.bounds.max.y));
            }
            while (Vector2.Distance(position, spaceship.Transform.position) <= 1);
            return position;
        }

    }
}
