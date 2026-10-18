using System.Data.Odbc;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using TrenaEscala.Clases;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrenaEscala.Utilidades
{
    internal class ImportadorRailwayCollection
    {
        private readonly string _cadenaConexion;
        public ImportadorRailwayCollection(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        private OdbcConnection ObtenerConexion()
        {
            try
            {
                OdbcConnection _conexion = new(_cadenaConexion);
                _conexion.Open();
                return _conexion;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar a la base de datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }            
        }

        /// <summary>
        /// Obtiene un archivo desde una ruta implícita (a partir de la cadena de conexión, porque el archivo de logo de RailwayConnection debe estar en la carpeta logos, 
        /// manual o images en la misma localización que el archivo de BBDD de Access collection.dat
        /// </summary>
        /// <param name="nombreArchivoLogo">Nombre del archivoque aparece en la BBDD de RailwayCollection</param>
        /// <returns>System.byte[] con el arrya de bytes del archivo de la imagen del logo</returns>
        private byte[]? ArchivoPath2byte(string nombreArchivo, string carpeta)
        {
            byte[]? archivo = null;
            if (nombreArchivo != null && nombreArchivo != "")
            {
                string rutaArchivo = Path.GetDirectoryName(_cadenaConexion.Split(';').FirstOrDefault(part => part.StartsWith("Dbq="))?.Substring(4) ?? "") + "\\" + carpeta + "\\" + nombreArchivo;
                if (File.Exists(rutaArchivo))
                {
                    archivo = File.ReadAllBytes(rutaArchivo);
                }
            }
            return archivo;
        }

        public List<string> ListarTablas()
        {
            List<string> tablas = new();
            
            using OdbcConnection conexion = ObtenerConexion();
            
            var esquema = conexion.GetSchema("Tables");

            foreach (System.Data.DataRow fila in esquema.Rows)
            {
                string nombreTabla = fila["TABLE_NAME"].ToString() ?? "";
                tablas.Add(nombreTabla);
            }

            return tablas;
        }

        public List<Escala> ObtenerTablaEscalas()
        {
            using OdbcConnection conexion = ObtenerConexion();

            List<Escala> escalas = new();
            string sql = "SELECT * FROM SCALES";

            try
            {
                using OdbcCommand comando = new(sql, conexion);
                using OdbcDataReader lector = comando.ExecuteReader();

                while(lector.Read())
                {
                    Escala escala = new()
                    {
                        Id = Convert.ToInt32(lector["SCID"]),
                        Nombre = lector["SCNAME"].ToString() ?? "",
                        Proporcion = lector["SCSCALE"].ToString() ?? "",
                        AnchoViaModelo = Convert.ToDecimal(lector["SCTWIDTH"]),
                        AnchoViaReal = Convert.ToInt32(lector["SCRTWIDTH"])
                    };
                    escalas.Add(escala);

                }                
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error al obtener las escalas: {ex.Message}");
                MessageBox.Show($"Error al obtener las escalas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return escalas;
        }

        public List<Fabricante> ObtenerTablaFabricantes()
        {
            using OdbcConnection conexion = ObtenerConexion();

            List<Fabricante> fabricantes = new();
            string sql = "SELECT * FROM BUILDERS";

            try
            {
                using OdbcCommand comando = new(sql, conexion);
                using OdbcDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Fabricante fabricante = new()
                    {
                        Id = Convert.ToInt32(lector["BUILDID"]),
                        Nombre = lector["BUILDNAME"].ToString() ?? "",
                        Descripcion = lector["BUILDDESC"].ToString() ?? "",
                        Direccion = lector["BUILDADDRESS"].ToString() ?? "",
                        Url = lector["BUILDURL"].ToString() ?? "",
                        Telefono = "",
                        Comentarios = "",
                        Activo = true
                    };
                    fabricantes.Add(fabricante);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error al obtener los operadores: {ex.Message}");
                MessageBox.Show($"Error al obtener los operadores: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return fabricantes;
        }

        public List<Tienda> ObtenerTablaTiendas()
        {
            using OdbcConnection conexion = ObtenerConexion();

            List<Tienda> tiendas = new();
            string sql = "SELECT * FROM STORES";

            try
            {
                using OdbcCommand comando = new(sql, conexion);
                using OdbcDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Tienda tienda = new()
                    {
                        Id = Convert.ToInt32(lector["STOREID"]),
                        Nombre = lector["STORENAME"].ToString() ?? "",
                        Direccion = lector["STOREADDRESS"].ToString() ?? "",
                        Telefono = lector["STOREPHONE"].ToString() ?? "",
                        Fax = lector["STOREFAX"].ToString() ?? "",
                        CorreoElectronico = lector["STOREMAIL"].ToString() ?? "",
                        Url = lector["STOREURL"].ToString() ?? "",
                        Descripcion = lector["STOREDESC"].ToString() ?? "",
                        SigueAbierta = true
                    };
                    tiendas.Add(tienda);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error al obtener los operadores: {ex.Message}");
                MessageBox.Show($"Error al obtener los operadores: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return tiendas;
        }

        public List<Decoder> ObtenerTablaDecoders()
        {
            using OdbcConnection conexion = ObtenerConexion();

            List<Decoder> decoders = new();
            string sql = "SELECT * FROM DECODERS";

            try
            {
                using OdbcCommand comando = new(sql, conexion);
                using OdbcDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Decoder decoder = new()
                    {
                        Id = Convert.ToInt32(lector["DECID"]),
                        Nombre = lector["DECNAME"].ToString() ?? "",
                        Descripcion = lector["DECDESC"].ToString() ?? "",
                        Url = lector["DECWEB"].ToString() ?? "",
                        ManualPDF = ArchivoPath2byte(lector["DECFILE"].ToString() ?? "", "manuals"),
                        Comentarios = ""
                    };
                    decoders.Add(decoder);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error al obtener los operadores: {ex.Message}");
                MessageBox.Show($"Error al obtener los operadores: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return decoders;
        }

        public List<Operador> ObtenerTablaOperadores()
        {
            using OdbcConnection conexion = ObtenerConexion();

            List<Operador> operadores = new();
            string sql = "SELECT * FROM ADMINS";

            try
            {
                using OdbcCommand comando = new(sql, conexion);
                using OdbcDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Operador operador = new()
                    {
                        Id = Convert.ToInt32(lector["ADMINID"]),
                        Nombre = lector["ADMINNAME"].ToString() ?? "",
                        Descripcion = lector["ADMINDESC"].ToString() ?? "",
                        Url = lector["ADMINURL"].ToString() ?? "",
                        LogoOperador = ArchivoPath2byte(lector["ADMINFILE"].ToString() ?? "", "logos"),
                        //NombreArchivoLogo = lector["ADMINFILE"].ToString() ?? "",
                        Comentarios = ""
                    };
                    operadores.Add(operador);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error al obtener los operadores: {ex.Message}");
                MessageBox.Show($"Error al obtener los operadores: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return operadores;
        }

        

        public List<TipoModelo> ObtenerTablaTiposModelos()
        {
            using OdbcConnection conexion = ObtenerConexion();

            List<TipoModelo> tiposModelos = new();
            string sql = "SELECT * FROM TYPES";

            try
            {
                using OdbcCommand comando = new(sql, conexion);
                using OdbcDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    TipoModelo tipoModelo = new()
                    {
                        Id = Convert.ToInt32(lector["TYPEID"]),
                        Nombre = lector["TYPENAME"].ToString() ?? "",
                        Icono = "" // No hay un campo de icono en la tabla TYPES, así que lo dejamos vacío
                    };
                    tiposModelos.Add(tipoModelo);                    
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error al obtener los operadores: {ex.Message}");
                MessageBox.Show($"Error al obtener los operadores: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return tiposModelos;
        }

        public List<Modelo> FormarModelos()
        {
            using OdbcConnection conexion = ObtenerConexion();

            List<Modelo> modelos = new();
            string sql = "SELECT * FROM MODELS";

            try
            {
                using OdbcCommand comando = new(sql, conexion);
                using OdbcDataReader lector = comando.ExecuteReader();

                lector.Read(); // Muevo el lector al primer registro antes de entrar en el bucle while
                for (int i = 0; i < lector.FieldCount; i++) 
                { 
                    System.Diagnostics.Debug.WriteLine(lector.GetName(i)); 
                }


                while (lector.Read())
                {
                    Modelo modelo = new()
                    {
                        Id = Convert.ToInt32(lector["MODID"]),
                        Nombre = lector["MODNAME"].ToString() ?? "",
                        Referencia = lector["MODREF"].ToString() ?? "",
                        Decoracion = lector["MODPAINT"].ToString() ?? "",
                        Epoca = lector["MODERA"].ToString() ?? "",
                        Numeral = lector["MODREGNUMBER"].ToString() ?? "",
                        TipoUic = lector["MODTYPEUIC"].ToString() ?? "",
                        DescripcionTrenReal = "",
                        PrecioCatalogo = Convert.ToDecimal(lector["MODCATPRICE"]),
                        PrecioCompra = Convert.ToDecimal(lector["MODBUYPRICE"]),
                        FechaCompra = lector["MODBUYDATE"] != DBNull.Value ? Convert.ToDateTime(lector["MODBUYDATE"]) : null,
                        Vendido = false,
                        PrecioVenta = 0,
                        FechaVenta = null,
                        Unidades = Convert.ToInt32(lector["MODUNITS"]), //Cantidad de unidades que tengo de este modelo
                        ModeloDeseado = false, //Si quiero comprarlo o no, por defecto false, que es el valor si ya lo tengo
                        LongitudModelo = Convert.ToInt32(lector["MODLENGTH"]),
                        TieneNem = Convert.ToBoolean(lector["MODNEM"]),
                        TieneKkk = Convert.ToBoolean(lector["MODKKK"]), //Sistema de enganche corto
                        TomaEnPantografo = Convert.ToBoolean(lector["MODPANT"]),
                        TieneSonido = Convert.ToBoolean(lector["MODSOUND"]),
                        EsEdicionLimitada = Convert.ToBoolean(lector["MODLIM"]),
                        AnioEdicionLimitada = lector["MODLIMYEAR"] != DBNull.Value ? Convert.ToInt32(lector["MODLIMYEAR"]) : null, //Año de edición limitada, pero aparece el año de compra de todos los modelos metidos porque RailwayMania lo permitia
                        DireccionDigital = lector["MODDIGITALADD"] != DBNull.Value ? Convert.ToInt32(lector["MODDIGITALADD"]) : 0,
                        IluminacionFrontal = lector["MODFRONTLIGHTS"] != DBNull.Value ? Convert.ToInt32(lector["MODFRONTLIGHTS"]) : 0,
                        //IluminacionTrasera = lector["MODLIGHTREAR"] != DBNull.Value ? 0: Convert.ToInt32(lector["MODLIGHTREAR"]),
                        IluminacionTrasera = 0,
                        //IluminacionInterior = lector["MODLIGHTINTERIOR"] != DBNull.Value ? Convert.ToInt32(lector["MODLIGHTINTERIOR"]) : 0,
                        IluminacionInterior = 0,
                        Imagenes = ObtenerImagenModelo(Convert.ToInt32(lector["MODID"]), lector["MODPHOTO"].ToString() ?? ""),
                        Operador = ObtenerOperadorModelo(Convert.ToInt32(lector["MODID"])),
                        Fabricante = ObtenerFabricanteModelo(Convert.ToInt32(lector["MODID"])),
                        Escala = ObtenerEscalaModelo(Convert.ToInt32(lector["MODID"])),
                        TipoModelo = ObtenerTipoModelo(Convert.ToInt32(lector["MODID"])),
                        Tienda = ObtenerTiendaModelo(Convert.ToInt32(lector["MODID"])),
                        Decoder = ObtenerDecoderModelo(Convert.ToInt32(lector["MODID"])),
                        Comentarios = lector["MODDESC"].ToString() ?? ""
                    };
                    modelos.Add(modelo);

                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error al obtener los operadores: {ex.Message}");
                MessageBox.Show($"Error al obtener los operadores: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return modelos;
        }

        private List<ImagenModelo> ObtenerImagenModelo(int modeloId, string nombreImagen)
        {
            using OdbcConnection conexion = ObtenerConexion();

            List<ImagenModelo> imagenes = new();
            string sql = "SELECT MODPHOTO FROM MODELS WHERE MODID = ?";

            try
            {
                using OdbcCommand comando = new(sql, conexion);
                comando.Parameters.AddWithValue("@MODID", modeloId); // Agregamos el parámetro para el ID del modelo, que se engancha al ? de la sentencia SQL

                using OdbcDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    ImagenModelo imagen = new()
                    {
                        //Id no lo pongo, porque no estoy leyendo de ninguna tabla... que lo asigne la BBDD automáticamente
                        DatosImagen = ArchivoPath2byte(nombreImagen ?? "", "images"),
                        NombreArchivo = nombreImagen ?? "",
                        Descripcion = "", // No hay un campo de descripción en la tabla MODELS, así que lo dejamos vacío
                        Principal = true // Por defecto, la primera imagen que se obtiene es la principal
                    };
                    imagenes.Add(imagen);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener las imágenes del modelo {modeloId}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return imagenes;
        }

        private Operador ObtenerOperadorModelo(int modeloId)
        {
            using OdbcConnection conexion = ObtenerConexion();

            Operador operador = new();

            string sqlAdminInModels = "SELECT MODADMINID FROM MODELS WHERE MODID = ?";
            string sql = "SELECT * FROM ADMINS WHERE ADMINID = ?";
            int adminId = -1;

            try
            {
                using OdbcCommand comando = new(sqlAdminInModels, conexion);
                comando.Parameters.AddWithValue("@MODID", modeloId); // Agregamos el parámetro para el ID del modelo, que se engancha al ? de la sentencia SQL

                using OdbcDataReader lector = comando.ExecuteReader();

                if(lector.Read())
                {
                    adminId = lector[0] != DBNull.Value ? Convert.ToInt32(lector[0]) : 0;
                }

                if(adminId > 0)
                {
                    using OdbcCommand comando2 = new(sql, conexion);

                    comando2.Parameters.AddWithValue("@ADMINID", adminId);

                    using OdbcDataReader lectorAdmin = comando2.ExecuteReader();

                    if(lectorAdmin.Read())
                    {
                        operador.Id = Convert.ToInt32(lectorAdmin["ADMINID"]);
                        operador.Nombre = lectorAdmin["ADMINNAME"].ToString() ?? "";
                        operador.Descripcion = lectorAdmin["ADMINDESC"].ToString() ?? "";
                        operador.Url = lectorAdmin["ADMINURL"].ToString() ?? "";
                        operador.LogoOperador = ArchivoPath2byte(lectorAdmin["ADMINFILE"].ToString() ?? "", "logos");
                        operador.Comentarios = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el Operador del modelo {modeloId}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return operador;
        }

        private Fabricante ObtenerFabricanteModelo(int modeloId)
        {
            return new Fabricante();
        }

        private Escala ObtenerEscalaModelo(int modeloId)
        {
            return new Escala();
        }

        private TipoModelo ObtenerTipoModelo(int modeloId)
        {
            return new TipoModelo();
        }

        private Tienda ObtenerTiendaModelo(int modeloId)
        {
            return new Tienda();
        }

        private Decoder ObtenerDecoderModelo(int modeloId)
        {
            return new Decoder();
        }
    }
}
