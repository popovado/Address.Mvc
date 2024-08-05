using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Address.Mvc.Models;
using static Address.Mvc.Models.DadataResponse;
using Microsoft.Extensions.Logging;

namespace Address.Mvc
{
    public class DadataService: IDadataService
{
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DadataService> _logger;

        public DadataService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<DadataService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<DadataResponse> CleanAddressAsync(string rawAddress)
        {
            if (string.IsNullOrWhiteSpace(rawAddress))
            {
                _logger.LogWarning("Получен пустой адрес!");
                throw new ArgumentException("Адрес не может быть пустым.", nameof(rawAddress));
            }

            try
            {
                var client = _httpClientFactory.CreateClient("DadataClient");
                var content = new StringContent($"[\"{rawAddress}\"]", Encoding.UTF8, "application/json");
                var response = await client.PostAsync(string.Empty, content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Ошибка {response.StatusCode} при вызове Dadata API! Содержание ошибки: {errorContent}");
                    response.EnsureSuccessStatusCode();
                }

                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<DadataResponse>>(responseContent);

                if (result == null || result.Count == 0)
                {
                    _logger.LogWarning("Ответ является пустым!");
                    throw new Exception("Из Dadata API не был получен результат!");
                }

                _logger.LogInformation("Получен успешный ответ от Dadata API!");
                return result[0];
            }

            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении запроса к Dadata API!");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Ошибка при десериализации ответа от Dadata API!");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла непредвиденная ошибка!");
                throw;
            }
        }
    }
}

