﻿using System;
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
    internal class Broad
    {
        public Square[,] squares;
        private Form parentForm;
        private int squareWidth;

        public Broad(Form parent,  int DefaultSquareWidth = 64)
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
                    SquareSelect.Color = SquareSelect.Color;
                sq.BackColor = Color.Yellow;
                SquareSelect = sq;
                
                ///Remove click nhung quan co khac mau khac
                 ///Add move squareSelect
                 ///
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        bool IsTeam;
                        //Try catch trach o trong : xoa cac o trong click
                        try
                        {
                            IsTeam = sq.Piece.Color == squares[i, j].Piece.Color;
                        }
                        catch
                        {
                            IsTeam=false;
                        }
                            squares[i, j].Click += new EventHandler(MovePiece);
                            squares[i, j].Click += new EventHandler(ClickSquare);
                    }
                
            }
        }
        void MovePiece(object sender, EventArgs args)
        {
            Square sq = sender as Square;
            if (sq.Piece == null || sq.Piece.Color != SquareSelect.Piece.Color)
            {
                //Set lai bg cu
                SquareSelect.Color = SquareSelect.Color;
                //Piece in new square
                sq.Piece = SquareSelect.Piece;
               //Square of piece
                sq.Piece.Square = sq;
                //New Square Image:
                sq.Image = SquareSelect.Image;
                //Remove Piece Before Square
                SquareSelect.Piece= null;
                SquareSelect.Image = null;
                //Move thanh cong them lai cac click cho cac o khac
                //Xoa move vi Squareselect.piece = null;
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        //Them clicksquare lai cho cac squares

                        squares[i, j].Click += new EventHandler(ClickSquare);
                        squares[i, j].Click -= new EventHandler(MovePiece);
                    }
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
                    squares[i, j].Click+=new EventHandler(ClickSquare);
                }
                top += squareWidth;
                if (c == Square.SquareColor.white)
                    c = Square.SquareColor.black;
                else
                    c = Square.SquareColor.white;
            }
            
            //White ROOK
            Piece White_Rook1 = new ROOK(ref squares[0, 0], Piece.ColorPiece.white,this);
            Piece White_Rook2 = new ROOK(ref squares[0, 7], Piece.ColorPiece.white, this);
            //White KNIGHT
            Piece White_Kngiht1 = new KNIGHT(ref squares[0, 1], Piece.ColorPiece.white, this);
            Piece White_Knight2 = new KNIGHT(ref squares[0, 6], Piece.ColorPiece.white, this);
            //White KNIGHT
            Piece White_Bishop1 = new BISHOP(ref squares[0, 2], Piece.ColorPiece.white, this);
            Piece White_Bishop2 = new BISHOP(ref squares[0, 5], Piece.ColorPiece.white, this);
            //White Queen
            Piece White_Queen = new QUEEN(ref squares[0, 3], Piece.ColorPiece.white, this);
            //White King
            Piece White_King = new KING(ref squares[0, 4], Piece.ColorPiece.white, this);
            //White Pawn
            Piece[] White_Pawn = new PAWN[8];
            for (int i = 0; i < 8; i++)
                White_Pawn[i] = new PAWN(ref squares[1,i], Piece.ColorPiece.white, this);
            
            //Black ROOK
            Piece Black_Rook1 = new ROOK(ref squares[7, 0], Piece.ColorPiece.black, this);
            Piece Black_Rook2 = new ROOK(ref squares[7, 7], Piece.ColorPiece.black, this);
            //Black KNIGHT
            Piece Black_Kngiht1 = new KNIGHT(ref squares[7, 1], Piece.ColorPiece.black, this);
            Piece Black_Knight2 = new KNIGHT(ref squares[7, 6], Piece.ColorPiece.black, this);
            //Black KNIGHT
            Piece Black_Bishop1 = new BISHOP(ref squares[7, 2], Piece.ColorPiece.black, this);
            Piece Black_Bishop2 = new BISHOP(ref squares[7, 5], Piece.ColorPiece.black, this);
            //Black Queen
            Piece Black_Queen = new QUEEN(ref squares[7, 4], Piece.ColorPiece.black, this);
            //Black King
            Piece Black_King = new KING(ref squares[7, 3], Piece.ColorPiece.black, this);
            //Black Pawn
            Piece[] Black_Pawn = new PAWN[8];
            for (int i = 0; i < 8; i++)
                Black_Pawn[i] = new PAWN(ref squares[6, i], Piece.ColorPiece.black, this);

        }
    }
}