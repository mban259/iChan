using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using iChanServer.CryptoCurrency;
using iChanServer.MySql;
using iChanServer.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;


namespace iChanServer
{
    class Program
    {
        private TaskQueue _taskQueue;
        private MethodManager _methodManager;
        private MySqlClient _mySqlClient;
        private XPC _xpc;
        static void Main(string[] args)
        {
            new Program().Start();
        }

        internal void Start()
        {
            Debug.Log("start");
            _xpc = new XPC();
            _taskQueue = new TaskQueue();
            _mySqlClient = new MySqlClient();
            _methodManager = new MethodManager(_mySqlClient, _xpc);

            _taskQueue.Start();
            using (HttpListener listener = new HttpListener())
            {
                //ﾍｲ! ﾘｯｽﾝ!
                //とりあえずlocalhostだけ
                //TODO 何らかの方法で認証 discordbot,iChan公式webサービスだけは全部 それ以外は[addidea]以外許可
                //↑その人本人がその入力をしたことが証明できるならその限りではない
                //TODO socketでサーバーからclientにも情報を送れるようにする
                listener.Prefixes.Add(EnvManager.UriPrefix);
                listener.Start();
                Console.CancelKeyPress += CancelKeyPress;
                while (true)
                {
                    var context = listener.GetContext();
                    //ReceiveRequest()が終わらないと次のが始まらない
                    //try-catchましましでだいじょーぶ?
                    _taskQueue.Enqueue(() => ReceiveRequest(context));
                }
            }
        }

        private string Response(string request)
        {
            try
            {
                var jRequest = JsonConvert.DeserializeObject<JObject>(request);
                string method = (string)jRequest["method"];
                switch (method)
                {
                    case "ping":
                        return _methodManager.Ping();
                    case "addidea":
                        return _methodManager.AddIdea(jRequest["params"]);
                    default:
                        return _methodManager.UnknownMethod(method);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
                return _methodManager.Failed("request failed");
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
            Debug.Log($"Receive \"{requestText}\" from {request.RemoteEndPoint}");
            string responseText = Response(requestText);
            Console.WriteLine($"Send {responseText}");
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
            _taskQueue.Stop();
            Debug.Log("exit");
        }
    }
}
