using System;
using System.Threading;

namespace GameLoop
{
    class Program
    {
        static void Main(string[] args)
        {
            MyGame game = new MyGame();
            game.initialize();
            game.run();
        }

    }
}
