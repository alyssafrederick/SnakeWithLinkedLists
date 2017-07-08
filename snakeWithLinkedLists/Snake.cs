using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snakeWithLinkedLists
{
    class Snake : binarySearchTrees.LinkedList<SnakePiece>
    {
        private int size;

        private SnakePiece tail => Last.Value;

        public Dictionary<Keys, SnakeDirection> Moves { get; set; }

        public Snake(SnakeDirection direction, int x, int y, int size = 10)
        {
            this.size = size;
            Add(new SnakePiece(direction, x, y, size));

            Grow(); //Start with a tail... to make movement simpler
        }

        public void Grow()
        {
            Point newPiecePosition = tail.Position;

            switch (tail.Direction)
            {
                case SnakeDirection.Up:
                    newPiecePosition.Y += tail.Size;
                    break;

                case SnakeDirection.Down:
                    newPiecePosition.Y -= tail.Size;
                    break;

                case SnakeDirection.Left:
                    newPiecePosition.X += tail.Size;
                    break;

                case SnakeDirection.Right:
                    newPiecePosition.X -= tail.Size;
                    break;
            }
            
            AddAfter(new SnakePiece(tail.Direction, newPiecePosition, size), size--);
        }

        public void SetDirection(Keys keyCode)
        {
            if (!Moves.ContainsKey(keyCode))
            {
                return;
            }

            First.Value.Direction = Moves[keyCode];
        }

        public void Move()
        {
            Point newPosition = First.Value.Position;
            switch (First.Value.Direction)
            {
                case SnakeDirection.Up:
                    newPosition.Y -= size;
                    break;

                case SnakeDirection.Down:
                    newPosition.Y += size;
                    break;

                case SnakeDirection.Left:
                    newPosition.X -= size;
                    break;

                case SnakeDirection.Right:
                    newPosition.X += size;
                    break;
            }

            var temp = tail;
            temp.Direction = First.Value.Direction;
            temp.Position = newPosition;

            DeleteLast();
            AddToStart(temp);



        }

        public void Draw(Graphics gfx)
        {
            First.Value.Draw(gfx);

            var node = First.nextnode;
            do
            {
                node.Value.Draw(gfx);
                node = node.nextnode;
            } while (node != First);

        }
    }
}
