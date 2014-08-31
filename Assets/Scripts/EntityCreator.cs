using Assets.Scripts.Components;
using Assets.Scripts.Graphics;
using Net.RichardLord.Ash.Core;
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

        public void DestroyEntity(EntityBase entity)
        {
            var ashentity = entity.Get<Entity>(typeof(Entity));
            GameObject.DestroyImmediate(ashentity.gameObject);
        }

        private Entity LoadPrefabEntity(string path, string entityName)
        {       
            // Load the prefab and instantiate it
            var prefab = Resources.Load<GameObject>(path);
            var instance = (GameObject)GameObject.Instantiate(prefab);

            // Make sure its a child of the game
            instance.transform.parent = game.transform;
            instance.name = entityName;

            // Get the ash entity component and set the neccessary properties
            var entity = instance.GetComponent<Entity>();
            entity.Engine = game.Engine;
            entity.Name = entityName;
            return entity;
        }

        public Entity CreateGame()
        {
            return LoadPrefabEntity("Prefabs/Game", "game");
        }

        public Entity CreateSpaceship()
        {
            return LoadPrefabEntity("Prefabs/Spaceship", "spaceship");
        }

        public Entity CreateSpaceshipInDeathroes(Transform at)
        {
            var deathroes = LoadPrefabEntity("Prefabs/Spaceship Deathroes", "spaceship deathroes");
            deathroes.transform.position = at.transform.position;
            deathroes.transform.rotation = at.rotation;
            return deathroes;
        }

        public Entity CreateAsteroidInDeathroes(Transform at)
        {
            var deathroes = LoadPrefabEntity("Prefabs/Asteroid Deathroes", "asteroid deathroes");
            deathroes.transform.position = at.transform.position;
            deathroes.transform.rotation = at.rotation;
            return deathroes;
        }

        public Entity CreateWaitForClick()
        {
            var waitEntity = LoadPrefabEntity("Prefabs/Wait For Click", "wait");
            return waitEntity;
        }

        public Entity CreateAsteroid(AsteroidSize size, Vector3 pos)
        {
            var asteroid = LoadPrefabEntity("Prefabs/Asteroid "+size, "asteroid");
            asteroid.transform.position = pos;  
            return asteroid;
        }

        public Entity CreateUserBullet(Gun gun, Transform gunTransform)
        {
            var bullet = LoadPrefabEntity("Prefabs/Player Bullet", "player bullet");
            
            bullet.transform.position = gunTransform.position;
            bullet.transform.rotation = gunTransform.rotation;
            bullet.GetComponent<Bullet>().lifeRemaining = gun.bulletLifetime;
            bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 80));

            return bullet;
        }
    }
}
