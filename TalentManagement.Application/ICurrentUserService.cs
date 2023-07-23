using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManagement.Application
{
    public interface ICurrentUserService
    {
        Task<string> GetCurrentUserId();
    }
}
