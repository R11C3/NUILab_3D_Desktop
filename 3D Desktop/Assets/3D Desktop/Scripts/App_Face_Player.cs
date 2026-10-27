using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class App_Face_Player : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform billboard;
    [SerializeField] XRGrabInteractable grab;
    bool facePlayer = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        target = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (grab.isSelected)
            return;
        
        if(!facePlayer)
            return; 

        Vector3 direction = target.position - transform.position;

        direction.y = 0;

        if(direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = lookRotation * Quaternion.Euler(0, 180, 0);
            billboard.rotation = lookRotation * Quaternion.Euler(0, 180, 0);
        }
    }

    public void facePlayerToggle()
    {
        facePlayer = !facePlayer;
        // facePlayer = true;
    }
}
