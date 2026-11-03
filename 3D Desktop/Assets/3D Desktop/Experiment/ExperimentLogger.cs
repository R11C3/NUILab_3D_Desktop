using System.IO;
using System.Text;
using UnityEngine;

public class ExperimentLogger
{
    private static string reportDirectoryName = "GameData";
    private static string reportFileName = "participant_log.csv";

    private static string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, reportDirectoryName, reportFileName);
    }

    public static void AppendToCSV(string[] rowData)
    {
        string filePath = GetFilePath();

        string directoryPath = Path.GetDirectoryName(filePath);
        if(!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        bool fileExists = File.Exists(filePath);

        using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8))
        {
            if(!fileExists)
            {
                writer.WriteLine("ParticipantID, Time, TaskNumber");

                for (int i = 0; i < rowData.Length; i++)
                {
                    if(rowData[i].Contains(",") || rowData[i].Contains("\""))
                    {
                        rowData[i] = $"\"{rowData[i].Replace("\"", "\"\"")}\"";
                    }
                }

                string line = string.Join(",", rowData);
                writer.WriteLine(line);    
            }
        }
    }
}
