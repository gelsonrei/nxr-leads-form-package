using UnityEngine;
using System.Collections.Generic;
using SqlCipher4Unity3D;

namespace Nxr.FormLeads
{
    public enum OrderByEnum
    {
        Nome = 0,
        DtIni = 1,
        DtFim = 2,
        Aual = 3,
        None = 4,

    }

    public class AtivacaoManager : TableManager
    {
        private static readonly string orderByNome = "order by nome ";
        private static readonly string orderByDtIni = "order by substr(dataIni, 7, 4) || '-' || substr(dataIni, 4, 2) || '-' || substr(dataIni, 1, 2) ";
        private static readonly string orderByDtFim = "order by substr(dataFim, 7, 4) || '-' || substr(dataFim, 4, 2) || '-' || substr(dataFim, 1, 2) ";
        private static readonly string orderByDtAtual = "order by atual ";
        private static readonly string orderByNo = "";
        private static readonly string[] orderByArray = { orderByNome, orderByDtIni, orderByDtFim, orderByDtAtual, orderByNo };
        public static OrderByEnum orderByEnum = OrderByEnum.DtFim;
        public static bool orderByDesc = true;

        protected override void Start()
        {
            base.Start();
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public static bool CreateOrUpdate(int id, string nome, string dataIni, string dataFim, int atual)
        {
            if (GetOne(id) == null)
                return Create(id, nome, dataIni, dataFim, atual);
            else return Update(id, nome, dataIni, dataFim, atual);
        }

        public static bool Create(int id, string nome, string dataIni, string dataFim, int atual)
        {
            var ativacao = new Ativacao
            {
                Nome = nome,
                DataIni = dataIni,
                DataFim = dataFim,
                Atual = atual,
            };

            try
            {
                dbCon.Insert(ativacao);
                Debug.Log("CREATE Ativacao");
                Print(ativacao);
            }
            catch (SQLiteException e)
            {
                Debug.LogWarning("CREATE Ativacao " + e.ToString());
                return false;
            }

            return true;
        }

        public static bool Update(int id, string nome, string dataIni, string dataFim, int atual)
        {
            var ativacao = new Ativacao
            {
                Id = id,
                Nome = nome,
                DataIni = dataIni,
                DataFim = dataFim,
                Atual = atual,
            };

            try
            {
                dbCon.Update(ativacao);
                Debug.Log("UPDATE Ativacao");
                Print(ativacao);
            }
            catch (SQLiteException e)
            {
                Debug.LogWarning("UPDATE Ativacao " + e.ToString());
                return false;
            }

            return true;

        }

        public static bool Delete(int id)
        {
            Ativacao ativacaoExists = GetOne(id);
            if (ativacaoExists != null)
            {
                dbCon.Delete(ativacaoExists);
                Debug.Log("DELETED Ativacao");
                Print(ativacaoExists);
            }
            else
            {
                Debug.LogWarning("Delete: Ativacao ID not found");
                return false;
            }
            return true;
        }

        private static Ativacao Get(string sql)
        {
            var ativacao = QueryList<Ativacao>(sql);
            return ativacao.Count > 0 ? ativacao[0] : null;
        }

        public static Ativacao GetActual()
        {
            //data armazenada com formato DD/MM/YYYY precisa ser reconvertida para YYYY-MM-DD ordena��o
            string sql = "SELECT * FROM ativacao WHERE atual = 1 " + orderByDtIni + "DESC LIMIT 1";
            return Get(sql);
        }

        public static Ativacao GetOne(int id)
        {
            string sql = "SELECT * FROM ativacao where id=" + id;
            return Get(sql);
        }


        public static Ativacao GetOne(string nome)
        {
            string sql = "SELECT * FROM ativacao where nome='" + nome + "'";
            return Get(sql);
        }

        public static List<Ativacao> GetAll()
        {
            string sql = "SELECT * FROM ativacao " + orderByArray[(int)orderByEnum] +
                (orderByDesc && orderByEnum != OrderByEnum.None ? " DESC" : "");

            var ativacoes = QueryList<Ativacao>(sql);
            return ativacoes;
        }

    }
}