using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sah
{
    public class Board
    {
        private const int BoardSize = 10;
        private const int CellSize = 50;


        private readonly Form form;
        private Dictionary<string, Color> pieceColors;
        private Dictionary<string, Point[]> piecePositions;
        private readonly Dictionary<string, Image> pieceImages;
        private string selectedPiece;
        private Point selectedPosition;
        private bool isPieceSelected;
        private string currentPlayer;
        private bool canMovePlayer1;
        private bool canMovePlayer2;
        private bool canMovePlayer3;
        private bool canMovePlayer4;


        public Board(Form form)
        {
            this.form = form;


            pieceColors = new Dictionary<string, Color>

        {

            {"Player1", Color.Purple },
            {"Player2", Color.Red },
            {"Player3", Color.Green },
            {"Player4", Color.Blue }


        };

            pieceImages = new Dictionary<string, Image>
        {
                { "PawnBlack",  Resource1.pion },
                { "RookBlack",  Resource1.tura },
                { "BishopBlack",  Resource1.nebun },
                { "KnightBlack",  Resource1.cal },
                { "KingBlack",  Resource1.rege },
                { "QueenBlack",  Resource1.regina },


                {"PawnGreen", Resource1.pionVerde },
                {"RookGreen", Resource1.turaVerde },
                {"BishopGreen", Resource1.nebunVerde },
                {"KnightGreen", Resource1.calVerde },
                {"KingGreen", Resource1.regeVerde },
                {"QueenGreen", Resource1.reginaVerde },


                {"PawnRed", Resource1.pionRosu },
                {"RookRed", Resource1.turaRosu },
                {"BishopRed", Resource1.nebunRosu },
                {"KnightRed", Resource1.calRosu },
                {"KingRed", Resource1.regeRosu },
                {"QueenRed", Resource1.reginaRosu },


                {"PawnYellow", Resource1.pionGalben },
                {"RookYellow", Resource1.turaGalben },
                {"BishopYellow", Resource1.nebunGalben },
                {"KnightYellow", Resource1.calGalben },
                {"KingYellow", Resource1.regeGalben },
                {"QueenYellow", Resource1.reginaGalben }



        };

            bool shouldDrawRange = false;

            InitializePiecePositions();

            form.Paint += DrawChessboard;
            form.Paint += DrawPieces;
            form.MouseClick += HandleMouseClick;

            selectedPiece = string.Empty;
            selectedPosition = Point.Empty;
            isPieceSelected = false;
            currentPlayer = "Player 1";
            canMovePlayer1 = true;
            canMovePlayer2 = true;
            canMovePlayer3 = true;
            canMovePlayer4 = true;
        }

        bool shouldDrawRange = false;
        private void InitializePiecePositions()
        {
            piecePositions = new Dictionary<string, Point[]>
        {
                 { "PawnRed", new Point[]{new Point(3, 0),new Point(3, 1) ,new Point(2, 2),new Point(0, 3) ,new Point(1, 3)}},
                 { "RookRed", new Point[]{new Point(2, 0), new Point(0, 2) } },
                 { "BishopRed", new Point[]{new Point(1, 0), new Point(1, 1) } },
                 { "KnightRed", new Point[]{ new Point(2, 1), new Point(1, 2) } },
                 { "KingRed", new Point[]{new Point(0, 0) } },
                 { "QueenRed", new Point[]{new Point(0, 1) } },


                 { "PawnBlack", new Point[]{new Point(6, 0),new Point(6, 1) ,new Point(7, 2),new Point(9, 3) ,new Point(8, 3)}},
                 { "RookBlack", new Point[]{new Point(7, 0), new Point(9, 2) } },
                 { "BishopBlack", new Point[]{new Point(8, 1), new Point(9, 1) } },
                 { "KnightBlack", new Point[]{ new Point(8, 2), new Point(7, 1) } },
                 { "KingBlack", new Point[]{new Point(9, 0) } },
                 { "QueenBlack", new Point[]{new Point(8, 0) } },


                 { "PawnGreen", new Point[]{new Point(6, 9),new Point(6, 8) ,new Point(7, 7),new Point(9, 6) ,new Point(8, 6)}},
                 { "RookGreen", new Point[]{new Point(9, 7), new Point(7, 9) } },
                 { "BishopGreen", new Point[]{new Point(8, 9), new Point(8, 8) } },
                 { "KnightGreen", new Point[]{ new Point(8, 7), new Point(7, 8) } },
                 { "KingGreen", new Point[]{new Point(9, 9) } },
                 { "QueenGreen", new Point[]{new Point(9, 8) } },


                 { "PawnYellow", new Point[]{new Point(3, 9),new Point(3, 8) ,new Point(2,7),new Point(0, 6) ,new Point(1, 6)}},
                 { "RookYellow", new Point[]{new Point(2, 9), new Point(0, 7) } },
                 { "BishopYellow", new Point[]{new Point(1, 8), new Point(0, 8) } },
                 { "KnightYellow", new Point[]{ new Point(2, 8), new Point(1, 7) } },
                 { "KingYellow", new Point[]{new Point(0, 9) } },
                 { "QueenYellow", new Point[]{new Point(1,9 ) } }

            };
        }

        private void DrawChessboard(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    int x = col * CellSize;
                    int y = row * CellSize;

                    Color cellColor = (row + col) % 2 == 0 ? Color.Gray : Color.Orange;

                    Brush brush = new SolidBrush(cellColor);
                    g.FillRectangle(brush, x, y, CellSize, CellSize);

                }
            }
        }
        List<Point> availableMoves(string pieceName, Point currentPosition)
        {
            List<Point> moves = new List<Point>();
            for (int i = 0; i < Board.BoardSize; i++)
            {
                for (int j = 0; j < Board.BoardSize; j++)
                {
                    Point attempt = new Point(i, j);
                    if (IsValidMove(pieceName, currentPosition, attempt))
                    {
                        moves.Add(attempt);
                    }
                }
            }
            return moves;
        }
        private void DrawRange(List<Point> points, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Color white = Color.Yellow;
            using (SolidBrush whiteBrush = new SolidBrush(white))
                foreach (Point piesa in points)
                {
                    g.FillRectangle(whiteBrush, piesa.X * Board.CellSize, piesa.Y * Board.CellSize, Board.CellSize, Board.CellSize);
                }

        }
        private void DrawPieces(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (KeyValuePair<string, Point[]> kvp in piecePositions)
            {
                string pieceName = kvp.Key;
                Point[] positions = kvp.Value;

                Image pieceImage = pieceImages[pieceName];


                foreach (Point position in positions)
                {
                    int x = position.X * CellSize;
                    int y = position.Y * CellSize;

                    g.DrawImage(pieceImage, x, y, CellSize, CellSize);

                }
            }
            if (shouldDrawRange)
            {
                DrawRange(availableMovesToDraw, e);
            }
        }
        List<Point> availableMovesToDraw;
        private void HandleMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int row = e.Y / CellSize;
                int col = e.X / CellSize;
                Point clickedPosition = new Point(col, row);

                if (!isPieceSelected)
                {
                    if (piecePositions.ContainsKey(GetPieceName(clickedPosition)) && GetPieceOwner(clickedPosition) == currentPlayer)
                    {
                        selectedPiece = GetPieceName(clickedPosition);
                        selectedPosition = clickedPosition;
                        isPieceSelected = true;
                        availableMovesToDraw = availableMoves(GetPieceName(new Point(clickedPosition.X, clickedPosition.Y)), clickedPosition);
                        shouldDrawRange = true;
                        form.Refresh();
                    }
                }
                else
                {
                    if (IsValidMove(selectedPiece, selectedPosition, clickedPosition))
                    {
                        MovePiece(selectedPiece, selectedPosition, clickedPosition);
                        selectedPiece = string.Empty;
                        selectedPosition = Point.Empty;
                        isPieceSelected = false;
                        currentPlayer = GetNextPlayer();
                        form.Invalidate();
                        shouldDrawRange = false;
                        availableMovesToDraw.Clear();
                    }
                }
            }
        }

        private bool IsValidMove(string pieceName, Point currentPosition, Point targetPosition)
        {
            // Validate the move based on the rules of chess
            // ...
            // Implement the specific rules for each type of piece and the interactions in the 4-army chess game
            // Return true if the move is valid, false otherwise
            // ...
            if (pieceName.StartsWith("King"))
            {
                return IsKingMoveValid(currentPosition, targetPosition);
            }
            if (pieceName.StartsWith("Queen"))
            {
                return IsQueenMoveValid(currentPosition, targetPosition);
            }

            else if (pieceName.StartsWith("Rook"))
            {
                return IsRookMoveValid(currentPosition, targetPosition);
            }
            else if (pieceName.StartsWith("Bishop"))
            {
                return IsBishopMoveValid(currentPosition, targetPosition);
            }
            else if (pieceName.StartsWith("Knight"))
            {
                return IsKnightMoveValid(currentPosition, targetPosition);
            }
            else if (pieceName.StartsWith("Cardinal"))
            {
                return IsCardinalMoveValid(currentPosition, targetPosition);
            }
            else if (pieceName.StartsWith("Pawn"))
            {
                return IsPawnMoveValid(pieceName, currentPosition, targetPosition);
            }


            return false;
        }

        private bool IsKingMoveValid(Point currentPosition, Point targetPosition)
        {
            int deltaX = Math.Abs(targetPosition.X - currentPosition.X);
            int deltaY = Math.Abs(targetPosition.Y - currentPosition.Y);

            // The King can move one square in any direction (horizontal, vertical, or diagonal)
            if ((deltaX == 1 && deltaY == 0) || (deltaX == 0 && deltaY == 1) || (deltaX == 1 && deltaY == 1))
            {
                return true; // Valid move
            }

            return false; // Invalid move
        }
        private bool IsQueenMoveValid(Point currentPosition, Point targetPosition)
        {
            // Queen movement logic
            int deltaX = Math.Abs(targetPosition.X - currentPosition.X);
            int deltaY = Math.Abs(targetPosition.Y - currentPosition.Y);

            // The Queen can move horizontally, vertically, or diagonally any number of squares
            if ((deltaX > 0 && deltaY == 0) || (deltaX == 0 && deltaY > 0) || (deltaX == deltaY && deltaX > 0))
            {
                // Check if there are any pieces blocking the path
                int startX = currentPosition.X;
                int startY = currentPosition.Y;
                int endX = targetPosition.X;
                int endY = targetPosition.Y;

                int stepX = Math.Sign(endX - startX);
                int stepY = Math.Sign(endY - startY);

                int x = startX + stepX;
                int y = startY + stepY;

                while (x != endX || y != endY)
                {
                    if (GetPieceName(new Point(x, y)) != null)
                    {
                        return false; // Path is blocked
                    }

                    x += stepX;
                    y += stepY;
                }

                return true; // Valid move
            }
            return false; // Invalid move
        }




        private bool IsRookMoveValid(Point currentPosition, Point targetPosition)
        {
            int deltaX = Math.Abs(targetPosition.X - currentPosition.X);
            int deltaY = Math.Abs(targetPosition.Y - currentPosition.Y);

            // The Rook can move horizontally or vertically any number of squares, but not diagonally
            if ((deltaX > 0 && deltaY == 0) || (deltaX == 0 && deltaY > 0))
            {
                // Check if there are any pieces blocking the path
                int startX = currentPosition.X;
                int startY = currentPosition.Y;
                int endX = targetPosition.X;
                int endY = targetPosition.Y;

                if (deltaX > 0) // Horizontal movement
                {
                    int stepX = Math.Sign(endX - startX);
                    for (int x = startX + stepX; x != endX; x += stepX)
                    {
                        if (GetPieceName(new Point(x, startY)) != null)
                        {
                            return false; // Path is blocked
                        }
                    }
                }
                else // Vertical movement
                {
                    int stepY = Math.Sign(endY - startY);
                    for (int y = startY + stepY; y != endY; y += stepY)
                    {
                        if (GetPieceName(new Point(startX, y)) != null)
                        {
                            return false; // Path is blocked
                        }
                    }
                }

                return true; // Valid move
            }

            return false; // Invalid move
        }

        private bool IsBishopMoveValid(Point currentPosition, Point targetPosition)
        {
            int deltaX = Math.Abs(targetPosition.X - currentPosition.X);
            int deltaY = Math.Abs(targetPosition.Y - currentPosition.Y);

            // Bishops move diagonally
            if (deltaX == deltaY && deltaX > 0)
            {
                int stepX = Math.Sign(targetPosition.X - currentPosition.X);
                int stepY = Math.Sign(targetPosition.Y - currentPosition.Y);

                int x = currentPosition.X + stepX;
                int y = currentPosition.Y + stepY;

                while (x != targetPosition.X && y != targetPosition.Y)
                {
                    if (GetPieceName(new Point(x, y)) != null)
                    {
                        return false; // Path is blocked
                    }

                    x += stepX;
                    y += stepY;
                }

                return true; // Valid move
            }

            return false; // Invalid move
        }

        private bool IsKnightMoveValid(Point currentPosition, Point targetPosition)
        {
            int deltaX = Math.Abs(targetPosition.X - currentPosition.X);
            int deltaY = Math.Abs(targetPosition.Y - currentPosition.Y);

            // Knights move in an L-shape pattern: two squares in one direction and one square perpendicular to it
            if ((deltaX == 2 && deltaY == 1) || (deltaX == 1 && deltaY == 2))
            {
                return true; // Valid move
            }

            return false; // Invalid move
        }

        private bool IsCardinalMoveValid(Point currentPosition, Point targetPosition)
        {
            int deltaX = Math.Abs(targetPosition.X - currentPosition.X);
            int deltaY = Math.Abs(targetPosition.Y - currentPosition.Y);

            // Cardinals move as a bishop or as a knight
            // Check if the move is valid as a bishop
            if (deltaX == deltaY && deltaX > 0)
            {
                int stepX = Math.Sign(targetPosition.X - currentPosition.X);
                int stepY = Math.Sign(targetPosition.Y - currentPosition.Y);

                int x = currentPosition.X + stepX;
                int y = currentPosition.Y + stepY;

                while (x != targetPosition.X && y != targetPosition.Y)
                {
                    if (GetPieceName(new Point(x, y)) != null)
                    {
                        return false; // Path is blocked
                    }

                    x += stepX;
                    y += stepY;
                }

                return true; // Valid move as a bishop
            }

            // Check if the move is valid as a knight
            if ((deltaX == 2 && deltaY == 1) || (deltaX == 1 && deltaY == 2))
            {
                return true; // Valid move as a knight
            }

            return false; // Invalid move
        }

        private bool IsPawnMoveValid(string pieceName, Point currentPosition, Point targetPosition)
        {
            int deltaX = Math.Abs(targetPosition.X - currentPosition.X);
            int deltaY = Math.Abs(targetPosition.Y - currentPosition.Y);

            // Pawns can move one or two squares forward orthogonally (without jumping)
            // and capture one square diagonally
            if ((deltaX == 1 && deltaY == 0) && currentPlayer.StartsWith("Player 1")) // Player 1's pawns can only move forward
            {
                if (targetPosition.X < currentPosition.X) // Moving towards the opponent's side
                {
                    return true; // Valid move
                }
            }
            else if ((deltaX == 1 && deltaY == 0) && currentPlayer.StartsWith("Player 3")) // Player 3's pawns can only move forward
            {
                if (targetPosition.X > currentPosition.X) // Moving towards the opponent's side
                {
                    return true; // Valid move
                }
            }
            else if ((deltaX == 0 && deltaY == 1) && (currentPlayer.StartsWith("Player 2") || currentPlayer.StartsWith("Player 4"))) // Player 2 and Player 4's pawns can only move vertically
            {
                if (targetPosition.Y < currentPosition.Y) // Moving towards the opponent's side
                {
                    return true; // Valid move
                }
            }
            else if (deltaX == 1 && deltaY == 1) // Pawn capture diagonally
            {
                string capturedPiece = GetPieceName(targetPosition);
                if (capturedPiece != null && !capturedPiece.StartsWith(currentPlayer.Substring(0, 6))) // Check if the target position has an opponent's piece
                {
                    return true; // Valid capture
                }
            }

            return false; // Invalid move
        }
        private void MovePiece(string pieceName, Point currentPosition, Point targetPosition)
        {
            // Move the piece from the current position to the target position
            // Update the piecePositions dictionary accordingly
            // Implement the logic for capturing opponent pieces if necessary
            // ...
            if (piecePositions.ContainsKey(pieceName))
            {
                Point[] positions = piecePositions[pieceName];
                for (int i = 0; i < positions.Length; i++)
                {
                    if (positions[i] == currentPosition)
                    {
                        positions[i] = targetPosition;
                        break;
                    }
                }

                if (pieceName.StartsWith("Pawn") && Math.Abs(targetPosition.Y - currentPosition.Y) == 2)
                {

                }

                if (piecePositions.ContainsKey(GetPieceName(targetPosition)))
                {
                    // Capture opponent's piece
                    // ...
                }
            }
        }

        private string GetPieceName(Point position)
        {
            foreach (KeyValuePair<string, Point[]> kvp in piecePositions)
            {
                Point[] positions = kvp.Value;
                if (positions.Contains(position))
                {
                    return kvp.Key;
                }
            }
            return string.Empty;
        }

        private string GetPieceOwner(Point position)
        {
            foreach (KeyValuePair<string, Point[]> kvp in piecePositions)
            {
                Point[] positions = kvp.Value;
                if (positions.Contains(position))
                {
                    string pieceName = kvp.Key;
                    if (pieceName.EndsWith("Yellow"))
                    {
                        return "Player 1";
                    }
                    else if (pieceName.EndsWith("Black"))
                    {
                        return "Player 2";
                    }
                    else if (pieceName.EndsWith("Red"))
                    {
                        return "Player 3";
                    }
                    else if (pieceName.EndsWith("Green"))
                    {
                        return "Player 4";
                    }
                }
            }
            return string.Empty;
        }


        private string GetNextPlayer()
        {
            if (currentPlayer == "Player 1")
            {
                if (canMovePlayer2)
                    return "Player 2";
                else if (canMovePlayer3)
                    return "Player 3";
                else if (canMovePlayer4)
                    return "Player 4";
            }
            else if (currentPlayer == "Player 2")
            {
                if (canMovePlayer3)
                    return "Player 3";
                else if (canMovePlayer4)
                    return "Player 4";
                else if (canMovePlayer1)
                    return "Player 1";
            }
            else if (currentPlayer == "Player 3")
            {
                if (canMovePlayer4)
                    return "Player 4";
                else if (canMovePlayer1)
                    return "Player 1";
                else if (canMovePlayer2)
                    return "Player 2";
            }
            else if (currentPlayer == "Player 4")
            {
                if (canMovePlayer1)
                    return "Player 1";
                else if (canMovePlayer2)
                    return "Player 2";
                else if (canMovePlayer3)
                    return "Player 3";
            }

            return currentPlayer;
        }



    }


}



