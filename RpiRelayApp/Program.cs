using System;
using System.Device.Gpio;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RpiRelayApp.BusinessLogic;
using RpiRelayApp.GpioCore;

namespace RpiRelayApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            
            
            Console.WriteLine("Starting Relay Controller");
            var controller = new GpioController();


            OpenPin(26, controller);
            OpenPin(20, controller);
            OpenPin(21, controller);

            while (true)
            {
                int pinNumber = 26;
                PerformTestOnPin(controller, pinNumber);
                Thread.Sleep(500);

                pinNumber = 20;
                PerformTestOnPin(controller, pinNumber);

                Thread.Sleep(500);
                pinNumber = 21;
                PerformTestOnPin(controller, pinNumber);
                Thread.Sleep(500);

                Console.WriteLine();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddSingleton<IRelayController, RelayController>();
                    services.AddSingleton<IGpioControllerAdapter, GpioControllerAdapter>();
                });

        private static void PerformTestOnPin(GpioController controller, int pinNumber)
        {
            try
            {
                

                Console.WriteLine($"Writing to pin: {pinNumber}");
                controller.Write(pinNumber, PinValue.Low);
                Console.WriteLine($"Written to pin: {pinNumber}");

                Thread.Sleep(1000);

                controller.Write(pinNumber, PinValue.High);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void OpenPin(int pinNumber, GpioController controller)
        {
            Console.WriteLine($"Opening pin: {pinNumber}");
            controller.OpenPin(pinNumber, PinMode.Output);
        }
    }
}
