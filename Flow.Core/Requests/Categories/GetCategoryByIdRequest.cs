using System.ComponentModel.DataAnnotations;

namespace Flow.Core.Requests.Categories;

public class GetCategoryByIdRequest : Request
{
    [Required(ErrorMessage = "ID inválido.")]
    public long Id { get; set; }
}
