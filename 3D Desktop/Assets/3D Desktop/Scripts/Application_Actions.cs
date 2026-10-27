using System;
using TMPro;
using UnityEngine;

public class Application_Actions : MonoBehaviour
{
    T_Application mainApp;
    App_Face_Player appFacePlayer;
    
    [SerializeField] GameObject focusScreen;
    [SerializeField] GameObject icon;
    [SerializeField] TextMeshProUGUI appTitle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainApp = GetComponent<T_Application>();
        appFacePlayer = GetComponent<App_Face_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MinimizeApp()
    {
        mainApp.Minimize();
        mainApp.Deactivate();
        focusScreen.SetActive(false);
        icon.SetActive(true);
        appTitle.rectTransform.localPosition = Vector3.zero;
        GameObject wrist = GameObject.Find("AppInventory");
        transform.parent = wrist.transform;
    }

    public void DestroyApp()
    {
        Destroy(gameObject);
    }

    public void RefreshActivation()
    {
        mainApp.Activate();
    }

    public void FocusApp()
    {
        focusScreen.SetActive(!focusScreen.activeInHierarchy);
        icon.SetActive(!icon.activeInHierarchy);

        if(icon.activeInHierarchy)
        {
            appTitle.rectTransform.localPosition = Vector3.zero;
        }
        else
        {
            Vector3 pos = new Vector3(-0.5f, 1.4f, 0f);
            appTitle.rectTransform.localPosition = pos;
        }
    }

    public void LockAppRotation()
    {
        appFacePlayer.facePlayerToggle();
    }

    public void DuplicateApp()
    {
        Vector3 spawnPosition = new Vector3(
            transform.position.x + 0.5f,
            transform.position.y + 0.5f,
            transform.position.z + 0.5f);
        Instantiate(gameObject, spawnPosition, Quaternion.identity);
    }
}
