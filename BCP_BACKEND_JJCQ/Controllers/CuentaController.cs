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
            }

            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = error.Message, response = cuentas});
            }
        }
    //Crear una Web api para realizar una transaccion entre cuentas
    [HttpPost]
    [Route("TRANSACCION_BE_JJCQ")]
    public IActionResult transaccion(String numero_cuenta_origen, String numero_cuenta_destino, double importe)
    {
        //lista de cuenta
        List<Cuenta> cuentas = new List<Cuenta>();
        try
        {
            //conectarme al sql
            using (var conexion = new SqlConnection(cadena_Sql))
            {
                conexion.Open();
                var cmd = new SqlCommand("TRANSACCION_MOVIMIENTO", conexion);
                SqlParameter sqlParameter = cmd.Parameters.AddWithValue("@nro_cuenta_origen", numero_cuenta_origen);
                sqlParameter = cmd.Parameters.AddWithValue("@nro_cuenta_destino", numero_cuenta_destino);
                sqlParameter = cmd.Parameters.AddWithValue("@importe", importe);
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
        }

        catch (Exception error)
        {
            return StatusCode(StatusCodes.Status200OK, new { mensaje = error.Message, response = cuentas });
        }
    }
    // Crear una web api para agregar cuentas del banco
    [HttpPost]
    [Route("AGREGAR_CUENTA_BE_JJCQ")]
   // public IActionResult agregar_cuenta(String nro_cuenta,String tipo, int moneda,  String nombre)
    public IActionResult agregar_cuenta(String nro_cuenta, String tipo, string moneda, String nombre)
        {
        //lista de cuenta
        List<Cuenta> cuentas = new List<Cuenta>();
        try
        {
            //conectarme al sql
            using (var conexion = new SqlConnection(cadena_Sql))
            {
                conexion.Open();
                var cmd = new SqlCommand("SP_AGREGAR_CUENTA", conexion);
                SqlParameter sqlParameter = cmd.Parameters.AddWithValue("@tipo", tipo);
                sqlParameter = cmd.Parameters.AddWithValue("@moneda", moneda);
                sqlParameter = cmd.Parameters.AddWithValue("@nro_cuenta", nro_cuenta);
                sqlParameter = cmd.Parameters.AddWithValue("@nombre", nombre);
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
        }

        catch (Exception error)
        {
            return StatusCode(StatusCodes.Status200OK, new { mensaje = error.Message, response = cuentas });
        }
    }
    //Crear web api para mostrar los movimientos que realizo una cuenta
    [HttpGet]
    [Route("MOVIMIENTOS_BE_JJCQ")]
    public IActionResult movimientos(String numero_cuenta)
    {
        //lista de cuenta
        List<Cuenta> cuentas = new List<Cuenta>();
        try
        {
            //conectarme al sql
            using (var conexion = new SqlConnection(cadena_Sql))
            {
                conexion.Open();
                var cmd = new SqlCommand("MOVIMIENTOS_CUENTA", conexion);
                SqlParameter sqlParameter = cmd.Parameters.AddWithValue("@nro_cuenta", numero_cuenta);
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
        }

        catch (Exception error)
        {
            return StatusCode(StatusCodes.Status200OK, new { mensaje = error.Message, response = cuentas });
        }
    }
    
    }
}
