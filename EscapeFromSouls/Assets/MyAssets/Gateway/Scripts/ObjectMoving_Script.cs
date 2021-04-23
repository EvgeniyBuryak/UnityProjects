using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MyStuff;

public class ObjectMoving_Script : MonoBehaviour
{
    public UnityEvent OnMoving = null;

    public Transform target;
    public float speed = 0.4f;

    private AudioSource m_MyAudioSource;
    private Vector3     previousPos;
    //private bool lampIsActive;

    //Play the music
    //private bool m_Play;
    //Detect when you use the toggle, ensures music isn’t played multiple times
    private bool m_ToggleChange;
    private bool isPress;
    // Можем получить и установить значение переменной только через обертку
    public bool OnPress
    {
        get => isPress;
        set => isPress = value;
    }

    void Start()
    {        
        m_MyAudioSource = GetComponent<AudioSource>();
        //textScene = textDebug.GetComponent<TextMesh>();
        //lampIsActive = LampActivator_Script.OnPress;

        OnPress = false;
        previousPos = transform.position;
        m_ToggleChange = true;
    }

    void FixedUpdate()
    {
        bool isArrived = IsArrived(transform.position, target.position);
        //CheckMoving(arrived);

        //if (Gateway.OnPress == true)

        // Enable moving platform
        if (OnPress == true)
        {
            //float step = speed * Time.fixedDeltaTime;
            //transform.position = Vector3.MoveTowards(current: transform.position, target.position, step);
            transform.position = DirectionMovement();
        }

        // Enable sound
        if (OnPress == true && m_ToggleChange == true)
        {
            //Play the audio you attach to the AudioSource component
            OnMoving.Invoke();
            //Ensure audio doesn’t play more than once
            m_ToggleChange = false;
            LampActivator_Script.OnPress = true;
            //lampIsActive = true;
        }

        // Disable sound
        if (isArrived == true)
        {
            //Stop the audio
            m_MyAudioSource.Stop();
            //Ensure audio doesn’t play more than once
            m_ToggleChange = true;
            LampActivator_Script.OnPress = false;
            //lampIsActive = false;
        }
    }

    public Vector3 DirectionMovement()
    {
        float step = speed * Time.fixedDeltaTime;
        return Vector3.MoveTowards(current: transform.position, target.position, step);
    }

    /*private void CheckMoving(bool isArrived)
    {        
        if (OnPress == true && isArrived == true)
        {
            textScene.text = "Enabled";
            OnMoving.Invoke();
        }
    }*/

    private bool IsArrived(Vector3 pos, Vector3 trg)
    {
        if (pos.z != trg.z)
        {
            return false;
        }        
        OnPress = false;
        //string str = pos.z.ToString() + OnPress.ToString();
        //MyStuff.TextVisible_Script.onVisible(str); // for debug
        target.position = previousPos;// new Vector3(0f,0f,previousPos.z);
        previousPos = transform.position;
        return true;
    }
}