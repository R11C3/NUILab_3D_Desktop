
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
