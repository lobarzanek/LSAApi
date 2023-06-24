namespace LSAApi.Models
{
    public class Producent
    {
        public int ProducentId { get; set; }
        public string ProducentName { get; set; }
        public ICollection<Model> Models { get; set; }
    }
}
