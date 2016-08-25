using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    public enum Directions
    {
        North,
        South,
        East,
        West
    }

    public enum Command
    {
        Left,
        Right,
        Straight,
        Unassigned
    }

    static class Program
    {
        static void Main(string[] args)
        {
            World world = new World(50, 20);
            Snake snake = new Snake(new Vector(10, 10));

            int count = 0;
            bool eat = true;
            bool collision = false;
            Vector destination;

            while (true)
            {
                world.Render(snake);
                snake.ProcessCommands();
                snake.Destination(out destination);
                world.CheckDestination(destination, out collision, out eat);

                if (collision)
                {
                    break;
                }

                //Temporary
                if (count < 4)
                {
                    eat = true;
                }

                snake.Act(destination, eat);
                count++;
            }

            Console.Clear();
            Console.WriteLine("You collided with something!");
            Console.Write("Press any key to exit: ");
            Console.ReadKey();
        }
    }
}
