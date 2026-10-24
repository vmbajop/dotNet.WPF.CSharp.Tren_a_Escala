using System;
using System.Collections.Generic;
using System.Text;
using TrenaEscala.Clases;
using Microsoft.Data.Sqlite;
using System.Drawing.Text;
using System.Windows.Navigation;

namespace TrenaEscala.Data
{
    internal class AccesoSQLite
    {
        #region constructor
        
        private readonly string _rutaBbddSQLite;

        public AccesoSQLite(string rutaBbddSQLite)
        {
            _rutaBbddSQLite = rutaBbddSQLite;
        }

        #endregion

        public bool CrearBaseDatos()
        {
            string cadenaConexion = $"Data Source={_rutaBbddSQLite}";
            using SqliteConnection conexion = new(cadenaConexion);
            try
            {
                conexion.Open(); //En SQLite, al abrir una conexion a un archivo que no existe, se crea automáticamente el archivo de base de datos.
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la base de datos: {ex.Message}");
                return false;
            }
        }

        /*CrearTablas();

        InsertarOperadores(List<Operador> operadores);

        InsertarFabricantes(List<Fabricante> fabricantes);

        InsertarEscalas(List<Escala> escalas);

        InsertarTiendas(List<Tienda> tiendas);

        InsertarDecoders(List<TrenaEscala.Clases.Decoder> decoders);

        InsertarTiposModelo(List<TipoModelo> tipos);

        InsertarModelos(List<Modelo> modelos);*/
    }
}
