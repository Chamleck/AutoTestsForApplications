using AutoTestsForApplications.DTO.PetStore;
using Refit;

namespace AutoTestsForApplications.Interfaces;

public interface IPetStoreApi
{
    [Get("/pets")]
    Task<AllPetsResponseDTO> GetAllPetsAsync();

    [Get("/pets/{id}")]
    Task<PetDTO> GetPetByIdAsync(string id);

    // [Query] превращает параметры в строку запроса: /pets?status=ADOPTED&limit=12
    [Get("/pets")]
    Task<AllPetsResponseDTO> GetPetsByStatusAsync([Query] string status, [Query] int limit);
}
