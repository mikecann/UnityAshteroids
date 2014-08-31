using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Performance
{
    public class AshPerformanceTests : MonoBehaviour
    {
        public int iterations = 10000000;
        public int repeats = 3;

        public GameObject noComponents;
        public GameObject manyComponents;
        public List<Component> components;

        public void TestAll()
        {
            components = new List<Component>();
            Execute(iterations, repeats, "Empty Function", TestEmpty);
            Execute(iterations, repeats, "Get Components With No Components", NoComponentsGetComponents);
            Execute(iterations, repeats, "Get Components With Many Components", ManyComponentsGetComponents);
            Execute(iterations, repeats, "Get Components With No Components List", NoComponentsGetComponentsList);
            Execute(iterations, repeats, "Get Components With Many Components List", ManyComponentsGetComponentsList);

            Debug.Log("components " + components.Count);
        }        

        public void TestEmpty()
        {
        }

        public void NoComponentsGetComponents()
        {
            noComponents.GetComponents<Component>();
        }

        public void ManyComponentsGetComponents()
        {
            manyComponents.GetComponents<Component>();
        }

        public void NoComponentsGetComponentsList()
        {
            noComponents.GetComponents<Component>(components);
        }

        public void ManyComponentsGetComponentsList()
        {     
            manyComponents.GetComponents<Component>(components);
        }

        private void Execute(int iterations, int repeats, string name, Action TestEmpty)
        {
            Debug.Log("");
            Debug.Log(String.Format("Running tests for '{0}'", name));

            var deltas = new List<TimeSpan>();
            for (int i = 0; i < repeats; i++)
            {
                var startTime = DateTime.Now;
                for (int j = 0; j < iterations; j++)
                {
                    TestEmpty();
                }
                var endTime = DateTime.Now;
                var deltaTime = endTime - startTime;
                deltas.Add(deltaTime);
                Debug.Log(String.Format("'{0}' iteration[{1}] took {2}ms",name, i,deltaTime.Milliseconds));
            }

            var average = deltas.Sum(d => d.Milliseconds) / deltas.Count;
            Debug.Log(String.Format("'{0}' average elapsed time was {1}ms", name, average));            
        }

    }
}
