using System.ComponentModel.DataAnnotations;

namespace NetCore.Template.Entities
{
    public abstract class EntityWithKey : BaseEntity
    {
        [Required]
        [StringLength(36)]
        public string Key { get; set; }
    }
}
