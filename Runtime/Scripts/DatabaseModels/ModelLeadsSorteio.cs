using SQLite.Attribute;

//https://github.com/praeclarum/sqlite-net/wiki


[Table("LeadSorteio")]
public class LeadSorteio : Base
{
    [PrimaryKey, AutoIncrement]
    [Column("id")]
    public int Id { get; set; }

    [Column("lead_id")]
    public int LeadId { get; set; }

    [Column("premio")]
    public int Premio { get; set; }

    [Column("ativacao")]
    public int Ativacao { get; set; }

}



