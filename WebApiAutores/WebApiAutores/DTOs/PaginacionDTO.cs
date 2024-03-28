namespace WebApiAutores.DTOs
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;
        private int RegistrosPorPagina { get; set; } = 10;
        private readonly int CantidadMaximaPorPagina = 50;

        public int RegistrosXPagina
        { 
            get
            {
                return RegistrosPorPagina;
            }
            set
            {
                RegistrosPorPagina = (value > CantidadMaximaPorPagina) ? CantidadMaximaPorPagina : value;
            }
        }
    }
}
