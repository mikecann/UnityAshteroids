using Assets.Scripts.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ash.Core;
using UnityEngine;

namespace Assets.Scripts
{
    public class AsteroidsBootstrapper : MonoBehaviour
    {
        public PrefabRepository prefabs;
        public GameConfig config;

        private EntityCreator creator;
        private Engine engine;

		void Awake()
		{
            creator = new EntityCreator(prefabs);
            engine = new Engine();
            config.InitBounds(Camera.main);

            engine.AddSystem(new MenusSystem(), SystemPriorities.PreUpdate);
            engine.AddSystem(new PlayerRespawningSystem(creator), SystemPriorities.PreUpdate);
            engine.AddSystem(new LevelingSystem(creator, config), SystemPriorities.Update);
            engine.AddSystem(new MotionControlSystem(), SystemPriorities.Update);
            engine.AddSystem(new GunControlSystem(creator), SystemPriorities.Update);
            engine.AddSystem(new BulletAgeSystem(), SystemPriorities.Update);
            engine.AddSystem(new DeathThroesSystem(), SystemPriorities.Update);
            engine.AddSystem(new MovementSystem(config), SystemPriorities.Move);
            engine.AddSystem(new BulletCollisionSystem(creator), SystemPriorities.ResolveCollisions);
            engine.AddSystem(new SpaceshipCollisionSystem(creator), SystemPriorities.ResolveCollisions);
            engine.AddSystem(new HudSystem(), SystemPriorities.Animate);
            engine.AddSystem(new AudioSystem(), SystemPriorities.Render);

            creator.CreateGame();
		}

        void Update()
        {
            engine.Update(Time.deltaTime);
        }
	}    
}
