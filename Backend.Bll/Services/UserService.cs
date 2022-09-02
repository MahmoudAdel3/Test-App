using Backend.Bll.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Bll.Services
{
    public class UserService : IUserService
    {
        private readonly AppDBContext _dbContext;
        public UserService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserDTO> GetUserAsync(string userame, string password)
        {
            userame = userame.ToLower();
            var user = await _dbContext.Users.Where(u => u.UserName == userame)
                                        .Select(u => new
                                        {
                                            UserName = u.UserName,
                                            Name = u.Name,
                                            u.Password
                                        }).FirstOrDefaultAsync();

            return user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password) ? null : new UserDTO
            {
                Name = user.Name,
                UserName = user.UserName
            };
        }
    }
    public interface IUserService
    {
        Task<UserDTO> GetUserAsync(string userame, string password);
    }
}
