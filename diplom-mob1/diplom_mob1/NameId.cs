using SQLite;

namespace diplom_mob1
{
    [Table("NameId")]
    public class NameId
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }

        public int IdTest { get; set; }
        public string Name { get; set; }
        public string Pdf { get; set; }
        [Indexed]
        public int IdStudent { get; set; }
    }
}