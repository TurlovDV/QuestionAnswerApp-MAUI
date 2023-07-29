using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.Mobile.Services
{
    public class StorageOptionsService : IStorageOptionsService
    {
        private string refreshToken;
        private string accessToken;
        private Guid userId;

        public string GetRefreshToken() =>        
            refreshToken ?? Preferences.Get("RefreshToken", Guid.Empty.ToString());        

        public void SetAccessToken(string accessToken)
        {
            this.accessToken = accessToken;
            Preferences.Set("AccessToken", accessToken);
        }

        public string GetAccessToken() =>
            accessToken ?? Preferences.Get("AccessToken", Guid.Empty.ToString());

        public void SetRefreshToken(string refreshToken)
        {
            this.refreshToken = refreshToken;
            Preferences.Set("RefreshToken", refreshToken);
        }

        public void SetUserId(Guid userId)
        {
            this.userId = userId;
            Preferences.Set("UserId", userId.ToString());
        }

        public Guid GetUserId() =>
            userId != Guid.Empty ? userId : Guid.Parse(Preferences.Get("UserId", Guid.Empty.ToString()));
    }
}
