using SQLite.Attribute;

//https://github.com/praeclarum/sqlite-net/wiki


[Table("Lead")]
public class Lead: Base
{
    [PrimaryKey, AutoIncrement]
    [Column("id")]
    public int Id { get; set; }

    [Column("cpf")]
    public string Cpf { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("fone")]
    public string Fone { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("data_nasc")]
    public string DataNasc { get; set; }

    [Column("complianceAgree")]
    public bool ComplianceAgree { get; set; }

}



