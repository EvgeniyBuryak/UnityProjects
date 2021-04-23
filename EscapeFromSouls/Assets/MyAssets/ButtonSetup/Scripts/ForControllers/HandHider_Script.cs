using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandHider_Script : MonoBehaviour
{
    public MeshRenderer meshRenderer = null;
    private XRDirectInteractor interactor = null;

    private void Awake()
    {
        //meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        interactor = GetComponent<XRDirectInteractor>();

        interactor.onHoverEntered.AddListener(Hide);
        interactor.onHoverExited.AddListener(Show);
    }

    private void OnDestroy()
    {
        interactor.onHoverEntered.RemoveListener(Hide);
        interactor.onHoverExited.RemoveListener(Show);
    }

    private void Show(XRBaseInteractable interactable)
    {
        meshRenderer.enabled = true;
    }

    private void Hide(XRBaseInteractable interactable)
    {
        meshRenderer.enabled = false;
    }
}
