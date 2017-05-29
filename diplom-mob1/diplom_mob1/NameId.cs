using SQLite;

namespace diplom_mob1
{
    [Table("NameId")]
    public class NameId
    {
        [PrimaryKey, Column("_id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pdf { get; set; }
    }
}