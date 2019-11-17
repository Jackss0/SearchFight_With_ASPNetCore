using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SearchFight_With_ASPNetCore
{
    public class MyClassListener : IHostedService
    {
        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Listening...");

            _timer = new Timer(
                MyCallback,
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(2));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Listener Stopped");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void MyCallback(object state)
        {
            Console.WriteLine("Working...");
        }
    }
}
