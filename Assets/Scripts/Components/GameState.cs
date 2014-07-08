using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class GameState : MonoBehaviour
    {
        public int lives;
        public int level;
        public int hits;
        public bool playing;

        public void SetForStart()
        {
            lives = 3;
            level = 0;
            hits = 0;
            playing = true;
        }
    }
}
