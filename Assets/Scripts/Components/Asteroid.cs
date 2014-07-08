using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public enum AsteroidSize
    {
        Large,
        Medium,
        Small,
        Tiny
    }

    public class Asteroid : MonoBehaviour
    {
        public AsteroidSize size = AsteroidSize.Large;
    }
}
