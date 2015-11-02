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
    public class PlayerRespawningSystem : ISystem
    {
        private readonly EntityCreator _creator;

        private INodeList<GameNode> _gameNodes;
        private INodeList<Node<Asteroid, Transform>> _asteroids;
        private INodeList<SpaceshipNode> _spaceships;
        private INodeList<Node<MainMenu>> _mainMenus;

        public PlayerRespawningSystem(EntityCreator creator)
        {
            _creator = creator;
        }

        public void AddedToEngine(IEngine engine)
        {
            _gameNodes = engine.GetNodes<GameNode>();
            _asteroids = engine.GetNodes<Node<Asteroid, Transform>>();
            _spaceships = engine.GetNodes<SpaceshipNode>();
            _mainMenus = engine.GetNodes<Node<MainMenu>>();
        }

        public void RemovedFromEngine(IEngine engine)
        {
        }

        public void Update(float delta)
        {
            foreach (var game in _gameNodes)
            {
                if (!game.State.playing)
                    continue;

                if (!_spaceships.Any())
                    RespawnPlayer(game);
            }
        }

        private void RespawnPlayer(GameNode game)
        {
            if (game.State.lives > 0)
            {
                if (IsClearToAddShip(Vector2.zero))
                    _creator.CreateSpaceship();
            }
            else
            {
                game.State.playing = false;
                _mainMenus.First().Component1.view.Show();
            }
        }

        private bool IsClearToAddShip(Vector2 newSpaceshipPosition)
        {
            foreach (var asteroid in _asteroids)
                if (Vector2.Distance(asteroid.Component2.position, newSpaceshipPosition) <= 1f)
                    return false;

            return true;
        }
    }
}
