using Assets.Scripts.Nodes;
using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Systems
{
    public class HudSystem : SystemBase
    {
        private NodeList nodes;

        override public void AddToGame(IGame game)
        {
            nodes = game.GetNodeList<HudNode>();
        }

        override public void Update(float time)
        {
            for (var node = (HudNode)nodes.Head; node != null; node = (HudNode)node.Next)
            {
                node.Hud.view.SetLives(node.State.lives);
                node.Hud.view.SetScore(node.State.hits);
            }
        }

        override public void RemoveFromGame(IGame game)
        {
            nodes = null;
        }
    }
}
