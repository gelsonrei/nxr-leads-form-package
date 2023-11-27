using UnityEngine;
using SqlCipher4Unity3D;

//https://github.com/praeclarum/sqlite-net/wiki

namespace Nxr.FormLeads
{
    public class DatabaseHandler
    {

        private static readonly string resourceFolderPath = Application.dataPath + "/NXR Leads Form Package/Resources";
        private string dbUri = resourceFolderPath + "/Data/" + "FortuneWheel2.db";
        public SQLiteConnection dbConnection;
        static public DatabaseHandler instance;
        public static bool isDatabaseLoaded = false;

        private DatabaseHandler()
        {
            Debug.Log(dbUri);
            dbConnection = new SQLiteConnection(dbUri, "130366");
        }

        public static DatabaseHandler GetInstance()
        {
            // Se ainda não houver uma instância, crie uma
            instance ??= new DatabaseHandler();

            isDatabaseLoaded = instance != null;

            return instance;
        }

        public void Close()
        {
            dbConnection.Close();
        }

    }

}