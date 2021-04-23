using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStuff
{
    public class TextVisible_Script : MonoBehaviour
    {
        private static TextMesh textScene;

        void Start()
        {
            textScene = GetComponent<TextMesh>();
        }

        public static void OnVisible(string text)
        {
            textScene.text = "MyStuff" + text;
        }
    }
}