using Domain.Interfaces;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
