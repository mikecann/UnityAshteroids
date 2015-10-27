using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameConfig : MonoBehaviour
    {
        public Bounds bounds;

        public void InitBounds(Camera cam)
        {
            var size = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            bounds = new Bounds(Vector3.zero, new Vector3(size.x * 2, size.y * 2));
        }
    }
}
