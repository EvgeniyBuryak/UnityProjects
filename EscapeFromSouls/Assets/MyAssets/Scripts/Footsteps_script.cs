using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace MyStuff
{
    public class Footsteps_script : MonoBehaviour
    {
        public XRNode               inputSource;

        private Vector2             inputAxis;
        private AudioSource         audioSource;
        private float               startAudioPitch;
        private bool                isMoving;
        private ContinuousMovement_Script movement;

        void Awake()
        {
            audioSource           = GetComponent<AudioSource>();
            movement              = GetComponent<ContinuousMovement_Script>();
        }

        private void Start()
        {
            // Скорость воспроизведения звуков шага
            startAudioPitch       = audioSource.pitch;
        }

        void Update()
        {            
            // Обнуляем слайдер звука, когда слайдер доходит до 5.7 секунд
            resettingAudioSlider();

            // Воспроизводим звук шагов, когда игрок двигается
            PlayFootStepAudio();

            // Start TEST
            string str = " Time: " + audioSource.time.ToString("0.00");
            str += " Clip_Lenth: " + audioSource.clip.length.ToString("0.00");
            //audioSource.pitch.ToString("0.00");
            //audioSource.pitch = startAudioPitch;
            //float pitch = audioSource.pitch;
            //string str = " Sum: " + inputAxisX.ToString("0.00");
            //str += " Pitch: " + pitch.ToString("0.00");
            //string str = " X: " + inputAxisX.ToString("0.00");
            //str += " Y: " + inputAxisY.ToString("0.00");
            //MyStuff.TextVisible_Script.OnVisible(PlayFootStepAudio().ToString());            
            MyStuff.TextVisible_Script.OnVisible(str);
            // End TEST
        }

        private void PlayFootStepAudio()
        {
            // if player is moving and audiosource is not playing play it
            /*PlayFootStepAudio() == true && !audioSource.isPlaying*/
            if (IsMoving() == true && audioSource.isPlaying == false)
            {
                audioSource.Play();
            }

            // if player is not moving and audiosource is playing stop it
            if (IsMoving() == false)
            {
                audioSource.Stop();
                resetAudioSource();
            }
        }

        private float GetLargestInputAxis()
        {
            InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

            float inputAxisX = inputAxis.x;
            float inputAxisY = inputAxis.y;

            // Делаем только положительные значения
            if (inputAxisX < 0)
            {
                inputAxisX = inputAxisX * -1f;
            }
            // Делаем только положительные значения
            if (inputAxisY < 0)
            {
                inputAxisY = inputAxisY * -1f;
            }
            // Выбираем наибольшее значение в inputAxisX
            if (inputAxisX < inputAxisY)
            {
                inputAxisX = inputAxisY;
            }
            return inputAxisX;
        }

        private bool IsMoving()
        {
            // Было: imputAxis даёт положительные или отрицательные данные, если их двигать, иначе ноль
            // Стало: inputAxisX - это наибольшее положительное значение, на которое умножим скорость звука.
            if (GetLargestInputAxis() > 0.35f)//(largestInputAxis > 0.35f)// || inputAxisY > 0.35f)
            {
                return true;
            }
            else
                return false;
        }

        private void resettingAudioSlider()
        {
            // Ускоряем скорость воспроизведения звука
            audioSource.pitch = startAudioPitch * GetLargestInputAxis() * movement.speed;
            // Когда аудио запись доходит до 5.7 секунд, обнуляем
            if (audioSource.time > 5.7f)
            {
                audioSource.time = 0.00f;
            }
        }

        private void resetAudioSource()
        {
            // устанавливаем высоту и начало воспроизведения звука в начальное значение
            audioSource.pitch = startAudioPitch;
            audioSource.time = 0.00f;
        }

        private void OnSliderChanger(float step)
        {
            audioSource.time = step * audioSource.clip.length;
        }

        private bool CheckOnGrounded()
        {
            if (movement.OnGrounded == false)
            {
                return false;
            }
            return true;
        }
    }
}