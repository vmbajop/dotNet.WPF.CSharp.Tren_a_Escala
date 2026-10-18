using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrenaEscala.Clases;
using TrenaEscala.Utilidades;

namespace TrenaEscala;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        string cadenaConexion = @"Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=C:\trabajo\desarrollo\dotnet\dotNet.WPF.CSharp.Tren_a_Escala\data\collection.dat;Uid=Admin;Pwd=;";

        ImportadorRailwayCollection importador = new(cadenaConexion);

        /*string c = "";
        string conexionExitosa = importador.ProbarConexion(c);

        MessageBox.Show(conexionExitosa);*/

        /*if(importador.ProbarConexion())
        {
            List<string> tablas = importador.ListarTablas();
            MessageBox.Show("Tablas encontradas:\n" + string.Join("\n", tablas));
        }
        else
        {
            MessageBox.Show("No se pudo conectar a la base de datos.");
        }*/

        /*List<Escala> escalas = importador.ObtenerTablaEscalas();
        MessageBox.Show("Escalas encontradas:\n" + string.Join("\n", escalas.Select(e => e.Nombre)));

        List<Fabricante> fabricantes = importador.ObtenerTablaFabricantes();
        MessageBox.Show("Fabricantes encontrados:\n" + string.Join("\n", fabricantes.Select(f => f.Nombre)));

        List<Tienda> tiendas = importador.ObtenerTablaTiendas();
        MessageBox.Show("Tiendas encontradas:\n" + string.Join("\n", tiendas.Select(t => t.Nombre)));

        List<TrenaEscala.Clases.Decoder> decoders = importador.ObtenerTablaDecoders();
        MessageBox.Show("Decoders encontrados:\n" + string.Join("\n", decoders.Select(d => d.Nombre)));

        List<Operador> operadores = importador.ObtenerTablaOperadores();
        MessageBox.Show("Operadores encontrados:\n" + string.Join("\n", operadores.Select(o => o.Nombre)));*/

        
        List<Modelo> modelos = importador.FormarModelos();
        MessageBox.Show("Modelos encontrados:\n" + string.Join("\n", modelos.Select(m => m.Nombre)));



    }
}