using LSAApi.Models;

namespace LSAApi.Dto
{
    public class CreateModelDto
    {
        public string ModelName { get; set; }
        public int ModelPortNumber { get; set; }
        public int ProducentId { get; set; }

    }
}
