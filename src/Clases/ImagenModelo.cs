namespace TrenaEscala.Clases
{
    public class ImagenModelo
    {
        public int Id { get; set; }

        //public string RutaImagen { get; set; } = "";

        public byte[]? DatosImagen { get; set; }

        public string NombreArchivo { get; set; } = "";

        public string Descripcion { get; set; } = "";

        public bool Principal { get; set; } = false;
    }
}
