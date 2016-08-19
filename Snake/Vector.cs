using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Vector
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Value { get; set; }

        public Vector(int x, int y, string value = " ")
        {
            this.X = x;
            this.Y = y;
            this.Value = value;
        }

        public void Add(Vector vector)
        {
            this.X += vector.X;
            this.Y += vector.Y;
        }

        public override bool Equals(object obj)
        {
            Vector vector = (Vector)obj;
            return (X == vector.X && Y == vector.Y) ? true : false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value} - X: {X}, Y: {Y}";
        }
    }
}
