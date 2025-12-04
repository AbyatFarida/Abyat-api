using Abyat.Bl.Dtos.Base;
using Abyat.Bl.Validation;
using System.ComponentModel.DataAnnotations;

namespace Abyat.Bl.Dtos.User
{
    /// <summary>
    /// Data Transfer Object for handling refresh token operations.
    /// Used to manage authentication token refresh functionality.
    /// </summary>
    public class RefreshTokenDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the refresh token value.
        /// This token is used to obtain a new access token when the current access token expires.
        /// </summary>
        /// <value>
        /// The refresh token as a string. Must be a valid, non-expired token.
        /// This field is required for token refresh operations.
        /// </value>
        /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...</example>
        [Required(ErrorMessage = "Refresh token is required.")]
        [StringLength(500, ErrorMessage = "Token cannot exceed 500 characters.")]
        public string Token { get; set; } = null!;

        /// <summary>
        /// Gets or sets the unique identifier of the user associated with the refresh token.
        /// </summary>
        /// <value>
        /// The user's unique identifier (int). Must correspond to an existing user in the system.
        /// This field is required to associate the token with a specific user account.
        /// </value>
        /// <example>12345678-1234-1234-1234-123456789abc</example>
        [Required(ErrorMessage = "User ID is required.")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the expiration date and time of the refresh token.
        /// After this datetime, the token becomes invalid and cannot be used for refresh operations.
        /// </summary>
        /// <value>
        /// The UTC datetime when the token expires. Must be a future datetime.
        /// This field is required to ensure token validity period enforcement.
        /// </value>
        /// <example>2024-12-31T23:59:59Z</example>
        [Required(ErrorMessage = "Expiration date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid datetime.")]
        [FutureDate(ErrorMessage = "Expiration date must be in the future.")]
        public DateTimeOffset Expires { get; set; }
    }
}





