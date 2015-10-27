using Assets.Scripts.Components;
using Assets.Scripts.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ash.Core;
using UnityEngine;

namespace Assets.Scripts
{
    public class EntityCreator
    {
        private readonly PrefabRepository prefabs;

        public EntityCreator(PrefabRepository prefabs)
        {
            this.prefabs = prefabs;
        }

        public void CreateGame()
        {
            Instantiate(prefabs.game);
        }

        public void CreateSpaceship()
        {
            Instantiate(prefabs.spaceship);
        }

        public Entity CreateSpaceshipInDeathroes(Transform at)
        {
            var deathroes = Instantiate(prefabs.spaceshipDeathroes);
            deathroes.transform.position = at.transform.position;
            deathroes.transform.rotation = at.rotation;
            return deathroes;
        }

        public Entity CreateAsteroidInDeathroes(Transform at)
        {
            var deathroes = Instantiate(prefabs.asteroidInDeathroes);
            deathroes.transform.position = at.transform.position;
            deathroes.transform.rotation = at.rotation;
            return deathroes;
        }

        public Entity CreateAsteroid(AsteroidSize size, Vector3 pos)
        {
            var prefab = prefabs.GetAsteroid(size);
            var asteroid = Instantiate(prefab);
            asteroid.transform.position = pos;
            return asteroid;
        }

        public Entity CreateUserBullet(Gun gun, Transform gunTransform)
        {
            var bullet = Instantiate(prefabs.playerBullet);

            bullet.transform.position = gunTransform.position;
            bullet.transform.rotation = gunTransform.rotation;
            bullet.GetComponent<Bullet>().lifeRemaining = gun.bulletLifetime;
            bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 80));

            return bullet;
        }

        public Entity Instantiate(GameObject prefab)
        {
#if UNITY_EDITOR
            var obj = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(prefab);
            return obj.GetComponent<Entity>();
#else
            return GameObject.Instantiate(prefab).GetComponent<Entity>();
#endif
        }
    }
}
