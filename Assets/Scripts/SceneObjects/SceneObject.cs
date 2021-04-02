using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneObjects
{
    public class SceneObject : MonoBehaviour
    {
        [SerializeField]
        protected float crossSpeed;

        public float CrossSpeed
        {
            get { return crossSpeed; }
            set { crossSpeed = value; }
        }
    }
}