using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


namespace AOP.Services
{
    public class UserProfile :IService
    {
        public async Task<string> GetUserName()
        {
            await Task.Delay(2500);
            return "Mustafa";
        }

    }
}
