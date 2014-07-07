using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Net.RichardLord.Ash.Unity
{
    public class AshEntity : MonoBehaviour
    {
        private Entity entity;
        private IGame engine;

        void Awake()
        {
            entity = new Entity();
            engine = FindEngine();
            if (engine == null) throw new Exception("Entity could not find Ash engine!");
            AddNewComponents(GetComponents<Component>());
            engine.AddEntity(entity);
        }

        private IGame FindEngine()
        {
            var p = transform.parent;
            while (p != null)
            {
                var e = p.GetComponent<AshGame>();
                if (e != null) return e.Engine;
                p = p.parent;
            }
            return null;
        }

        void Update()
        {
            var components = GetComponents<Component>();
            AddNewComponents(components);
            RemoveOldComponents(components);
        }

        private void AddNewComponents(Component[] components)
        {
            foreach (var component in components)
            {
                if (!entity.Has(component.GetType()))
                    entity.Add(component);
            }
        }

        private void RemoveOldComponents(Component[] components)
        {
            var toRemove = new List<Type>();
            foreach (var pair in entity.Components)
            {
                if (!components.Contains(pair.Value))
                    toRemove.Add(pair.Key);
            }
            foreach (var type in toRemove)
                entity.Remove(type);
        }

        void OnDestroy()
        {
            if (engine == null) return;
            engine.RemoveEntity(entity);
        }

    }
}
