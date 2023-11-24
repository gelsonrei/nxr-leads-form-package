using SQLite.Attribute;

//https://github.com/praeclarum/sqlite-net/wiki

namespace Nxr.FormLeads
{

    [Table("Premio")]
    public class Premio : Base
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("seq")]
        public int Seq { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("categoria")]
        public int Categoria { get; set; }

        [Column("qtde")]
        public int Qtde { get; set; }

    }
}