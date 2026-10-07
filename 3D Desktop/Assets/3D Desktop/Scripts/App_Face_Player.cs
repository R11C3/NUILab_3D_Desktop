using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class App_Face_Player : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform billboard;
    XRGrabInteractable grab;
    bool facePlayer = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        target = GameObject.Find("MR Interaction Setup").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (grab.isSelected)
            return;
        
        if(!facePlayer)
            return; 

        transform.LookAt(target);
        billboard.LookAt(target);
    }

    public void facePlayerToggle()
    {
        facePlayer = !facePlayer;
    }
}
