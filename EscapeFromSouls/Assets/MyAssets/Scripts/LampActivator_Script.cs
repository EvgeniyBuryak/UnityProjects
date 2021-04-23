using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace MyStuff
{
    public class LampActivator_Script : MonoBehaviour
    {
        public Material selectMaterial = null;

        private MeshRenderer meshRenderer = null;
        private Material originalMaterial = null;
        private static bool isPress = false;

        // Можем получить и установить значение переменной только через обертку
        public static bool OnPress
        {
            get => isPress;
            set => isPress = value;
        }

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            originalMaterial = meshRenderer.material;            
        }

        private void Update()
        {
            if (OnPress)
            {
                meshRenderer.material = selectMaterial;
            }
            else
            {
                meshRenderer.material = originalMaterial;
            }
        }
    }
}