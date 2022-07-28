using System.Text;
using CodingGameBase;

CodingGameServer codingGameServer = new CodingGameServer();

codingGameServer.Tick = Tick;

codingGameServer.StartServer();
Console.ReadLine();

void Tick()
{
    Console.WriteLine(Encoding.UTF8.GetString(codingGameServer.data));
}