using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace MyStuff
{
    [RequireComponent(typeof(CharacterController))]
    public class ContinuousMovement_Script : MonoBehaviour
    {
        public float speed = 1;      // speed character
        public float gravity = -9.81f; // gravity character
        public XRNode inputSource;     // Получаем входные данные с указанного источника (в нашем случае контроллер)
        public LayerMask groundLayer;
        public float additionalHeight = 0.2f;

        private XRRig rig;
        private float fallingSpeed;   // Скорость падения
        private Vector2 inputAxis;      // записываем значение вектора, с которым мы укажем направления движения персонажа
        private bool inputAxisClick; // регистрируем нажатие стика для бега
        private CharacterController character;      // наш персонаж, направление движения которого мы будем менять
        private static bool onGrounded;

        public bool OnGrounded
        {
            get => onGrounded;
            set => onGrounded = value;
        }

        private void Awake()
        {
            character = GetComponent<CharacterController>();
            //character.center      = new Vector3(0f, 1f, 0f);
            //character.radius      = 0.25f;
            rig = GetComponent<XRRig>();
        }

        void Update()
        {
            // Из входящего компонента устройства получаем значение вектора
            InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
            device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out inputAxisClick); // нажатие стика
        }

        private void FixedUpdate()
        {
            CapsuleFollowHeadset();

            if (inputAxisClick == true)
                speed = 2.5f;
            else
                speed = 1;

            // берём значение угла оси Y относительно камеры, чтобы сложить с траекторией движения персонажа
            Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);

            // = = = = = = = = = = = = = Вписать сюда - добавление движения с помощью платформы

            // Полученные значение вектора применяем к направлению движения персонажу
            Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

            character.Move(direction * Time.fixedDeltaTime * speed);

            // = = = = = = = = = = = = =

            // gravity
            bool isGrounded = CheckIfGrounded();
            if (isGrounded)
                fallingSpeed = 0;
            else
                fallingSpeed += gravity * Time.fixedDeltaTime;
            character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
        }

        void CapsuleFollowHeadset() // Не разобрался как работает
        {
            character.height = rig.cameraInRigSpaceHeight + additionalHeight;
            Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
            character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
        }

        bool CheckIfGrounded()
        {
            // tells us if on ground
            Vector3 rayStart = transform.TransformPoint(character.center);
            float rayLength = character.center.y + 0.01f;
            bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
            if (hasHit)
            {
                //textScene.text    = hitInfo.collider.name;
            }
            OnGrounded = hasHit;
            return hasHit;
        }
    }
}