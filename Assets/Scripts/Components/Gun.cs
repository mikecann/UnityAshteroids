using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class Gun : MonoBehaviour
    {
        public bool shooting;
        public Vector2 offsetFromParent;
        public float timeSinceLastShot;
        public float minimumShotInterval = 0.3f;
        public float bulletLifetime = 2f;
        public AudioClip shootSound;
    }
}
