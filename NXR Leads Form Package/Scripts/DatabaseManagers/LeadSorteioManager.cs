using System;
using UnityEngine;
using System.Linq;
using System.Globalization;
namespace Nxr.FormLeads
{
    public class LeadSorteioManager : TableManager
    {
        protected override void Start()
        {
            base.Start();
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public static void Create(int id, int premioId, int categoriaId, int ativacaoId = -1)
        {
            int stock = PremioManager.GetStock(premioId);
            if (stock < 0)
                Debug.Log("**** SEM STOQUE PARA PREMIO " + premioId);

            var leadSorteio = new LeadSorteio
            {
                LeadId = id,
                Premio = premioId,
                Ativacao = GetActualAtivacao(ativacaoId),
            };
            int rows = dbCon.Insert(leadSorteio);
            if (rows == 1)
            {
                PremioManager.DecreaseStock(premioId);
                CategoriaManager.AddRaffle(categoriaId);
                CategoriaManager.DecreaseStock(categoriaId);
            }
            Debug.Log("CREATE LeadSorteio");
            Print(leadSorteio);
        }

        public static void Update(int id, int premioId, int ativacaoId = -1)
        {
            var leadSorteio = new LeadSorteio
            {
                LeadId = id,
                Premio = premioId,
                Ativacao = GetActualAtivacao(ativacaoId),
            };
            dbCon.Update(leadSorteio);
            Debug.Log("UPDATE LeadSorteio");
            Print(leadSorteio);
        }

        public static LeadSorteio GetOne(int id)
        {
            string sql = "SELECT * FROM LeadSorteio where lead_id='" + id + "' and ativacao=" + GetActualAtivacao(-1);
            var leadSorteios = QueryList<LeadSorteio>(sql);
            return leadSorteios.Count > 0 ? leadSorteios[0] : null;
        }

        public static LeadSorteio GetOneToday(string cpf)
        {
            var lead = LeadManager.GetOne(cpf);
            if (lead == null)
                return null;

            string sql = "SELECT * FROM LeadSorteio where lead_id='" + lead.Id + "' and ativacao=" + GetActualAtivacao(-1);
            var leadSorteios = QueryList<LeadSorteio>(sql);
            //formato armazenado no BD
            string formato = "yyyy-MM-dd HH:mm:ss";
            DateTime today = DateTime.Today;
            //verifica se existe um registro de sorteio no dia com o mesmo CPF
            var sorteiosDoDia = leadSorteios.Where(sorteio =>
            DateTime.ParseExact(sorteio.UpdatedAt, formato, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") ==
                today.ToString("yyyy-MM-dd"));

            //retorna 1 (ou o primeiro) ou nulo se n�o existir
            return sorteiosDoDia.Count() > 0 ? sorteiosDoDia.First() : null;
        }


        public static int GetCount()
        {
            var sql = "SELECT COUNT(*) FROM LeadSorteio WHERE ativacao = (SELECT id FROM ativacao WHERE atual = 1 ORDER BY dataIni DESC LIMIT 1);";
            var command = dbCon.CreateCommand(sql);
            int rowCount = command.ExecuteScalar<int>();
            return rowCount;
        }


        public static bool ContainsAtivacao(int ativacaoId)
        {
            string sql = "SELECT * FROM LeadSorteio where ativacao=" + ativacaoId;
            var leads = QueryList<LeadSorteio>(sql);

            return leads.Count > 0;
        }

    }
}