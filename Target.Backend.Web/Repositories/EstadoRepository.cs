using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Target.Backend.Web.Interfaces.Repositories;

namespace Target.Backend.Web.Repositories
{
    public class EstadoRepository : IEstadoRepository
    {
        HttpClient cliente = new HttpClient();

        public EstadoRepository()
        {
            cliente.BaseAddress = new Uri("https://servicodados.ibge.gov.br");
            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<object> GetCidadesByUFAsync(string UF)
        {
            try
            {
                HttpResponseMessage response = await cliente.GetAsync($"/api/v1/localidades/estados/{UF}/distritos");
                string jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<object>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<object> GetEstadosAsync()
        {
            try
            {
                HttpResponseMessage response = await cliente.GetAsync("/api/v1/localidades/estados");
                string jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<object>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
