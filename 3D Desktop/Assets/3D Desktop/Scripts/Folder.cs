using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Folder : T_Application
{
    [SerializeField] GameObject[] folderContents;
    [SerializeField] GameObject sphereOrigin;
    [SerializeField] GameObject mediaAppPrefab;
    [SerializeField] GameObject textAppPrefab;
    [SerializeField] GameObject folderAppPrefab;

    string filePath;

    [Header("Spawning Settings")]
    [SerializeField] float radius = 0.8f;
    
    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, baseFilePath);

        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        Debug.Log("Path: " + filePath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This will instantiate each file inside of the directory in a sphere around the folder
    public void GenerateDirectoryApps()
    {
        ClearFiles();
        FillFolderContents();
        
        float angleStep = 360f / sphereOrigin.transform.childCount;

        for(int i = 0; i < sphereOrigin.transform.childCount; i++)
        {
            float angle = angleStep * i * Mathf.Deg2Rad;

            Vector3 offset = new Vector3(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius,
                0f
            );

            sphereOrigin.transform.GetChild(i).localPosition = offset;
            sphereOrigin.transform.GetChild(i).localScale = Vector3.one * 0.4f;
        }
    }

    void FillFolderContents()
    {
        // Go through all the files in files and instantiate a new SO_Application in each, with a designated file path in the SO
        List<String> paths = new List<string>();
        paths.AddRange(Directory.GetFiles(filePath));
        paths.AddRange(Directory.GetDirectories(filePath));
        
        if(paths.Count == 0)
            return;
        
        folderContents = new GameObject[paths.Count];

        for(int i = 0; i < paths.Count; i++)
        {
            Console.WriteLine(paths[i]);
            string extension = Path.GetExtension(paths[i]).ToLower();

            GameObject appToSpawn;

            if(Directory.Exists(paths[i]))
                appToSpawn = folderAppPrefab;
            else if(extension.Equals(".txt"))
                appToSpawn = textAppPrefab;
            else if(extension.Equals(".mp4"))
                appToSpawn = mediaAppPrefab;
            else if(extension.Equals("folder"))
                appToSpawn = folderAppPrefab;
            else
                appToSpawn = mediaAppPrefab;
            
            GameObject spawn = Instantiate(appToSpawn, sphereOrigin.transform);
            T_Application appDetails = spawn.GetComponent<T_Application>();
            int length = Application.persistentDataPath.Length + 1;
            appDetails.SetFilePath(paths[i].Substring(length));
            Debug.Log(paths[i].Substring(length));
            folderContents[i] = spawn;
            appDetails.Deactivate();
        }
    }

    void ClearFiles()
    {
        for (int i = sphereOrigin.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(sphereOrigin.transform.GetChild(i).gameObject);
        }
    }

    public override void Activate()
    {
        Debug.Log(Application.persistentDataPath);
        buttonHolder.SetActive(true);
        GenerateDirectoryApps();
    }

    public override void Deactivate()
    {
        ClearFiles();
        buttonHolder.SetActive(false);
        fileName += "\\";
    }
}
