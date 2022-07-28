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
        public Dictionary<string, CodingGameData> sharedata;
        public byte[] data;
        public Action Tick;

        private HttpListener listener;

        public CodingGameServer()
        {
            this.port = 59108;
            sharedata = new Dictionary<string, CodingGameData>();
        }

        public CodingGameServer(int port)
        {
            this.port = port;
            sharedata = new Dictionary<string, CodingGameData>();
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

                        DecodeData(data);

                        if (Tick != null) Tick();

                        DataWriter writer = new DataWriter();
                        //長さを追加
                        writer.Put(sharedata.Count);
                        foreach (var item in sharedata.Keys)
                        {
                            //string（パラメーターネーム）を追加
                            writer.Put(item);
                            //bytesを追加
                            writer.Put(sharedata[item].ToDataWriter());
                        }

                        response.OutputStream.Write(writer.GetData(), 0, writer.GetData().Length);
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

        public void DecodeData(byte[] data)
        {
            DataReader reader = new DataReader(data);
            //長さを取得
            int length = reader.GetInt();
            for (int i = 0; i < length; i++)
            {
                //パラメーターネームを取得
                string param = reader.GetString();
                //値が存在するなら
                if (sharedata.TryGetValue(param,out CodingGameData? value))
                {
                    value.Decode(reader);
                }
                else
                {
                    value = new CodingGameData();
                    value.Decode(reader);

                    sharedata.Add(param, value);
                }
            }
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