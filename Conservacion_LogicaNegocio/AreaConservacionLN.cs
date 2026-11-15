
using Conservacion.AccesoDatos;
using Conservacion.Entidades;

namespace Conservacion_LogicaNegocio
{
    public class AreaConservacionLN
    {
        private readonly AreaConservacionAD _areaAD;

        public AreaConservacionLN(AreaConservacionAD areaAD)
        {
            _areaAD = areaAD;
        }

        public List<AreaConservacion> Listar()
        {
            return _areaAD.Listar();
        }

        public AreaConservacion? ObtenerPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El identificador debe ser mayor que cero.");
            }

            return _areaAD.ObtenerPorId(id);
        }


        public int Insertar(AreaConservacion area)
        {
            if (area == null)
                throw new ArgumentException("Debe proporcionar los datos del área.");

            if (string.IsNullOrWhiteSpace(area.Nombre))
                throw new ArgumentException("El nombre del área es obligatorio.");

            if (string.IsNullOrWhiteSpace(area.Ubicacion))
                throw new ArgumentException("La ubicación es obligatoria.");

            if (area.ExtensionHectareas <= 0)
                throw new ArgumentException("La extensión debe ser mayor que cero.");

            if (area.FechaCreacion == DateTime.MinValue)
                throw new ArgumentException("Debe indicar una fecha de creación.");

            return _areaAD.Insertar(area);
        }


        public bool Actualizar(AreaConservacion area)
        {
            if (area == null)
                throw new ArgumentException("Debe proporcionar los datos del área.");

            if (area.IdArea <= 0)
                throw new ArgumentException("El identificador del área no es válido.");

            if (string.IsNullOrWhiteSpace(area.Nombre))
                throw new ArgumentException("El nombre del área es obligatorio.");

            if (string.IsNullOrWhiteSpace(area.Ubicacion))
                throw new ArgumentException("La ubicación es obligatoria.");

            if (area.ExtensionHectareas <= 0)
                throw new ArgumentException("La extensión debe ser mayor que cero.");

            if (area.FechaCreacion == DateTime.MinValue)
                throw new ArgumentException("La fecha de creación no es válida.");

            return _areaAD.Actualizar(area);
        }


        public bool Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException(
                    "El identificador no es válido.");

            return _areaAD.Eliminar(id);
        }




    }
}
