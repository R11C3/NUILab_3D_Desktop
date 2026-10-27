using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class AppDisconnect : MonoBehaviour
{
    [SerializeField] XRGrabInteractable grab;
    T_Application application;

    void Awake()
    {
        grab.selectExited.AddListener(OnGrab);
        application = GetComponent<T_Application>();
    }

    void OnDestroy()
    {
        grab.selectExited.RemoveListener(OnGrab);
    }

    void OnGrab(SelectExitEventArgs args)
    {
        transform.parent = null;
        transform.localScale = Vector3.one * 0.8f;
        transform.rotation = Quaternion.identity;
        application.Activate();
    }
        
}