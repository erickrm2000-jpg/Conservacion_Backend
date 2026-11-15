
using Conservacion.AccesoDatos;
using Conservacion.Entidades;

namespace Conservacion_LogicaNegocio
{
    public class EspecieLN
    {
        private readonly EspecieAD _especieAD;

        public EspecieLN(EspecieAD especieAD)
        {
            _especieAD = especieAD;
        }

        public List<Especie> Listar()
        {
            return _especieAD.Listar();
        }

        public Especie? ObtenerPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El identificador debe ser mayor que cero.");
            }

            return _especieAD.ObtenerPorId(id);
        }

        //Método verifica datos válidos antes de enviarlos a SQL Server
        public int Insertar(Especie especie)
        {
            if (especie == null)
                throw new ArgumentException("Debe proporcionar los datos de la especie.");

            if (string.IsNullOrWhiteSpace(especie.NombreCientifico))
                throw new ArgumentException("El nombre científico es obligatorio.");

            if (string.IsNullOrWhiteSpace(especie.NombreComun))
                throw new ArgumentException("El nombre común es obligatorio.");

            if (especie.Tipo != "Flora" && especie.Tipo != "Fauna")
                throw new ArgumentException("El tipo debe ser Flora o Fauna.");

            string[] estadosPermitidos =
                {
                    "Preocupación menor",
                    "Casi amenazada",
                    "Vulnerable",
                    "En peligro",
                    "En peligro crítico",
                    "Extinta en estado silvestre"
    };

            if (!estadosPermitidos.Contains(especie.EstadoConservacion))
                throw new ArgumentException("El estado de conservación no es válido.");

            return _especieAD.Insertar(especie);
        }

        //Validacion de datos antes de actualizar el registro
        public bool Actualizar(Especie especie)
        {
            if (especie == null)
                throw new ArgumentException("Debe proporcionar los datos de la especie.");

            if (especie.IdEspecie <= 0)
                throw new ArgumentException("El identificador no es válido.");

            if (string.IsNullOrWhiteSpace(especie.NombreCientifico))
                throw new ArgumentException("El nombre científico es obligatorio.");

            if (string.IsNullOrWhiteSpace(especie.NombreComun))
                throw new ArgumentException("El nombre común es obligatorio.");

            if (especie.Tipo != "Flora" && especie.Tipo != "Fauna")
                throw new ArgumentException("El tipo debe ser Flora o Fauna.");

            string[] estadosPermitidos =
            {
                "Preocupación menor",
                "Casi amenazada",
                "Vulnerable",
                "En peligro",
                "En peligro crítico",
                "Extinta en estado silvestre"
            };

            if (!estadosPermitidos.Contains(especie.EstadoConservacion))
                throw new ArgumentException("El estado de conservación no es válido.");

            return _especieAD.Actualizar(especie);
        }


        public bool Eliminar(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El identificador de la especie no es válido.");
            }
            return _especieAD.Eliminar(id);
        }


    }
}
