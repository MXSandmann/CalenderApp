using Newtonsoft.Json;
using WebUI.Clients.Contracts;
using WebUI.Models.Dtos;

namespace WebUI.Clients
{
    public class InvitiationsClient : IInvitationsClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IInvitationsClient> _logger;

        public InvitiationsClient(HttpClient httpClient, ILogger<IInvitationsClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<InvitationDto> CreateInvitation(InvitationDto invitationDto)
        {
            var response = await _httpClient.PostAsJsonAsync("Invitations", invitationDto);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogInformation("--> Http request on InvitationsService unsuccessful, status code: {responseMessage.StatusCode}", response.StatusCode);
                throw new HttpRequestException($"Error during creating an invitation: {response.StatusCode}");
            }
            var content = await response.Content.ReadAsStringAsync();
            var newInvitation = JsonConvert.DeserializeObject<InvitationDto>(content);
            ArgumentNullException.ThrowIfNull(newInvitation);
            return newInvitation;
        }
    }
}
