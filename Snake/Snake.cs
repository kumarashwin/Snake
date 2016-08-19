using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Snake
    {
        public Vector Head { get { return Parts.First(); } }
        public List<Vector> Parts { get; set; }
        public Directions LastDirection { get; set; }
        public static Dictionary<Directions, Vector> DirectionToVector { get; private set; }

        static Snake()
        {
            DirectionToVector = new Dictionary<Directions, Vector>() {
                { Directions.North, new Vector(0, -1) },
                { Directions.South, new Vector(0, 1) },
                { Directions.East, new Vector(1, 0) },
                { Directions.West, new Vector(-1, 0) }};
        }

        public Snake(Vector vector)
        {
            vector.Value = "O";
            Parts = new List<Vector>() { vector };
            LastDirection = Directions.South;
        }

        public void Propogate(Vector destination)
        {
            Vector currentPosition;
            for (int i = 1; i < Parts.Count; i++)
            {
                currentPosition = Parts[i];
                Parts[i] = destination;
                destination = currentPosition;
            }
        }

        public void Move(Command command = Command.Straight)
        {
            Vector currentPosition = new Vector(Head.X, Head.Y);

            Directions destinationDirection = LastDirection;

            if (command == Command.Left)
            {
                switch (LastDirection)
                {
                    case Directions.North:
                        destinationDirection = Directions.West;
                        break;
                    case Directions.South:
                        destinationDirection = Directions.East;
                        break;
                    case Directions.East:
                        destinationDirection = Directions.North;
                        break;
                    case Directions.West:
                        destinationDirection = Directions.South;
                        break;
                    default:
                        break;
                }
            }
            else if (command == Command.Right)
            {
                switch (LastDirection)
                {
                    case Directions.North:
                        destinationDirection = Directions.East;
                        break;
                    case Directions.South:
                        destinationDirection = Directions.West;
                        break;
                    case Directions.East:
                        destinationDirection = Directions.South;
                        break;
                    case Directions.West:
                        destinationDirection = Directions.North;
                        break;
                    default:
                        break;
                }
            }

            Head.Add(DirectionToVector[destinationDirection]);
            LastDirection = destinationDirection;
            Propogate(currentPosition);
        }
    }
}
