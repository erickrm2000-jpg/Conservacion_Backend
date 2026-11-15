using Conservacion.AccesoDatos;
using Conservacion.Entidades;

namespace Conservacion_LogicaNegocio
{
    public class MonitoreoAmbientalLN
    {
        private readonly MonitoreoAmbientalAD _monitoreoAD;

        public MonitoreoAmbientalLN(MonitoreoAmbientalAD monitoreoAD)
        {
            _monitoreoAD = monitoreoAD;
        }

        // Obtener todos los monitoreos
        public List<MonitoreoAmbiental> Listar()
        {
            return _monitoreoAD.Listar();
        }

        // Obtener un monitoreo por ID
        public MonitoreoAmbiental? ObtenerPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El identificador debe ser mayor que cero.");
            }

            return _monitoreoAD.ObtenerPorId(id);
        }

        public int Insertar(MonitoreoAmbiental monitoreo)
        {
            if (monitoreo == null)
                throw new ArgumentException("Debe proporcionar los datos del monitoreo.");

            if (monitoreo.Fecha == DateTime.MinValue)
                throw new ArgumentException("La fecha del monitoreo es obligatoria.");

            if (string.IsNullOrWhiteSpace(monitoreo.TipoMonitoreo))
                throw new ArgumentException("El tipo de monitoreo es obligatorio.");

            if (string.IsNullOrWhiteSpace(monitoreo.Resultados))
                throw new ArgumentException("Los resultados del monitoreo son obligatorios.");

            if (monitoreo.IdArea <= 0)
                throw new ArgumentException("Debe seleccionar un área válida.");

            return _monitoreoAD.Insertar(monitoreo);
        }

                public bool Actualizar(MonitoreoAmbiental monitoreo)
        {
            if (monitoreo == null)
                throw new ArgumentException("Debe proporcionar los datos del monitoreo.");

            if (monitoreo.IdMonitoreo <= 0)
                throw new ArgumentException("El identificador del monitoreo no es válido.");

            if (monitoreo.Fecha == DateTime.MinValue)
                throw new ArgumentException("La fecha del monitoreo es obligatoria.");

            if (string.IsNullOrWhiteSpace(monitoreo.TipoMonitoreo))
                throw new ArgumentException("El tipo de monitoreo es obligatorio.");

            if (string.IsNullOrWhiteSpace(monitoreo.Resultados))
                throw new ArgumentException("Los resultados son obligatorios.");

            if (monitoreo.IdArea <= 0)
                throw new ArgumentException("Debe seleccionar un área válida.");

            return _monitoreoAD.Actualizar(monitoreo);
        }

        public bool Eliminar(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El identificador del monitoreo no es válido.");
            }

            return _monitoreoAD.Eliminar(id);
        }



    }
}
