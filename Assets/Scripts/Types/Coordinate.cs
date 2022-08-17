using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AOP
{
    public struct Coordinate
    {
        public int x { get; private set; }

        public int y { get; private set; }

        public static Coordinate Empity = new Coordinate(-1 , -1);

        public Coordinate(int row = 0, int column = 0)
        {
            x = row;
            y = column;
        }

        public void Translate(Direction dir)
        {
            switch (dir)
            {
                case Direction.Right:
                    x += 1;
                    break;
                case Direction.Left:
                    x -= 1;
                    break;
                case Direction.Up:
                    y += 1;
                    break;
                case Direction.Down:
                    y -= 1;
                    break;
                case Direction.RightDown:
                    x += 1;
                    y -= 1;
                    break;
                case Direction.LeftDown:
                    x -= 1;
                    y -= 1;
                    break;
                case Direction.RightUp:
                    x += 1;
                    y += 1;
                    break;
                case Direction.LeftUp:
                    x -= 1;
                    y += 1;
                    break;
            }
        }

        public bool IsDiagonalTo(Coordinate other)
        {
            return x != other.x &&
                y != other.y;
        }

        public static bool operator ==(Coordinate a, Coordinate b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Coordinate a, Coordinate b)
        {
            return !a.Equals(b);
        }

        public static implicit operator Vector2(Coordinate a)=>new Vector2(a.x,a.y);

        public override bool Equals(object obj)
        {
            if (obj is Coordinate otherPoint)
            {
                return x == otherPoint.x && y == otherPoint.y;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"[{x}.{y}]";
        }

    }
}

