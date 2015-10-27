using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ash.Helpers;
using Assets.Scripts.Components;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class AudioSystem : NodelessSystem<Audio, Transform>
    {
        public AudioSystem()
        {
            _updateCallback = OnUpdate;
        }

        private void OnUpdate(float delta, Audio audio, Transform transform)
        {
            foreach (var clip in audio.toPlay)
                AudioSource.PlayClipAtPoint(clip, transform.position);

            audio.toPlay.Clear();
        }
    }
}
