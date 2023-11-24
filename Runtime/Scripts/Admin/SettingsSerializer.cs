using UnityEngine;
using System.IO;
using System;

public class SettingsSerializer : ScriptableObject
{
    private static string resourceFolderPath;
    private static string filePath;

    public void Awake()
    {
        resourceFolderPath = Application.dataPath + "/Resources/Data";
        filePath = resourceFolderPath + "/LeadsFormSettings.json";
    }

    public void Serialize(LeadsFormSettings data)
    {
        string jsonData = JsonUtility.ToJson(data);
        try
        {
            File.WriteAllText(filePath, jsonData);
            Debug.Log($"{filePath} Criado com sucesso!");
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao escrever no arquivo {filePath}: {e.Message}");
        }
    }

    public LeadsFormSettings Deserialize()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            LeadsFormSettings data = JsonUtility.FromJson<LeadsFormSettings>(jsonData);
            return data;
        }
        else
        {
            Debug.LogWarning("Arquivo de dados não encontrado.");
            return null;
        }
    }
}
