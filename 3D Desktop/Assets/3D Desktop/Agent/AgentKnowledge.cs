using System.IO;
using UnityEngine;

public class AgentKnowledge : MonoBehaviour
{
    private string rootPath;

    private void Awake()
    {
        rootPath = Application.persistentDataPath;
    }

    public string FindFilePath(string targetFileName)
    {
        if (string.IsNullOrEmpty(targetFileName))
            return null;

        try
        {
            string[] results = Directory.GetFiles(
                rootPath,
                targetFileName,
                SearchOption.AllDirectories
            );

            if (results.Length == 0)
            {
                Debug.LogWarning("Target file not found: " + targetFileName);
                return null;
            }

            return results[0];
        }
        catch (System.Exception e)
        {
            Debug.LogError("File search failed: " + e.Message);
            return null;
        }
    }

    public string GetDisplayPath(string fullPath)
    {
        if (string.IsNullOrEmpty(fullPath))
            return null;

        return fullPath
            .Replace(rootPath, "")
            .Replace("\\", "/")
            .TrimStart('/');
    }
}