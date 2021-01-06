using System;
using System.Threading.Tasks;

namespace Domain
{
    public interface IBetLogic
    {
        Task<object> GetBetsToday();
    }
}
