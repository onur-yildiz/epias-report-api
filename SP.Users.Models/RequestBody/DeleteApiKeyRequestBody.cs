using System.ComponentModel.DataAnnotations;

namespace SP.Users.Models.RequestBody
{
    public class DeleteApiKeyRequestBody : IDeleteApiKeyRequestBody
    {
        /// <summary>
        /// API key
        /// </summary>
        [Required]
        public string ApiKey { get; set; } = String.Empty;
    }
}
