using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStuff
{
    public class PlayerController_Script : MonoBehaviour
    {
        public LayerMask platformLayer;

        private CharacterController character;
        private RaycastHit hitInfo; // информация об объекте, на котором стоит
        private float speed;

        public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
        int elapsedFrames = 0;

        private void Start()
        {
            character = GetComponent<CharacterController>();
            speed = GetComponent<ContinuousMovement_Script>().speed;
        }

        //public LayerMask escapeLayer;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Escape"))
            {
                other.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            bool hasHit = CheckOnPlatform();
            ObjectMoving_Script hitDetect = hitInfo.transform.GetComponent<ObjectMoving_Script>();

            if (hasHit && hitDetect.OnPress == true)
            {
                //float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
                float step = speed * Time.fixedDeltaTime;

                // Полученные значение вектора применяем к направлению движения персонажу
                float between = hitDetect.DirectionMovement().z - transform.position.z;
                Vector3 direction = new Vector3(transform.position.x, 0, hitDetect.DirectionMovement().z);
                transform.position = Vector3.Lerp(transform.position, direction, step);
                // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)
                //elapsedFrames           = (elapsedFrames + 1) % (interpolationFramesCount + 1); 
            }
            //string strhitDetect.DirectionMovement().z.ToString();
            string str = transform.position.z.ToString("0.00");
            //MyStuff.TextVisible_Script.OnVisible(str);
        }

        bool CheckOnPlatform()
        {
            // tells us if on platform
            Vector3 rayStart = transform.TransformPoint(character.center);
            float rayLength = character.center.y + 0.01f;
            bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out hitInfo, rayLength, platformLayer);
            if (hasHit)
            {
                //textScene.text        = hitInfo.collider.name;            
            }
            return hasHit;
        }
    }
}