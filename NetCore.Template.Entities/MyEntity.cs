using System.ComponentModel.DataAnnotations;

namespace NetCore.Template.Entities
{
    public class MyEntity : EntityWithKey
    {
        [Required]
        [StringLength(50)]
        public string Value { get; set; }
    }
}
