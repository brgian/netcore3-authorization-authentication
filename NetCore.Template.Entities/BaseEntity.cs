using System.ComponentModel.DataAnnotations;

namespace NetCore.Template.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}