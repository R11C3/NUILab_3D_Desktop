
using UnityEngine;

public enum AgentAction
{
    None,
    SuggestNavigation,
    HighlightTarget,
    OpenTargetFolder,
    ExplainTask,
    CelebrateCompletion
}

public class AgentDecisionEngine : MonoBehaviour
{
    [Header("Agent Actions")]
    public AgentActionController actionController;
    public ExperimentManager experimentManager;

    [Header("References")]
    public AgentObserver observer;
    public AgentKnowledge knowledge;

    [Header("Decision settings")]
    public float inactivityThreshold = 30f;
    public float actionCooldown = 12f;
    public int wrongFolderThreshold = 2;

    private float lastInteractionTime;
    private float lastActionTime;

    private bool targetFound;
    private bool taskCompleted;

    private AgentAction lastAction;

    private void OnEnable()
    {
        if (observer == null)
            return;

        // observer.OnFolderOpened += HandleFolderOpened;
        observer.OnFileGrabbed += HandleFileGrabbed;
        observer.OnFileReleased += HandleFileReleased;
        experimentManager.onTaskStarted.AddListener(OnTaskStarted);
        experimentManager.onTaskCompleted.AddListener(OnTaskCompleted);
    }

    private void OnDisable()
    {
        if (observer == null)
            return;

        // observer.OnFolderOpened -= HandleFolderOpened;
        observer.OnFileGrabbed -= HandleFileGrabbed;
        observer.OnFileReleased -= HandleFileReleased;
        experimentManager.onTaskStarted.RemoveListener(OnTaskStarted);
        experimentManager.onTaskCompleted.RemoveListener(OnTaskCompleted);
    }

    private void Start()
    {
        lastInteractionTime = Time.time;
        lastActionTime = -actionCooldown;
    }

    private void Update()
    {
        if (!IsAgentActive())
            return;

        if (taskCompleted || targetFound)
            return;

        if (Time.time - lastInteractionTime >=
            inactivityThreshold)
        {
            TryAction(AgentAction.ExplainTask);
        }
    }

    private bool IsAgentActive()
    {
        return experimentManager != null &&
            experimentManager.condition ==
                ExperimentCondition.AgentAssisted &&
            experimentManager.State ==
                ExperimentState.Running;
    }

    private void HandleFolderOpened(string folderId)
    {
        lastInteractionTime = Time.time;

        if (!IsAgentActive())
            return;

        if (knowledge == null ||
            experimentManager.CurrentTask == null)
            return;

        if (observer.WrongFolderCount >=
            wrongFolderThreshold)
        {
            TryAction(AgentAction.HighlightTarget);
        }
    }

    private void HandleFileGrabbed(string fileId)
    {
        lastInteractionTime = Time.time;

        if (!IsAgentActive())
            return;

        if (fileId ==
            experimentManager.CurrentTask.targetId)
        {
            targetFound = true;
        }
    }

    private void HandleFileReleased(string fileId)
    {
        lastInteractionTime = Time.time;
    }

    private void TryAction(AgentAction action)
    {
        if (Time.time - lastActionTime < actionCooldown)
            return;

        lastActionTime = Time.time;
        lastAction = action;

        ExecuteDecision(action);
    }

    private void ExecuteDecision(AgentAction action)
    {
        if (actionController == null)
            return;

        string message = "";

        switch (action)
        {
            case AgentAction.HighlightTarget:
                message = "I noticed you may be having trouble locating the file. I've highlighted the relevant location.";
                break;

            case AgentAction.OpenTargetFolder:
                message = "I've opened the folder that contains your target file.";
                break;

            case AgentAction.ExplainTask:
                message = "I noticed you haven't made progress recently. Remember, you're looking for the file specified in your current task.";
                break;

            case AgentAction.SuggestNavigation:
                message = "I think another folder may be more relevant to your current task.";
                break;

            case AgentAction.CelebrateCompletion:
                message = "Great work! You've completed the task.";
                break;
        }

        actionController.PerformAction(action, message);
    }

    public void NotifyTaskCompleted()
    {
        taskCompleted = true;

        ExecuteDecision(
            AgentAction.CelebrateCompletion
        );
    }

    public void ResetForNextTask()
    {
        targetFound = false;
        taskCompleted = false;
        lastAction = AgentAction.None;

        if (observer != null)
            observer.ResetObservations();

        lastInteractionTime = Time.time;
        lastActionTime = -actionCooldown;
    }

    private void OnTaskStarted(ExperimentTask task)
    {
        if (experimentManager.condition !=
            ExperimentCondition.AgentAssisted)
            return;

        if (task == null || string.IsNullOrEmpty(task.targetId))
            return;

        actionController.ShowTaskTarget(task.targetId);
    }

    private void OnTaskCompleted(ExperimentTask task)
    {
        NotifyTaskCompleted();
    }
}
