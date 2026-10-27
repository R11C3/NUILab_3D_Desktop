
using TMPro;
using UnityEngine;

public class ExperimentTimerUI : MonoBehaviour
{
    public ExperimentManager experimentManager;
    public TMP_Text timerText;
    public TMP_Text taskText;

    private void Update()
    {
        if (experimentManager == null)
            return;

        if (timerText != null)
        {
            timerText.text =
                experimentManager.GetFormattedElapsedTime();
        }

        if (taskText != null)
        {
            ExperimentTask task =
                experimentManager.CurrentTask;

            if (task != null)
            {
                taskText.text = task.instruction;
            }
            else if (experimentManager.State ==
                     ExperimentState.Finished)
            {
                taskText.text = "Experiment complete";
            }
            else
            {
                taskText.text =
                    "Ready to begin the next task";
            }
        }
    }
}
