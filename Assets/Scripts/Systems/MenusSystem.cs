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
    public class MenusSystem : NodelessSystem<MainMenu>
    {
        private IEnumerable<WaitForStartNode> waitNodes;
        private IEnumerable<GameNode> gameNodes;
        private IEnumerable<Node<Asteroid, Entity>> asteroids;

        public MenusSystem()
        {
            _addedToEngineCallback = OnAddedToEngine;
            _updateCallback = OnUpdate;
        }

        private void OnAddedToEngine(Engine engine)
        {
            gameNodes = engine.GetNodes<GameNode>();
            asteroids = engine.GetNodes<Node<Asteroid, Entity>>();
        }

        private void OnUpdate(float arg1, MainMenu menus)
        {
            if (!menus.view.StartClicked)
                return;

            foreach (var game in gameNodes)
            {
                foreach (var asteroid in asteroids)
                    asteroid.Component2.Destroy();

                game.State.SetForStart();
                menus.view.Hide();
                menus.view.StartClicked = false;
            }
        }
    }
}
