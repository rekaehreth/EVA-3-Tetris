using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WinFormsTetris.Persistence;

namespace WinFormsTetris.Model
{
    public class TetrisModel
    {
        private TetrisPersistence persistence = new TetrisPersistence();
        public int Size { get; set; } // oszlopok száma
        public int[,] Table { get; set; }
        public TetrisPiece CurrentPiece { get; set; }
        public bool GameActive { get; set; }
        public EventHandler UpdateTable;
        public EventHandler GameOver;
        #region Game Controls
        private void EndGame()
        {
            Size = 0;
            Table = null;
            CurrentPiece = null;
            GameActive = false;
        }
        private void IsGameOver()
        {
            if( LineFull(0))
            {
                GameOver?.Invoke(this, null);
                EndGame();
            }
        }
        public void PauseGame()
        {
            GameActive = false;
        }
        internal void ContinueGame()
        {
            GameActive = true;
        }
        public void NewGame(int size)
        {
            Size = size;
            Table = new int[16, Size];
            GameActive = true;
            CurrentPiece = new TetrisPiece();
            Table[CurrentPiece.Coordinates[0].Item1, CurrentPiece.Coordinates[0].Item2] = (int)CurrentPiece.Type + 1;
            Table[CurrentPiece.Coordinates[1].Item1, CurrentPiece.Coordinates[1].Item2] = (int)CurrentPiece.Type + 1;
            Table[CurrentPiece.Coordinates[2].Item1, CurrentPiece.Coordinates[2].Item2] = (int)CurrentPiece.Type + 1;
            Table[CurrentPiece.Coordinates[3].Item1, CurrentPiece.Coordinates[3].Item2] = (int)CurrentPiece.Type + 1;
            UpdateTable?.Invoke(this, null);
        }
        #endregion
        #region Persistence calls
        public async Task LoadGameAsync(string path)
        {
            await persistence.LoadAsync(path);
            Size = persistence.Size;
            CurrentPiece = persistence.CurrentPiece;
            Table = persistence.Table;
        }
        public async Task SaveGameAsync(string path)
        {
            persistence.Size = Size;
            persistence.CurrentPiece = CurrentPiece;
            persistence.Table = Table;
            await persistence.SaveAsync(path);
        }
        #endregion
        #region Update CurrentPiece coordinates
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
        #endregion
        #region Movements
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
                        CurrentPiece = new TetrisPiece();
                        return;
                    }
                }
                SaveMovedPiece(movedCoordinates);
                RemoveFullLines();
                IsGameOver();
            }
        }
        #endregion
        #region Rotation
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
                CurrentPiece.Direction = (PieceDirection)((int)CurrentPiece.Direction + 1 % 4);
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
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2 - 1 ));
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
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2     ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2 - 1 ));
                    rotatedCoordinates.Add((rotatedCoordinates[1].Item1 + 1, rotatedCoordinates[1].Item2 - 2 ));
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
        #endregion
        #region Full Lines 
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
        #endregion
    }
}
