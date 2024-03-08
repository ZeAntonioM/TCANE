using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonDown : MonoBehaviour
{

    public Transform visualTarget;

    private Vector3 offset;
    private Transform pokeAttachTransform;

    private XRBaseInteractor interactable;
    private bool isPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<XRBaseInteractor>();
        interactable.hoverEntered.AddListener(Click);
    }

    public void Click(BaseInteractionEventArgs onHover) {
        
        if (onHover.interactorObject is XRPokeInteractor){
            XRPokeInteractor interactor = (XRPokeInteractor)onHover.interactorObject;
            isPressed = true;

            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed) {
            visualTarget.position = pokeAttachTransform.position + offset;
        }
    }
}
