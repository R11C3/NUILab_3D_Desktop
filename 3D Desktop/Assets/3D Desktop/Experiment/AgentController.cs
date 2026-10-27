
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public ExperimentManager experimentManager;

    public GameObject agentUI;

    public bool IsAgentEnabled =>
        experimentManager != null &&
        experimentManager.condition ==
        ExperimentCondition.AgentAssisted;

    private void Start()
    {
        if (agentUI != null)
            agentUI.SetActive(IsAgentEnabled);
    }

    public void RequestHelp()
    {
        if (!IsAgentEnabled ||
            experimentManager.State !=
            ExperimentState.Running)
            return;

        // Connect your existing agent here.
        // Supply the current task and relevant
        // desktop state to its request handler.
    }
}
