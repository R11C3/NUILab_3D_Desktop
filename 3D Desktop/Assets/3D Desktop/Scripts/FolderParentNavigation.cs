using UnityEngine;
using UnityEngine.InputSystem;

using System.IO;

public class FolderParentNavigation : MonoBehaviour
{
    [Header("References")]
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rayInteractor;

    [Header("Controller Input")]
    public InputActionReference navigateParentAction;

    private void OnEnable()
    {
        if (navigateParentAction != null)
        {
            navigateParentAction.action.performed += OnNavigatePressed;
            navigateParentAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (navigateParentAction != null)
        {
            navigateParentAction.action.performed -= OnNavigatePressed;
            navigateParentAction.action.Disable();
        }
    }

    private void OnNavigatePressed(InputAction.CallbackContext context)
    {
        Debug.Log("Navigate Pressed");
        NavigateToParentFolder();
    }

    private void NavigateToParentFolder()
    {
        if (rayInteractor == null)
        {
            Debug.LogError("Missing ray Interactor");
            return;
        }

        Vector3 rayOrigin;
        Vector3 rayDirection;

        rayInteractor.GetLineOriginAndDirection(
            out rayOrigin,
            out rayDirection
        );

        Debug.DrawRay(rayOrigin, rayDirection * 10f, Color.red, 2f);

        if (!Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, 10f))
        {
            Debug.Log("Raycast still missed. Origin: " + rayOrigin +
                      " Direction: " + rayDirection);
            return;
        }

        Debug.Log("Physics ray hit: " + hit.collider.name);

        Folder folder =
            hit.collider.GetComponentInParent<Folder>();

        if (folder == null)
        {
            Debug.Log("Hit object does not contain FileCube");
            return;
        }

        Folder folderCube =
            hit.collider.GetComponentInParent<Folder>();

        if (folderCube == null)
        {
            Debug.Log("Hit object does not contain FileCube");
            return;
        }

        if (folderCube == null)
        {
            Debug.Log("Hovered object does not contain a FileCube");
            return;
        }

        string folderPath = folderCube.baseFilePath;
        Debug.Log(folderPath);

        // Get the folder's parent directory
        DirectoryInfo parentDirectory =
            Directory.GetParent(folderPath);

        if (parentDirectory == null || parentDirectory.FullName.Equals(Application.persistentDataPath))
            return;

        string parentPath = parentDirectory.FullName;

        // Prevent navigation outside the original Files directory
        string rootPath = Path.GetFullPath(
            folderCube.rootFolderPath
        );

        string normalizedParent = Path.GetFullPath(parentPath);

        if (normalizedParent != rootPath &&
            !normalizedParent.StartsWith(
                rootPath + Path.DirectorySeparatorChar,
                System.StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        // Navigate to the parent
        folderCube.NavigateToDirectory(normalizedParent);
    }


}