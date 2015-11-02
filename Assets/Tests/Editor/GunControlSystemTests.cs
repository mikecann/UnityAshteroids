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
    public class GunControlSystemTests : UnityUnitTest
    {
        private IEngine _engine;
        private IEntityCreator _creator;
        private GunControlSystem _system;

        [SetUp]
        public void Setup()
        {
            _creator = Substitute.For<IEntityCreator>();
            _system = new GunControlSystem(_creator);
            _engine = new Engine();
            _engine.AddSystem(_system, 0);
        }

        [Test]
        public void IncrementsTimeSinceGunWasShot()
        {
            var e = AddEntityToEngine();

            _engine.Update(0.16f);

            Assert.AreEqual(0.16f, e.GetComponent<Gun>().timeSinceLastShot, 0.1f);
        }

        [Test]
        public void IsShootingWhenControlsAreTriggering()
        {
            var e = AddEntityToEngine();
            e.GetComponent<GunControls>().isTriggering = true;

            _engine.Update(0.16f);

            Assert.AreEqual(true, e.GetComponent<Gun>().shooting);
        }

        [Test]
        public void IsNotShootingWhenControlsAreNotTriggering()
        {
            var e = AddEntityToEngine();
            e.GetComponent<GunControls>().isTriggering = false;

            _engine.Update(0.16f);

            Assert.AreEqual(false, e.GetComponent<Gun>().shooting);
        }

        [Test]
        public void DoesntCreateBulletWhenNotShooting()
        {
            var e = AddEntityToEngine();
            e.GetComponent<GunControls>().isTriggering = false;
            e.GetComponent<Gun>().minimumShotInterval = 10;

            _engine.Update(100);

            _creator.DidNotReceive().CreateUserBullet(e.GetComponent<Gun>(), e.transform);
        }

        [Test]
        public void DoesntCreateBulletWhenMinimalShotIntervalNotReached()
        {
            var e = AddEntityToEngine();
            e.GetComponent<GunControls>().isTriggering = true;
            e.GetComponent<Gun>().minimumShotInterval = 101;

            _engine.Update(100);

            _creator.DidNotReceive().CreateUserBullet(e.GetComponent<Gun>(), e.transform);
        }

        [Test]
        public void WhenShotCreatesABullet()
        {
            var e = AddEntityToEngine();
            AllowGunToShoot(e);

            _engine.Update(100);

            _creator.Received().CreateUserBullet(e.GetComponent<Gun>(), e.transform);
        }

        [Test]
        public void WhenShotPlaysAudio()
        {
            var e = AddEntityToEngine();
            AllowGunToShoot(e);

            _engine.Update(100);

            Assert.AreEqual(1, e.GetComponent<Audio>().toPlay.Count);
            Assert.AreEqual(e.GetComponent<Gun>().shootSound, e.GetComponent<Audio>().toPlay[0]);
        }

        [Test]
        public void ResetsTimeSinceGunShot()
        {
            var e = AddEntityToEngine();
            AllowGunToShoot(e);

            _engine.Update(100);

            Assert.AreEqual(0, e.GetComponent<Gun>().timeSinceLastShot);
        }

        private void AllowGunToShoot(GameObject obj)
        {
            obj.GetComponent<GunControls>().isTriggering = true;
            obj.GetComponent<Gun>().minimumShotInterval = 10;
        }

        private GameObject AddEntityToEngine()
        {
            var obj = CreateGameObject();
            obj.AddComponent<GunControls>();
            obj.AddComponent<Gun>();
            obj.AddComponent<Audio>();
            _engine.AddEntity(obj.AddComponent<Entity>());
            return obj;
        }
    }
}
