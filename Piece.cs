﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

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
        private Broad _broad;
        public Square Square
        {
            get { return _square; }
            set { _square = value;
                _square.Image = value.Image;
            }
        }
        public Broad Broad { get; set; }
        protected Image _image;
        public Image Image => _image;
        public enum ColorPiece { white, black}; 
        private ColorPiece _color;
        public ColorPiece Color=>_color;

        Square[,] squares;
        public Piece(ref Square square, ColorPiece color, Broad broad)
        {
            _square = square;
            _color = color;
            square.Piece = this;
            _broad = broad;
            squares= _broad.squares;
        }
        public Stack<Square> stack;
        public virtual void Ways()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                { }

        }
    }
    internal class ROOK : Piece
    {
        public ROOK(ref Square sq, ColorPiece color,Broad broad) : base(ref sq, color,broad)
        {
            if (color == ColorPiece.white)
                _image = Resource.IMG_ROOK_WHITE;
            else
                _image = Resource.IMG_ROOK_BLACK;
            sq.Image = _image;
        }
        public override void Ways()
        { 
        }
    }

    internal class BISHOP : Piece
    {
        public BISHOP(ref Square sq, ColorPiece color, Broad broad) : base(ref sq, color, broad)
        {
            if (color == ColorPiece.white)
                _image = Resource.IMG_BISHOP_WHITE;
            else
                _image = Resource.IMG_BISHOP_BLACK;
            sq.Image = _image;
        }
    }
    internal class KING : Piece
    {
        public KING(ref Square sq, ColorPiece color, Broad broad) : base(ref sq, color, broad)
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
        public KNIGHT(ref Square sq, ColorPiece color, Broad broad) : base(ref sq, color, broad)
        {
            if (color == ColorPiece.white)
                _image = Resource.IMG_KNIGHT_WHITE;
            else
                _image = Resource.IMG_KNIGHT_BLACK;
            sq.Image = _image;
        }
    }
    internal class PAWN : Piece
    {
        public PAWN(ref Square sq, ColorPiece color, Broad broad) : base(ref sq, color, broad)
        {
            if (color == ColorPiece.white)
                _image = Resource.IMG_PAWN_WHITE;
            else
                _image = Resource.IMG_PAWN_BLACK;
            sq.Image = _image;
        }
    }
    internal class QUEEN : Piece
    {
        public QUEEN(ref Square sq, ColorPiece color, Broad broad) : base(ref sq, color, broad)
        {
            if (color == ColorPiece.white)
                _image = Resource.IMG_QUEEN_WHITE;
            else
                _image = Resource.IMG_QUEEN_BLACK;
            sq.Image = _image;

        }
    }
}