using Assets.Scripts.Components;
using Assets.Scripts.Graphics;
using Net.RichardLord.Ash.Core;
using Net.RichardLord.Ash.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class EntityCreator
    {
        private AshGame game;

        public EntityCreator(AshGame game)
        {
            this.game = game;
        }

        public void DestroyEntity(Entity entity)
        {
            var ashentity = entity.Get<AshEntity>(typeof(AshEntity));
            GameObject.DestroyImmediate(ashentity.gameObject);
        }

        private AshEntity LoadPrefabEntity(string path, string entityName)
        {
            // Load the prefab and instantiate it
            var prefab = Resources.Load<GameObject>(path);
            var instance = (GameObject)GameObject.Instantiate(prefab);

            // Make sure its a child of the game
            instance.transform.parent = game.transform;
            instance.name = entityName;

            // Get the ash entity component and set the neccessary properties
            var entity = instance.GetComponent<AshEntity>();
            entity.Engine = game.Engine;
            entity.Entity.Name = entityName;
            return entity;
        }

        public Entity CreateGame()
        {
            return LoadPrefabEntity("Prefabs/Game", "game").Entity;
        }

        public Entity CreateSpaceship()
        {
            return LoadPrefabEntity("Prefabs/Spaceship", "spaceship").Entity;
        }

        public Entity CreateSpaceshipInDeathroes(Transform at)
        {
            var deathroes = LoadPrefabEntity("Prefabs/Spaceship Deathroes", "spaceship deathroes");
            deathroes.transform.position = at.transform.position;
            deathroes.transform.rotation = at.rotation;
            return deathroes.Entity;
        }

        public Entity CreateAsteroidInDeathroes(Transform at)
        {
            var deathroes = LoadPrefabEntity("Prefabs/Asteroid Deathroes", "asteroid deathroes");
            deathroes.transform.position = at.transform.position;
            deathroes.transform.rotation = at.rotation;
            return deathroes.Entity;
        }        

        public Entity CreateWaitForClick()
        {
            var waitEntity = LoadPrefabEntity("Prefabs/Wait For Click", "wait").Entity;
            return waitEntity;
        }

        public Entity CreateAsteroid(AsteroidSize size, Vector3 pos)
        {
            var asteroid = LoadPrefabEntity("Prefabs/Asteroid "+size, "asteroid");
            asteroid.transform.position = pos;  
            return asteroid.Entity;
        }

        public Entity CreateUserBullet(Gun gun, Transform gunTransform)
        {
            var bullet = LoadPrefabEntity("Prefabs/Player Bullet", "player bullet");
            
            bullet.transform.position = gunTransform.position;
            bullet.transform.rotation = gunTransform.rotation;
            bullet.GetComponent<Bullet>().lifeRemaining = gun.bulletLifetime;
            bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 80));

            return bullet.Entity;
        }
    }
}
