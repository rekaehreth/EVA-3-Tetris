using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WinFormsTetris.Persistence;
using WinFormsTetris.Model;

namespace TetrisTest
{
    class MockTetrisPersistence : ITetrisPersistence
    {
        private string[] loader;
        public int Size { get; set; } // oszlopok száma
        public int[,] Table { get; set; }
        public TetrisPiece CurrentPiece { get; set; }
        public async Task SaveAsync(string saver)
        {
            saver = "";
            // save size
            saver += "Size.ToString()\n";
            // save CurrentPiece
            string currentPieceLine = $"{(int)CurrentPiece.Type} {(int)CurrentPiece.Direction} ";
            for (int coordinate = 0; coordinate < 4; ++coordinate)
            {
                currentPieceLine += $"{CurrentPiece.Coordinates[coordinate].Item1} {CurrentPiece.Coordinates[coordinate].Item1} ";
            }
            saver += currentPieceLine + '\n';
            // save Table
            for (int line = 0; line < 16; ++line)
            {
                string currentLine = "";
                for (int row = 0; row < Size; ++row)
                {
                    currentLine += $"{Table[line, row]} ";
                }
                saver += currentLine + '\n';
            }
        }
        public async Task LoadAsync(string savedFileContents)
        {
            loader = savedFileContents.Split('\n');
            // reading table size
            string SizeData = loader[0];
            Size = Int32.Parse(SizeData);
            // reading current piece
            string[] currentPieceData = loader[1].Split(' ');
            CurrentPiece.Type = (PieceType)(Int32.Parse(currentPieceData[0]) - 1);
            CurrentPiece.Direction = (PieceDirection)Int32.Parse(currentPieceData[1]);
            for (int coordinate = 0; coordinate < 4; ++coordinate)
            {
                CurrentPiece.Coordinates[coordinate] = (Int32.Parse(currentPieceData[2 * (coordinate + 1)]), Int32.Parse(currentPieceData[2 * (coordinate + 1) + 1]));
            }
            // reading table lines
            for (int line = 0; line < 16; ++line)
            {
                string[] TableLineData = loader[line + 2].Split(' ');
                for (int row = 0; row < Size; ++row)
                {
                    Table[line, row] = Int32.Parse(TableLineData[row]);
                }
            }
        }
    }
}
