using Conservacion.AccesoDatos;
using Conservacion.Entidades;

namespace Conservacion_LogicaNegocio
{
    public class PersonalLN
    {
        private readonly PersonalAD _personalAD;

        public PersonalLN(PersonalAD personalAD)
        {
            _personalAD = personalAD;
        }

        // Obtiene todo el personal
        public List<Personal> Listar()
        {
            return _personalAD.Listar();
        }

        // Obtener una persona por su ID
        public Personal? ObtenerPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El identificador debe ser mayor que cero.");
            }

            return _personalAD.ObtenerPorId(id);
        }


        public int Insertar(Personal personal)
        {
            if (personal == null)
                throw new ArgumentException("Debe proporcionar los datos del personal.");

            if (string.IsNullOrWhiteSpace(personal.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(personal.Cargo))
                throw new ArgumentException("El cargo es obligatorio.");

            return _personalAD.Insertar(personal);
        }


        public bool Actualizar(Personal personal)
        {
            if (personal == null)
                throw new ArgumentException("Debe proporcionar los datos del personal.");

            if (personal.IdPersonal <= 0)
                throw new ArgumentException("El identificador no es válido.");

            if (string.IsNullOrWhiteSpace(personal.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(personal.Cargo))
                throw new ArgumentException("El cargo es obligatorio.");

            return _personalAD.Actualizar(personal);
        }


        public bool Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El identificador no es válido.");

            return _personalAD.Eliminar(id);
        }

    }
}

