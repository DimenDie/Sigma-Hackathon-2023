using System.IO;
using UnityEngine;


public static class SaveManager
{
        
    private const string SaveFileExtension = ".json";

    private static string _mainSavePath = Application.dataPath;

    public static void Save(string folderName, string fileName, object data)
    {
        CheckOrCreateDirectory(folderName);
            
        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_mainSavePath + $"/{folderName}/" + fileName + SaveFileExtension, json);
            
        Debug.Log(Green($"Saved {fileName} to {folderName}"));
    }
        
    public static T Load<T>(string folderName, string fileName)
    {
        if (!CheckFolder(folderName)) return default;
            
        var json = File.ReadAllText(_mainSavePath + $"/{folderName}/" + fileName + SaveFileExtension);
        var obj = JsonUtility.FromJson<T>(json);
            
        Debug.Log(Green($"Loaded {fileName} from {folderName}"));
            
        return obj;
    }

    public static T[] LoadAllFromDirectory<T>(string folderName)
    {
        CheckOrCreateDirectory(folderName);
        var files = Directory.GetFiles(_mainSavePath + $"/{folderName}/");
        var data = new T[files.Length];
        for (var i = 0; i < files.Length; i++)
        {
            var json = File.ReadAllText(files[i]);
            data[i] = JsonUtility.FromJson<T>(json);
        }
            
        Debug.Log(Green($"Loaded all files from {folderName}"));

        return data;
    }
        
    public static (string[] names, T[] objects) LoadAllFromDirectoryWithNames<T>(string subPath)
    {
        CheckOrCreateDirectory(subPath);
        var files = Directory.GetFiles(_mainSavePath + $"/{subPath}/");

        var names = new string[files.Length];
        var data = new T[files.Length];
        for (var i = 0; i < files.Length; i++)
        {
            names[i] = Path.GetFileNameWithoutExtension(files[i]);
                
            var json = File.ReadAllText(files[i]);
            data[i] = JsonUtility.FromJson<T>(json);
        }
            
        Debug.Log(Green($"Loaded all files from {subPath}"));
            
        return (names, data);
    }

    private static void CheckOrCreateDirectory(string folderName)
    {
        if (Directory.Exists(_mainSavePath + $"/{folderName}/")) return;
            
        Debug.LogWarning($"New directory created: {_mainSavePath}/{folderName}/");
        Directory.CreateDirectory(_mainSavePath + $"/{folderName}/");
    }
        
    private static bool CheckPath(string path)
    {
        if (Directory.Exists(Path.GetDirectoryName(path))) return true;
            
        Debug.LogError("Directory does not exist");
        return false;
    }

    private static bool CheckFolder(string subPath) => CheckPath(_mainSavePath + $"/{subPath}/");
        
    /// <summary>
    /// Sets the color of the text to green (via rich text)
    /// </summary>
    private static string Green(string text) => $"<b><color=green>{text}</color></b>";
}
