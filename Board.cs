using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Chess
{
    internal class Board
    {
        public Square[,] squares;
        private Form parentForm;
        private int squareWidth;

        public Board(Form parent,  int DefaultSquareWidth = 64)
        {
            squares = new Square[8, 8];
            parentForm = parent;
            squareWidth = DefaultSquareWidth;
        }
        //Click Square
        static Square SquareSelect = null;
        void ClickSquare(object sender, EventArgs e)
        {
            Square sq = sender as Square;
            //Chi chon dc nhung Piece : 
            if (sq.Piece != null )
            {
                //First time
                if (SquareSelect!= null)
                {
                    SquareSelect.Color = SquareSelect.Color;
                    try
                    {
                        foreach (Square square in Piece.ActiveSquares)
                        {
                            square.Click -= new EventHandler(MovePiece);
                            square.Color = square.Color;
                        }
                    }
                    catch { }
                }
                sq.BackColor = Color.Yellow;
                SquareSelect = sq;

                ///Remove click nhung quan co khac mau khac
                ///Add move squareSelect
                ///
                sq.Piece.Ways();
                try
                {
                    foreach (Square square in Piece.ActiveSquares)
                    {
                        square.Click -= new EventHandler(ClickSquare);
                        square.Click += new EventHandler(MovePiece);
                    }
                }
                catch { }

            }
        }
        void MovePiece(object sender, EventArgs args)
        {
            Square sq = sender as Square;
            foreach (Square square in Piece.ActiveSquares)
            {
                square.Color = square.Color;
            }
            try
            {
                if (sq.Piece == null || sq.Piece != null && sq.Piece.Color != SquareSelect.Piece.Color)
                {
                    //Set lai bg cu
                    if(sq.Piece != null && sq.Piece is KING)
                    {
                        MessageBox.Show("{0} win", SquareSelect.Piece.Color.ToString());
                        for (int i = 0; i < 8; i++)
                            for (int j = 0; j < 8; j++)
                                MessageBox.Show("Restart to play game");
                    }
                    
                    SquareSelect.Color = SquareSelect.Color;
                    //Piece in new square
                    sq.Piece = SquareSelect.Piece;
                    //Square of piece
                    sq.Piece.Square = sq;
                    //New Square Image:
                    sq.Image = SquareSelect.Image;
                    //Remove Piece Before Square
                    SquareSelect.Piece = null;
                    SquareSelect.Image = null;
                    SquareSelect = null;
                    if (sq.Piece is PAWN)
                        for (int i = 0; i < 8; i++)
                            if (sq == squares[7, i] && sq.Piece.Color == Piece.ColorPiece.white)
                            { 
                                sq.Piece = new QUEEN(ref sq, Piece.ColorPiece.white, ref squares);
                                sq.Image = sq.Piece.Image;
                            }
                            else if (sq == squares[0, i] && sq.Piece.Color == Piece.ColorPiece.black)
                            {
                                sq.Piece = new QUEEN(ref sq, Piece.ColorPiece.black, ref squares);
                                sq.Image = sq.Piece.Image;
                            }
                    //Move thanh cong them lai cac click cho cac o khac
                    //Xoa move vi Squareselect.piece = null;
                    foreach (Square square in Piece.ActiveSquares)
                    {
                        square.Click -= new EventHandler(MovePiece);
                    }

                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                            if (squares[i, j].Piece != null && squares[i, j].Piece.Color != sq.Piece.Color)
                                squares[i, j].Click += new EventHandler(ClickSquare);
                            else if (squares[i, j].Piece != null && squares[i, j].Piece.Color == sq.Piece.Color)
                                squares[i, j].Click -= new EventHandler(ClickSquare);
                    //Sua lai mau

                    foreach (Square square in Piece.ActiveSquares)
                    {
                        square.Color = square.Color;
                    }
                }

            }
            catch(Exception ex) {
                Console.WriteLine(ex);
            }

        }
        //Create Board
        private Square.SquareColor c = Square.SquareColor.white;
        public void init()
        {
            int left;
            int top = 0;
            for (int i = 0; i < 8; i++)
            {
                left = 0;
                for (int j = 0; j < 8; j++)
                {
                    squares[i, j] = new Square();
                    squares[i, j].Location = new System.Drawing.Point(left, top);
                    squares[i, j].Size = new System.Drawing.Size(squareWidth,squareWidth);
                    squares[i,j].Color = c;
                    squares[i, j].SizeMode = PictureBoxSizeMode.Zoom;
                    if (c == Square.SquareColor.white)
                        c = Square.SquareColor.black;
                    else
                        c = Square.SquareColor.white;
                    parentForm.Controls.Add(squares[i, j]);  
                    left += squareWidth;
                 }
                top += squareWidth;
                if (c == Square.SquareColor.white)
                    c = Square.SquareColor.black;
                else
                    c = Square.SquareColor.white;
            }
            
            //White ROOK
            Piece White_Rook1 = new ROOK(ref squares[0, 0], Piece.ColorPiece.white, ref squares);
            Piece White_Rook2 = new ROOK(ref squares[0, 7], Piece.ColorPiece.white, ref squares);
            //White KNIGHT
            Piece White_Kngiht1 = new KNIGHT(ref squares[0, 1], Piece.ColorPiece.white, ref squares);
            Piece White_Knight2 = new KNIGHT(ref squares[0, 6], Piece.ColorPiece.white, ref squares);
            //White KNIGHT
            Piece White_Bishop1 = new BISHOP(ref squares[0, 2], Piece.ColorPiece.white, ref squares);
            Piece White_Bishop2 = new BISHOP(ref squares[0, 5], Piece.ColorPiece.white, ref squares);
            //White Queen
            Piece White_Queen = new QUEEN(ref squares[0, 3], Piece.ColorPiece.white, ref squares);
            //White King
            Piece White_King = new KING(ref squares[0, 4], Piece.ColorPiece.white, ref squares);
            //White Pawn
            Piece[] White_Pawn = new PAWN[8];
            for (int i = 0; i < 8; i++)
                White_Pawn[i] = new PAWN(ref squares[1,i], Piece.ColorPiece.white, ref squares);
            
            //Black ROOK
            Piece Black_Rook1 = new ROOK(ref squares[7, 0], Piece.ColorPiece.black, ref squares);
            Piece Black_Rook2 = new ROOK(ref squares[7, 7], Piece.ColorPiece.black, ref squares);
            //Black KNIGHT
            Piece Black_Kngiht1 = new KNIGHT(ref squares[7, 1], Piece.ColorPiece.black, ref squares);
            Piece Black_Knight2 = new KNIGHT(ref squares[7, 6], Piece.ColorPiece.black, ref squares);
            //Black KNIGHT
            Piece Black_Bishop1 = new BISHOP(ref squares[7, 2], Piece.ColorPiece.black, ref squares);
            Piece Black_Bishop2 = new BISHOP(ref squares[7, 5], Piece.ColorPiece.black, ref squares);
            //Black Queen
            Piece Black_Queen = new QUEEN(ref squares[7, 4], Piece.ColorPiece.black, ref squares);
            //Black King
            Piece Black_King = new KING(ref squares[7, 3], Piece.ColorPiece.black, ref squares);
            //Black Pawn
            Piece[] Black_Pawn = new PAWN[8];
            for (int i = 0; i < 8; i++)
                Black_Pawn[i] = new PAWN(ref squares[6, i], Piece.ColorPiece.black, ref squares);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (squares[i,j].Piece!= null )
                        squares[i, j].Click += new EventHandler(ClickSquare);
        }
    }
}
