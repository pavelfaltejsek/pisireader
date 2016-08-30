using SQLite;

namespace PISI.Net.ConsoleApplication
{
    public class Zavody
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string NazevZavodu { get; set; }
        public string PoznamkaZavodu { get; set; }
        public string DatumZavodu { get; set; }
    }
}