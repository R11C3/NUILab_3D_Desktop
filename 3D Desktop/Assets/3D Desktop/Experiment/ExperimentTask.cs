using UnityEngine;

[CreateAssetMenu(fileName = "ExperimentTask", menuName = "ExperimentTask")]
public class ExperimentTask : ScriptableObject
{
    public string taskId;
    [TextArea(2,5)]
    public string instruction;

    public string targetId;
    public TaskGoal goal;
}

public enum TaskGoal
{
    LocateFile,
    OpenFolder,
    DuplicateFile,
    OrganizeWorkspace
}
