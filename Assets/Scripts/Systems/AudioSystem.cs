using Assets.Scripts.Nodes;
using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class AudioSystem : SystemBase
    {
        private NodeList nodes;

        override public void AddToGame(IGame game)
        {
            nodes = game.GetNodeList<AudioNode>();
        }

        override public void Update(float time)
        {
            for (var node = (AudioNode)nodes.Head; node != null; node = (AudioNode)node.Next)
            {
                foreach (var clip in node.Audio.toPlay)
                {
                    AudioSource.PlayClipAtPoint(clip, node.Transform.position);
                }
                node.Audio.toPlay.Clear();                
            }
        }

        override public void RemoveFromGame(IGame game)
        {
            nodes = null;
        }
    }
}
