using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PushNotification
{
    //creamos nuestro hub
    public class NotificationHub :Hub
    {
        //aqui pudes inejctar lo que quieras
     
        //este metodo simpre se ejecutara al lo que alguien se conecte 
        public  override async Task OnConnectedAsync()
        {
            //Puedes usar el context para accerder diferentes cosas 
            //Context.ConnectionId
            //agregar a grupo si quieres
            await Groups.AddToGroupAsync(Context.ConnectionId, "migrupo");
             await base.OnConnectedAsync();
        }
    }
}
