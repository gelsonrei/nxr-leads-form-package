using SqlCipher4Unity3D;
using System.Collections.Generic;
using UnityEngine;
namespace Nxr.FormLeads
{
    public class CategoriaManager : TableManager
    {

        protected override void Start()
        {
            base.Start();
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public static bool CreateOrUpdate(int seq, int id, string nome, int probabilidade, int qtde, string logoPath, string parabensPath)
        {

            Categoria categoriaExists = GetOne(id);

            if (id < 0 || categoriaExists == null)
                return Create(seq, nome, probabilidade, qtde, logoPath, parabensPath);
            else return Update(categoriaExists, seq, nome, probabilidade, qtde, logoPath, parabensPath);
        }

        public static bool Create(int seq, string nome, int probabilidade, int qtde, string logoPath, string parabensPath)
        {
            var categoria = new Categoria
            {
                Seq = seq,
                Nome = nome,
                Probabilidade = probabilidade,
                Qtde = qtde,
                LogoPath = logoPath,
                ParabensPath = parabensPath,
            };
            try
            {
                dbCon.Insert(categoria);
                Debug.Log("CREATE Categoria");
                Print(categoria);
            }
            catch (SQLiteException e)
            {
                Debug.LogWarning("CREATE Categoria " + e.ToString());
                return false;
            }

            return true;

        }

        public static bool Update(Categoria oldCategoria, int seq, string nome, int probabilidade, int qtde, string logoPath, string parabensPath)
        {
            oldCategoria.Seq = seq;
            oldCategoria.Nome = nome;
            oldCategoria.Probabilidade = probabilidade;
            oldCategoria.Qtde = qtde;
            oldCategoria.LogoPath = logoPath;
            oldCategoria.ParabensPath = parabensPath;
            try
            {
                dbCon.Update(oldCategoria);
                Debug.Log("UPDATE Categoria");
                Print(oldCategoria);
            }
            catch (SQLiteException e)
            {
                Debug.LogWarning("UPDATE Categoria " + e.ToString());
                return false;
            }

            return true;
        }

        private static bool UpdateSeqAfterDelete()
        {
            List<Categoria> categorias = GetAll();
            for (int i = 0; i < categorias.Count; i++)
            {
                categorias[i].Seq = i + 1;
                try
                {
                    dbCon.Update(categorias[i]);
                }
                catch (SQLiteException e)
                {
                    Debug.LogWarning("UpdateIdAfterDelete Categoria " + e.ToString());
                    return false;
                }
            }

            return true;
        }

        public static bool Delete(int id)
        {
            Categoria categoriaExists = GetOne(id);
            if (categoriaExists != null)
            {
                dbCon.Delete(categoriaExists);
                Debug.Log("DELETED Categoria");
                Print(categoriaExists);
            }
            else
            {
                Debug.LogWarning("Delete: Categoria ID not found");
                return false;
            }
            UpdateSeqAfterDelete();
            return true;
        }

        public static Categoria GetOne(int id)
        {
            string sql = "SELECT * FROM Categoria where id=" + id;
            return Get(sql);
        }

        public static Categoria GetOne(string nome)
        {
            string sql = "SELECT * FROM Categoria where nome='" + nome + "'";
            return Get(sql);
        }

        private static Categoria Get(string sql)
        {
            var categorias = QueryList<Categoria>(sql);
            return categorias.Count > 0 ? categorias[0] : null;
        }

        public static List<Categoria> GetAll(int limit = -1)
        {
            string sql = "SELECT * FROM Categoria order by seq";

            if (limit > 0)
                sql += " LIMIT " + limit;

            var categorias = QueryList<Categoria>(sql);
            return categorias;
        }

        public static int AddRaffle(int categoriaId)
        {
            string sql = "UPDATE Categoria SET qtdeSorteio = qtdeSorteio + 1 WHERE Id = " + categoriaId;
            var categorias = QueryList<Categoria>(sql);
            return categorias.Count;
        }

        public static int DecreaseStock(int categoriaId)
        {
            string sql = "UPDATE Categoria SET qtde = qtde - 1 WHERE Id = " + categoriaId;
            var categoria = QueryList<Categoria>(sql);
            return categoria.Count;
        }

    }
}