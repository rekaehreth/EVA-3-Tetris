using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WinFormsTetris.Persistence;

namespace WinFormsTetris.Model
{
    public class TetrisModel
    {
        public ITetrisPersistence persistence = new TetrisPersistence();
        public int Size { get; set; } // oszlopok száma
        public int[,] Table { get; set; }
        public TetrisPiece CurrentPiece { get; set; }
        public bool GameActive { get; set; }
        public event EventHandler UpdateTable;
        public event EventHandler GameOver;
        #region Game Controls
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
        }
        public void EndGame()
        {
            Size = 0;
            Table = null;
            CurrentPiece = null;
            GameActive = false;
        }
        private void IsGameOver()
        {
            for (int i = 0; i < 4; ++i)
            {
                if (Table[CurrentPiece.Coordinates[i].Item1, CurrentPiece.Coordinates[i].Item2] != 0)
                {
                    EndGame();
                    GameOver?.Invoke(this, null);
                    return;
                }
            }
        }
        public void PauseGame()
        {
            GameActive = false;
        }
        public void ContinueGame()
        {
            GameActive = true;
        }
        #endregion
        #region Persistence calls
        public async Task LoadGameAsync(string path)
        {
            if(!GameActive)
            {
                await persistence.LoadAsync(path);
                Size = persistence.Size;
                CurrentPiece = persistence.CurrentPiece;
                Table = persistence.Table;
                ContinueGame();
                UpdateTable?.Invoke(this, null);
            }
        }
        public async Task<string> SaveGameAsync(string path)
        {
            string contents = "";
            if (!GameActive)
            {
                persistence.Size = Size;
                persistence.CurrentPiece = CurrentPiece;
                persistence.Table = Table;
                contents = await persistence.SaveAsync(path);
            }
            return contents;
        }
        #endregion
        #region Update CurrentPiece coordinates
        private void SaveMovedPiece(List<(int, int)> NewCoordinates)
        {            
            CurrentPiece.Coordinates = NewCoordinates;
            
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
                for (int i = 0; i < 4; ++i)
                {
                    Table[CurrentPiece.Coordinates[i].Item1, CurrentPiece.Coordinates[i].Item2] = 0;
                }
                List<(int, int)> movedCoordinates = new List<(int, int)>(4);
                for (int i = 0; i < 4; ++i)
                {
                    movedCoordinates.Add((CurrentPiece.Coordinates[i].Item1, CurrentPiece.Coordinates[i].Item2 - 1));
                    if (movedCoordinates[i].Item2 < 0 || Table[movedCoordinates[i].Item1, movedCoordinates[i].Item2] != 0)
                    {
                        SaveMovedPiece(CurrentPiece.Coordinates);
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
                for (int i = 0; i < 4; ++i)
                {
                    Table[CurrentPiece.Coordinates[i].Item1, CurrentPiece.Coordinates[i].Item2] = 0;
                }
                List<(int, int)> movedCoordinates = new List<(int, int)>(4);
                for (int i = 0; i < 4; ++i)
                {
                    movedCoordinates.Add((CurrentPiece.Coordinates[i].Item1, CurrentPiece.Coordinates[i].Item2 + 1));
                    if (movedCoordinates[i].Item2 >= Size || Table[movedCoordinates[i].Item1, movedCoordinates[i].Item2] != 0)
                    {
                        SaveMovedPiece(CurrentPiece.Coordinates);
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
                for(int i = 0; i < 4; ++i)
                {
                    Table[CurrentPiece.Coordinates[i].Item1, CurrentPiece.Coordinates[i].Item2] = 0;
                }
                List<(int, int)> movedCoordinates = new List<(int, int)>(4);
                for (int i = 0; i < 4; ++i)
                {
                    movedCoordinates.Add((CurrentPiece.Coordinates[i].Item1 + 1, CurrentPiece.Coordinates[i].Item2));
                    if (movedCoordinates[i].Item1 >= 16 || Table[movedCoordinates[i].Item1, movedCoordinates[i].Item2] != 0)
                    {
                        SaveMovedPiece(CurrentPiece.Coordinates);
                        RemoveFullLines();
                        CurrentPiece = new TetrisPiece();
                        IsGameOver();
                        return;
                    }
                }
                SaveMovedPiece(movedCoordinates);
            }
        }
        #endregion
        #region Rotation
        public void RotatePiece()
        {
            if(GameActive)
            {
                for (int i = 0; i < 4; ++i)
                {
                    Table[CurrentPiece.Coordinates[i].Item1, CurrentPiece.Coordinates[i].Item2] = 0;
                }
                List<(int, int)> rotatedCoordinates = CurrentPiece.Coordinates; ;
                switch (CurrentPiece.Type)
                {
                    case PieceType.Smashboy:
                        break;
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
                        return;
                }
                for (int i = 0; i < 4; ++i)
                {
                    if (rotatedCoordinates[i].Item1 >= 16 || rotatedCoordinates[i].Item1 < 0  || rotatedCoordinates[i].Item2 < 0 || rotatedCoordinates[i].Item2 >= Size || Table[rotatedCoordinates[i].Item1, rotatedCoordinates[i].Item2] != 0)
                    {
                        SaveMovedPiece(CurrentPiece.Coordinates);
                        return;
                    }
                }
                CurrentPiece.Direction = (PieceDirection)(((int)CurrentPiece.Direction + 1) % 4);
                SaveMovedPiece(rotatedCoordinates);
            }
        }
        private List<(int, int)> RotateZ()
        {
            List<(int, int)> rotatedCoordinates = new List<(int, int)>(4);
            switch ( CurrentPiece.Direction)
            {
                case PieceDirection.Up:
                    rotatedCoordinates.Add((CurrentPiece.Coordinates[0].Item1 - 2,  CurrentPiece.Coordinates[0].Item2   ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 1,        rotatedCoordinates[0].Item2         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 1,        rotatedCoordinates[0].Item2 + 1     ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 2,        rotatedCoordinates[0].Item2 + 1     ));
                    break;
                case PieceDirection.Right:
                    rotatedCoordinates.Add((CurrentPiece.Coordinates[0].Item1 + 2,  CurrentPiece.Coordinates[0].Item2   ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1,            rotatedCoordinates[0].Item2 + 1     ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 - 1,        rotatedCoordinates[0].Item2 + 1     ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 - 1,        rotatedCoordinates[0].Item2 + 2     ));
                    break;
                case PieceDirection.Down:
                    rotatedCoordinates.Add((CurrentPiece.Coordinates[0].Item1 - 2,  CurrentPiece.Coordinates[0].Item2   ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 1,        rotatedCoordinates[0].Item2         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 1,        rotatedCoordinates[0].Item2 + 1     ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 2,        rotatedCoordinates[0].Item2 + 1     ));
                    break;
                case PieceDirection.Left:
                    rotatedCoordinates.Add((CurrentPiece.Coordinates[0].Item1 + 2,  CurrentPiece.Coordinates[0].Item2   ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1,            rotatedCoordinates[0].Item2 + 1     ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 - 1,        rotatedCoordinates[0].Item2 + 1     ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 - 1,        rotatedCoordinates[0].Item2 + 2     ));
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
                    rotatedCoordinates.Add((CurrentPiece.Coordinates[2].Item1,  CurrentPiece.Coordinates[2].Item2       ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 1,    rotatedCoordinates[0].Item2             ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 1,    rotatedCoordinates[0].Item2 + 1         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 2,    rotatedCoordinates[0].Item2             ));
                    break;
                case PieceDirection.Right:
                    rotatedCoordinates.Add((CurrentPiece.Coordinates[2].Item1,  CurrentPiece.Coordinates[2].Item2   ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1,        rotatedCoordinates[0].Item2 - 1     ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 1,    rotatedCoordinates[0].Item2 - 1     ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1,        rotatedCoordinates[0].Item2 - 2     ));
                    break;
                case PieceDirection.Down:
                    rotatedCoordinates.Add((CurrentPiece.Coordinates[2].Item1,  CurrentPiece.Coordinates[2].Item2       ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 - 1,    rotatedCoordinates[0].Item2             ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 - 1,    rotatedCoordinates[0].Item2 - 1         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 - 2,    rotatedCoordinates[0].Item2             ));
                    break;
                case PieceDirection.Left:
                    rotatedCoordinates.Add((CurrentPiece.Coordinates[2].Item1,  CurrentPiece.Coordinates[2].Item2   ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1,        rotatedCoordinates[0].Item2 + 1     ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 - 1,    rotatedCoordinates[0].Item2 + 1     ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1,        rotatedCoordinates[0].Item2 + 2     ));
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
                    rotatedCoordinates.Add((CurrentPiece.Coordinates[1].Item1,  CurrentPiece.Coordinates[1].Item2 + 1   ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1,            rotatedCoordinates[0].Item2 - 1     ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1,            rotatedCoordinates[0].Item2 - 2     ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 1,        rotatedCoordinates[0].Item2 - 2     ));
                    break;
                case PieceDirection.Right:
                    rotatedCoordinates.Add((CurrentPiece.Coordinates[1].Item1 + 1,  CurrentPiece.Coordinates[1].Item2   ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 - 1,        rotatedCoordinates[0].Item2         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 - 2,        rotatedCoordinates[0].Item2         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 - 2,        rotatedCoordinates[0].Item2 - 1     ));
                    break;
                case PieceDirection.Down:
                    rotatedCoordinates.Add((CurrentPiece.Coordinates[1].Item1,  CurrentPiece.Coordinates[1].Item2 - 1   ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1,        rotatedCoordinates[0].Item2 + 1         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1,        rotatedCoordinates[0].Item2 + 2         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 - 1,    rotatedCoordinates[0].Item2 + 2         ));
                    break;
                case PieceDirection.Left:
                    rotatedCoordinates.Add((CurrentPiece.Coordinates[1].Item1 - 1,  CurrentPiece.Coordinates[1].Item2   ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 1,        rotatedCoordinates[0].Item2         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 2,        rotatedCoordinates[0].Item2         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 2,        rotatedCoordinates[0].Item2 + 1     ));
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
                    rotatedCoordinates.Add((CurrentPiece.Coordinates[2].Item1 - 1,  CurrentPiece.Coordinates[2].Item2   ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 1,        rotatedCoordinates[0].Item2         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 2,        rotatedCoordinates[0].Item2         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 3,        rotatedCoordinates[0].Item2         ));
                    break;
                case PieceDirection.Right:
                    rotatedCoordinates.Add((CurrentPiece.Coordinates[1].Item1,  CurrentPiece.Coordinates[1].Item2 - 2   ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1,        rotatedCoordinates[0].Item2 + 1         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1,        rotatedCoordinates[0].Item2 + 2         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1,        rotatedCoordinates[0].Item2 + 3         ));
                    break;
                case PieceDirection.Down:
                    rotatedCoordinates.Add((CurrentPiece.Coordinates[2].Item1 - 1,  CurrentPiece.Coordinates[2].Item2   ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 1,        rotatedCoordinates[0].Item2         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 2,        rotatedCoordinates[0].Item2         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1 + 3,        rotatedCoordinates[0].Item2         ));
                    break;
                case PieceDirection.Left:
                    rotatedCoordinates.Add((CurrentPiece.Coordinates[1].Item1,  CurrentPiece.Coordinates[1].Item2 - 2   ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1,        rotatedCoordinates[0].Item2 + 1         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1,        rotatedCoordinates[0].Item2 + 2         ));
                    rotatedCoordinates.Add((rotatedCoordinates[0].Item1,        rotatedCoordinates[0].Item2 + 3         ));
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