using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Address.Mvc.Models;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;


namespace Address.Mvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<AddressController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _dadataApiUrl;

        public AddressController(IConfiguration configuration, IMapper mapper, ILogger<AddressController> logger, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _dadataApiUrl = _configuration["Dadata:BaseUrl"] ?? throw new ArgumentNullException("API URL не настроен!");
        }

        [HttpGet("clean")]
        public async Task<IActionResult> CleanAddress([FromQuery] string rawAddress)
        {
            if (string.IsNullOrWhiteSpace(rawAddress))
            {
                return BadRequest("Поле пустое! Укажите адрес!");
            }

            try
            {
                var client = _httpClientFactory.CreateClient("DadataClient");
                client.BaseAddress = new Uri(_dadataApiUrl);

                _logger.LogInformation($"Отправка запроса в Dadata API с адресом: {rawAddress}");
                var requestContent = new StringContent($"[\"{rawAddress}\"]", Encoding.UTF8, "application/json");
                var response = await client.PostAsync(string.Empty, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    
                    _logger.LogInformation("Получен успешный ответ от Dadata API!");
                    _logger.LogDebug($"Содержимое ответа: {content}");

                    var jsonResponse = JArray.Parse(content);
                    var result = jsonResponse.FirstOrDefault();

                    if (result == null)
                    {
                        _logger.LogWarning($"Некорректный или несуществующий адрес: {rawAddress}");
                        return BadRequest("Введен некорректный или несуществующий адрес.");
                    }

                    return Ok(content);
                }

                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Запрос не удался с кодом состояния {response.StatusCode}");
                    return StatusCode((int)response.StatusCode, "Произошла ошибка при обработке адреса!");
                }
            }

            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Произошла ошибка при вызове API!");
                return StatusCode(503, "Произошла ошибка при вызове API!");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла непредвиденная ошибка!");
                return StatusCode(500, "Произошла непредвиденная ошибка!");
            }
            
        }
    }
}
