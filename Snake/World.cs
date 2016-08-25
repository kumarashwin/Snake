using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
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
                    if (x == Width && y != Height)
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

        internal void CheckDestination(Vector destination, out bool collision, out bool eat)
        {
            var value = Grid[destination.X, destination.Y].Value;
            switch (value)
            {
                case "+":
                    collision = false;
                    eat = true;
                    break;
                case "#":
                    collision = true;
                    eat = false;
                    break;
                case "o":
                    collision = true;
                    eat = false;
                    break;
                default:
                    collision = false;
                    eat = false;
                    break;
            }
        }
    }
}
