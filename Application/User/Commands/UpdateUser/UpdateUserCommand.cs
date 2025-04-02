using Application.Common.Requests;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.User.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<RequestResult>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        [EmailAddress]
        public string? OldEmail { get; set; }
        [EmailAddress]
        public required string NewEmail { get; set; }

        public string? Password { get; set; }
        [MaxLength(35)]
        public string? Firstname { get; set; }
        [MaxLength(35)]
        public string? Lastname { get; set; }
        [MaxLength(255)]
        public string? Address { get; set; }
        [MaxLength(255)]
        public string? ComplementaryAddress { get; set; }
        [MaxLength(12)]
        public string? PostalCode { get; set; }
        [MaxLength(255)]
        public string? City { get; set; }
        [MaxLength(2)]
        public string? Country { get; set; }
        public bool MarketingEmail { get; set; }
    }
}
