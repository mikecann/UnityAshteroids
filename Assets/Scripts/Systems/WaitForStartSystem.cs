using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ash.Core;
using Ash.Helpers;
using Assets.Scripts.Components;
using Assets.Scripts.Nodes;

namespace Assets.Scripts.Systems
{
    public class WaitForStartSystem : ISystem
    {
        private IEnumerable<WaitForStartNode> waitNodes;
        private IEnumerable<GameNode> gameNodes;
        private IEnumerable<Node<Asteroid, Entity>> asteroids;

        public void AddedToEngine(Engine engine)
        {
            waitNodes = engine.GetNodes<WaitForStartNode>();
            gameNodes = engine.GetNodes<GameNode>();
            asteroids = engine.GetNodes<Node<Asteroid, Entity>>();
        }

        public void RemovedFromEngine(Engine engine)
        {
        }

        public void Update(float delta)
        {
            foreach (var wait in waitNodes)
            {
                if (!wait.wait.startGame)
                    continue;

                foreach (var game in gameNodes)
                {
                    foreach (var asteroid in asteroids)
                        asteroid.component2.Destroy();

                    game.state.SetForStart();
                    wait.wait.startGame = false;
                    wait.entity.Destroy();
                }
            }
        }
    }
}
