using System.ComponentModel.DataAnnotations;

namespace WAGym.Common.Enum
{
    public enum StatusEnum
    {
        [Display(Name = "Ativo")]
        Active = 1,

        [Display(Name = "Inativo")]
        Inactive = 2,
        
        [Display(Name = "Arquivado")]
        Archived = 3
    }
}
