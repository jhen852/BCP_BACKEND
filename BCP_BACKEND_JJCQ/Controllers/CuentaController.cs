using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using BCP_BACKEND_JJCQ.Models;
namespace BCP_BACKEND_JJCQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        public readonly string cadena_Sql;
        //para conectarnos con la base de datos
        public CuentaController(IConfiguration config)
        {
            //cadena de coneccion a la base de datos
            this.cadena_Sql = config.GetConnectionString("ConnectionString");
        }

        //lista de cuentas       
        [HttpGet]
        [Route("LISTA_BE_JJCQ")]
        public IActionResult lista_cuentas()
        {
            //lista de cuenta
            List<Cuenta> cuentas = new List<Cuenta>();
            try
            {
                //conectarme al sql
                using (var conexion = new SqlConnection(cadena_Sql))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("SP_LISTA_CUENTAS", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            cuentas.Add(new Cuenta()
                            {
                                //devolver todos los datos de la consutla
                                tipo = rd["tipo"].ToString(),
                                moneda = rd["sigla"].ToString(),
                                nro_cuenta = rd["nro_cuenta"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                saldo = Convert.ToDouble(rd["saldo"])

                            }
                             );
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, cuentas);
                //var cliente = cuentas.ToList();
                //return (IActionResult)cliente;
            }

            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = error.Message, response = cuentas });
            }
        }

        //ABONO Y RETIROS  
        //[HttpPost]
        [HttpPut]
        [Route("ABONO_RETIRO_BE_JJCQ")]
        public IActionResult abono_retiro(String numero_cuenta,double importe, string tipomovimiento)
        {
            //lista de cuenta
            List<Cuenta> cuentas = new List<Cuenta>();
            try
            {
                //conectarme al sql
                using (var conexion = new SqlConnection(cadena_Sql))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("ABONO_RETIRO_MOVIMIENTO", conexion);
                    SqlParameter sqlParameter = cmd.Parameters.AddWithValue("@nro_cuenta", numero_cuenta);
                    sqlParameter = cmd.Parameters.AddWithValue("@importe", importe);
                    sqlParameter = cmd.Parameters.AddWithValue("@tipoMov", tipomovimiento);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            cuentas.Add(new Cuenta()
                            {
                                //devolver todos los datos de la consutla
                                tipo = rd["tipo"].ToString(),
                                moneda = rd["sigla"].ToString(),
                                nro_cuenta = rd["nro_cuenta"].ToString(),
                                nombre = rd["nombre"].ToString(),
                                saldo = Convert.ToDouble(rd["saldo"])

                            }
                             );
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, cuentas);
                //var cliente = cuentas.ToList();
                //return (IActionResult)cliente;
            }

            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = error.Message, response = cuentas});
            }
        }

    }
}
