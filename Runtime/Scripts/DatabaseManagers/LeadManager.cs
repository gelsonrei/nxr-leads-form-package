using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
namespace Nxr.FormLeads
{
    public class LeadManager : TableManager
    {
        public static Lead ActualLead;
        protected override void Start()
        {
            base.Start();
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public static void CreateOrUpdate(string cpf, string nome = "", string fone = "", string email = "", string dataNasc = "")
        {
            Lead leadExists = cpf != "000.000.000-00" ? GetOne(cpf) : null;

            if (leadExists == null)
                Create(cpf, nome, fone, email, dataNasc);
            else Update(leadExists, nome, fone, email, dataNasc);
        }

        public static void Create(string cpf, string nome = "", string fone = "", string email = "", string dataNasc = "")
        {
            var lead = new Lead
            {
                Cpf = cpf,
                Name = nome,
                Fone = fone,
                Email = email,
                DataNasc = dataNasc,
                ComplianceAgree = true
            };
            dbCon.Insert(lead);
            ActualLead = lead;
            Debug.Log("CREATE Lead");
            Print(lead);
        }

        public static void Update(Lead oldLead, string nome = "", string fone = "", string email = "", string dataNasc = "")
        {
            oldLead.Name = nome;
            oldLead.Fone = fone;
            oldLead.Email = email;
            oldLead.DataNasc = dataNasc;

            dbCon.Update(oldLead);
            ActualLead = oldLead;
            Debug.Log("UPDATE Lead");
            Print(oldLead);
        }

        public static Lead GetOne(string cpf)
        {
            string sql = "SELECT * FROM Lead where cpf='" + cpf + "'";
            return Get(sql);
        }

        public static Lead GetOne(int id)
        {
            string sql = "SELECT * FROM Lead where id=" + id;
            return Get(sql);
        }


        public static Lead Get(string sql)
        {
            var leads = QueryList<Lead>(sql);
            return leads.Count > 0 ? leads[0] : null;
        }

        public static List<Lead> GetAll(int limit = -1)
        {
            string sql = "SELECT * FROM Lead";

            if (limit > 0)
                sql += " LIMIT " + limit;

            var leads = QueryList<Lead>(sql);
            return leads;
        }


    }
}