using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsTetris
{
    public class TetrisModel
    {
        int[,] table;
        int size; // oszlopok száma
        TetrisPiece currentPiece;
        public bool GameOver { get; set; }
        public bool GameActive { get; set; }

        public void newGame(int size)
        {
            table = new int[16, size];
            currentPiece = new TetrisPiece();
        }
        public bool RotatePiece()
        {
            List<(int, int)> rotatedCoordinates = new List<(int, int)>(4);
            switch (currentPiece.Type)
            {
                case PieceType.Smashboy:
                    return true;
                case PieceType.Hero:
                    rotatedCoordinates = RotateHero();
                    break;
                case PieceType.Ricky:
                    rotatedCoordinates = RotateRicky();
                    break;
                case PieceType.TeeWee:
                    rotatedCoordinates = RotateTeeWee();
                    break;
                case PieceType.Z:
                    rotatedCoordinates = RotateZ();
                    break;
                default:
                    return false;
            }
            for (int i = 0; i < 4; ++i)
            {
                if (rotatedCoordinates[i].Item1 > 16 || table[rotatedCoordinates[i].Item1, rotatedCoordinates[i].Item2] != 0 || rotatedCoordinates[i].Item2 < 0 || rotatedCoordinates[i].Item2 > size)
                {
                    return false;
                }
            }
            table[ currentPiece.Coordinates[0].Item1, currentPiece.Coordinates[0].Item2] = 0;
            table[currentPiece.Coordinates[1].Item1, currentPiece.Coordinates[1].Item2] = 0;
            table[currentPiece.Coordinates[2].Item1, currentPiece.Coordinates[2].Item2] = 0;
            table[currentPiece.Coordinates[3].Item1, currentPiece.Coordinates[3].Item2] = 0;
            currentPiece.Coordinates = rotatedCoordinates;
            currentPiece.Direction = (PieceDirection)((int)currentPiece.Direction + 1 % 4);
            table[currentPiece.Coordinates[0].Item1, currentPiece.Coordinates[0].Item2] = (int)currentPiece.Type + 1;
            table[currentPiece.Coordinates[1].Item1, currentPiece.Coordinates[1].Item2] = (int)currentPiece.Type + 1;
            table[currentPiece.Coordinates[2].Item1, currentPiece.Coordinates[2].Item2] = (int)currentPiece.Type + 1;
            table[currentPiece.Coordinates[3].Item1, currentPiece.Coordinates[3].Item2] = (int)currentPiece.Type + 1;
            return true;
        }
        private List<(int, int)> RotateZ()
        {
            List<(int, int)> rotatedCoordinates = new List<(int, int)>(4);
            switch ( currentPiece.Direction)
            {
                case PieceDirection.Up:
                    rotatedCoordinates.Add(currentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2     ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 2, rotatedCoordinates[1].Item2 + 1 ));
                    break;
                case PieceDirection.Right:
                    rotatedCoordinates.Add(currentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1,     rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 - 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 - 1, rotatedCoordinates[1].Item2 + 2 ));
                    break;
                case PieceDirection.Down:
                    rotatedCoordinates.Add(currentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2     ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 2, rotatedCoordinates[1].Item2 + 1 ));
                    break;
                case PieceDirection.Left:
                    rotatedCoordinates.Add(currentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1,     rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 - 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 - 1, rotatedCoordinates[1].Item2 + 2 ));
                    break;
            }
            return rotatedCoordinates;
        }

        private List<(int, int)> RotateTeeWee()
        {
            List<(int, int)> rotatedCoordinates = new List<(int, int)>(4);
            switch (currentPiece.Direction)
            {
                case PieceDirection.Up:
                    rotatedCoordinates.Add(currentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2     ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 2, rotatedCoordinates[1].Item2     ));
                    break;
                case PieceDirection.Right:
                    rotatedCoordinates.Add(currentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1,     rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1,     rotatedCoordinates[1].Item2 + 2 ));
                    break;
                case PieceDirection.Down:
                    rotatedCoordinates.Add(currentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2     ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 2, rotatedCoordinates[1].Item2     ));
                    break;
                case PieceDirection.Left:
                    rotatedCoordinates.Add(currentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1,     rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 - 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1,     rotatedCoordinates[1].Item2 + 2 ));
                    break;
            }
            return rotatedCoordinates;
        }

        private List<(int, int)> RotateRicky()
        {
            List<(int, int)> rotatedCoordinates = new List<(int, int)>(4);
            switch (currentPiece.Direction)
            {
                case PieceDirection.Up:
                    rotatedCoordinates.Add(currentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 - 1, rotatedCoordinates[1].Item2     ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 - 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 - 1, rotatedCoordinates[1].Item2 + 2 ));
                    break;
                case PieceDirection.Right:
                    rotatedCoordinates.Add(currentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1,     rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 2, rotatedCoordinates[1].Item2 + 1 ));
                    break;
                case PieceDirection.Down:
                    rotatedCoordinates.Add(currentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1,     rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1,     rotatedCoordinates[1].Item2 + 2 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 - 1, rotatedCoordinates[1].Item2 + 2 ));
                    break;
                case PieceDirection.Left:
                    rotatedCoordinates.Add(currentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2     ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 2, rotatedCoordinates[1].Item2     ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 2, rotatedCoordinates[1].Item2 + 1 ));
                    break;
            }
            return rotatedCoordinates;
        }

        private List<(int, int)> RotateHero()
        {
            List<(int, int)> rotatedCoordinates = new List<(int, int)>(4);
            switch (currentPiece.Direction)
            {
                case PieceDirection.Up:
                    rotatedCoordinates.Add(currentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 2, rotatedCoordinates[1].Item2));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 3, rotatedCoordinates[1].Item2));
                    break;
                case PieceDirection.Right:
                    rotatedCoordinates.Add(currentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1, rotatedCoordinates[1].Item2 + 1));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1, rotatedCoordinates[1].Item2 + 2));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1, rotatedCoordinates[1].Item2 + 3));
                    break;
                case PieceDirection.Down:
                    rotatedCoordinates.Add(currentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 2, rotatedCoordinates[1].Item2));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 3, rotatedCoordinates[1].Item2));
                    break;
                case PieceDirection.Left:
                    rotatedCoordinates.Add(currentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1, rotatedCoordinates[1].Item2 + 1));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1, rotatedCoordinates[1].Item2 + 2));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1, rotatedCoordinates[1].Item2 + 3));
                    break;
            }
            return rotatedCoordinates;
        }
        public bool MovePieceLeft()
        {
            List<(int, int)> rotatedCoordinates = new List<(int, int)>(4);
            for (int i = 0; i < 4; ++i)
            {
                if (rotatedCoordinates[i].Item1 > 16 || table[rotatedCoordinates[i].Item1, rotatedCoordinates[i].Item2] != 0 || rotatedCoordinates[i].Item2 < 0 || rotatedCoordinates[i].Item2 > size)
                {
                    return false;
                }
            }
            table[currentPiece.Coordinates[0].Item1, currentPiece.Coordinates[0].Item2] = 0;
            table[currentPiece.Coordinates[1].Item1, currentPiece.Coordinates[1].Item2] = 0;
            table[currentPiece.Coordinates[2].Item1, currentPiece.Coordinates[2].Item2] = 0;
            table[currentPiece.Coordinates[3].Item1, currentPiece.Coordinates[3].Item2] = 0;
            currentPiece.Coordinates = rotatedCoordinates;
            currentPiece.Direction = (PieceDirection)((int)currentPiece.Direction + 1 % 4);
            table[currentPiece.Coordinates[0].Item1, currentPiece.Coordinates[0].Item2] = (int)currentPiece.Type + 1;
            table[currentPiece.Coordinates[1].Item1, currentPiece.Coordinates[1].Item2] = (int)currentPiece.Type + 1;
            table[currentPiece.Coordinates[2].Item1, currentPiece.Coordinates[2].Item2] = (int)currentPiece.Type + 1;
            table[currentPiece.Coordinates[3].Item1, currentPiece.Coordinates[3].Item2] = (int)currentPiece.Type + 1;
            return true;
            throw new NotImplementedException();
        }
        public bool MovePieceRight()
        {
            throw new NotImplementedException();
        }
        public bool MovePieceDown()
        {
            throw new NotImplementedException();
        }
    }

    public class TetrisPiece
    {
        public List<(int, int)> Coordinates { get; set; }
        public PieceDirection Direction { get; set; }
        public PieceType Type { get; set; }
        Random randomPicker;
        public TetrisPiece()
        {
            Coordinates = new List<(int, int)>(4);
            Direction = PieceDirection.Up;
            Type = (PieceType)randomPicker.Next(0, 5);
            switch(Type)
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
    public enum PieceDirection
    {
        Up,     // 0
        Right,  // 1
        Down,   // 2
        Left    // 3
    }
    public enum PieceType
    {
                    // what, index, colour
        Smashboy,   // Square, 0, yellow
        Hero,       // I, 1, blue
        Ricky,      // L, 2, orange
        Z,          // Z, 3, green
        TeeWee      // Podium, 4, purple
    }
}

/*
Code: 
    int size = 8;
	int[,] myArray = new int[16, size];;
	for(int i = 0; i < 16; ++i)
	{
		for(int j = 0; j < size; ++j) 
		{
			Console.Write(myArray[i, j] + " ");
		}
		Console.Write("\n");
	}

Output: 
    0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0

Code: 
    int size = 8;
	int[,] myArray = new int[size, 16];;
	for(int i = 0; i < size; ++i)
	{
		for(int j = 0; j < 16; ++j) 
		{
			Console.Write(myArray[i, j] + " ");
		}
		Console.Write("\n");
	}

Output: 
    0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 
    0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
*/
