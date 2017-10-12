using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Business.Dto.IOBalance;
namespace PL.Business.Interface.IOBalance
{
    public interface IUserService
    {
        IQueryable<UserTypeDto> GetAllUserType();
        IQueryable<UserDto> GetAll();
        UserDto FindUserByUserId(int userId);
        UserTypeDto FindUserTypeById(int userTypeId);
        bool SaveUser(UserDto dto);
        bool UpdateUser(int userId, UserDto newUserDetails);
        bool UpdateInactiveUser(int userId, int? updatedBy);
    }
}
