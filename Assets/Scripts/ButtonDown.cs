using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonDown : MonoBehaviour
{

    public Transform visualTarget;
    public Vector3 localAxis;
    public float resetSpeed = 0.1f;
    public float followAngleThreshold = 45f;
    private Vector3 InitialPosition;

    private Vector3 offset;
    private Transform pokeAttachTransform;

    private XRBaseInteractable interactable;
    private bool isPressed = false;
    private bool freeze = false;

    // Start is called before the first frame update
    void Start()
    {

        InitialPosition = visualTarget.position;

        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Click);
        interactable.hoverExited.AddListener(UnClick);
        interactable.selectEntered.AddListener(Freeze);
    }


    public void Click(BaseInteractionEventArgs onHover) {
        
        if (onHover.interactorObject is XRPokeInteractor){
            XRPokeInteractor interactor = (XRPokeInteractor)onHover.interactorObject;

            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;
        
            float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));

            if (pokeAngle < followAngleThreshold) {
                isPressed = true;
                freeze = false;
            }
        }

    }

    public void UnClick(BaseInteractionEventArgs onHover) {
        if (onHover.interactorObject is XRPokeInteractor){
            isPressed = false;
        }
    }

    public void Freeze(BaseInteractionEventArgs onHover) {
        if (onHover.interactorObject is XRPokeInteractor){
            freeze = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(freeze) return;

        if (isPressed) {
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);

            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
        }
        else {
            visualTarget.position = Vector3.Lerp(visualTarget.position, InitialPosition, Time.deltaTime * resetSpeed);
        }
    }


}

