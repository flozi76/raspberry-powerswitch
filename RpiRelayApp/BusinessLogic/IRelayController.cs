using System.Threading.Tasks;

namespace RpiRelayApp.BusinessLogic
{
    public interface IRelayController
    {
        Task PerformGpioCheck();
        void InitializeBoard();
    }
}