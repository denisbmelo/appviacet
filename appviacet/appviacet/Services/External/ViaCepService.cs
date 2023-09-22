using appviacet.Models;
using Newtonsoft.Json;
using RestSharp;

namespace appviacet.Services.External
{
    public class ViaCepService
    {
        private readonly RestClient _client;

        public ViaCepService()
        {
            _client = new RestClient("http://viacep.com.br/ws");
        }

        public async Task<ViaCepResponse> ConsultarCEPAsync(string cep)
        {
            var request = new RestRequest($"{cep}/json", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<ViaCepResponse>(response.Content);
            }

            return new ViaCepResponse(); 
        }
    }
}
