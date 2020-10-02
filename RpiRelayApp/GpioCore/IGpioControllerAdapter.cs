using System.Device.Gpio;
using RpiRelayApp.BusinessLogic;

namespace RpiRelayApp.GpioCore
{
    public interface IGpioControllerAdapter
    {
        void OpenPin(RpiRelayPins pinNumber, PinMode mode);
        void ClosePin(RpiRelayPins pinNumber);
        void Write(RpiRelayPins pinNumber, PinValue value);
        void OpenRelay(RpiRelayPins pinNumber);
        void CloseRelay(RpiRelayPins pinNumber);
    }
}