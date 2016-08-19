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
        Straight
    }

    class Program
    {
        static void Main(string[] args)
        {
            int width = 10;
            int height = 10;

            World world = new World(width, height);
            Snake snake = new Snake(new Vector(3, 3));
            while (true)
            {
                snake.Move(Command.Left);
                world.Render(snake);
                Thread.Sleep(1000);
            }
        }
    }

    class World
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector[,] Grid { get; set; }

        public World(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            CreateGrid();
        }

        public void Render(Snake snake)
        {
            Console.Clear();
            CreateGrid();
            foreach (var part in snake.Parts)
            {
                Grid[part.X, part.Y] = part;
            }
            Console.WriteLine(this.ToString());
        }

        private void CreateGrid()
        {
            this.Grid = new Vector[Width, Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (x == 0 || x == Width - 1 || y == 0 || y == Height - 1)
                    {
                        Grid[x, y] = new Vector(x, y, "#");
                    }
                    else
                    {
                        Grid[x, y] = new Vector(x, y);
                    }
                }
            }
        }

        public override string ToString()
        {
            string result = "";
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x <= Width; x++)
                {
                    if(x == Width && y != Height)
                    {
                        result += "\n";
                    }
                    else
                    {
                        result += Grid[x, y].Value;
                    }
                }
            }
            return result;
        }
    }
}
