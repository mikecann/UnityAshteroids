using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Graphics
{
    public class HudView : MonoBehaviour
    {
        public Text livesTxt;
        public Text scoreTxt;
        
        public void SetLives(int lives)
        {
            livesTxt.text = lives + "";
        }

        public void SetScore(int score)
        {
            scoreTxt.text = "Score: " + score;
        }
    }
}
