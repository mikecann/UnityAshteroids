using Ash.Core;
using Assets.Scripts.Components;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IEntityCreator
    {
        void CreateGame();
        void CreateSpaceship();
        Entity CreateSpaceshipInDeathroes(Transform at);
        Entity CreateAsteroidInDeathroes(Transform at);
        Entity CreateAsteroid(AsteroidSize size, Vector3 pos);
        Entity CreateUserBullet(Gun gun, Transform gunTransform);
    }
}