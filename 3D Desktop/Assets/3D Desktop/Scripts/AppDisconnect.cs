using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class AppDisconnect : MonoBehaviour
{
    [SerializeField] XRGrabInteractable grab;

    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        if(grab.isSelected)
            transform.parent = null;
    }
}