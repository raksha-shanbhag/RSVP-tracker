using RSVPtracker.Core.Models;
using RSVPtracker.Core.ViewModels;

namespace RSVP_tracker.Services
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetAllUsers();
        Task<bool> CreateNewUser(SaveUserViewModel userViewModel);
        Task<UserViewModel> GetUserById(int userId);
        Task<bool> UpdateUser(int id, SaveUserViewModel userViewModel);
        Task<bool> DeleteUser(int userId);

    }
}
