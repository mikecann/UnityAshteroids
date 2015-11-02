using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class Hitpoints : MonoBehaviour
    {
        public int startingHp = 1;
        public int hp;

        public void ResetToStart()
        {
            hp = startingHp;
        }
    }
}
