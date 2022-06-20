using System.ComponentModel.DataAnnotations;

namespace SP.Users.Models.RequestBody
{
    public class DeleteApiKeyRequestBody
    {
        public DeleteApiKeyRequestBody(string apiKey)
        {
            ApiKey = apiKey;
        }

        [Required]
        public string ApiKey { get; set; }
    }
}
