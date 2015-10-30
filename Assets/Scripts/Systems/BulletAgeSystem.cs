using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ash.Core;
using Ash.Helpers;
using Assets.Scripts.Components;

namespace Assets.Scripts.Systems
{
    public class BulletAgeSystem : NodelessSystem<Bullet, Entity>
    {
        public BulletAgeSystem()
        {
            _updateCallback = OnUpdate;
        }

        private void OnUpdate(float delta, Bullet bullet, Entity entity)
        {
            bullet.age += delta;
            if (bullet.age >= bullet.maxAge)
                entity.Destroy();
        }
    }
}
