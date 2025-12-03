using Abyat.Core.Resources;
using System.ComponentModel.DataAnnotations;

namespace Abyat.Bl.Dtos.User;

/// <summary>
/// Data Transfer Object for handling forgot password requests.
/// Contains the necessary information to initiate a password reset process.
/// </summary>
public class ForgotPasswordDto
{
    /// <summary>
    /// Gets or sets the email address of the user requesting a password reset.
    /// This email will be used to send password reset instructions.
    /// </summary>
    /// <value>
    /// The email address as a string. Must be a valid email format.
    /// This field is required for password reset functionality.
    /// </value>
    /// <example>user@example.com</example>
    [Required(ErrorMessageResourceType = typeof(ResMessages), ErrorMessageResourceName = "EmailRequired")]
    [EmailAddress(ErrorMessageResourceType = typeof(ResMessages), ErrorMessageResourceName = "InvalidEmail")]
    public string Email { get; set; } = null!;
}