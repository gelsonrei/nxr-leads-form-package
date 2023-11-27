using SQLite.Attribute;

//https://github.com/praeclarum/sqlite-net/wiki
namespace Nxr.FormLeads
{

    [Table("Base")]
    public class Base
    {

        [Column("createdAt")]
        public string CreatedAt { get; set; }

        [Column("updatedAt")]
        public string UpdatedAt { get; set; }

        public Base() { }
    }
}