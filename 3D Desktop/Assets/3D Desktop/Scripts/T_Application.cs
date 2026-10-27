using System.IO;
using TMPro;
using UnityEngine;

public abstract class T_Application : MonoBehaviour
{
    public SO_Application appSO;

    [SerializeField] TextMeshProUGUI appName;
    public GameObject buttonHolder;
    public string baseFilePath;
    public string fileName;
    string fileExtension;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        appName.text = fileName;
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

    // Activate should be called when the application is moved into 3D Space
    public abstract void Activate();

    public abstract void Deactivate();
}
