using Talaqah.Domain.Common;

namespace Talaqah.Domain.Entities
{
    public class School : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
