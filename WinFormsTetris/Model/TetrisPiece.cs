using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsTetris.Model
{
    public class TetrisPiece
    {
        public List<(int, int)> Coordinates { get; set; }
        public PieceDirection Direction { get; set; }
        public PieceType Type { get; set; }
        Random randomPicker;
        public TetrisPiece()
        {
            randomPicker = new Random();
            Coordinates = new List<(int, int)>(4);
            Direction = PieceDirection.Up;
            Type = (PieceType)randomPicker.Next(0, 5);
            switch (Type)
            {
                case PieceType.Smashboy:
                    Coordinates.Add((0, 0));
                    Coordinates.Add((0, 1));
                    Coordinates.Add((1, 0));
                    Coordinates.Add((1, 1));
                    break;
                case PieceType.Hero:
                    Coordinates.Add((0, 0));
                    Coordinates.Add((0, 1));
                    Coordinates.Add((0, 2));
                    Coordinates.Add((0, 3));
                    break;
                case PieceType.Ricky:
                    Coordinates.Add((0, 0));
                    Coordinates.Add((1, 0));
                    Coordinates.Add((2, 0));
                    Coordinates.Add((2, 1));
                    break;
                case PieceType.TeeWee:
                    Coordinates.Add((1, 0));
                    Coordinates.Add((1, 1));
                    Coordinates.Add((0, 1));
                    Coordinates.Add((1, 2));
                    break;
                case PieceType.Z:
                    Coordinates.Add((1, 0));
                    Coordinates.Add((1, 1));
                    Coordinates.Add((0, 1));
                    Coordinates.Add((0, 2));
                    break;
            }
        }
    }
}
