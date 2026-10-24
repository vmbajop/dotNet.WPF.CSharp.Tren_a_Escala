using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using TrenaEscala.Clases;

/*PRIMERA PIEZA PARA LA CONSTRUCCIÓN DEL CONTEXTO DE LA BASE DE DATOS PARA SU USO MEDIANTE ORM
    Un ORM es un técnica de programación que permite interactuar con una base de datos utilizando un lenguaje de programación orientado a objetos 
    como sería el caso, en 
        - JAVA de Hibernate o JPA+JavaBeans
        - en .NET de Entity Framework Core (EF Core) o Dapper
    Una vez creada esta clase, debe lanzarse:
        Herramientas > Administrador de paquetes NuGet > Consola del Administrador de paquetes
        y allí el comando: PM> Add-Migration Inicial
        que generará un archivo de migración que contendrá el código para crear las tablas en la base de datos según las clases definidas en el proyecto
        dentro de la carpeta Migrations
   Luego, para aplicar la migración y crear la base de datos, se ejecuta el comando: PM> Update-Database
*/

namespace TrenaEscala.Data;

internal class TrenaEscalaContext : DbContext
{
    #region Constructor
    /// <summary>
    /// Constructor sin parámetros para que pueda ser usado por Add-Migration para crear el esquema de BBDD mediante ORM a partir de las clases declaradas
    /// sin parametrizar la ruta. Se va a crear en la ruta por defecto "trenaescala.db" en la carpeta raíz del proyecto.
    /// </summary>
    public TrenaEscalaContext()
    {
        _rutaBbddSQLite = "trenaescala.db";
    }


    private readonly string _rutaBbddSQLite;

    public TrenaEscalaContext(string rutaBbddSQLite)
    {
        _rutaBbddSQLite = rutaBbddSQLite;
    }

    #endregion
    public DbSet<Operador> Operadores { get; set; }

    public DbSet<Fabricante> Fabricantes { get; set; }

    public DbSet<Escala> Escalas { get; set; }

    public DbSet<Tienda> Tiendas { get; set; }

    public DbSet<Decoder> Decoders { get; set; }

    public DbSet<TipoModelo> TiposModelo { get; set; }

    public DbSet<Modelo> Modelos { get; set; }

    public DbSet<ImagenModelo> ImagenesModelo { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_rutaBbddSQLite}");
    }
}
