using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class HandButton_Script : XRBaseInteractable
{
    public UnityEvent OnPress        = null;
    public GameObject activateObject;
    public bool       zActivate      = false;

    private float yMin = 0.0f;
    private float yMax = 0.0f;
    private bool previousPress = false;

    private float previousHandHeight = 0.0f;
    private XRBaseInteractor hoverInteractor = null;

    protected override void Awake()
    {
        base.Awake();
        onHoverEntered.AddListener(StartPress);
        onHoverExited.AddListener(EndPress);
    }

    protected override void OnDestroy()
    {
        onHoverEntered.RemoveListener(StartPress);
        onHoverExited.RemoveListener(EndPress);
    }

    private void StartPress(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
        previousHandHeight = GetLocalYPosition(hoverInteractor.transform.position); //interactor.transform.position.y;
    }

    private void EndPress(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
        previousHandHeight = 0.0f;

        previousPress = false;
        SetYPosition(yMax);
    }

    private void Start()
    {
        //ObjectMoving_Script objectMove = activateObject.GetComponent<ObjectMoving_Script>();

        SetMinMax();
    }

    private void SetMinMax()
    {
        Collider collider = GetComponent<Collider>();
        
        if (zActivate == false)
        {
            yMin = transform.localPosition.y - (collider.bounds.size.y * 0.5f);
            yMax = transform.localPosition.y;
        }
        else
        {
            yMin = transform.localPosition.x - (collider.bounds.size.x * 0.5f);
            yMax = transform.localPosition.x;
        }
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        // Заменяем старое
        //base.ProcessInteractable(updatePhase);
        // На новое
        if (hoverInteractor)
        {
            float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
            float handDifference = previousHandHeight - newHandHeight;
            previousHandHeight = newHandHeight;

            float newPosition;
            if (zActivate == false)
            {
                newPosition = transform.localPosition.y - handDifference;
                
            }
            else
            {
                newPosition = transform.localPosition.x - handDifference;
                
            }
            SetYPosition(newPosition);

            CheckPress();
        }
    }

    private float GetLocalYPosition(Vector3 position)
    {
        Vector3 localPosition = transform.root.InverseTransformPoint(position);
        if (zActivate == false)
            return localPosition.y;
        else
            return localPosition.x;
    }

    private void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        if (zActivate == false) 
            newPosition.y = Mathf.Clamp(position, yMin, yMax);
        else
            newPosition.x = Mathf.Clamp(position, yMin, yMax);
        transform.localPosition = newPosition;
    }

    private void CheckPress()
    {
        bool inPosition = InPosition();

        // Кнопка находится в нажатом состояние
        // значит активируем звук нажатия
        if(inPosition && inPosition != previousPress)
        {
            OnPress.Invoke();
            // Вызвад своё namespace MyButtonStuff, чтобы получить глобальную переменную
            // использовав её чтобы открыть дверь
            // потом попробовать тоже самое через UnityEvent
            //Gateway.OnPress = true;
            activateObject.GetComponent<ObjectMoving_Script>().OnPress = true;
            //MyStuff.LampActivator_Script.OnPress = true;
        }

        previousPress = inPosition;
    }

    private bool InPosition()
    {
        if (zActivate == false)
        {
            float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMin + 0.01f);
            return transform.localPosition.y == inRange;
        }
        else
        {
            float inRange = Mathf.Clamp(transform.localPosition.x, yMin, yMin + 0.01f);
            return transform.localPosition.x == inRange;
        }
            
    }
}
