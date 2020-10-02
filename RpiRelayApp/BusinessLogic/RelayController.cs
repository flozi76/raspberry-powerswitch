using System;
using System.Device.Gpio;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RpiRelayApp.GpioCore;

namespace RpiRelayApp.BusinessLogic
{
    public class RelayController : IRelayController
    {
        private readonly ILogger<RelayController> _logger;
        private readonly IGpioControllerAdapter _gpioController;
        private bool _boardInitialized;

        public RelayController(ILogger<RelayController> logger, IGpioControllerAdapter gpioController)
        {
            _logger = logger;
            _gpioController = gpioController;
        }

        public async Task PerformGpioCheck()
        {
            _gpioController.OpenRelay(RpiRelayPins.Ch1);
            _gpioController.OpenRelay(RpiRelayPins.Ch2);
            _gpioController.OpenRelay(RpiRelayPins.Ch3);
            await Task.Delay(1000);

            _gpioController.CloseRelay(RpiRelayPins.Ch1);
            _gpioController.CloseRelay(RpiRelayPins.Ch2);
            _gpioController.CloseRelay(RpiRelayPins.Ch3);
        }

        public void InitializeBoard()
        {
            if (!_boardInitialized)
            {

                _gpioController.OpenPin(RpiRelayPins.Ch1, PinMode.Output);
                _gpioController.OpenPin(RpiRelayPins.Ch2, PinMode.Output);
                _gpioController.OpenPin(RpiRelayPins.Ch3, PinMode.Output);


                _boardInitialized = true;
            }
        }
    }
}
