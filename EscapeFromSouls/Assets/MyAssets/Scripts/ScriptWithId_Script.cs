using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptWithId_Script : MonoBehaviour
{
    /*public static int id;

    private void Awake()
    {
        id++;
    }*/

    private void Update()
    {
        Destroy(this.gameObject, 2);
    }
}
