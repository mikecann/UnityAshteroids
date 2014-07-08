using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Graphics
{
    public class HudView : MonoBehaviour
    {
        private GUIText livesTxt;
        private GUIText scoreTxt;

        void Awake()
        {
            livesTxt = transform.FindChild("Lives").GetComponent<GUIText>();
            scoreTxt = transform.FindChild("Score").GetComponent<GUIText>();
        }

        public void SetLives(int lives)
        {
            if (livesTxt!=null)
                livesTxt.text = lives + "";
        }

        public void SetScore(int score)
        {
            if (scoreTxt!=null)
                scoreTxt.text = "Score: " + score;
        }
    }
}
