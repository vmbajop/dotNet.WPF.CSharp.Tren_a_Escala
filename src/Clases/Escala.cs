namespace TrenaEscala.Clases;

public class Escala
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    public string Proporcion { get; set; } = "";

    public decimal AnchoViaModelo { get; set; }

    public int AnchoViaReal { get; set; }
}