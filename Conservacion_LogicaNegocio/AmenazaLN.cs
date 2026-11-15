using Conservacion.AccesoDatos;
using Conservacion.Entidades;

namespace Conservacion_LogicaNegocio
{
    public class AmenazaLN
    {
        private readonly AmenazaAD _amenazaAD;

        public AmenazaLN(AmenazaAD amenazaAD)
        {
            _amenazaAD = amenazaAD;
        }

        // Listar todas las amenazas
        public List<Amenaza> Listar()
        {
            return _amenazaAD.Listar();
        }

        // Obtener una amenaza por ID
        public Amenaza? ObtenerPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El identificador debe ser mayor que cero.");
            }

            return _amenazaAD.ObtenerPorId(id);
        }

        public int Insertar(Amenaza amenaza)
        {
            if (amenaza == null)
                throw new ArgumentException("Debe proporcionar los datos de la amenaza.");

            if (string.IsNullOrWhiteSpace(amenaza.Tipo))
                throw new ArgumentException("El tipo de amenaza es obligatorio.");

            string[] nivelesPermitidos =
            {
                "Bajo", "Medio", "Alto", "Crítico"
    };

            if (!nivelesPermitidos.Contains(amenaza.NivelRiesgo))
                throw new ArgumentException("El nivel de riesgo no es válido.");

            if (amenaza.FechaReporte == DateTime.MinValue)
                throw new ArgumentException("La fecha del reporte es obligatoria.");

            if (amenaza.IdArea <= 0)
                throw new ArgumentException("Debe seleccionar un área válida.");

            return _amenazaAD.Insertar(amenaza);
        }

        public bool Actualizar(Amenaza amenaza)
        {
            if (amenaza == null)
                throw new ArgumentException("Debe proporcionar los datos de la amenaza.");

            if (amenaza.IdAmenaza <= 0)
                throw new ArgumentException("El identificador de la amenaza no es válido.");

            if (string.IsNullOrWhiteSpace(amenaza.Tipo))
                throw new ArgumentException("El tipo de amenaza es obligatorio.");

            string[] nivelesPermitidos =        
                {
                    "Bajo", "Medio", "Alto", "Crítico"
    };

            if (!nivelesPermitidos.Contains(amenaza.NivelRiesgo))
                throw new ArgumentException("El nivel de riesgo no es válido.");

            if (amenaza.FechaReporte == DateTime.MinValue)
                throw new ArgumentException("La fecha del reporte es obligatoria.");

            if (amenaza.IdArea <= 0)
                throw new ArgumentException("Debe seleccionar un área válida.");

            return _amenazaAD.Actualizar(amenaza);
        }

        public bool Eliminar(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El identificador de la amenaza no es válido.");
            }

            return _amenazaAD.Eliminar(id);
        }


    }
}
