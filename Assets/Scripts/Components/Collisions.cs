using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class Collisions : MonoBehaviour
    {
        public List<Collision2D> hits = new List<Collision2D>();

        void OnCollisionEnter2D(Collision2D coll)
        {
            hits.Add(coll);
        }
    }
}
