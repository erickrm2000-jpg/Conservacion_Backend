using Conservacion.AccesoDatos;
using Conservacion.Entidades;

namespace Conservacion_LogicaNegocio
{
    public class RegistroEspecieLN
    {
        private readonly RegistroEspecieAD _registroAD;

        public RegistroEspecieLN(RegistroEspecieAD registroAD)
        {
            _registroAD = registroAD;
        }

        // Listar todos los registros
        public List<RegistroEspecie> Listar()
        {
            return _registroAD.Listar();
        }

        // Obtener un registro por ID
        public RegistroEspecie? ObtenerPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El identificador debe ser mayor que cero.");
            }

            return _registroAD.ObtenerPorId(id);
        }


        public int Insertar(RegistroEspecie registro)
        {
            if (registro == null)
                throw new ArgumentException("Debe proporcionar los datos del registro.");

            if (registro.IdArea <= 0)
                throw new ArgumentException("Debe seleccionar un área válida.");

            if (registro.IdEspecie <= 0)
                throw new ArgumentException("Debe seleccionar una especie válida.");

            if (registro.CantidadEstimada < 0)
                throw new ArgumentException("La cantidad estimada no puede ser negativa.");

            if (registro.FechaRegistro == DateTime.MinValue)
                throw new ArgumentException("La fecha de registro es obligatoria.");

            return _registroAD.Insertar(registro);
        }


        public bool Actualizar(RegistroEspecie registro)
        {
            if (registro == null)
                throw new ArgumentException("Debe proporcionar los datos del registro.");

            if (registro.IdRegistro <= 0)
                throw new ArgumentException("El identificador del registro no es válido.");

            if (registro.IdArea <= 0)
                throw new ArgumentException("Debe seleccionar un área válida.");

            if (registro.IdEspecie <= 0)
                throw new ArgumentException("Debe seleccionar una especie válida.");

            if (registro.CantidadEstimada < 0)
                throw new ArgumentException("La cantidad estimada no puede ser negativa.");

            if (registro.FechaRegistro == DateTime.MinValue)
                throw new ArgumentException("La fecha de registro es obligatoria.");

            return _registroAD.Actualizar(registro);
        }

        public bool Eliminar(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El identificador del registro no es válido.");
            }

            return _registroAD.Eliminar(id);
        }


    }
}
