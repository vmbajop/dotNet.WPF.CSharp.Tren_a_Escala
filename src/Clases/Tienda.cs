namespace TrenaEscala.Clases;

public class Tienda
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    public string Direccion { get; set; } = "";

    public string Telefono { get; set; } = "";

    public string Fax { get; set; } = "";

    public string CorreoElectronico { get; set; } = "";

    public string Url { get; set; } = "";

    public string Descripcion { get; set; } = "";

    public bool SigueAbierta { get; set; } = true;
}