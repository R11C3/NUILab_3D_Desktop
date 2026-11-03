
using UnityEngine;
using System;

public class AgentObserver : MonoBehaviour
{
    public event Action<string> OnFolderOpened;
    public event Action<string> OnFolderClosed;
    public event Action<string> OnFileGrabbed;
    public event Action<string> OnFileReleased;
    public event Action<string> OnFileDuplicated;

    public string CurrentFolderId { get; private set; }

    public string LastGrabbedFileId { get; private set; }

    public int FolderOpenCount { get; private set; }

    public int WrongFolderCount { get; private set; }

    public void FolderOpened(string folderId)
    {
        CurrentFolderId = folderId;
        FolderOpenCount++;

        OnFolderOpened?.Invoke(folderId);
    }

    public void FolderClosed(string folderId)
    {
        OnFolderClosed?.Invoke(folderId);
    }

    public void FileGrabbed(string fileId)
    {
        LastGrabbedFileId = fileId;

        OnFileGrabbed?.Invoke(fileId);
    }

    public void FileReleased(string fileId)
    {
        OnFileReleased?.Invoke(fileId);
    }

    public void FileDuplicated(string fileId)
    {
        OnFileDuplicated?.Invoke(fileId);
    }

    public void RegisterWrongFolder()
    {
        WrongFolderCount++;
    }

    public void ResetObservations()
    {
        FolderOpenCount = 0;
        WrongFolderCount = 0;
        CurrentFolderId = "";
        LastGrabbedFileId = "";
    }
}
