using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableInclude_Script : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnablePower"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
