using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Microsoft.AspNetCore.Sockets.Features
{
    public interface IAuthenticationFeature
    {
        ClaimsPrincipal User { get; set; }
    }
}
