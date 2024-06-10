using MagicVilla_API.Modelos;

namespace MagicVilla_API.Repositorio.Contrato
{
    public interface INumeroVillaRepositorio : IRepositorio<NumeroVilla>
    {
        Task<NumeroVilla> Actualizar(NumeroVilla entidad);
    }
}
