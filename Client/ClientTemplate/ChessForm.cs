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
    public partial class ChessForm : Form
    {
        ChatRoom chatform;
        int space = 5;
        
        public ChessForm()
        {
            InitializeComponent();
        }

        public void setchat(ChatRoom chat)
        {
            this.chatform = chat;
        }

        public void drawfigure(Image img, int x, int y, int sz, PaintEventArgs e)
        {
            e.Graphics.DrawImage(img, space + x * sz, space + y * sz, sz, sz);
        }

        public void drawchess(PaintEventArgs e)
        {
            int sz = (Math.Min(Height, Width) - space * 2) / 9;
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
                }
            }

            drawfigure(Properties.Resources.w_p, 3, 3, sz, e);
            drawfigure(Properties.Resources.w_p, 3, 4, sz, e);
            drawfigure(Properties.Resources.w_p, 3, 5, sz, e);

            drawfigure(Properties.Resources.b_p, 4, 3, sz, e);
            drawfigure(Properties.Resources.b_p, 4, 4, sz, e);
            drawfigure(Properties.Resources.b_p, 4, 5, sz, e);
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
            int sz = Math.Min(Width, Height);
            Width = sz;
            Height = sz;
        }

        private void ChessForm_FormClosed(object sender, FormClosedEventArgs e)
        {
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
