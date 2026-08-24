using AutoTestsForApplications.DTO;
using Refit;

namespace AutoTestsForApplications.Interfaces;

public interface IUserApiClient
{
    [Get("/users/{id}")]
    Task<UserResponseDTO> GetUserAsync(int id);

    [Post("/users")]
    Task<CreatedUserDTO> PostUserAsync([Body] CreateUserRequestDTO user);

    [Put("/users/{id}")]
    Task<CreatedUserDTO> PutUserAsync(int id, [Body] CreateUserRequestDTO user);

    [Delete("/users/{id}")]
    Task<ApiResponse<string>> DeleteUserAsync(int id);
}