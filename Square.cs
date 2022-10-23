using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    //Square

    internal class Square : PictureBox
    {
        private Piece _piece = null;
        public Piece Piece
        {
            get { return _piece; }
            set { 
                _piece = value;
            }
        }
        public enum SquareColor { white, black};
        private SquareColor _color;
        public SquareColor Color
        {
            get { return _color; }
            set
            {
                _color = value;
                if (_color == SquareColor.white)
                    BackColor = System.Drawing.Color.White;
                else
                    BackColor = System.Drawing.Color.Gray;
            }
        }

    }

}
