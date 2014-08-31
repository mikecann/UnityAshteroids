using Assets.Scripts.Systems;
using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Asteroids : AshGame
    {
        private EntityCreator creator;
        private GameConfig config;

		void Awake()
		{
            creator = new EntityCreator(this);
            config = new GameConfig();

            var size = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            config.Bounds = new Bounds(Vector3.zero, new Vector3(size.x*2, size.y*2));

            Engine.AddSystem(new WaitForStartSystem(creator), SystemPriorities.PreUpdate);
            Engine.AddSystem(new GameManagerSystem(creator, config), SystemPriorities.PreUpdate);
            Engine.AddSystem(new MotionControlSystem(), SystemPriorities.Update);
            Engine.AddSystem(new GunControlSystem(creator), SystemPriorities.Update);
            Engine.AddSystem(new BulletAgeSystem(creator), SystemPriorities.Update);
            Engine.AddSystem(new DeathThroesSystem(creator), SystemPriorities.Update);
            Engine.AddSystem(new MovementSystem(config), SystemPriorities.Move);
            Engine.AddSystem(new CollisionSystem(creator), SystemPriorities.ResolveCollisions);
            Engine.AddSystem(new HudSystem(), SystemPriorities.Animate);
            Engine.AddSystem(new AudioSystem(), SystemPriorities.Render);   
            
            creator.CreateWaitForClick();
            creator.CreateGame();
		}
	}    
}
