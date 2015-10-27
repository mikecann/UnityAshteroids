using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ash.Core;
using Ash.Helpers;
using Assets.Scripts.Components;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class GunControlSystem : NodelessSystem<GunControls, Gun, Transform, Audio>
    {
        private readonly EntityCreator _creator;

        public GunControlSystem(EntityCreator creator)
        {
            _creator = creator;
            _updateCallback = OnUpdate;
        }

        private void OnUpdate(float delta, GunControls controls, Gun gun, Transform transform, Audio audio)
        {
            gun.shooting = Input.GetKey(controls.trigger);
            gun.timeSinceLastShot += delta;

            if (CanShoot(gun))
                Shoot(gun, transform, audio);
        }

        private void Shoot(Gun gun, Transform transform, Audio audio)
        {
            _creator.CreateUserBullet(gun, transform.FindChild("Gun"));
            audio.Play(gun.shootSound);
            gun.timeSinceLastShot = 0;
        }

        private bool CanShoot(Gun gun)
        {
            return gun.shooting && gun.timeSinceLastShot >= gun.minimumShotInterval;
        }
    }
}
