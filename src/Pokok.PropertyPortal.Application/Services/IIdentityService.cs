using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokok.PropertyPortal.Application.Services
{
    public interface IIdentityService
    {
        Task<Guid> CreateUserAsync(string email, string name, CancellationToken cancellationToken = default);
    }
}
