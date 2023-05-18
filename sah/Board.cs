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

       

            InitializePiecePositions();

            form.Paint += DrawChessboard;
            form.Paint += DrawPieces;
        }

        private void InitializePiecePositions()
        {
            piecePositions = new Dictionary<string, Point[]>
        {
                { "Player1", new Point[]
                {  new Point(1, 3),
                new Point(0, 3),
                new Point(2, 2),
                new Point(3, 1),
                new Point(3, 0),
                 new Point(1, 1),
                new Point(1, 0),
                new Point(2, 0),
                new Point(0, 2),
                new Point(2, 1),
                new Point(1, 2),
                new Point(0, 1),
                new Point(0, 0)
                } },
                {"Player2", new Point[]
                {   new Point(6, 0),
                new Point(6, 1),
                new Point(7, 2),
                new Point(9, 3),
                new Point(8, 3),
                new Point(8, 0),
                new Point(8, 1),
                new Point(7, 0),
                new Point(9, 2),
                new Point(7, 1),
                new Point(8, 2),
                new Point(9, 1),
                new Point(9, 0)
                } },
                 {"Player3", new Point[]
                {   new Point(0, 6),
                new Point(1, 6),
                new Point(2, 7),
                new Point(3, 8),
                new Point(3, 9),
                new Point(2, 9),
                new Point(1, 9),
                new Point(0, 9),
                new Point(2,8),
                new Point(1, 8),
                new Point(0, 8),
                new Point(1, 7),
                new Point(0, 7)
                } },
                 {"Player4", new Point[]
                {   new Point(9, 9),
                new Point(9, 8),
                new Point(9, 7),
                new Point(9, 6),
                new Point(8, 9),
                new Point(8, 8),
                new Point(8, 7),
                new Point(8, 6),
                new Point(7,9),
                new Point(7, 8),
                new Point(7, 7),
                new Point(6, 9),
                new Point(6, 8)
                } },


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

                    Color cellColor = (row + col) % 2 == 0 ? Color.Gray : Color.Yellow;

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

                Color pieceColor = pieceColors[pieceName];
                Brush brush = new SolidBrush(pieceColor);

                foreach (Point position in positions)
                {
                    int x = position.X * CellSize;
                    int y = position.Y * CellSize;

                    g.FillEllipse(brush, x, y, CellSize, CellSize);
                }
            }
        }
    }
}
