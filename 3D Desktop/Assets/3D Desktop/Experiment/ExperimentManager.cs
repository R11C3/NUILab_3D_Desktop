
using System;
using UnityEngine;
using UnityEngine.Events;

public enum ExperimentCondition
{
    Solo,
    AgentAssisted
}

public enum ExperimentState
{
    Ready,
    Running,
    Completed,
    Abandoned,
    Finished
}

public class ExperimentManager : MonoBehaviour
{
    [Header("Study configuration")]
    public string participantId = "001";

    public ExperimentCondition condition =
        ExperimentCondition.Solo;

    public ExperimentTask[] tasks;

    [Header("Events")]
    public UnityEvent<ExperimentTask> onTaskStarted;
    public UnityEvent<ExperimentTask> onTaskCompleted;
    public UnityEvent<ExperimentTask> onTaskAbandoned;
    public UnityEvent onExperimentFinished;

    public ExperimentState State { get; private set; }
        = ExperimentState.Ready;

    public ExperimentTask CurrentTask { get; private set; }

    public int CurrentTaskIndex { get; private set; } = -1;

    // Elapsed time for the currently running task.
    public float ElapsedSeconds { get; private set; }

    // Final elapsed time for the most recently ended task.
    public float LastTaskDuration { get; private set; }

    private float taskStartTime;

    public void StartNextTask()
    {
        if (State == ExperimentState.Running ||
            State == ExperimentState.Finished)
            return;

        CurrentTaskIndex++;

        if (tasks == null ||
            CurrentTaskIndex >= tasks.Length)
        {
            State = ExperimentState.Finished;
            CurrentTask = null;
            onExperimentFinished?.Invoke();
            return;
        }

        CurrentTask = tasks[CurrentTaskIndex];

        ElapsedSeconds = 0f;
        LastTaskDuration = 0f;
        taskStartTime = Time.realtimeSinceStartup;

        State = ExperimentState.Running;

        onTaskStarted?.Invoke(CurrentTask);
    }

    private void Update()
    {
        if (State != ExperimentState.Running)
            return;

        ElapsedSeconds =
            Time.realtimeSinceStartup - taskStartTime;
    }

    public void CompleteCurrentTask()
    {
        if (State != ExperimentState.Running)
            return;

        ElapsedSeconds =
            Time.realtimeSinceStartup - taskStartTime;

        LastTaskDuration = ElapsedSeconds;
        State = ExperimentState.Completed;

        onTaskCompleted?.Invoke(CurrentTask);
    }

    public void AbandonCurrentTask()
    {
        if (State != ExperimentState.Running)
            return;

        ElapsedSeconds =
            Time.realtimeSinceStartup - taskStartTime;

        LastTaskDuration = ElapsedSeconds;
        State = ExperimentState.Abandoned;

        onTaskAbandoned?.Invoke(CurrentTask);
    }

    public string GetFormattedElapsedTime()
    {
        int totalSeconds = Mathf.FloorToInt(
            State == ExperimentState.Running
                ? ElapsedSeconds
                : LastTaskDuration
        );

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        return $"{minutes:00}:{seconds:00}";
    }

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }
}
