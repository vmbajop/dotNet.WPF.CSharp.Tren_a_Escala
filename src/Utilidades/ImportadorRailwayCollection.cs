using System.Data.Odbc;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Windows;
using TrenaEscala.Clases;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrenaEscala.Utilidades
{
    internal class ImportadorRailwayCollection
    {
        #region Constructor
        private readonly string _cadenaConexion;
        public ImportadorRailwayCollection(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }
        #endregion

        #region Auxiliares
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
        #endregion

        #region Listas de Objetos
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

        private ImagenModelo ObtenerImagenModelo(int modeloId, string nombreImagen)
        {

            ImagenModelo imagen = new();

            try
            {
                {
                    //imagen.Id no lo pongo, porque no estoy leyendo de ninguna tabla... que lo asigne la BBDD automáticamente
                    imagen.DatosImagen = ArchivoPath2byte(nombreImagen ?? "", "images");
                    imagen.NombreArchivo = nombreImagen ?? "";
                    imagen.Descripcion = ""; // No hay un campo de descripción en la tabla MODELS, así que lo dejamos vacío
                    imagen.Principal = true; // Por defecto, la primera imagen que se obtiene es la principal
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener una imagen del modelo {modeloId}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return imagen;
        }
        #endregion

        #region Modelos
        public List<Modelo> FormarModelos()
        {
            using OdbcConnection conexion = ObtenerConexion();

            List<Modelo> modelos = new();
            string sql = "SELECT * FROM MODELS";

            try
            {
                using OdbcCommand comando = new(sql, conexion);
                using OdbcDataReader lector = comando.ExecuteReader();                

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
                        AnioEdicionLimitada = lector["MODLIMYEAR"].ToString() ?? "", //Año de edición limitada, pero aparece el año de compra de todos los modelos metidos porque RailwayMania lo permitia
                        DireccionDigital = lector["MODDIGITALADD"].ToString() ?? "",
                        IluminacionFrontal = lector["MODFRONTLIGHTS"].ToString() ?? "",
                        IluminacionTrasera = "0", //Como en el modelo de datos original no aparece ["MODREARLIGHTS"] no puede hacerse lector["MODREARLIGHTS"].ToString() ?? "0",
                                                  //Pero el campo existe si se activa la propiedad en algun modelo, pero no lo encuentra
                        IluminacionInterior = "0", //lector["MODINTERIORLIGHTS"].ToString() ?? "0",
                        Imagenes = new List<ImagenModelo>{ ObtenerImagenModelo(Convert.ToInt32(lector["MODID"]), lector["MODPHOTO"].ToString() ?? "") },
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
                MessageBox.Show($"Error al obtener los modelos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return modelos;
        }

        public List<Modelo> FormarModelos(bool sobreCarga = true)
        {
            using OdbcConnection conexion = ObtenerConexion();

            List<Modelo> modelos = new();

            // Cargamos los catálogos una única vez
            List<Operador> operadores = ObtenerTablaOperadores();
            List<Fabricante> fabricantes = ObtenerTablaFabricantes();
            List<Escala> escalas = ObtenerTablaEscalas();
            List<TipoModelo> tiposModelo = ObtenerTablaTiposModelos();
            List<Tienda> tiendas = ObtenerTablaTiendas();
            List<Decoder> decoders = ObtenerTablaDecoders();

            string sql = "SELECT * FROM MODELS";

            try
            {
                using OdbcCommand comando = new(sql, conexion);
                using OdbcDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    int operadorId = lector["MODADMINID"] != DBNull.Value
                        ? Convert.ToInt32(lector["MODADMINID"])
                        : 0;

                    int fabricanteId = lector["MODBUILDERID"] != DBNull.Value
                        ? Convert.ToInt32(lector["MODBUILDERID"])
                        : 0;

                    int escalaId = lector["MODSCALE"] != DBNull.Value
                        ? Convert.ToInt32(lector["MODSCALE"])
                        : 0;

                    int tipoModeloId = lector["MODTYPEID"] != DBNull.Value
                        ? Convert.ToInt32(lector["MODTYPEID"])
                        : 0;

                    int tiendaId = lector["MODSTOREID"] != DBNull.Value
                        ? Convert.ToInt32(lector["MODSTOREID"])
                        : 0;

                    int decoderId = lector["MODDECODERID"] != DBNull.Value
                        ? Convert.ToInt32(lector["MODDECODERID"])
                        : 0;

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

                        FechaCompra = lector["MODBUYDATE"] != DBNull.Value
                            ? Convert.ToDateTime(lector["MODBUYDATE"])
                            : null,

                        Vendido = false,
                        PrecioVenta = 0,
                        FechaVenta = null,

                        Unidades = Convert.ToInt32(lector["MODUNITS"]),
                        ModeloDeseado = false,

                        LongitudModelo = Convert.ToInt32(lector["MODLENGTH"]),

                        TieneNem = Convert.ToBoolean(lector["MODNEM"]),
                        TieneKkk = Convert.ToBoolean(lector["MODKKK"]),
                        TomaEnPantografo = Convert.ToBoolean(lector["MODPANT"]),
                        TieneSonido = Convert.ToBoolean(lector["MODSOUND"]),

                        EsEdicionLimitada = Convert.ToBoolean(lector["MODLIM"]),

                        AnioEdicionLimitada =
                            lector["MODLIMYEAR"].ToString() ?? "",

                        DireccionDigital =
                            lector["MODDIGITALADD"].ToString() ?? "",

                        IluminacionFrontal =
                            lector["MODFRONTLIGHTS"].ToString() ?? "",

                        IluminacionTrasera = "0", //lector["MODREARLIGHTS"].ToString() ?? "0",
                        IluminacionInterior = "0", //lector["MODINTERIORLIGHTS"].ToString() ?? "0",

                        Imagenes = new List<ImagenModelo> { ObtenerImagenModelo(
                            Convert.ToInt32(lector["MODID"]),
                            lector["MODPHOTO"].ToString() ?? "") },

                        Operador = operadores.FirstOrDefault(o => o.Id == operadorId),

                        //Esto hace --> De la lista operadores, dame el primer operador cuyo Id sea igual a operadorId.
                        //La lista de operadores ha sido obtenida arriba con List<Operador> operadores = ObtenerTablaOperadores();
                        //Esto es lo mismo que hacer lo siguiente:
                        /*
                        
                        foreach (Operador o in operadores)
                        {
                            if (o.Id == operadorId)
                            {
                                Operador? operadorEncontrado = o;
                                break;
                            }
                        }*/

                        Fabricante = fabricantes.FirstOrDefault(f => f.Id == fabricanteId),

                        Escala = escalas.FirstOrDefault(e => e.Id == escalaId),

                        TipoModelo = tiposModelo.FirstOrDefault(t => t.Id == tipoModeloId),

                        Tienda = tiendas.FirstOrDefault(t => t.Id == tiendaId),

                        Decoder = decoders.FirstOrDefault(d => d.Id == decoderId),

                        Comentarios = lector["MODDESC"].ToString() ?? ""
                    };

                    modelos.Add(modelo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al obtener los modelos: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            return modelos;
        }
        #endregion

        #region Obtener objetos relacionados con el modelo
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
            using OdbcConnection conexion = ObtenerConexion();

            Fabricante fabricante = new();

            string sqlAdminInModels = "SELECT MODBUILDERID FROM MODELS WHERE MODID = ?";
            
            string sql = "SELECT * FROM BUILDERS WHERE BUILDID = ?";

            int fabricanteId = -1;

            try
            {
                using OdbcCommand comando = new(sqlAdminInModels, conexion);
                comando.Parameters.AddWithValue("@MODID", modeloId); // Agregamos el parámetro para el ID del modelo, que se engancha al ? de la sentencia SQL

                using OdbcDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    fabricanteId = lector[0] != DBNull.Value ? Convert.ToInt32(lector[0]) : 0;
                }

                if (fabricanteId > 0)
                {
                    using OdbcCommand comando2 = new(sql, conexion);

                    comando2.Parameters.AddWithValue("@BUILDID", fabricanteId);

                    using OdbcDataReader lectorFabricante = comando2.ExecuteReader();

                    if (lectorFabricante.Read())
                    {
                        fabricante.Id = Convert.ToInt32(lectorFabricante["BUILDID"]);
                        fabricante.Nombre = lectorFabricante["BUILDNAME"].ToString() ?? "";
                        fabricante.Descripcion = lectorFabricante["BUILDDESC"].ToString() ?? "";
                        fabricante.Direccion = lectorFabricante["BUILDADDRESS"].ToString() ?? "";
                        fabricante.Url = lectorFabricante["BUILDURL"].ToString() ?? "";
                        fabricante.Telefono = "";
                        fabricante.Comentarios = "";
                        fabricante.Activo = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el fabricante del modelo {modeloId}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            return fabricante;
        }

        private Escala ObtenerEscalaModelo(int modeloId)
        {
            using OdbcConnection conexion = ObtenerConexion();

            Escala escala = new();

            string sqlAdminInModels = "SELECT MODSCALE FROM MODELS WHERE MODID = ?";

            string sql = "SELECT * FROM SCALES WHERE SCID = ?";

            int escalaId = -1;

            try
            {
                using OdbcCommand comando = new(sqlAdminInModels, conexion);
                comando.Parameters.AddWithValue("@MODID", modeloId); // Agregamos el parámetro para el ID del modelo, que se engancha al ? de la sentencia SQL

                using OdbcDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    escalaId = lector[0] != DBNull.Value ? Convert.ToInt32(lector[0]) : 0;
                }

                if (escalaId > 0)
                {
                    using OdbcCommand comando2 = new(sql, conexion);

                    comando2.Parameters.AddWithValue("@SCID", escalaId);

                    using OdbcDataReader lectorEscala = comando2.ExecuteReader();

                    if (lectorEscala.Read())
                    {
                        escala.Id = Convert.ToInt32(lectorEscala["SCID"]);
                        escala.Nombre = lectorEscala["SCNAME"].ToString() ?? "";
                        escala.Proporcion = lectorEscala["SCSCALE"].ToString() ?? "";
                        escala.AnchoViaModelo = Convert.ToDecimal(lectorEscala["SCTWIDTH"]);
                        escala.AnchoViaReal = Convert.ToInt32(lectorEscala["SCRTWIDTH"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener la Escala del modelo {modeloId}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return escala;
        }

        private TipoModelo ObtenerTipoModelo(int modeloId)
        {
            using OdbcConnection conexion = ObtenerConexion();

            TipoModelo tipoModelo = new();

            string sqlAdminInModels = "SELECT MODTYPEID FROM MODELS WHERE MODID = ?";

            string sql = "SELECT * FROM TYPES WHERE TYPEID = ?";

            int tipoId = -1;

            try
            {
                using OdbcCommand comando = new(sqlAdminInModels, conexion);
                comando.Parameters.AddWithValue("@MODID", modeloId); // Agregamos el parámetro para el ID del modelo, que se engancha al ? de la sentencia SQL

                using OdbcDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    tipoId = lector[0] != DBNull.Value ? Convert.ToInt32(lector[0]) : 0;
                }

                if (tipoId > 0)
                {
                    using OdbcCommand comando2 = new(sql, conexion);

                    comando2.Parameters.AddWithValue("@TYPEID", tipoId);

                    using OdbcDataReader lectorEscala = comando2.ExecuteReader();

                    if (lectorEscala.Read())
                    {
                        tipoModelo.Id = Convert.ToInt32(lectorEscala["TYPEID"]);
                        tipoModelo.Nombre = lectorEscala["TYPENAME"].ToString() ?? "";
                        tipoModelo.Icono = ""; // El TypeIcon está vacio en RailwayCollection, así que lo dejamos vacío
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el tipo de modelo {modeloId}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return tipoModelo;
        }

        private Tienda ObtenerTiendaModelo(int modeloId)
        {
            using OdbcConnection conexion = ObtenerConexion();

            Tienda tienda = new();

            string sqlAdminInModels = "SELECT MODSTOREID FROM MODELS WHERE MODID = ?";

            string sql = "SELECT * FROM STORES WHERE STOREID = ?";

            int tiendaId = -1;

            try
            {
                using OdbcCommand comando = new(sqlAdminInModels, conexion);
                comando.Parameters.AddWithValue("@MODID", modeloId); // Agregamos el parámetro para el ID del modelo, que se engancha al ? de la sentencia SQL

                using OdbcDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    tiendaId = lector[0] != DBNull.Value ? Convert.ToInt32(lector[0]) : 0;
                }

                if (tiendaId > 0)
                {
                    using OdbcCommand comando2 = new(sql, conexion);

                    comando2.Parameters.AddWithValue("@STOREID", tiendaId);

                    using OdbcDataReader lectorEscala = comando2.ExecuteReader();

                    if (lectorEscala.Read())
                    {
                        tienda.Id = Convert.ToInt32(lectorEscala["STOREID"]);
                        tienda.Nombre = lectorEscala["STORENAME"].ToString() ?? "";
                        tienda.Direccion = lectorEscala["STOREADDRESS"].ToString() ?? "";
                        tienda.Telefono = lectorEscala["STOREPHONE"].ToString() ?? "";
                        tienda.Fax = lectorEscala["STOREFAX"].ToString() ?? "";
                        tienda.CorreoElectronico = lectorEscala["STOREMAIL"].ToString() ?? "";
                        tienda.Url = lectorEscala["STOREURL"].ToString() ?? "";
                        tienda.Descripcion = lectorEscala["STOREDESC"].ToString() ?? "";
                        tienda.SigueAbierta = true; // No hay un campo de estado en la tabla STORES, así que lo dejamos como true por defecto
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener la tienda del modelo {modeloId}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return tienda;
        }

        private Decoder ObtenerDecoderModelo(int modeloId)
        {
            using OdbcConnection conexion = ObtenerConexion();

            Decoder decoder = new();

            string sqlAdminInModels = "SELECT MODDECODERID FROM MODELS WHERE MODID = ?";

            string sql = "SELECT * FROM DECODERS WHERE DECID = ?";

            int decoderId = -1;

            try
            {
                using OdbcCommand comando = new(sqlAdminInModels, conexion);
                comando.Parameters.AddWithValue("@MODID", modeloId); // Agregamos el parámetro para el ID del modelo, que se engancha al ? de la sentencia SQL

                using OdbcDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    decoderId = lector[0] != DBNull.Value ? Convert.ToInt32(lector[0]) : 0;
                }

                if (decoderId > 0)
                {
                    using OdbcCommand comando2 = new(sql, conexion);

                    comando2.Parameters.AddWithValue("@DECID", decoderId);

                    using OdbcDataReader lectorEscala = comando2.ExecuteReader();

                    if (lectorEscala.Read())
                    {
                        decoder.Id = Convert.ToInt32(lectorEscala["DECID"]);
                        decoder.Nombre = lectorEscala["DECNAME"].ToString() ?? "";
                        decoder.Descripcion = lectorEscala["DECDESC"].ToString() ?? "";
                        decoder.Url = lectorEscala["DECWEB"].ToString() ?? "";
                        decoder.ManualPDF = ArchivoPath2byte(lectorEscala["DECFILE"].ToString() ?? "", "manuals");
                        decoder.Comentarios = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el decoder del modelo {modeloId}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return decoder;
        }
        #endregion

        
    }
}
