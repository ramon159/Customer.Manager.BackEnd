namespace Target.Backend.Web.Models
{
    public class Estado
    {
        public int id { get; set; }
        public string nome { get; set; }
        public distrito distrito { get; set; }
    }
    public class distrito
    {
        public int id { get; set; }
        public string nome { get; set; }
    }
}
