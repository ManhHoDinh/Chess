using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace Chess
{

    public class Resource
    {
        public static Image IMG_ROOK_WHITE = Image.FromFile(Application.StartupPath + @"\images\rook_white.png");
        public static Image IMG_ROOK_BLACK = Image.FromFile(Application.StartupPath + @"\images\rook_black.png");
        public static Image IMG_BISHOP_WHITE = Image.FromFile(Application.StartupPath + @"\images\bishop_white.png");
        public static Image IMG_BISHOP_BLACK = Image.FromFile(Application.StartupPath + @"\images\bishop_black.png");
        public static Image IMG_KING_WHITE = Image.FromFile(Application.StartupPath + @"\images\king_white.png");
        public static Image IMG_KING_BLACK = Image.FromFile(Application.StartupPath + @"\images\king_black.png");
        public static Image IMG_KNIGHT_WHITE = Image.FromFile(Application.StartupPath + @"\images\knight_white.png");
        public static Image IMG_KNIGHT_BLACK = Image.FromFile(Application.StartupPath + @"\images\knight_black.png");
        public static Image IMG_PAWN_WHITE = Image.FromFile(Application.StartupPath + @"\images\pawn_white.png");
        public static Image IMG_PAWN_BLACK = Image.FromFile(Application.StartupPath + @"\images\pawn_black.png");
        public static Image IMG_QUEEN_WHITE = Image.FromFile(Application.StartupPath + @"\images\queen_white.png");
        public static Image IMG_QUEEN_BLACK = Image.FromFile(Application.StartupPath + @"\images\queen_black.png");
    };

    internal class Piece
    { 
        private Square _square;
        public Square Square
        {
            get { return _square; }
            set { _square = value;
                _square.Image = value.Image;
            }
        }
        protected Image _image;
        public Image Image => _image;
        public enum ColorPiece { white, black}; 
        private ColorPiece _color;
        public ColorPiece Color=>_color;

        protected Square[,] _squares;

        public Piece(ref Square square, ColorPiece color, ref Square[,] squares)
        {
            _square = square;
            _color = color;
            square.Piece = this;
            _squares=  squares;
        }
        protected static int k = 0;
        protected static Square[] _activeSquares = new Square[64];
        private static Square[] _acSquares;
        public static Square[] ActiveSquares
        {
            get { return _acSquares; }
            set {
                _acSquares = new Square[Piece.k];
                for (int i = 0; i < _acSquares.Length; i++)
                {
                    _acSquares[i] = _activeSquares[i];
                }
            }
        }

        protected int IndexRow, IndexCol;
        public virtual void Ways()
        {
            _acSquares = null;
            k = 0;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (Square == _squares[i, j])
                    {
                        IndexCol = j;
                        IndexRow = i;
                    }
        }
    }
    internal class ROOK : Piece
    {
        public ROOK(ref Square sq, ColorPiece color, ref Square[,] squares) : base(ref sq, color,ref squares)
        {
            if (color == ColorPiece.white)
                _image = Resource.IMG_ROOK_WHITE;
            else
                _image = Resource.IMG_ROOK_BLACK;
            sq.Image = _image;
        }

        public override void Ways()
        {
            base.Ways();
            //[i, fix]
            for (int i = IndexRow + 1; i < 8; i++)
            {
                if (_squares[i, IndexCol].Piece != null)
                {
                    if (_squares[i, IndexCol].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[i, IndexCol];
                        
                        _squares[i, IndexCol].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[i, IndexCol];
                
                _squares[i, IndexCol].BackColor = System.Drawing.Color.Blue;
            }
            //[i,fix]
            for (int i = IndexRow - 1; i >= 0; i--)
            {
                if (_squares[i, IndexCol].Piece != null)
                {
                    if (_squares[i, IndexCol].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[i, IndexCol];
                        
                        _squares[i, IndexCol].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[i, IndexCol];
                
                _squares[i, IndexCol].BackColor = System.Drawing.Color.Blue;
            }
            for (int i = IndexCol + 1; i < 8; i++)
            {
                if (_squares[IndexRow, i].Piece != null)
                {
                    if (_squares[IndexRow, i].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[IndexRow, i];
                        
                        _squares[IndexRow, i].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }

                _activeSquares[k++] = _squares[IndexRow, i];
                
                _squares[IndexRow, i].BackColor = System.Drawing.Color.Blue;
            }
            for (int i = IndexCol - 1; i >= 0; i--)
            {
                if (_squares[IndexRow, i].Piece != null)
                {
                    if (_squares[IndexRow, i].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[IndexRow, i];
                        
                        _squares[IndexRow, i].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[IndexRow, i];
                
                _squares[IndexRow, i].BackColor = System.Drawing.Color.Blue;
            }
            ActiveSquares = _activeSquares;
        }
    }

    internal class BISHOP : Piece
    {
        public BISHOP(ref Square sq, ColorPiece color, ref Square[,] squares) : base(ref sq, color, ref squares)
        {
            if (color == ColorPiece.white)
                _image = Resource.IMG_BISHOP_WHITE;
            else
                _image = Resource.IMG_BISHOP_BLACK;
            sq.Image = _image;
        }
        public override void Ways()
        {
            base.Ways();
            //[i, fix]
            int p = IndexRow + 1, t = IndexCol + 1;
            while (p < 8 && t < 8)
            {
                if (_squares[p, t].Piece != null)
                {
                    if (_squares[p, t].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[p, t];
                        
                        _squares[p, t].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[p, t];
                
                _squares[p, t].BackColor = System.Drawing.Color.Blue;
                p++; t++;
            }

            p = IndexRow - 1; t = IndexCol - 1;
            while (p >= 0 && t >= 0)
            {
                if (_squares[p, t].Piece != null)
                {
                    if (_squares[p, t].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[p, t];
                        
                        _squares[p, t].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[p, t];
                
                _squares[p, t].BackColor = System.Drawing.Color.Blue;
                p--; t--;
            }

            p = IndexRow - 1; t = IndexCol + 1;
            while (p >= 0 && t < 8)
            {
                if (_squares[p, t].Piece != null)
                {
                    if (_squares[p, t].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[p, t];
                        
                        _squares[p, t].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[p, t];
                
                _squares[p, t].BackColor = System.Drawing.Color.Blue;
                p--; t++;
            }
            p = IndexRow + 1; t = IndexCol - 1;
            while (p < 8 && t >= 0)
            {
                if (_squares[p, t].Piece != null)
                {
                    if (_squares[p, t].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[p, t];
                        
                        _squares[p, t].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[p, t];
                
                _squares[p, t].BackColor = System.Drawing.Color.Blue;
                p++; t--;
            }
            ActiveSquares = _activeSquares;
        }
    }
        internal class KING : Piece
    {
        public KING(ref Square sq, ColorPiece color, ref Square[,] squares) : base(ref sq, color, ref squares)
        {
            if (color == ColorPiece.white)
                _image = Resource.IMG_KING_WHITE;
            else
                _image = Resource.IMG_KING_BLACK;
            sq.Image = _image;
        }
    }
    internal class KNIGHT : Piece
    {
        public KNIGHT(ref Square sq, ColorPiece color, ref Square[,] squares) : base(ref sq, color, ref squares)
        {
            if (color == ColorPiece.white)
                _image = Resource.IMG_KNIGHT_WHITE;
            else
                _image = Resource.IMG_KNIGHT_BLACK;
            sq.Image = _image;
        }

        public override void Ways()
        {
            base.Ways();
            ///1

            if (IndexRow < 6 && IndexCol < 7)
            {
                if (_squares[IndexRow + 2, IndexCol + 1].Piece == null)
                {
                    _squares[IndexRow + 2, IndexCol + 1].BackColor = System.Drawing.Color.Blue;
                    _activeSquares[k++] = _squares[IndexRow + 2, IndexCol + 1];
                }
                else if (_squares[IndexRow + 2, IndexCol + 1].Piece.Color != this.Color)
                {
                    _activeSquares[k++] = _squares[IndexRow + 2, IndexCol + 1];

                    _squares[IndexRow + 2, IndexCol + 1].BackColor = System.Drawing.Color.Red;
                }
            }
            //2
            if (IndexRow < 6 && IndexCol > 0)
            {
                if (_squares[IndexRow + 2, IndexCol - 1].Piece == null)
                {
                    _squares[IndexRow + 2, IndexCol - 1].BackColor = System.Drawing.Color.Blue;
                    _activeSquares[k++] = _squares[IndexRow + 2, IndexCol - 1];
                }
                else if (_squares[IndexRow + 2, IndexCol - 1].Piece.Color != this.Color)
                {
                    _activeSquares[k++] = _squares[IndexRow + 2, IndexCol - 1];

                    _squares[IndexRow + 2, IndexCol - 1].BackColor = System.Drawing.Color.Red;
                }
            }
            ///3
            if (IndexRow < 7 && IndexCol < 6)
            {
                if (_squares[IndexRow + 1, IndexCol + 2].Piece == null)
                {
                    _squares[IndexRow + 1, IndexCol + 2].BackColor = System.Drawing.Color.Blue;
                    _activeSquares[k++] = _squares[IndexRow + 1, IndexCol + 2];
                }
                else if (_squares[IndexRow + 1, IndexCol + 2].Piece.Color != this.Color)
                {
                    _activeSquares[k++] = _squares[IndexRow + 1, IndexCol + 2];

                    _squares[IndexRow + 1, IndexCol + 2].BackColor = System.Drawing.Color.Red;
                }
            }
            //4
            if (IndexRow < 6 && IndexCol > 1)
            {
                if (_squares[IndexRow + 1, IndexCol - 2].Piece == null)
                {
                    _squares[IndexRow + 1, IndexCol - 2].BackColor = System.Drawing.Color.Blue;
                    _activeSquares[k++] = _squares[IndexRow + 1, IndexCol - 2];
                }
                else if (_squares[IndexRow + 1, IndexCol - 2].Piece.Color != this.Color)
                {
                    _activeSquares[k++] = _squares[IndexRow + 1, IndexCol - 2];

                    _squares[IndexRow + 1, IndexCol - 2].BackColor = System.Drawing.Color.Red;
                }
            }///5
            if (IndexRow > 0 && IndexCol < 6)
            {
                if (_squares[IndexRow - 1, IndexCol + 2].Piece == null)
                {
                    _squares[IndexRow - 1, IndexCol + 2].BackColor = System.Drawing.Color.Blue;
                    _activeSquares[k++] = _squares[IndexRow - 1, IndexCol + 2];
                }
                else if (_squares[IndexRow - 1, IndexCol + 2].Piece.Color != this.Color)
                {
                    _activeSquares[k++] = _squares[IndexRow - 1, IndexCol + 2];

                    _squares[IndexRow - 1, IndexCol + 2].BackColor = System.Drawing.Color.Red;
                }
            }
            //6
            if (IndexRow > 0 && IndexCol > 1)
            {
                if (_squares[IndexRow - 1, IndexCol - 2].Piece == null)
                {
                    _squares[IndexRow - 1, IndexCol - 2].BackColor = System.Drawing.Color.Blue;
                    _activeSquares[k++] = _squares[IndexRow - 1, IndexCol - 2];
                }
                else if (_squares[IndexRow - 1, IndexCol - 2].Piece.Color != this.Color)
                {
                    _activeSquares[k++] = _squares[IndexRow - 1, IndexCol - 2];

                    _squares[IndexRow - 1, IndexCol - 2].BackColor = System.Drawing.Color.Red;
                }
            }///7
            if (IndexRow > 1 && IndexCol > 0)
            {
                if (_squares[IndexRow - 2, IndexCol - 1].Piece == null)
                {
                    _squares[IndexRow - 2, IndexCol - 1].BackColor = System.Drawing.Color.Blue;
                    _activeSquares[k++] = _squares[IndexRow - 2, IndexCol - 1];
                }
                else if (_squares[IndexRow - 2, IndexCol - 1].Piece.Color != this.Color)
                {
                    _activeSquares[k++] = _squares[IndexRow - 2, IndexCol - 1];

                    _squares[IndexRow - 2, IndexCol - 1].BackColor = System.Drawing.Color.Red;
                }
            }
            //8
            if (IndexRow > 1 && IndexCol < 7)
            {
                if (_squares[IndexRow - 2, IndexCol + 1].Piece == null)
                {
                    _squares[IndexRow - 2, IndexCol + 1].BackColor = System.Drawing.Color.Blue;
                    _activeSquares[k++] = _squares[IndexRow - 2, IndexCol + 1];
                }
                else if (_squares[IndexRow - 2, IndexCol + 1].Piece.Color != this.Color)
                {
                    _activeSquares[k++] = _squares[IndexRow - 2, IndexCol + 1];

                    _squares[IndexRow - 2, IndexCol + 1].BackColor = System.Drawing.Color.Red;
                }
            }
            ActiveSquares = _activeSquares;
        }
    }
        internal class PAWN : Piece
    {
        public PAWN(ref Square sq, ColorPiece color, ref Square[,] squares) : base(ref sq, color, ref squares)
        {
            if (color == ColorPiece.white)
                _image = Resource.IMG_PAWN_WHITE;
            else
                _image = Resource.IMG_PAWN_BLACK;
            sq.Image = _image;
        }
 
        public override void Ways()
        {
            base.Ways();
            if (Color == Piece.ColorPiece.white)
            {
                if (IndexRow == 1&&_squares[2,IndexCol].Piece == null)
                    if (_squares[3,IndexCol].Piece == null)
                    {
                        _squares[3, IndexCol].BackColor = System.Drawing.Color.Blue;
                        _activeSquares[k++] = _squares[3, IndexCol];
                    }
                if (IndexRow <7 && _squares[IndexRow+1, IndexCol].Piece == null)
                {
                    _squares[IndexRow+1, IndexCol].BackColor = System.Drawing.Color.Blue;
                    _activeSquares[k++] = _squares[IndexRow+1, IndexCol];
                }
                if (IndexRow < 7 &&IndexCol<7&& _squares[IndexRow + 1, IndexCol + 1].Piece != null && _squares[IndexRow + 1, IndexCol + 1].Piece.Color == ColorPiece.black)
                {
                    _squares[IndexRow + 1, IndexCol + 1].BackColor = System.Drawing.Color.Red;
                    _activeSquares[k++] = _squares[IndexRow + 1, IndexCol + 1];
                }

                if (IndexRow < 7 && IndexCol > 0 && _squares[IndexRow + 1, IndexCol - 1].Piece != null && _squares[IndexRow + 1, IndexCol - 1].Piece.Color == ColorPiece.black)
                {
                    _squares[IndexRow + 1, IndexCol - 1].BackColor = System.Drawing.Color.Red;
                    _activeSquares[k++] = _squares[IndexRow + 1, IndexCol -1];
                }
            }
            else
            {

                if (IndexRow == 6 && _squares[5, IndexCol].Piece == null)
                    if (_squares[4, IndexCol].Piece == null)
                    {
                        _squares[4, IndexCol].BackColor = System.Drawing.Color.Blue;
                        _activeSquares[k++] = _squares[4, IndexCol];
                    }
                if (IndexRow > 0 && _squares[IndexRow - 1, IndexCol].Piece == null)
                {
                    _squares[IndexRow - 1, IndexCol].BackColor = System.Drawing.Color.Blue;
                    _activeSquares[k++] = _squares[IndexRow - 1, IndexCol];
                }
                if (IndexRow >0 && IndexCol < 7 && _squares[IndexRow - 1, IndexCol + 1].Piece != null && _squares[IndexRow - 1, IndexCol + 1].Piece.Color == ColorPiece.white)
                {
                    _squares[IndexRow - 1, IndexCol + 1].BackColor = System.Drawing.Color.Red;
                    _activeSquares[k++] = _squares[IndexRow - 1, IndexCol+1];
                }

                if (IndexRow > 0 && IndexCol > 0&&_squares[IndexRow - 1, IndexCol - 1].Piece != null && _squares[IndexRow - 1, IndexCol - 1].Piece.Color == ColorPiece.white)
                {
                    _squares[IndexRow - 1, IndexCol - 1].BackColor = System.Drawing.Color.Red;
                    _activeSquares[k++] = _squares[IndexRow - 1, IndexCol -1];
                }
            }
            
            ///[i, fix]
            /*
             int p = IndexRow+ 1, t = IndexCol + 1;
            while(p < 8 && t < 8)
            {
                if (_squares[p, t].Piece != null)
                {
                    if (_squares[p, t].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[p, t];
                        
                        _squares[p, t].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[p, t];
                
                _squares[p,t].BackColor = System.Drawing.Color.Blue;
                p++; t++;
            }

            p = IndexRow - 1; t = IndexCol - 1;
            while (p >= 0 && t >= 0)
            {
                if (_squares[p, t].Piece != null)
                {
                    if (_squares[p, t].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[p, t];
                        
                        _squares[p, t].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[p, t];
                
                _squares[p, t].BackColor = System.Drawing.Color.Blue;
                p--; t--;
            }

            p = IndexRow - 1; t = IndexCol + 1;
            while (p >= 0 && t < 8)
            {
                if (_squares[p, t].Piece != null)
                {
                    if (_squares[p, t].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[p, t];
                        
                        _squares[p, t].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[p, t];
                
                _squares[p, t].BackColor = System.Drawing.Color.Blue;
                p--; t++;
            }
            p = IndexRow + 1; t = IndexCol - 1;
            while (p < 8 && t >= 0)
            {
                if (_squares[p, t].Piece != null)
                {
                    if (_squares[p, t].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[p, t];
                        
                        _squares[p, t].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[p, t];
                
                _squares[p, t].BackColor = System.Drawing.Color.Blue;
                p++; t--;
            }
            for (int i = IndexRow + 1; i < 8; i++)
            {
                if (_squares[i, IndexCol].Piece != null)
                {
                    if (_squares[i, IndexCol].Piece.Color != this.Color)
                    {
                         _activeSquares[k++]=_squares[i, IndexCol];
                        
                        _squares[i, IndexCol].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                 _activeSquares[k++]=_squares[i, IndexCol];
                
                _squares[i, IndexCol].BackColor = System.Drawing.Color.Blue;
            }
            //[i,fix]
            for (int i = IndexRow - 1; i >= 0; i--)
            {
                if (_squares[i, IndexCol].Piece != null)
                {
                    if (_squares[i, IndexCol].Piece.Color != this.Color)
                    {
                         _activeSquares[k++]=_squares[i, IndexCol];
                        
                        _squares[i, IndexCol].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                 _activeSquares[k++]=_squares[i, IndexCol];
                
                _squares[i, IndexCol].BackColor = System.Drawing.Color.Blue;
            }
            for (int i = IndexCol + 1; i < 8; i++)
            {
                if (_squares[IndexRow, i].Piece != null)
                {
                    if (_squares[IndexRow, i].Piece.Color != this.Color)
                    {
                         _activeSquares[k++]=_squares[IndexRow, i];
                        
                        _squares[IndexRow, i].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }

                _activeSquares[k++] = _squares[IndexRow, i];
                
                _squares[IndexRow, i].BackColor = System.Drawing.Color.Blue;
            }
            for (int i = IndexCol - 1; i >= 0; i--)
            {
                if (_squares[IndexRow, i].Piece != null)
                {
                    if (_squares[IndexRow, i].Piece.Color != this.Color)
                    {
                         _activeSquares[k++]=_squares[IndexRow, i];
                        
                        _squares[IndexRow, i].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                 _activeSquares[k++]=_squares[IndexRow, i];
                
                _squares[IndexRow, i].BackColor = System.Drawing.Color.Blue;
            }*/
            ActiveSquares = _activeSquares;
        }
        
    }
    internal class QUEEN : Piece
    {
        public QUEEN(ref Square sq, ColorPiece color, ref Square[,] squares) : base(ref sq, color, ref squares)
        {
            if (color == ColorPiece.white)
                _image = Resource.IMG_QUEEN_WHITE;
            else
                _image = Resource.IMG_QUEEN_BLACK;
            sq.Image = _image;

        }
        public override void Ways()
        {
            base.Ways();
            //[i, fix]
            int p = IndexRow + 1, t = IndexCol + 1;
            while (p < 8 && t < 8)
            {
                if (_squares[p, t].Piece != null)
                {
                    if (_squares[p, t].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[p, t];
                        
                        _squares[p, t].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[p, t];
                
                _squares[p, t].BackColor = System.Drawing.Color.Blue;
                p++; t++;
            }

            p = IndexRow - 1; t = IndexCol - 1;
            while (p >= 0 && t >= 0)
            {
                if (_squares[p, t].Piece != null)
                {
                    if (_squares[p, t].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[p, t];
                        
                        _squares[p, t].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[p, t];
                
                _squares[p, t].BackColor = System.Drawing.Color.Blue;
                p--; t--;
            }

            p = IndexRow - 1; t = IndexCol + 1;
            while (p >= 0 && t < 8)
            {
                if (_squares[p, t].Piece != null)
                {
                    if (_squares[p, t].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[p, t];
                        
                        _squares[p, t].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[p, t];
                
                _squares[p, t].BackColor = System.Drawing.Color.Blue;
                p--; t++;
            }
            p = IndexRow + 1; t = IndexCol - 1;
            while (p < 8 && t >= 0)
            {
                if (_squares[p, t].Piece != null)
                {
                    if (_squares[p, t].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[p, t];
                        
                        _squares[p, t].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[p, t];
                
                _squares[p, t].BackColor = System.Drawing.Color.Blue;
                p++; t--;
            }
            for (int i = IndexRow + 1; i < 8; i++)
            {
                if (_squares[i, IndexCol].Piece != null)
                {
                    if (_squares[i, IndexCol].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[i, IndexCol];
                        
                        _squares[i, IndexCol].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[i, IndexCol];
                
                _squares[i, IndexCol].BackColor = System.Drawing.Color.Blue;
            }
            //[i,fix]
            for (int i = IndexRow - 1; i >= 0; i--)
            {
                if (_squares[i, IndexCol].Piece != null)
                {
                    if (_squares[i, IndexCol].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[i, IndexCol];
                        
                        _squares[i, IndexCol].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[i, IndexCol];
                
                _squares[i, IndexCol].BackColor = System.Drawing.Color.Blue;
            }
            for (int i = IndexCol + 1; i < 8; i++)
            {
                if (_squares[IndexRow, i].Piece != null)
                {
                    if (_squares[IndexRow, i].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[IndexRow, i];
                        
                        _squares[IndexRow, i].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }

                _activeSquares[k++] = _squares[IndexRow, i];
                
                _squares[IndexRow, i].BackColor = System.Drawing.Color.Blue;
            }
            for (int i = IndexCol - 1; i >= 0; i--)
            {
                if (_squares[IndexRow, i].Piece != null)
                {
                    if (_squares[IndexRow, i].Piece.Color != this.Color)
                    {
                        _activeSquares[k++] = _squares[IndexRow, i];
                        
                        _squares[IndexRow, i].BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                _activeSquares[k++] = _squares[IndexRow, i];
                
                _squares[IndexRow, i].BackColor = System.Drawing.Color.Blue;
            }
            ActiveSquares = _activeSquares;
        }

    }
}
