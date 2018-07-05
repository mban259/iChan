using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace iChanServer.Events
{
    class IChanBehavior : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            base.OnOpen();
        }

        protected override void OnMessage(MessageEventArgs messageEventArgs)
        {

        }
    }
}
