using AutoTestsForApplications.DTO.BookStore;
using Refit;

namespace AutoTestsForApplications.Interfaces;

public interface IBookStoreApi
{
    [Post("/Account/v1/User")]
    Task<BookStoreCreatedUserDTO> CreateUserAsync([Body] BookStoreCredentialsDTO credentials);

    [Post("/Account/v1/GenerateToken")]
    Task<BookStoreTokenDTO> GenerateTokenAsync([Body] BookStoreCredentialsDTO credentials);

    [Post("/Account/v1/Login")]
    Task<BookStoreLoginDTO> LoginAsync([Body] BookStoreCredentialsDTO credentials);

    [Get("/Account/v1/User/{userId}")]
    Task<BookStoreUserInfoDTO> GetUserAsync(string userId, [Header("Authorization")] string authorization);

    [Get("/BookStore/v1/Books")]
    Task<BooksListDTO> GetAllBooksAsync();

    // {isbn} из пути метода подставляется в query-строку: /Book?ISBN=...
    [Get("/BookStore/v1/Book?ISBN={isbn}")]
    Task<BookDTO> GetBookByIsbnAsync(string isbn);

    // authorization может быть null: тогда заголовок не отправляется (нужно для негативного теста)
    [Post("/BookStore/v1/Books")]
    Task<AddBooksResponseDTO> AddBooksAsync([Body] AddBooksRequestDTO request, [Header("Authorization")] string? authorization);

    // при успехе сервер отвечает 204 No Content, поэтому возвращаем просто Task без модели
    [Delete("/BookStore/v1/Book")]
    Task DeleteBookAsync([Body] DeleteBookRequestDTO request, [Header("Authorization")] string authorization);
}
