using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class MotionControl : MonoBehaviour
    {
        public KeyCode left = KeyCode.LeftArrow;
        public KeyCode right = KeyCode.RightArrow;
        public KeyCode accelerate = KeyCode.UpArrow;
        public float accelerationRate = 1f;
        public float rotationRate = 1f;
    }
}
