
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Classe auxiliar para representar pares chave-valor
[System.Serializable]
public class SerializableKeyValuePair
{
    public string Key;
    public bool Value;

    public SerializableKeyValuePair(string key, bool value)
    {
        Key = key;
        Value = value;
    }
}


[System.Serializable]
public class LeadsFormSettings
{
    [System.NonSerialized]
    public bool ConfigMode;

    public bool[] LeadsAdminToggles;
    public Dictionary<string, bool> leadsAdminFieldsToggles;

    // lista serializ�vel para representar o dicion�rio
    [SerializeField]
    private List<SerializableKeyValuePair> leadsAdminFieldsTogglesList = new();

    // Construtor vazio para inicializar a lista
    public LeadsFormSettings()
    {
        leadsAdminFieldsTogglesList = new();
        leadsAdminFieldsToggles = new();
    }

    // M�todo para converter o dicion�rio em lista antes da serializa��o
    public void ConvertDictionaryToList()
    {
        leadsAdminFieldsTogglesList.Clear();
        foreach (var pair in leadsAdminFieldsToggles)
        {
            leadsAdminFieldsTogglesList.Add(new SerializableKeyValuePair(pair.Key, pair.Value));
        }
    }

    // M�todo para converter a lista de volta para o dicion�rio ap�s a desserializa��o
    public void ConvertListToDictionary()
    {
        leadsAdminFieldsToggles.Clear();
        foreach (var kvp in leadsAdminFieldsTogglesList)
        {
            leadsAdminFieldsToggles[kvp.Key] = kvp.Value;
        }
    }

    public void SetLeadsAdminToggles(bool[] values)
    {
        if (LeadsAdminToggles == null || LeadsAdminToggles.Length != values.Length)
            LeadsAdminToggles = new bool[values.Length];

        for (int i = 0; i < LeadsAdminToggles.Length; i++)
        {
            LeadsAdminToggles[i] = values[i];
        }
    }

    public void SetLeadsAdminToggles(List<Toggle> toggles)
    {
        leadsAdminFieldsTogglesList.Clear();
        foreach (Toggle toggle in toggles)
        {
            leadsAdminFieldsTogglesList.Add(new SerializableKeyValuePair(toggle.name, toggle.isOn));
        }
        ConvertListToDictionary();
    }

    public bool[] GetLeadsAdminToggles()
    {
        return LeadsAdminToggles ?? new bool[0];
    }

    public Dictionary<string, bool> GetLeadsAdminTogglesDict()
    {
        return leadsAdminFieldsToggles;
    }
}








