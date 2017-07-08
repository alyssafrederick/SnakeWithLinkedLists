using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace snakeWithLinkedLists
{
    public partial class Form1 : Form
    {
        Bitmap canvas;
        Graphics gfx;

        Random random;
        Rectangle food;

        Snake snake = new Snake(SnakeDirection.Right, 30, 30)
        {
            Moves = new Dictionary<Keys, SnakeDirection>()
            {
                { Keys.Up, SnakeDirection.Up },
                { Keys.Down, SnakeDirection.Down },
                { Keys.Left, SnakeDirection.Left },
                { Keys.Right, SnakeDirection.Right }
            }
        };

        public Form1()
        {
            InitializeComponent();
            canvas = new Bitmap(ClientSize.Width, ClientSize.Height);
            gfx = Graphics.FromImage(canvas);

            Timer timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 45;
            timer.Start();

            KeyDown += keyDown;
            random = new Random();

            food.X = random.Next(5, ClientSize.Width - 15);
            food.Y = random.Next(5, ClientSize.Height - 15);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //clearing the screen
            gfx.Clear(Color.Black);

            snake.Move();

            bitBox.Image = canvas;

            //drawing food
            Rectangle foodArea = new Rectangle(food.X, food.Y, 10, 10);
            gfx.FillRectangle(Brushes.Purple, foodArea);


            //if the head intersected with the food
            if (foodArea.IntersectsWith(new Rectangle(snake.First.Value.Position.X, snake.First.Value.Position.Y, 10, 10)))
            {
                food.X = random.Next(5, ClientSize.Width - 15);
                food.Y = random.Next(5, ClientSize.Height - 15);

                snake.Grow();
            }


            snake.Draw(gfx);


                ////if the snake goes off the screen to reappear on the other side
                //if (snake.Head.Value.Positon.X >= ClientSize.Width)
                //{
                //    snake.Head.Value.Positon.X = 0;
                //    snake.Head.Value.Direction = SnakeDirection.Right;
                //}
                //else if (snake.Head.Value.X < 0)
                //{
                //    snake.Head.Value.X = ClientSize.Width;
                //    snake.Head.Value.Direction = SnakeDirection.Left;
                //}
                //else if (snake.Head.Value.Y >= ClientSize.Height)
                //{
                //    snake.Head.Value.Y = 0;
                //    snake.Head.Value.Direction = SnakeDirection.Down;
                //}
                //else if (snake.Head.Value.Y < 0)
                //{
                //    snake.Head.Value.Y = ClientSize.Height;
                //    snake.Head.Value.Direction = SnakeDirection.Up;
                //}
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            snake.SetDirection(e.KeyCode);
        }
    }
}
