using LSAApi.Models;

namespace LSAApi.Dto
{
    public class UpdateModelDto
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int ModelPortNumber { get; set; }
        public int ProducentId { get; set; }

    }
}
