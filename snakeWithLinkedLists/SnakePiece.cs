using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snakeWithLinkedLists
{
    public enum SnakeDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    struct SnakePiece : IComparable
    {
        public SnakeDirection Direction;
        private Rectangle rectangle;

        public Point Position
        {
            get
            {
                return new Point(rectangle.X, rectangle.Y);
            }

            set
            {
                rectangle.X = value.X;
                rectangle.Y = value.Y;
            }
        }

        public int Size => rectangle.Width;

        public SnakePiece(SnakeDirection direction, Point position, int size)
            : this(direction, position.X, position.Y, size)
        {
            //Pass-through constructor
        }

        public SnakePiece(SnakeDirection direction, int x, int y, int size)
        {
            Direction = direction;
            rectangle = new Rectangle(x, y, size, size);
        }

        public int CompareTo(object obj)
        {
            SnakePiece piece = (SnakePiece)obj;
            return piece.Direction == Direction && piece.rectangle == rectangle ? 0 : 1;
        }

        public void Draw(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.White, rectangle);
        }
    }
}
