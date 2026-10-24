# Paso de SQLite manual a Entity Framework Core

## Situación inicial

La idea inicial era generar las tablas SQLite manualmente mediante SQL.

Ejemplo:

```csharp
string sql = @"
CREATE TABLE IF NOT EXISTS Operadores
(
    Id INTEGER PRIMARY KEY,
    Nombre TEXT NOT NULL,
    Descripcion TEXT,
    Url TEXT,
    LogoOperador BLOB,
    Comentarios TEXT
)";
```

La clase `AccesoSQLite` iba a encargarse de generar todas las tablas de esta forma.

---

## Recordando Java: ORM

Durante el diseño surgió la idea de no generar las tablas manualmente.

Se recordó el concepto de ORM.

ORM significa:

```text
Object Relational Mapper
```

Su función es traducir automáticamente:

```text
Clase C#
    ↕
Tabla SQL
```

y

```text
Objeto
    ↕
Registro
```

En Java el equivalente conceptual sería:

```text
JavaBeans + Hibernate/JPA
```

donde una entidad Java termina convirtiéndose en una tabla de base de datos.

La diferencia es que en Java normalmente es frecuente que Hibernate genere directamente las tablas al arrancar la aplicación.

---

## Decisión

Se decide abandonar la generación manual de tablas y utilizar el ORM oficial de Microsoft:

```text
Entity Framework Core
```

por dos motivos:

1. Aprender y refrescar .NET moderno.
2. Evitar mantener SQL de creación de tablas escrito a mano.

---

## Comprobaciones previas

Antes de crear las migraciones se revisó que las clases no tuviesen problemas para ser interpretadas por Entity Framework.

### Operador

Se comprobó que:

```csharp
public class Operador
{
    ...
}
```

no tuviese referencias circulares.

Correcto.

---

### ImagenModelo

Se comprobó que:

```csharp
public class ImagenModelo
{
    ...
}
```

no tuviese referencias complejas.

Correcto.

---

### Qué hubiera impedido la migración

Problemas típicos:

```text
Referencias circulares
```

Ejemplo:

```csharp
Modelo -> Operador
Operador -> List<Modelo>

public class Modelo
{
    public Operador? Operador { get; set; }
}

public class Operador
{
    public List<Modelo> Modelos { get; set; } = new();
}
```

Es decir, que una instancia de "Modelo" contenga un "Operador" y que a su vez el "Operador" contenga una lista de "Modelo".
Eso sería una referencia circular que EF no podría resolver.

---

```text
Tipos no persistibles

Son tipos que Entity Framework no sabe convertir automáticamente a una columna SQL.
```

Ejemplo:

```csharp
Stream
    public Stream Datos { get; set; }
FileInfo:
    public FileInfo Fichero { get; set; }
Controles WPF
```

Entity Franework no sabe cómo convertir un `Stream` o un `FileInfo` a una columna SQL. Por ejemplo, para el caso de FileInfo... ¿Qué debería guardar SQLite? ¿La ruta? ¿El contenido? ¿El nombre? EF no lo sabe.

---

```text
Propiedades ambiguas
```

que EF no supiese convertir a tipos SQL.

Nada de esto apareció.

---

## Instalación de Entity Framework Core

Desde:

```text
Visual Studio Community
    → Proyecto → clic derecho sobre el proyecto
    → Administrar paquetes NuGet
```

se instalaron los paquetes:

```text
Microsoft.EntityFrameworkCore

Microsoft.EntityFrameworkCore.Sqlite

Microsoft.EntityFrameworkCore.Tools
```

---

## Creación del contexto

Se creó en el Proyecto la carpeta `Data` con el archivo `TrenaEscalaContext.cs`:

```text
Data
└── TrenaEscalaContext.cs
```

Heredando de:

```csharp
Microsoft.EntityFrameworkCore.DbContext
```

y definiendo:

```csharp
public DbSet<Operador> Operadores { get; set; }

public DbSet<Fabricante> Fabricantes { get; set; }

public DbSet<Escala> Escalas { get; set; }

public DbSet<Tienda> Tiendas { get; set; }

public DbSet<Decoder> Decoders { get; set; }

public DbSet<TipoModelo> TiposModelo { get; set; }

public DbSet<Modelo> Modelos { get; set; }

public DbSet<ImagenModelo> ImagenesModelo { get; set; }
```

---

## Configuración de SQLite

Se parametrizó la ruta de la base de datos mediante constructor:

```csharp
public TrenaEscalaContext(string rutaBbddSQLite)
{
    _rutaBbddSQLite = rutaBbddSQLite;
}
```

y:

```csharp
optionsBuilder.UseSqlite(
    $"Data Source={_rutaBbddSQLite}");
```

---

## Problema encontrado

La migración falló con:

```text
Unable to resolve service for type 'System.String'
```

Motivo:

Entity Framework intenta crear el contexto automáticamente.

Solo existía:

```csharp
public TrenaEscalaContext(string rutaBbddSQLite)
```

por lo que EF no sabía qué valor pasar al parámetro. Es decir, faltaba el parámetro de localización de la db.

---

## Solución

Se añadió un constructor sin parámetros sobrecargando el existente:

```csharp
public TrenaEscalaContext()
{
    _rutaBbddSQLite = "trenaescala.db";
}
```

manteniendo también el constructor parametrizado.

En este cvaso EF utilizará el constructor sin parámetros y la ruta por defecto, poniendo la BD resultante en la carpeta src, es decir, en la raiz del proyecto.

---

## Primera migración

Desde VS Community, en el menú:

```text
Herramientas
    → Administrador de paquetes NuGet
        → Consola del Administrador de paquetes
```

En esta consola que se abre en VS Community se ejecutó:

```powershell
Add-Migration Inicial
```

Resultado:

```text
Build succeeded.
To undo this action, use **Remove-Migration**.
```

O sea, se puede eliminar la migración y todos sus elementos desde la consola del Administrador de paquetes, con el comando:

```powershell
    Remove-Migration
```

La migración quedó creada correctamente.

---

## Aplicación de la migración

Se ejecutó desde la consola del Administrador de paquetes:

```powershell
    Update-Database
```

Esto:

1. Creó físicamente la base SQLite.
2. Generó todas las tablas.
3. Generó las relaciones deducidas a partir de las clases.

---

## Resultado

EF Core generó automáticamente:

```text
Operadores
Fabricantes
Escalas
Tiendas
Decoders
TiposModelo
Modelos
ImagenesModelo
```

Además generó tablas internas:

```text
_EFMigrationsHistory
_EFMigrationsLock
```

que son utilizadas por Entity Framework para controlar las migraciones y no deben eliminarse.

---

## Relaciones generadas automáticamente

A partir de:

```csharp
public Operador? Operador { get; set; }

public Fabricante? Fabricante { get; set; }

public Escala? Escala { get; set; }

public TipoModelo? TipoModelo { get; set; }

public Tienda? Tienda { get; set; }

public Decoder? Decoder { get; set; }
```

Entity Framework generó automáticamente:

```text
OperadorId
FabricanteId
EscalaId
TipoModeloId
TiendaId
DecoderId
```

dentro de la tabla `Modelos`.

Sin necesidad de escribir SQL manualmente.