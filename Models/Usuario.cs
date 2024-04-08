using System.ComponentModel.DataAnnotations;

namespace SandraConfecciones.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }
    [Required(ErrorMessage = "El campo Email es obligatorio.")]
    [EmailAddress(ErrorMessage = "El campo Email debe ser una dirección de correo electrónico válida.")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "El campo Password es obligatorio.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La longitud del campo Password debe ser de al menos 6 caracteres.")]
    [RegularExpression(@"^(?=.*[0-9]).{6,}$", ErrorMessage = "El campo Password debe contener al menos un carácter numérico.")]
    public string? Password { get; set; }
    public string[] Roles { get; set; }

    public Usuario()
    {
        Roles = new string[] { "Empleado" };
    }
}
