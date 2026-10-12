namespace TrenaEscala.Clases;

public class Fabricante
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    public string Descripcion { get; set; } = "";

    public string Direccion { get; set; } = "";

    public string Url { get; set; } = "";

    public string Telefono { get; set; } = "";

    public string Comentarios { get; set; } = "";

    /// <summary>
    /// Indica si el fabricante del modelo sigue existiendo
    /// </summary>
    public bool Activo { get; set; } = true;
}