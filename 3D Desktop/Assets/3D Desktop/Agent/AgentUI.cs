
using TMPro;
using UnityEngine;

public class AgentUI : MonoBehaviour
{
    public TMP_Text responseText;

    public void DisplayResponse(string message)
    {
        if (responseText != null)
            responseText.text = message;
    }

    public void ClearResponse()
    {
        if (responseText != null)
            responseText.text = "";
    }
}
