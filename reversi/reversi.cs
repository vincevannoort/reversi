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

        /*
         * status 1 = player blue
         * status 2 = player red
         * status 3 = blue won
         * status 4 = red won
         * status 5 = tie
         */
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
            this.Paint += calculatePossibleMoves;
            this.Paint += drawStones;
            this.Paint += drawGameState;
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
                    pea.Graphics.DrawRectangle(Pens.Gray, i*stoneSize + offset, j*stoneSize + offset, stoneSize, stoneSize);
				}
			}
        }

        void drawStones(Object obj, PaintEventArgs pea)
        {
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
                    // blue stones
                    if (board[i,j] == 1)
                    {
                        pea.Graphics.FillEllipse(Brushes.Blue, i * stoneSize + offset + stoneMargin / 2, j * stoneSize + offset + stoneMargin / 2, stoneSize - stoneMargin, stoneSize - stoneMargin);
                    }

                    // red stones
                    else if (board[i,j] == 2)
                    {
                        pea.Graphics.FillEllipse(Brushes.Red, i * stoneSize + offset + stoneMargin / 2, j * stoneSize + offset + stoneMargin /2, stoneSize - stoneMargin, stoneSize - stoneMargin);
                    }

                    // possible moves
                    else if (board[i,j] == -1)
                    {
                        pea.Graphics.DrawEllipse(Pens.Black, i * stoneSize + offset + stoneMargin, j * stoneSize + offset + stoneMargin, stoneSize - stoneMargin * 2, stoneSize - stoneMargin * 2);
                    }
				}
			}
        }

        void calculatePossibleMoves(Object obj, PaintEventArgs pea)
        {
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
                    int oppositePlayer = getOppositePlayer();

                    // check each stone of the current player
                    if (board[i,j] == status)
                    {

                        for (int k = -1; k <= 1; k++)
                        {
                            for (int l = -1; l <= 1; l++)
                            {
                                // check if there are stones of the opposite player around the stone
                                if(board[i + k, j + l] == oppositePlayer)
                                {
                                    // check if the next stone is empty
                                    if (board[i + (k * 2), j + (l * 2)] == 0)
                                    {
                                        board[i + (k * 2), j + (l * 2)] = -1;
                                    }
                                }
                            }
                        }
                    }
				}
			}
        }

        void drawGameState(Object obj, PaintEventArgs pea)
        {
            string player = "";
            player = (status == 1) ? "blue" : "red";
            pea.Graphics.DrawString(String.Format("Current player is {0}", player), this.Font, Brushes.Black, offset, offset/2);
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

                // check if the move is possible
                if (board[stoneX, stoneY] == -1)
                {
					board[stoneX, stoneY] = status;
                    cleanPossibleMoves();
					checkAndChangeStatus();
					this.Invalidate();   
                }
            }
        }

        void cleanPossibleMoves()
        {
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
                    if (board[i, j] == -1)
                    {
                        board[i, j] = 0;
                    }
				}
			}
        }

        int getOppositePlayer()
        {
            if (status == 1)
            {
                return 2;
            } else
            {
                return 1;
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