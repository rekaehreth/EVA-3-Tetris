using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsTetris
{
    public class TetrisModel
    {
        private TetrisPersistence persistence = new TetrisPersistence();
        public int Size { get; set; } // oszlopok száma
        public int[,] Table { get; set; }
        public TetrisPiece CurrentPiece { get; set; }
        public bool GameActive { get; set; }
        public EventHandler UpdateTable;
        public bool IsGameOver()
        {
            return LineFull(0);
        }
        public void PauseGame()
        {
            GameActive = false;
        }
        public void NewGame(int size)
        {
            Table = new int[16, size];
            CurrentPiece = new TetrisPiece();
            GameActive = true;
        }
        public void LoadGame(string path)
        {
            persistence.Save(path);
            Size = persistence.Size;
            CurrentPiece = persistence.CurrentPiece;
            Table = persistence.Table;
        }
        public void SaveGame(string path)
        {
            persistence.Size = Size;
            persistence.CurrentPiece = CurrentPiece;
            persistence.Table = Table;
            persistence.Save(path);
        }
        public void SaveMovedPiece(List<(int, int)> NewCoordinates)
        {
            Table[CurrentPiece.Coordinates[0].Item1, CurrentPiece.Coordinates[0].Item2] = 0;
            Table[CurrentPiece.Coordinates[1].Item1, CurrentPiece.Coordinates[1].Item2] = 0;
            Table[CurrentPiece.Coordinates[2].Item1, CurrentPiece.Coordinates[2].Item2] = 0;
            Table[CurrentPiece.Coordinates[3].Item1, CurrentPiece.Coordinates[3].Item2] = 0;
            CurrentPiece.Coordinates = NewCoordinates;
            CurrentPiece.Direction = (PieceDirection)((int)CurrentPiece.Direction + 1 % 4);
            Table[CurrentPiece.Coordinates[0].Item1, CurrentPiece.Coordinates[0].Item2] = (int)CurrentPiece.Type + 1;
            Table[CurrentPiece.Coordinates[1].Item1, CurrentPiece.Coordinates[1].Item2] = (int)CurrentPiece.Type + 1;
            Table[CurrentPiece.Coordinates[2].Item1, CurrentPiece.Coordinates[2].Item2] = (int)CurrentPiece.Type + 1;
            Table[CurrentPiece.Coordinates[3].Item1, CurrentPiece.Coordinates[3].Item2] = (int)CurrentPiece.Type + 1;
            UpdateTable?.Invoke(this, null);
        }
        public bool RotatePiece()
        {
            if(GameActive)
            {
                List<(int, int)> rotatedCoordinates = new List<(int, int)>(4);
                switch (CurrentPiece.Type)
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
                    if (rotatedCoordinates[i].Item1 > 16 || Table[rotatedCoordinates[i].Item1, rotatedCoordinates[i].Item2] != 0 || rotatedCoordinates[i].Item2 < 0 || rotatedCoordinates[i].Item2 > Size)
                    {
                        return false;
                    }
                }
                SaveMovedPiece(rotatedCoordinates);
                return true;
            }
            return false;
        }
        private List<(int, int)> RotateZ()
        {
            List<(int, int)> rotatedCoordinates = new List<(int, int)>(4);
            switch ( CurrentPiece.Direction)
            {
                case PieceDirection.Up:
                    rotatedCoordinates.Add(CurrentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2     ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 2, rotatedCoordinates[1].Item2 + 1 ));
                    break;
                case PieceDirection.Right:
                    rotatedCoordinates.Add(CurrentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1,     rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 - 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 - 1, rotatedCoordinates[1].Item2 + 2 ));
                    break;
                case PieceDirection.Down:
                    rotatedCoordinates.Add(CurrentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2     ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 2, rotatedCoordinates[1].Item2 + 1 ));
                    break;
                case PieceDirection.Left:
                    rotatedCoordinates.Add(CurrentPiece.Coordinates[3]);
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
            switch (CurrentPiece.Direction)
            {
                case PieceDirection.Up:
                    rotatedCoordinates.Add(CurrentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2     ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 2, rotatedCoordinates[1].Item2     ));
                    break;
                case PieceDirection.Right:
                    rotatedCoordinates.Add(CurrentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1,     rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1,     rotatedCoordinates[1].Item2 + 2 ));
                    break;
                case PieceDirection.Down:
                    rotatedCoordinates.Add(CurrentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2     ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 2, rotatedCoordinates[1].Item2     ));
                    break;
                case PieceDirection.Left:
                    rotatedCoordinates.Add(CurrentPiece.Coordinates[3]);
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
            switch (CurrentPiece.Direction)
            {
                case PieceDirection.Up:
                    rotatedCoordinates.Add(CurrentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 - 1, rotatedCoordinates[1].Item2     ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 - 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 - 1, rotatedCoordinates[1].Item2 + 2 ));
                    break;
                case PieceDirection.Right:
                    rotatedCoordinates.Add(CurrentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1,     rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 2, rotatedCoordinates[1].Item2 + 1 ));
                    break;
                case PieceDirection.Down:
                    rotatedCoordinates.Add(CurrentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1,     rotatedCoordinates[1].Item2 + 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1,     rotatedCoordinates[1].Item2 + 2 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 - 1, rotatedCoordinates[1].Item2 + 2 ));
                    break;
                case PieceDirection.Left:
                    rotatedCoordinates.Add(CurrentPiece.Coordinates[3]);
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
            switch (CurrentPiece.Direction)
            {
                case PieceDirection.Up:
                    rotatedCoordinates.Add(CurrentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 2, rotatedCoordinates[1].Item2));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 3, rotatedCoordinates[1].Item2));
                    break;
                case PieceDirection.Right:
                    rotatedCoordinates.Add(CurrentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1, rotatedCoordinates[1].Item2 + 1));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1, rotatedCoordinates[1].Item2 + 2));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1, rotatedCoordinates[1].Item2 + 3));
                    break;
                case PieceDirection.Down:
                    rotatedCoordinates.Add(CurrentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 2, rotatedCoordinates[1].Item2));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 3, rotatedCoordinates[1].Item2));
                    break;
                case PieceDirection.Left:
                    rotatedCoordinates.Add(CurrentPiece.Coordinates[3]);
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1, rotatedCoordinates[1].Item2 + 1));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1, rotatedCoordinates[1].Item2 + 2));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1, rotatedCoordinates[1].Item2 + 3));
                    break;
            }
            return rotatedCoordinates;
        }
        public void MovePieceLeft()
        {
            if(GameActive)
            {
                List<(int, int)> movedCoordinates = new List<(int, int)>(4);
                for (int i = 0; i < 4; ++i)
                {
                    movedCoordinates[i] = (CurrentPiece.Coordinates[i].Item1, CurrentPiece.Coordinates[i].Item2 - 1);
                    if (Table[movedCoordinates[i].Item1, movedCoordinates[i].Item2] != 0 || movedCoordinates[i].Item2 < 0)
                    {
                        return;
                    }
                }
                SaveMovedPiece(movedCoordinates);
            }
        }
        public void MovePieceRight()
        {
            if(GameActive)
            {
                List<(int, int)> movedCoordinates = new List<(int, int)>(4);
                for (int i = 0; i < 4; ++i)
                {
                    movedCoordinates[i] = (CurrentPiece.Coordinates[i].Item1, CurrentPiece.Coordinates[i].Item2 + 1);
                    if (Table[movedCoordinates[i].Item1, movedCoordinates[i].Item2] != 0 || movedCoordinates[i].Item2 >= Size)
                    {
                        return;
                    }
                }
                SaveMovedPiece(movedCoordinates);
            }
        }
        public void MovePieceDown()
        {
            if(GameActive)
            {
                List<(int, int)> movedCoordinates = new List<(int, int)>(4);
                for (int i = 0; i < 4; ++i)
                {
                    movedCoordinates[i] = (CurrentPiece.Coordinates[i].Item1 + 1, CurrentPiece.Coordinates[i].Item2);
                    if (Table[movedCoordinates[i].Item1, movedCoordinates[i].Item2] != 0 || movedCoordinates[i].Item1 >= 16)
                    {
                        return;
                    }
                }
                SaveMovedPiece(movedCoordinates);
            }
        }
        public bool LineFull(int line)
        {
            bool lineFull = true;
            for (int row = 0; row < Size; ++row)
            {
                if (Table[line, row] == 0)
                {
                    lineFull = false;
                }
            }
            return lineFull;
        }
        public void RemoveFullLines()
        {
            for(int line = 0; line< 16; ++line)
            {
                if (LineFull(line))
                {
                    for (int fullLineRow = 0; fullLineRow < Size; ++fullLineRow)
                    {
                        Table[line, fullLineRow] = 0;
                    }
                    for (int droppingLine = line - 1; droppingLine > 0; --droppingLine)
                    {
                        for (int row = 0; row < Size; ++row)
                        {
                            Table[droppingLine + 1, row] = Table[droppingLine, row];
                            Table[droppingLine, row] = 0;
                        }
                    }
                }
            }
            UpdateTable?.Invoke(this, null);
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
*/
