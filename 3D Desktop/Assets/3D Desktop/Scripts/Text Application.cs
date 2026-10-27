using System;
using UnityEngine;

public class TextApplication : T_Application
{
    public override void Activate()
    {
        Debug.Log("Activated");
        buttonHolder.SetActive(true);
    }

    public override void Deactivate()
    {
        Debug.Log("DeActivated");
        buttonHolder.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
