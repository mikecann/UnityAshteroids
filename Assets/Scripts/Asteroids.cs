using Assets.Scripts.Systems;
using Net.RichardLord.Ash.Core;
using Net.RichardLord.Ash.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Asteroids : AshGame
    {
		void Awake()
		{
            Engine.AddSystem(new MotionControlSystem(), 0);
		}
	}    
}
