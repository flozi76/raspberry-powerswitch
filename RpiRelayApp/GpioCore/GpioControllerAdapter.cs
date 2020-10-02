using System;
using System.Device.Gpio;
using Microsoft.Extensions.Logging;
using RpiRelayApp.BusinessLogic;

namespace RpiRelayApp.GpioCore
{
    public class GpioControllerAdapter : IGpioControllerAdapter, IDisposable
    {
        private readonly ILogger<GpioControllerAdapter> _logger;
        private readonly GpioController _gpioController;

        public GpioControllerAdapter(ILogger<GpioControllerAdapter> logger)
        {
            _logger = logger;
            _gpioController = new GpioController(PinNumberingScheme.Logical);
        }

        public void OpenPin(RpiRelayPins pinNumber, PinMode mode)
        {
            try
            {
                _logger.LogInformation($"Opening Pin: {pinNumber} in Mode: {mode}");
                _gpioController.OpenPin((int)pinNumber, mode);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error opening pin");
            }
        }

        public void ClosePin(RpiRelayPins pinNumber)
        {
            _logger.LogInformation($"Closing Pin: {pinNumber}");
            _gpioController.ClosePin((int)pinNumber);
        }

        public void Write(RpiRelayPins pinNumber, PinValue value)
        {
            _logger.LogInformation($"Writing value to pin {pinNumber}, value: {value}");
            _gpioController.Write((int)pinNumber, value);
        }

        public void OpenRelay(RpiRelayPins pinNumber)
        {
            _logger.LogInformation($"Opening Relay {pinNumber}");
            Write(pinNumber, PinValue.Low);
        }

        public void CloseRelay(RpiRelayPins pinNumber)
        {
            _logger.LogInformation($"Closing Relay {pinNumber}");
            Write(pinNumber, PinValue.High);
        }

        public void Dispose()
        {
            _gpioController.Dispose();
        }
    }
}
