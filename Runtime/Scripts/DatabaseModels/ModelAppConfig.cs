using SQLite.Attribute;

//https://github.com/praeclarum/sqlite-net/wiki


[Table("AppConfig")]
public class AppConfig : Base
{
    
    [Column("key")]
    public string Key { get; set; }

    [Column("value")]
    public string Value { get; set; }

    [Column("type")]
    public string Type { get; set; }

}



