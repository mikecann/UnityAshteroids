using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Graphics
{
    public class WaitForStartView : MonoBehaviour
    {
        public bool StartClicked { get; set; }

        void OnGUI()
        {
            var btnWidth = 200;
            var btnHeight = 100;
            var btnPos = new Rect(Screen.width / 2 - btnWidth / 2, Screen.height / 2 - btnHeight / 2, btnWidth, btnHeight);

            StartClicked = GUI.Button(btnPos, "Click To Start");
        }
    }

}
