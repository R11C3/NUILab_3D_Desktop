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
}
