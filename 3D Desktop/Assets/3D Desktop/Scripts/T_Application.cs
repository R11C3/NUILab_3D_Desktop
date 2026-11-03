using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public abstract class T_Application : MonoBehaviour
{
    public SO_Application appSO;

    public TextMeshProUGUI appName;
    public XRGrabInteractable grabInteractable;
    public AgentObserver agentObserver;
    public GameObject buttonHolder;
    public string baseFilePath;
    public string fileName;
    string fileExtension;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        appName.text = fileName;
        agentObserver = Object.FindAnyObjectByType<AgentObserver>();
        Deactivate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFilePath(string path)
    {
        baseFilePath = path;
        fileName = Path.GetFileName(path);
        Debug.Log(fileName);
        fileExtension = Path.GetExtension(path);
        appName.text = fileName;
    }

    public string GetFileName()
    {
        return fileName;
    }

    public string GetFileExtension()
    {
        return fileExtension;
    }

    public void Minimize()
    {
        transform.localScale = Vector3.one * 0.1f;
    }

    public void OnGrabbed(SelectEnterEventArgs args)
    {
        agentObserver.FileGrabbed(fileName);
    }

    public void OnReleased(SelectExitEventArgs args)
    {
        agentObserver.FileReleased(fileName);
    }

    // Activate should be called when the application is moved into 3D Space
    public abstract void Activate();

    public abstract void Deactivate();

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }
}
