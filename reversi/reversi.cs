using System;
using System.Drawing;
using System.Windows.Forms;

namespace Reversi
{
    public class Reversi : Form
    {
        int[,] board;
        int width, height;
        int stoneSize = 50;
        int stoneMargin = 10;
        int offset = 100;
        int status = 1;

        public Reversi()
        {
            board = new int[6, 6];
            this.width = 6;
            this.height = 6;
            this.InitialiseEventHandlers();
        }

		public Reversi(int width, int height)
		{
            if (width < 3) this.width = 3; else this.width = width;
            if (height < 3) this.height = 3; else this.height = height;
            board = new int[width, height];
            this.InitialiseEventHandlers();
		}

        void InitialiseEventHandlers()
        {
            this.ClientSize = new Size(width*stoneSize + offset*2, height*stoneSize + offset*2);
            this.BackColor = Color.White;
            this.addInitialStones();
            this.Paint += drawBoard;
            this.Paint += drawStones;
            this.MouseClick += addStone;
        }

        public void DebugReversi()
        {
            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    System.Diagnostics.Debug.Write(board[i, j]);
                }
                System.Diagnostics.Debug.Write('\n');
            }
        }

        void drawBoard(Object obj, PaintEventArgs pea)
        {
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
                    pea.Graphics.DrawRectangle(Pens.Black, i*stoneSize + offset, j*stoneSize + offset, stoneSize, stoneSize);
				}
			}
        }

        void drawStones(Object obj, PaintEventArgs pea)
        {
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
                    if (board[i,j] == 1)
                    {
                        pea.Graphics.FillEllipse(Brushes.Blue, i * stoneSize + offset + stoneMargin / 2, j * stoneSize + offset + stoneMargin / 2, stoneSize - stoneMargin, stoneSize - stoneMargin);
                    }

                    else if (board[i,j] == 2)
                    {
                        pea.Graphics.FillEllipse(Brushes.Red, i * stoneSize + offset + stoneMargin / 2, j * stoneSize + offset + stoneMargin /2, stoneSize - stoneMargin, stoneSize - stoneMargin);
                    }
				}
			}
        }

        void addInitialStones()
        {
            int tempx = width / 2;
            int tempy = height / 2;

            board[tempx-1, tempy-1] = 1;
            board[tempx, tempy-1] = 2;
            board[tempx-1, tempy] = 2;
            board[tempx, tempy] = 1;
        }

        void addStone(Object obj, MouseEventArgs mea)
        {
            // check if within bounds
            if (
                mea.X > offset
                && mea.Y > offset
                && mea.X < offset + width * stoneSize
                && mea.Y < offset + height * stoneSize
            )
            {

                int x = mea.X - offset;
                int y = mea.Y - offset;
                int stoneX = x / stoneSize;
                int stoneY = y / stoneSize;

                // check if the stone is already set
                if (board[stoneX, stoneY] == 0)
                {
					board[stoneX, stoneY] = status;
					checkAndChangeStatus();
					this.Invalidate();   
                }
            }
        }

        void checkAndChangeStatus()
        {
            // TODO: couple of checks (win or remise)

            // Switch active player
            if (status == 1)
                status = 2;
            else if (status == 2)
                status = 1;

        }
    }
}