namespace Fiap.Api.Semaforos.Models
{
    public class UsuarioModel
    {
        public long UsuarioId { get; set; }

        public required string Email { get; set; } 
        public required string Senha { get; set; } 
        public required string Role { get; set; } 

    }
}
