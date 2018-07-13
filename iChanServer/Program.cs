using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using iChanServer.CryptoCurrency;
using iChanServer.Events;
using iChanServer.MySql;
using iChanServer.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using WebSocketSharp;
using WebSocketSharp.Server;


namespace iChanServer
{
    class Program
    {
        private readonly TaskQueue _taskQueue;
        private readonly MethodManager _methodManager;
        private readonly MySqlClient _mySqlClient;
        private readonly WebSocketServer _webSocketServer;
        private readonly XPC _xpc;
        private readonly HttpListener _httpListener;
        static void Main(string[] args)
        {
            new Program().Start();
        }

        internal Program()
        {
            _webSocketServer = new WebSocketServer(EnvManager.WebSocketPort);
            _webSocketServer.AddWebSocketService<IChanBehavior>("/");
            _xpc = new XPC();
            _httpListener = new HttpListener();
            _taskQueue = new TaskQueue();
            _mySqlClient = new MySqlClient();
            _methodManager = new MethodManager(_mySqlClient, _webSocketServer.WebSocketServices, _xpc);

        }

        internal void Start()
        {
            Debug.Log("start");
            _webSocketServer.Start();
            _taskQueue.Start();
            _mySqlClient.Connect();
            //ﾍｲ! ﾘｯｽﾝ!
            //とりあえずlocalhostだけ
            //TODO 何らかの方法で認証 discordbot,iChan公式webサービスだけは全部 それ以外は[addidea]以外許可
            //↑その人本人がその入力をしたことが証明できるならその限りではない
            //TODO socketでサーバーからclientにも情報を送れるようにする
            _httpListener.Prefixes.Add(EnvManager.UriPrefix);
            _httpListener.Start();
            Console.CancelKeyPress += CancelKeyPress;
            while (true)
            {
                var context = _httpListener.GetContext();
                //ReceiveRequest()が終わらないと次のが始まらない
                //try-catchましましでだいじょーぶ?
                _taskQueue.Enqueue(() => ReceiveRequest(context));
            }
        }

        private void ReceiveRequest(HttpListenerContext context)
        {
            string requestText;
            var request = context.Request;
            using (var sr = new StreamReader(request.InputStream))
            {
                requestText = sr.ReadToEnd();
            }
            Debug.Log($"receive :{request}");
            string responseText = _methodManager.Response(requestText);
            Debug.Log($"send :{responseText}");
            try
            {
                using (var response = context.Response)
                {
                    using (var sw = new StreamWriter(response.OutputStream))
                    {
                        sw.Write(responseText);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        private void CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            _webSocketServer.Stop();
            _taskQueue.Stop();
            _httpListener.Close();
            Debug.Log("exit");
        }
    }
}
