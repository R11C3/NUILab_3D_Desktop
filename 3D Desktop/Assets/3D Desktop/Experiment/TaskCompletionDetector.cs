
using UnityEngine;

public class TaskCompletionDetector : MonoBehaviour
{
    public ExperimentManager experimentManager;

    // Call this from the existing file interaction code
    // when a file is placed in the designated workspace.
    public void NotifyFilePlaced(string fileId)
    {
        if (experimentManager == null ||
            experimentManager.State !=
            ExperimentState.Running)
            return;

        ExperimentTask task =
            experimentManager.CurrentTask;

        if (task.goal == TaskGoal.LocateFile &&
            fileId == task.targetId)
        {
            experimentManager.CompleteCurrentTask();
        }
    }

    // Call only after verifying both the duplicate
    // and the original are still present.
    public void NotifyDuplicateVerified(string fileId)
    {
        if (experimentManager == null ||
            experimentManager.State !=
            ExperimentState.Running)
            return;

        ExperimentTask task =
            experimentManager.CurrentTask;

        if (task.goal == TaskGoal.DuplicateFile &&
            fileId == task.targetId)
        {
            experimentManager.CompleteCurrentTask();
        }
    }
}
