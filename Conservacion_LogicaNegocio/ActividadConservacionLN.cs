using Conservacion.AccesoDatos;
using Conservacion.Entidades;

namespace Conservacion_LogicaNegocio
{
    public class ActividadConservacionLN
    {
        private readonly ActividadConservacionAD _actividadAD;

        public ActividadConservacionLN(ActividadConservacionAD actividadAD)
        {
            _actividadAD = actividadAD;
        }

        // Listar todas las actividades
        public List<ActividadConservacion> Listar()
        {
            return _actividadAD.Listar();
        }

        // Obtener una actividad por su identificador
        public ActividadConservacion? ObtenerPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El identificador debe ser mayor que cero.");
            }

            return _actividadAD.ObtenerPorId(id);
        }

        public int Insertar(ActividadConservacion actividad)
        {
            if (actividad == null)
                throw new ArgumentException("Debe proporcionar los datos de la actividad.");

            if (string.IsNullOrWhiteSpace(actividad.Nombre))
                throw new ArgumentException("El nombre de la actividad es obligatorio.");

            if (actividad.FechaInicio == DateTime.MinValue)
                throw new ArgumentException("La fecha de inicio es obligatoria.");

            if (actividad.FechaFin.HasValue && actividad.FechaFin.Value.Date < actividad.FechaInicio.Date)
                throw new ArgumentException("La fecha de finalización no puede ser anterior a la fecha de inicio.");

            if (actividad.IdArea <= 0)
                throw new ArgumentException("Debe seleccionar un área válida.");

            if (actividad.IdPersonal <= 0)
                throw new ArgumentException("Debe seleccionar un responsable válido.");

            return _actividadAD.Insertar(actividad);
        }

        public bool Actualizar(ActividadConservacion actividad)
        {
            if (actividad == null)
                throw new ArgumentException("Debe proporcionar los datos de la actividad.");

            if (actividad.IdActividad <= 0)
                throw new ArgumentException("El identificador de la actividad no es válido.");

            if (string.IsNullOrWhiteSpace(actividad.Nombre))
                throw new ArgumentException("El nombre de la actividad es obligatorio.");

            if (actividad.FechaInicio == DateTime.MinValue)
                throw new ArgumentException("La fecha de inicio es obligatoria.");

            if (actividad.FechaFin.HasValue &&
                actividad.FechaFin.Value.Date < actividad.FechaInicio.Date)
                throw new ArgumentException("La fecha final no puede ser anterior a la fecha inicial.");

            if (actividad.IdArea <= 0)
                throw new ArgumentException("Debe seleccionar un área válida.");

            if (actividad.IdPersonal <= 0)
                throw new ArgumentException("Debe seleccionar un responsable válido.");

            return _actividadAD.Actualizar(actividad);
        }

        public bool Eliminar(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El identificador de la actividad no es válido.");
            }

            return _actividadAD.Eliminar(id);
        }



    }
}
