using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAnswer.Mobile.Services
{
    public interface IStorageOptionsService
    {
        public Guid GetUserId();

        public void SetUserId(Guid userId);

        public string GetRefreshToken();

        public void SetRefreshToken(string refreshToken);

        public string GetAccessToken();

        public void SetAccessToken(string accessToken);
    }
}
