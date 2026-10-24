namespace TrenaEscala.Clases;

public class Modelo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    public string Referencia { get; set; } = "";

    public string Decoracion { get; set; } = "";

    public string Epoca { get; set; } = "";

    public string Numeral { get; set; } = "";

    public string TipoUic { get; set; } = "";

    public string DescripcionTrenReal { get; set; } = "";

    public decimal PrecioCatalogo { get; set; }

    public decimal PrecioCompra { get; set; }

    public bool Vendido { get; set; } = false;

    public DateTime? FechaVenta { get; set; }

    public decimal PrecioVenta { get; set; } = 0;

    public DateTime? FechaCompra { get; set; }

    /// <summary>
    /// Cantidad que tengo de este modelo concreto
    /// </summary>
    public int Unidades { get; set; } = 1;

    public bool ModeloDeseado { get; set; } = false;

    public int LongitudModelo { get; set; }

    public bool TieneNem { get; set; }

    public bool TieneKkk { get; set; }

    public bool TomaEnPantografo { get; set; }

    public bool TieneSonido { get; set; }

    public bool EsEdicionLimitada { get; set; }

    public string? AnioEdicionLimitada { get; set; }

    public string? DireccionDigital { get; set; } = "-1";

    /// <summary>
    /// Tipo de iluminación frontal
    /// </summary>
    public string IluminacionFrontal { get; set; } = "0";

    /// <summary>
    /// Tipo de iluminación trasera
    /// </summary>
    public string IluminacionTrasera { get; set; } = "0";

    /// <summary>
    /// cambiado a int porque así esta en la version 3 de RailwayCollection, son 0: No, 1: si, luces normales, 2: si, luces led
    /// </summary>
    public string IluminacionInterior { get; set; } = "0";

    public List<ImagenModelo> Imagenes { get; set; } = new();

    public Operador? Operador { get; set; }

    public Fabricante? Fabricante { get; set; }

    public Escala? Escala { get; set; }

    public TipoModelo? TipoModelo { get; set; }

    public Tienda? Tienda { get; set; }

    public Decoder? Decoder { get; set; }

    public string Comentarios { get; set; } = "";
}