
using System.Collections.Generic;
using UnityEngine;


public class WorkspaceDetectionArea : MonoBehaviour
{
    [Header("Experiment")]
    public TaskCompletionDetector completionDetector;

    [Header("Placement validation")]
    public float requiredStableSeconds = 0.4f;
    public float maxVelocity = 0.08f;

    private class PlacementState
    {
        public float stableTime;
        public bool notified;
    }

    private readonly Dictionary<T_Application, PlacementState>
        candidates = new();

    private void OnTriggerEnter(Collider other)
    {
        T_Application identity =
            other.GetComponentInParent<T_Application>();

        if (identity == null)
            return;

        if (!candidates.ContainsKey(identity))
        {
            candidates.Add(identity, new PlacementState());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        T_Application identity =
            other.GetComponentInParent<T_Application>();

        if (identity != null)
            candidates.Remove(identity);
    }

    private void Update()
    {
        var toRemove = new List<T_Application>();

        foreach (var entry in candidates)
        {
            T_Application identity = entry.Key;
            PlacementState state = entry.Value;

            if (identity == null)
            {
                toRemove.Add(identity);
                continue;
            }

            // Check every collider on the cube's Rigidbody root.
            Rigidbody rb =
                identity.GetComponentInParent<Rigidbody>();

            UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab =
                identity.GetComponentInParent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

            // Do not count a cube that is still being held.
            bool isHeld = grab != null && grab.isSelected;

            if (isHeld)
            {
                state.stableTime = 0f;
                continue;
            }

            // If there is no Rigidbody, we cannot measure velocity.
            // In that case, treat a released cube as stationary.
            bool isStable = rb == null ||
                rb.isKinematic ||
                rb.linearVelocity.magnitude <= maxVelocity;

            if (!isStable)
            {
                state.stableTime = 0f;
                continue;
            }

            state.stableTime += Time.deltaTime;

            if (!state.notified &&
                state.stableTime >= requiredStableSeconds)
            {
                state.notified = true;

                if (completionDetector != null)
                {
                    completionDetector.NotifyFilePlaced(
                        identity.fileName
                    );
                }
            }
        }

        foreach (var identity in toRemove)
        {
            if (identity != null)
                candidates.Remove(identity);
        }
    }
}
