using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Conservacion.Entidades;
using Conservacion_LogicaNegocio;

namespace ConservacionAPI.Controllers
{
    [Route("api/monitoreos")]
    [ApiController]
    [Authorize]
    public class MonitoreosController : ControllerBase
    {
        private readonly MonitoreoAmbientalLN _monitoreoLN;

        public MonitoreosController(MonitoreoAmbientalLN monitoreoLN)
        {
            _monitoreoLN = monitoreoLN;
        }

        // GET: api/monitoreos
        [HttpGet]
        public ActionResult<List<MonitoreoAmbiental>> Get()
        {
            try
            {
                var lista = _monitoreoLN.Listar();

                return Ok(lista);
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al consultar los monitoreos.");
            }
        }

        // GET: api/monitoreos/1
        [HttpGet("{id:int}")]
        public ActionResult<MonitoreoAmbiental> GetPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El identificador debe ser mayor que cero.");
            }

            try
            {
                var monitoreo = _monitoreoLN.ObtenerPorId(id);

                if (monitoreo == null)
                {
                    return NotFound("No se encontró el monitoreo solicitado.");
                }

                return Ok(monitoreo);
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al consultar el monitoreo.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult<MonitoreoAmbiental> Post([FromBody] MonitoreoAmbiental monitoreo)
        {
            try
            {
                int nuevoId = _monitoreoLN.Insertar(monitoreo);

                monitoreo.IdMonitoreo = nuevoId;

                return CreatedAtAction(nameof(GetPorId), new { id = nuevoId }, monitoreo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 547)
            {
                return BadRequest("El área de conservación seleccionada no existe.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar monitoreo: " + ex);

                return StatusCode(500,"Error al registrar el monitoreo.");
            }

        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Put(int id,[FromBody] MonitoreoAmbiental monitoreo)
        {
            if (id <= 0 || id != monitoreo.IdMonitoreo)
            {
                return BadRequest("El identificador no coincide.");
            }

            try
            {
                bool actualizado = _monitoreoLN.Actualizar(monitoreo);

                if (!actualizado)
                {
                    return NotFound("No se encontró el monitoreo solicitado.");
                }

                return Ok("Monitoreo actualizado correctamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 547)
            {
                return BadRequest("El área de conservación seleccionada no existe.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar monitoreo: " + ex);

                return StatusCode(500,"Error al actualizar el monitoreo.");
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El identificador no es válido.");
            }

            try
            {
                bool eliminado = _monitoreoLN.Eliminar(id);

                if (!eliminado)
                {
                    return NotFound("No se encontró el monitoreo solicitado.");
                }

                return Ok("Monitoreo eliminado correctamente.");
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 547)
            {
                return Conflict("No se puede eliminar el monitoreo porque tiene registros relacionados.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar monitoreo: " + ex);

                return StatusCode(500,"Error al eliminar el monitoreo.");
            }
        }



    }
}

