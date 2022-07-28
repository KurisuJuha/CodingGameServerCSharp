using System.Text;
using CodingGameBase;

CodingGameServer codingGameServer = new CodingGameServer();

codingGameServer.Tick = Tick;

codingGameServer.StartServer();
Console.ReadLine();

void Tick()
{
    foreach (var item in codingGameServer.sharedata.Keys)
    {
        Console.WriteLine(codingGameServer.sharedata[item].Name);
    }
}