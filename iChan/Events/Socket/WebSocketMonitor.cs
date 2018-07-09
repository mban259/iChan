using System;
using System.Collections.Generic;
using System.Text;
using iChan.Utils;
using WebSocketSharp;

namespace iChan.Events.Socket
{
    class WebSocketMonitor
    {
        private readonly WebSocket _webSocket;
        public WebSocketMonitor()
        {
            _webSocket = new WebSocket(EnvManager.WebsocketUri);
            _webSocket.Connect();
            _webSocket.OnMessage += OnMessage;
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            string message = e.Data;
            Console.WriteLine(message);
        }
    }
}
