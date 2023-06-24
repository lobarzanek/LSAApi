namespace LSAApi.Models
{
    public class Model
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int ModelPortNumber { get; set; }
        public int ProducentId { get; set; }
        public Producent Producent { get; set; }
        public ICollection<Switch> Switchs { get; set; }
    }
}
