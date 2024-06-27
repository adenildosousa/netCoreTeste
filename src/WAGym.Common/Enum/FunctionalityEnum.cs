using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WAGym.Common.Enum
{
    public enum FunctionalityEnum
    {
        [Display(Name = "Cadastro de usuário")]
        CreateUser = 1,

        [Display(Name = "Edição de usuário")]
        EditUser = 2,

        [Display(Name = "Visualização (Detalhes) de usuário")]
        DetailUser = 3,

        [Display(Name = "Exclusão de usuário")]
        DeleteUser = 4,

        [Display(Name = "Lista de usuários")]
        ListUser = 5,

        [Display(Name = "Cadastro de pessoa")]
        CreatePerson = 6,

        [Display(Name = "Edição de pessoa")]
        EditPerson = 7,

        [Display(Name = "Excluir pessoa")]
        DeletePerson = 8,

        [Display(Name = "Detalhe de pessoa")]
        DetailPerson = 9,

        [Display(Name = "Lista de pessoa")]
        ListPerson = 10,

        [Display(Name = "Cadastrar Perfil x Usuário")]
        CreateProfileUser = 11
    }
}
