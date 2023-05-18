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

            

            InitializePiecePositions();

            form.Paint += DrawChessboard;
            form.Paint += DrawPieces;
        }

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
        }
    }
}
