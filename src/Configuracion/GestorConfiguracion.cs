using System.Text.Json;
using System.IO;

namespace TrenaEscala.Configuracion
{
    internal class GestorConfiguracion
    {
        private readonly string _rutaJsonConfig;

        public GestorConfiguracion(string rutaJsonConfig)
        {
            _rutaJsonConfig = rutaJsonConfig;
        }


        public ConfiguracionAplicacion CargarConfiguracion()
        {
            if (!File.Exists(_rutaJsonConfig))
            {
                return new ConfiguracionAplicacion();
            }

            string json = File.ReadAllText(_rutaJsonConfig);
            return JsonSerializer.Deserialize<ConfiguracionAplicacion>(json) ?? new ConfiguracionAplicacion();
            
        }

        public void GuardarConfiguracion(ConfiguracionAplicacion configuracion)
        {
            string json = JsonSerializer.Serialize(configuracion, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_rutaJsonConfig, json);
        }
    }
}
