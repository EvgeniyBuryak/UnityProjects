using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCast_Script : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        float maxDistance = 1f;
        bool isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.up, out RaycastHit hitInfo, transform.rotation, maxDistance);
        if (isHit)
        {
            Debug.Log("Hit: " + hitInfo.collider.name);
        }

        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.up * hitInfo.distance);
            Gizmos.DrawWireCube(transform.position + transform.up * hitInfo.distance, transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.up * maxDistance);
            Gizmos.DrawWireCube(transform.position + transform.up * maxDistance, transform.lossyScale);
        }
    }
}
