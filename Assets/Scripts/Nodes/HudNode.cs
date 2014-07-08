using Assets.Scripts.Components;
using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Nodes
{
    public class HudNode : Node
    {
        public GameState State { get; set; }
        public Hud Hud { get; set; }
    }
}
