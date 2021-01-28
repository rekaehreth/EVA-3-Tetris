using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace WinFormsTetris
{
    class TetrisPersistence
    {
        private StreamReader loader;
        private StreamWriter saver;
        public int Size { get; set; } // oszlopok száma
        public int[,] Table { get; set; }
        public TetrisPiece CurrentPiece { get; set; }

        public void Save(string path)
        {
            try
            {
                saver = new StreamWriter(path);
                // save size
                saver.WriteLine(Size.ToString());
                // save CurrentPiece
                string currentPieceLine = $"{(int)CurrentPiece.Type} {(int)CurrentPiece.Direction} ";
                for(int coordinate = 0; coordinate < 4; ++coordinate)
                {
                    currentPieceLine += $"{CurrentPiece.Coordinates[coordinate].Item1} {CurrentPiece.Coordinates[coordinate].Item1} ";
                }
                saver.WriteLine(currentPieceLine);
                // save Table
                for(int line = 0; line < 16; ++line)
                {
                    string currentLine = "";
                    for(int row = 0; row < Size; ++row)
                    {
                        currentLine += $"{Table[line, row]} ";
                    }
                    saver.WriteLine(currentLine);
                }
                saver.Close();
            }
            catch (Exception)
            {
                throw new FileOperationException("Error while saving file.");
            }
        }
        public void Load(string path)
        {
            try
            {
                loader = new StreamReader(path);
                // reading table size
                string SizeData = loader.ReadLine();
                Size = Int32.Parse(SizeData);
                // reading current piece
                string[] currentPieceData = loader.ReadLine().Split(' ');
                // Format: Type(int), Direction(int), Coordinates(int, int, int, int) separated by spaces
                // e.g. 2 1 0 4 1 4 2 4 3 4
                CurrentPiece.Type = (PieceType)Int32.Parse(currentPieceData[0]);
                CurrentPiece.Direction = (PieceDirection)Int32.Parse(currentPieceData[1]);
                for (int coordinate = 0; coordinate < 4; ++coordinate )
                {
                    CurrentPiece.Coordinates[coordinate] = (Int32.Parse(currentPieceData[2 * (coordinate + 1)]), Int32.Parse(currentPieceData[ 2 * (coordinate + 1) + 1])); 
                }
                // reading table lines
                for(int line = 0; line < 16; ++ line)
                {
                    string[] TableLineData = loader.ReadLine().Split(' ');
                    for(int row = 0; row < Size; ++row)
                    {
                        Table[line, row] = Int32.Parse(TableLineData[row]);
                    }
                }
                loader.Close();
            }
            catch (Exception)
            {
                throw new FileOperationException("Error while loading saved game from file.");
            }
        }

    }
    class FileOperationException : Exception {
        public string Message { get; set; }
        public FileOperationException(string message = null)
        {
            Message = message;
        }
    }
}
