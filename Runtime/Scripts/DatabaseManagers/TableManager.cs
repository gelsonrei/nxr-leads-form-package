using SqlCipher4Unity3D;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    protected DatabaseHandler databaseHandler;
    protected static SQLiteConnection dbCon;

    protected virtual void Start() { }

    protected virtual void Awake()
    {
        databaseHandler = DatabaseHandler.GetInstance();
        dbCon = databaseHandler.dbConnection;
    }

    protected static int GetActualAtivacao(int ativacaoId = -1)
    {
        ativacaoId = ativacaoId < 0 ? AtivacaoManager.GetActual().Id : ativacaoId;
        return ativacaoId;
    }

    protected static List<T> QueryList<T>(string sql, bool list = false) where T : new()
    {

        var resultList = dbCon.Query<T>(sql);

        if (list)
        {
            foreach (var result in resultList)
                Print(result);
        }

        return resultList;
    }

    public static void Print<T>(T obj)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();

        // Iterar sobre as propriedades e exibir o nome da propriedade e o valor correspondente.
        string result = "Tabela: " + typeof(T).ToString() + " - ";
        foreach (var property in properties)
        {
            var value = property.GetValue(obj);
            result += ($"{property.Name}: {value} | ");
        }
        Debug.Log(result);
    }

    public static Dictionary<string, string> GetFieldNameValue<T>(T obj)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        Dictionary<string, string> keyValuePairs = new();

        foreach (var property in properties)
        {
            var value = property.GetValue(obj);
            keyValuePairs.Add(property.Name, value.ToString());
        }

        return keyValuePairs;
    }

    private void OnDestroy()
    {
        dbCon.Close();
    }


}
