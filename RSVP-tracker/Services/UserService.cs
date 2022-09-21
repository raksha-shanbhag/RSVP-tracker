using RSVPtracker.Core.Interfaces;
using RSVPtracker.Core.Models;
using RSVPtracker.Core.ViewModels;

namespace RSVP_tracker.Services
{
    public class UserService : IUserService
    {
        public IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User mapViewModelToUser(SaveUserViewModel userViewModel)
        {
            return new User
            {
                UserName = userViewModel.UserName,
                PhoneNumber = userViewModel.PhoneNumber,
                Email = userViewModel.Email,
                FullName = userViewModel.FullName,
            };
        }

        public async Task<bool> CreateNewUser(SaveUserViewModel userViewModel)
        {
            if (!(userViewModel is null))
            {
                var newUser = mapViewModelToUser(userViewModel);
                newUser.CreatedDate = DateTime.Now;
                newUser.CreatedBy = 1;
                await _unitOfWork.Users.Add(newUser);

                var result = _unitOfWork.Complete();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            if (userId > 0)
            {
                var user = await _unitOfWork.Users.Get(userId);
                if (!(user is null))
                {
                    _unitOfWork.Users.Delete(user);
                    var result = _unitOfWork.Complete();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public UserViewModel mapper_userView(User user)
        {
            return new UserViewModel() {
                FullName = user.FullName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };
        }

        public List<UserViewModel> mapper_userViewList(List<User> userList)
        {
            var result = new List<UserViewModel>();
            foreach (var user in userList)
            {
                var newItem = new UserViewModel();
                newItem.FullName = user.FullName;
                newItem.UserName = user.UserName;
                newItem.PhoneNumber = user.PhoneNumber;
                newItem.Email = user.Email;
                result.Add(newItem);
            }

            return result;
        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            var userList = await _unitOfWork.Users.GetAll();
            //return mapper_userView(userList);

            var result = new List<UserViewModel>();
            foreach (var user in userList)
            {
                var newItem = new UserViewModel();
                newItem.FullName = user.FullName;
                newItem.UserName = user.UserName;
                newItem.PhoneNumber = user.PhoneNumber;
                newItem.Email = user.Email;
                result.Add(newItem);
            }

            return result;
        }

        public async Task<UserViewModel> GetUserById(int userId)
        {
            if (userId > 0)
            {
                var user = await _unitOfWork.Users.Get(userId);
                if (!(user is null))
                {
                    return mapper_userView(user);
                }
            }
            return null;
        }

        public async Task<bool> UpdateUser(int userId, SaveUserViewModel userViewModel)
        {
            if (userId > 0)
            {
                var user = await _unitOfWork.Users.Get(userId);
                if (!(user is null))
                {
                    user.FullName = userViewModel.FullName;
                    user.PhoneNumber = userViewModel.PhoneNumber;
                    user.UpdatedDate = DateTime.Now;
                    user.UpdatedBy = userId;
                    _unitOfWork.Users.Update(user);

                    var result = _unitOfWork.Complete();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
    }
}
