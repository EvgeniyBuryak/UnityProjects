using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTouch_Script : MonoBehaviour
{
    //[SerializeField]
    //private Vector3 velocity;

    //private bool moving;

    /*public GameObject textDebug;

    private TextMesh textScene;
    private BoxCollider colliderPlatform;*/

    private void Start()
    {
        //textScene = textDebug.GetComponent<TextMesh>();
        //colliderPlatform = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //textScene.text = collision.collider.name;// "Player";
        if (collision.gameObject.tag == "Player")
        {
            
        }
        collision.collider.transform.parent.position = transform.position;
        //moving = true;
        // .SetParent(transform, false);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {            
        }
        collision.collider.transform.parent = null;
        //collision.collider.transform.SetParent(null);
        //moving = false;
    }

    private void Update()
    {
        //velocity = GetComponent<ObjectMoving_Script>().target.position;
    }

    private void FixedUpdate()
    {
        /*if (moving)
        {
            transform.position = transform.position + (velocity * Time.deltaTime);
        }*/
    }

    // Don't work with XR Rig (Character Controller) XR Toolkit
    /*bool CheckOtherCollider()
    {
        // tells us about other collider
        Vector3 rayStart = transform.TransformPoint(colliderPlatform.center);
        float rayLength = 10f; //colliderPlatform.center.y + 0.01f;
        bool hasHit = Physics.BoxCast(rayStart, colliderPlatform.size, Vector3.up, out RaycastHit hitInfo, transform.rotation,  rayLength);
        return hasHit;
    }*/
}
