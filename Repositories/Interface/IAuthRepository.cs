using KindHeartCharity.Models.DTO;

namespace KindHeartCharity.Repositories.Interface
{
    public interface IAuthRepository
    {
        Task<Status> LoginAsync(LoginRequestDto loginRequestDto);

        Task LogOutAsync();

        Task<Status> RegisterAsync(RegisterRequestDto registerRequestDto);

    }
}
