
using UnityEngine;

public class AgentActionController : MonoBehaviour
{
    public AgentKnowledge knowledge;
    public AgentUI agentUI;

    public void PerformAction(
        AgentAction action,
        string message = "")
    {
        switch (action)
        {
            case AgentAction.HighlightTarget:
                HighlightTarget();
                break;

            case AgentAction.OpenTargetFolder:
                OpenTargetFolder();
                break;

            case AgentAction.SuggestNavigation:
            case AgentAction.ExplainTask:
                agentUI.DisplayResponse(message);
                break;

            case AgentAction.CelebrateCompletion:
                agentUI.DisplayResponse(
                    "The task has been completed. " +
                    "I'm ready for the next task."
                );
                break;
        }
    }

    public void ShowTaskTarget(string targetFileName)
    {
        string fullPath = knowledge.FindFilePath(targetFileName);

        if (string.IsNullOrEmpty(fullPath))
        {
            agentUI.DisplayResponse(
                "I couldn't locate the target file."
            );

            return;
        }

        string displayPath = knowledge.GetDisplayPath(fullPath);

        agentUI.DisplayResponse(
            "Your target file is:\n" + displayPath
        );

        HighlightTarget(targetFileName);
    }


    private void HighlightTarget(string fileId)
    {
        // Find the target cube in the scene.
        T_Application[] cubes =
            FindObjectsByType<T_Application>();

        foreach (T_Application cube in cubes)
        {
            if (cube.fileName == fileId)
            {
                // Apply your project's existing highlight effect here.
                Debug.Log("Highlighting target: " + fileId);

                // Example: call your cube's highlight method.
                // cube.SetHighlighted(true);

                break;
            }
        }
    }

    private void HighlightTarget()
    {
        // Connect to your existing cube highlighting system.
        Debug.Log("Agent is highlighting the target.");
    }

    private void OpenTargetFolder()
    {
        // Connect to your existing folder-opening method.
        Debug.Log("Agent is opening the target folder.");
    }
}
