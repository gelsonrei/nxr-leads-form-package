using SQLite.Attribute;

//https://github.com/praeclarum/sqlite-net/wiki
namespace Nxr.FormLeads
{

    [Table("Ativacao")]
    public class Ativacao : Base
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("dataIni")]
        public string DataIni { get; set; }

        [Column("dataFim")]
        public string DataFim { get; set; }

        [Column("atual")]
        public int Atual { get; set; }

    }
}