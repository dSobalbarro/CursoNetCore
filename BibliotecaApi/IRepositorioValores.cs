using BibliotecaApi.Entidades;

namespace BibliotecaApi
{
    public interface IRepositorioValores
    {
        void InsertarValor(Valor valor);
        IEnumerable<Valor> ObtenerValores();
    }
}
