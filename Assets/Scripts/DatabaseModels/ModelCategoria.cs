using SQLite.Attribute;

//https://github.com/praeclarum/sqlite-net/wiki

namespace Nxr.FormLeads
{
    [Table("Categoria")]
    public class Categoria : Base
    {

        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("seq")]
        public int Seq { get; set; }

        [Column("qtde")]
        public int Qtde { get; set; }

        [Unique]
        [Column("nome")]
        public string Nome { get; set; }

        [Column("probabilidade")]
        public int Probabilidade { get; set; }

        [Column("logoPath")]
        public string LogoPath { get; set; }

        [Column("parabensPath")]
        public string ParabensPath { get; set; }

    }
}