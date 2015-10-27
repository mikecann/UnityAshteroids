using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ash.Helpers;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class MovementSystem : NodelessSystem<Transform, Rigidbody2D>
    {
        private readonly GameConfig _config;

        public MovementSystem(GameConfig config)
        {
            _config = config;
            _updateCallback = OnUpdate;
        }

        private void OnUpdate(float delta, Transform transform, Rigidbody2D rigidbody)
        {
            if (transform.position.x < _config.bounds.min.x)
            {
                transform.position = new Vector3(transform.position.x + 
                    _config.bounds.size.x, transform.position.y, transform.position.z);
            }
            if (transform.position.x > _config.bounds.max.x)
            {
                transform.position = new Vector3(transform.position.x - 
                    _config.bounds.size.x, transform.position.y, transform.position.z);
            }
            if (transform.position.y < _config.bounds.min.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y 
                    + _config.bounds.size.y, transform.position.z);
            }
            if (transform.position.y > _config.bounds.max.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y 
                    - _config.bounds.size.y, transform.position.z);
            }
        }
    }
}
