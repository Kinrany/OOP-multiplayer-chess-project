using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientNamespace
{
    partial class ChessForm : Form
    {
        int space = 5;
		UserData userData;
		GameData gameData;
		public ChessForm(UserData userData, GameData gameData)
        {
			this.userData = userData;
			this.gameData = gameData;
			InitializeComponent();
        }

        public void drawfigure(Image img, int x, int y, int sz, PaintEventArgs e)
        {
            e.Graphics.DrawImage(img, space + x * sz, space + y * sz, sz, sz);
        }

        public void drawchess(PaintEventArgs e)
        {
            int sz = (Math.Min(Height - 39, Width) - space * 2) / 8;
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    if ((i + j) % 2 == 0)
                    {
                        e.Graphics.FillRectangle(Brushes.Chocolate, space + i * sz, space + j * sz, sz, sz);
                    } else
                    {
                        e.Graphics.FillRectangle(Brushes.Bisque, space + i * sz, space + j * sz, sz, sz);
                    }
					switch (gameData.Board.Array[i, j]) {
						case ChessFigure.k:
							drawfigure(ChessGUI.Properties.Resources.b_k, i, j, sz, e);
							break;
						case ChessFigure.q:
							drawfigure(ChessGUI.Properties.Resources.b_q, i, j, sz, e);
							break;
						case ChessFigure.b:
							drawfigure(ChessGUI.Properties.Resources.b_b, i, j, sz, e);
							break;
						case ChessFigure.n:
							drawfigure(ChessGUI.Properties.Resources.b_n, i, j, sz, e);
							break;
						case ChessFigure.p:
							drawfigure(ChessGUI.Properties.Resources.b_p, i, j, sz, e);
							break;
						case ChessFigure.r:
							drawfigure(ChessGUI.Properties.Resources.b_r, i, j, sz, e);
							break;
						case ChessFigure.K:
							drawfigure(ChessGUI.Properties.Resources.w_k, i, j, sz, e);
							break;
						case ChessFigure.Q:
							drawfigure(ChessGUI.Properties.Resources.w_q, i, j, sz, e);
							break;
						case ChessFigure.B:
							drawfigure(ChessGUI.Properties.Resources.w_b, i, j, sz, e);
							break;
						case ChessFigure.N:
							drawfigure(ChessGUI.Properties.Resources.w_n, i, j, sz, e);
							break;
						case ChessFigure.P:
							drawfigure(ChessGUI.Properties.Resources.w_p, i, j, sz, e);
							break;
						case ChessFigure.R:
							drawfigure(ChessGUI.Properties.Resources.w_r, i, j, sz, e);
							break;
					}
                }
            }
            /*drawfigure(Properties.Resources.w_p, 3, 3, sz, e);
            drawfigure(Properties.Resources.w_p, 3, 4, sz, e);
            drawfigure(Properties.Resources.w_p, 3, 5, sz, e);

            drawfigure(Properties.Resources.b_p, 4, 3, sz, e);
            drawfigure(Properties.Resources.b_p, 4, 4, sz, e);
            drawfigure(Properties.Resources.b_p, 4, 5, sz, e);*/
        }

        private void ChessForm_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void ChessForm_Paint(object sender, PaintEventArgs e)
        {
            drawchess(e);
        }

        private void ChessForm_Resize(object sender, EventArgs e)
        {
			const int height_offset = 39;
            int sz = Math.Min(Width, Height - height_offset);
            Width = sz;
            Height = sz + height_offset;
        }

        private void ChessForm_FormClosed(object sender, FormClosedEventArgs e)
        {
			//userData.StopGame();
            Visible = false;
        }

        private void ChessForm_Resize_1(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void ChessForm_FormClosed(object sender, FormClosingEventArgs e)
        {
            Visible = false;
        }
    }
}
