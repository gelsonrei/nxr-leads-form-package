using System.Collections.Generic;
using UnityEngine;

public class AppConfigManager : TableManager
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    public static void CreateOrUpdate(string key, string value, string type)
    {
        if (GetOne(key) == null)
            Create(key, value, type);
        else Update(key, value, type);
    }

    public static void Create(string key, string value, string type)
    {
        var appConfig = new AppConfig
        {
            Key = key,
            Value = value,
            Type = type,
        };
        dbCon.Insert(appConfig);
        Debug.Log("CREATE AppConfig");
        Print(appConfig);
    }

    public static void Update(string key, string value, string type)
    {
        var appConfig = new AppConfig
        {
            Key = key,
            Value = value,
            Type = type,
        };
        dbCon.Update(appConfig);
        Debug.Log("UPDATE AppConfig");
        Print(appConfig);
    }

    public static AppConfig GetOne(string key)
    {
        string sql = $"SELECT * FROM AppConfig where key=\"{key}\"";
        return Get(sql);
    }

    
    public static AppConfig Get(string sql)
    {
        var appConfigs = QueryList<AppConfig>(sql);
        return appConfigs.Count > 0 ? appConfigs[0] : null;
    }

    public static int GetValueInt(string key)
    {
        AppConfig appConfig =  GetOne(key);
        bool success = int.TryParse(appConfig.Value, out int number);

        if (success)
        {
            return number;
        }
        else
        {
            return -1;
        }

    }

    public static string GetValueStr(string key)
    {
        AppConfig appConfig = GetOne(key);
        return appConfig.Value;
    }

    public static List<AppConfig> GetAll(int limit=-1)
    {
        string sql = "SELECT * FROM AppConfig";

        if (limit > 0)
            sql += " LIMIT " + limit;

        var appConfigs = QueryList<AppConfig>(sql);
        return appConfigs;
    }

}
