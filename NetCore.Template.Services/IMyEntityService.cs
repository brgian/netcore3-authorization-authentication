using NetCore.Template.DTOs;
using System.Collections.Generic;

namespace NetCore.Template.Services
{
    public interface IMyEntityService
    {
        IEnumerable<MyEntityDto> GetAll();
        MyEntityDto Get(string key);

        MyEntityDto Update(MyEntityDto updateUserRequest);
        MyEntityDto Create(MyEntityDto createUserRequest);

        void Delete(string key);
    }
}
