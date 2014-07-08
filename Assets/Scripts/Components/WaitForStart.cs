using Assets.Scripts.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class WaitForStart : MonoBehaviour
    {
        public bool startGame;
        public WaitForStartView waitForStart;

        void Update()
        {
            if (waitForStart.StartClicked) 
                startGame = true;
        }
    }
}
