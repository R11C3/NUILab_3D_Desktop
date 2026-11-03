using System;
using UnityEngine;

public class MediaApplication : T_Application
{
    public override void Activate()
    {
        Debug.Log("Activated");
        buttonHolder.SetActive(true);
        agentObserver.FileReleased(fileName);
    }

    public override void Deactivate()
    {
        Debug.Log("DeActivated");
        buttonHolder.SetActive(false);
    }
}
