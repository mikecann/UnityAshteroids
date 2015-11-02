using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ash.Core;
using Assets.Scripts;
using Assets.Scripts.Components;
using Assets.Scripts.Systems;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Tests
{
    [TestFixture]
    public class DeathThroesSystemTests : UnityUnitTest
    {
        private IEngine _engine;
        private DeathThroesSystem _system;

        [SetUp]
        public void Setup()
        {
            _system = new DeathThroesSystem();
            _engine = new Engine();
            _engine.AddSystem(_system, 0);
        }

        [Test]
        public void WhenANodeIsAdded_DeathSoundPlays()
        {
            var e = AddEntityToEngine();

            Assert.AreEqual(1, e.GetComponent<Audio>().toPlay.Count);
            Assert.AreEqual(e.GetComponent<DeathThroes>().deathSound, e.GetComponent<Audio>().toPlay[0]);
        }

        [Test]
        public void AfterUpdate_CountdownDecremented()
        {
            var e = AddEntityToEngine();

            var before = e.GetComponent<DeathThroes>().countdown;

            _engine.Update(1.5f);

            Assert.AreEqual(before - 1.5f, e.GetComponent<DeathThroes>().countdown, 0.01f);
        }

        [Test]
        public void AfterCountdownTimerEnds_EntityDestroyed()
        {
            var e = AddEntityToEngine();

            var before = e.GetComponent<DeathThroes>().countdown;

            _engine.Update(before + 1);

            Assert.IsTrue(e.GetComponent<Entity>().IsDestroyed);
        }

        private GameObject AddEntityToEngine()
        {
            var obj = CreateGameObject();
            obj.AddComponent<DeathThroes>();
            obj.AddComponent<Audio>();
            obj.AddComponent<Entity>();
            _engine.AddEntity(obj.AddComponent<Entity>());
            return obj;
        }
    }
}
