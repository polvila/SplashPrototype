using System;

namespace Core.Authentication
{
    public interface IAuthenticationService
    {
        void InitService(Action onComplete, Action onError = null);
        void DeviceAuthentication(Action onComplete, Action onError = null);
    }
}
