using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ash.Core;
using Ash.Helpers;
using Assets.Scripts.Components;

namespace Assets.Scripts.Systems
{
    public  class DeathThroesSystem : NodelessSystem<DeathThroes, Entity, Audio>
    {
        public DeathThroesSystem()
        {
            _updateCallback = OnUpdate;
        }

        private void OnUpdate(float delta, DeathThroes death, Entity entity, Audio audio)
        {
            if (death.countdown<0.001f)
                audio.Play(death.deathSound);

            death.countdown -= delta;
            if (death.countdown <= 0)
                entity.Destroy();
        }
    }
}
