using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PushNotification
{
    public class NotificationHostedService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _provider;
        private IServiceScope _Scope;
        private  NotificationHub _hub;
        private Timer _timer;

        //0 segundos 
        private int _startIn = 0;

        //1 segundo 
        private int _loopEvery = 20;

        public NotificationHostedService(IServiceProvider provider)
        {
            _provider = provider;

          
               
            
        }

        private void TimerLoop(object sender)
        {
            
            _hub?.Clients?.Group("migrupo")?.SendAsync("Perros", "Perro"+ new Random().Next(0, 100));

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _Scope = _provider.CreateScope();

            _hub = _Scope.ServiceProvider
                .GetRequiredService<NotificationHub>();

            _timer = new Timer(TimerLoop, null,
                TimeSpan.FromSeconds(_startIn),
                TimeSpan.FromSeconds(_loopEvery));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //parar el timer
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer.Dispose();
            _Scope.Dispose();
        }
    }
}
