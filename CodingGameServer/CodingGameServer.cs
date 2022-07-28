using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace CodingGameBase
{
    public class CodingGameServer
    {
        public int port { get; private set; }
        public Dictionary<string, byte[]> sharedata;
        public byte[] data;
        public Action Tick;

        private HttpListener listener;

        public CodingGameServer()
        {
            this.port = 59108;
            sharedata = new Dictionary<string, byte[]>();
        }

        public CodingGameServer(int port)
        {
            this.port = port;
            sharedata = new Dictionary<string, byte[]>();
        }

        public void StartServer()
        {
            listener = new HttpListener();

            listener.Prefixes.Clear();
            listener.Prefixes.Add("http://localhost:" + port + "/");
            Task.Run(() => OnProcess());
        }

        public void OnProcess()
        {
            try
            {
                listener.Start();

                while (true)
                {
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerRequest request = context.Request;

                    HttpListenerResponse response = context.Response;

                    if (request != null)
                    {
                        byte[] text = Encoding.UTF8.GetBytes("hogehogefoo");
                        data = GetByteArrayFromStream(request.InputStream);

                        DecodeData();

                        if (Tick != null) Tick();

                        response.OutputStream.Write(text, 0, text.Length);
                    }
                    else
                    {
                        response.StatusCode = 404;
                    }
                    response.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DecodeData()
        {

        }

        private static byte[] GetByteArrayFromStream(Stream sm)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                sm.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}