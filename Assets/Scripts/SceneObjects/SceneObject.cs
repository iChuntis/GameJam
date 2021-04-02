using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneObjects
{
    // Базовый класс для всех объектов игровой зоны
    public class SceneObject : MonoBehaviour
    {
        // Скорость которую приобретает игрок (или волонтеры, когда пересекают наш объект)
        [SerializeField]
        protected float crossSpeed;

        public float CrossSpeed
        {
            get { return crossSpeed; }
            set { crossSpeed = value; }
        }
    }
}