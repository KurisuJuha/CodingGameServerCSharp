using System.Text;
using CodingGameBase;

namespace Test
{
    public class Test : TestWrapper
    {
        public override void Tick()
        {
            Console.WriteLine("test");
            value = value + 1;
        }
    }
}