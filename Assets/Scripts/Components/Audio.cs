using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class Audio : MonoBehaviour
    {
        public List<AudioClip> toPlay = new List<AudioClip>();

        public void Play(AudioClip clip)
        {
            toPlay.Add(clip);
        }
    }
}
