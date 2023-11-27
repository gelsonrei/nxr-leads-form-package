using SqlCipher4Unity3D;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Nxr.FormLeads
{
    public class PremioManager : TableManager
    {

        protected override void Start()
        {
            base.Start();
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public static void CreateOrUpdate(int seq, int id, string nome, string descricao, int categoria, int qtde)
        {
            Premio premioExists = GetOne(id);

            if (id < 0 || premioExists == null)
                Create(seq, nome, descricao, categoria, qtde);
            else Update(premioExists, seq, nome, descricao, categoria, qtde);
        }

        public static void Create(int seq, string nome, string descricao, int categoria, int qtde)
        {
            var premio = new Premio
            {
                Seq = seq,
                Nome = nome,
                Descricao = descricao,
                Qtde = qtde,
                Categoria = categoria,
            };
            dbCon.Insert(premio);
            Debug.Log("CREATE Premio");
            Print(premio);
        }

        public static void Update(Premio oldPremio, int seq, string nome, string descricao, int categoria, int qtde)
        {
            oldPremio.Seq = seq;
            oldPremio.Nome = nome;
            oldPremio.Descricao = descricao;
            oldPremio.Categoria = categoria;
            oldPremio.Qtde = qtde;

            dbCon.Update(oldPremio);
            Debug.Log("UPDATE Premio");
            Print(oldPremio);
        }

        private static bool UpdateSeqAfterDelete()
        {
            List<Premio> premios = GetAll();
            for (int i = 0; i < premios.Count; i++)
            {
                premios[i].Seq = i + 1;
                try
                {
                    dbCon.Update(premios[i]);
                }
                catch (SQLiteException e)
                {
                    Debug.LogWarning("UpdateIdAfterDelete Premio " + e.ToString());
                    return false;
                }
            }

            return true;
        }

        public static bool Delete(int id)
        {
            Premio premioExists = GetOne(id);
            if (premioExists != null)
            {
                dbCon.Delete(premioExists);
                Debug.Log("DELETE Premio");
                Print(premioExists);
            }
            else
            {
                Debug.LogWarning("Delete: Premio ID not found");
                return false;
            }
            UpdateSeqAfterDelete();
            return true;
        }

        public static Premio GetOne(int id)
        {
            string sql = "SELECT * FROM Premio where id=" + id;
            return Get(sql);
        }

        public static Premio GetOne(string nome)
        {
            string sql = "SELECT * FROM Premio where nome=" + nome;
            return Get(sql);
        }

        public static Premio Get(string sql)
        {
            var premios = QueryList<Premio>(sql);
            return premios.Count > 0 ? premios[0] : null;
        }

        public static List<Premio> GetAll(int limit = -1)
        {
            string sql = "SELECT * FROM Premio order by seq";

            if (limit > 0)
                sql += " LIMIT " + limit;

            var premios = QueryList<Premio>(sql);
            return premios;
        }

        public static List<int> GetDistinctCatId()
        {
            var results = GetAll();
            List<int> categoriaIds = results.Select(p => p.Categoria).ToList().Distinct().ToList();

            return categoriaIds;
        }

        public static List<Categoria> GetDistinctCategorias()
        {
            List<Categoria> categoriaList = new();

            foreach (int id in GetDistinctCatId())
            {
                categoriaList.Add(CategoriaManager.GetOne(id));
            }
            return categoriaList;
        }

        public static int GetStock(int premioId)
        {
            string sql = "SELECT * FROM Premio where id=" + premioId;
            var premios = QueryList<Premio>(sql);
            return premios.Count > 0 ? premios[0].Qtde : 0;
        }

        public static int DecreaseStock(int premioId)
        {
            string sql = "UPDATE Premio SET qtde = qtde - 1 WHERE Id = " + premioId;
            var premios = QueryList<Premio>(sql);
            return premios.Count;
        }


    }
}