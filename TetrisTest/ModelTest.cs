using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using WinFormsTetris.Model;
using WinFormsTetris.Persistence;

namespace TetrisTest
{
    [TestClass]
    public class ModelTest
    {
        private TetrisModel model;
        string mockedSaveFile;
        [TestInitialize]
        public void Initialize()
        {
            model = new TetrisModel();
            model.CurrentPiece = new TetrisPiece();
            model.CurrentPiece.Coordinates = new List<(int, int)>();
            model.GameActive = true;
            model.GameOver += GameOverHandler;

            model.persistence = new MockTetrisPersistence();
            mockedSaveFile = "";
        }
        [TestCleanup]
        public void CleanUp()
        {
            model.Size = 0;
            model.Table = null;
            model.CurrentPiece = null;
            model.GameActive = false;
        }
        #region Game Statuses
        [TestMethod]
        public void Test_NewGame_Small()
        {
            model.NewGame(4);
            Assert.AreEqual(model.Size, 4);
            for(int line = 0; line < 16; ++line)
            {
                for(int row = 0; row < model.Size; ++row)
                {
                    for(int coordinate = 0; coordinate < 4; ++ coordinate)
                    {
                        if(line == model.CurrentPiece.Coordinates[coordinate].Item1 && row == model.CurrentPiece.Coordinates[coordinate].Item2)
                        {
                            Assert.AreEqual(model.Table[line, row], (int)model.CurrentPiece.Type + 1);
                            model.Table[line, row] = 0;
                        }
                    }
                }
            }
            for (int line = 0; line < 16; ++line)
            {
                for (int row = 0; row < model.Size; ++row)
                {
                    Assert.AreEqual(model.Table[line, row], 0);
                }
            }
        }
        [TestMethod]
        public void Test_NewGame_Medium()
        {
            model.NewGame(8);
            Assert.AreEqual(model.Size, 8);
            for (int line = 0; line < 16; ++line)
            {
                for (int row = 0; row < model.Size; ++row)
                {
                    for (int coordinate = 0; coordinate < 4; ++coordinate)
                    {
                        if (line == model.CurrentPiece.Coordinates[coordinate].Item1 && row == model.CurrentPiece.Coordinates[coordinate].Item2)
                        {
                            Assert.AreEqual(model.Table[line, row], (int)model.CurrentPiece.Type + 1);
                            model.Table[line, row] = 0;
                        }
                    }
                }
            }
            for (int line = 0; line < 16; ++line)
            {
                for (int row = 0; row < model.Size; ++row)
                {
                    Assert.AreEqual(model.Table[line, row], 0);
                }
            }
        }
        [TestMethod]
        public void Test_NewGame_Large()
        {
            model.NewGame(12);
            Assert.AreEqual(model.Size, 12);
            for (int line = 0; line < 16; ++line)
            {
                for (int row = 0; row < model.Size; ++row)
                {
                    for (int coordinate = 0; coordinate < 4; ++coordinate)
                    {
                        if (line == model.CurrentPiece.Coordinates[coordinate].Item1 && row == model.CurrentPiece.Coordinates[coordinate].Item2)
                        {
                            Assert.AreEqual(model.Table[line, row], (int)model.CurrentPiece.Type + 1);
                            model.Table[line, row] = 0;
                        }
                    }
                }
            }
            for (int line = 0; line < 16; ++line)
            {
                for (int row = 0; row < model.Size; ++row)
                {
                    Assert.AreEqual(model.Table[line, row], 0);
                }
            }
        }
        private void GameOverHandler(object sender, EventArgs e)
        {
            bool CoordinatesEmpty = true;
            for (int i = 0; i < 4; ++i)
            {
                if(model.Table[model.CurrentPiece.Coordinates[i].Item1, model.CurrentPiece.Coordinates[i].Item2] != 0)
                {
                    CoordinatesEmpty = false;
                }
            }
            Assert.IsFalse(CoordinatesEmpty);
        }
        [TestMethod]
        public void Test_GameOver()
        {
            model.Size = 4;
            model.Table = new int[16, 4];

            model.Table[2, 0] = (int)PieceType.Hero + 1;
            model.Table[2, 1] = (int)PieceType.Hero + 1;
            model.Table[3, 0] = (int)PieceType.Hero + 1;
            model.Table[3, 1] = (int)PieceType.Hero + 1;

            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((1, 1));

            model.MovePieceDown();
            // The GameIsOver method invokes the GameOver Event
            // effects of EndGame
            Assert.AreEqual(model.Size, 0);
            Assert.AreEqual(model.Table, null);
            Assert.AreEqual(model.CurrentPiece, null);
            Assert.IsFalse(model.GameActive);
        }
        #endregion
        #region Insertion
        [TestMethod]
        public void Test_InsertPiece_NoOtherPiece()
        {
            model.Size = 4;
            model.Table = new int[16, 4];

            model.CurrentPiece.Coordinates.Add((15, 0));
            model.CurrentPiece.Coordinates.Add((15, 1));
            model.CurrentPiece.Coordinates.Add((14, 1));
            model.CurrentPiece.Coordinates.Add((15, 2));
            model.CurrentPiece.Type = PieceType.TeeWee;

            model.MovePieceDown();

            Assert.AreEqual(model.Table[15, 0], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[15, 1], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[14, 1], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[15, 2], (int)PieceType.TeeWee + 1);

            Assert.AreNotEqual(model.CurrentPiece.Coordinates[0], (15, 0));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[1], (15, 1));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[2], (14, 1));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[3], (15, 2));
        }
        [TestMethod]
        public void Test_InsertPiece_NoFullLine()
        {
            model.Size = 4;
            model.Table = new int[16, 4];

            model.Table[15, 0] = (int)PieceType.TeeWee + 1;
            model.Table[15, 1] = (int)PieceType.TeeWee + 1;
            model.Table[14, 1] = (int)PieceType.TeeWee + 1;
            model.Table[15, 2] = (int)PieceType.TeeWee + 1;

            model.CurrentPiece.Coordinates.Add((12, 1));
            model.CurrentPiece.Coordinates.Add((13, 1));
            model.CurrentPiece.Coordinates.Add((13, 2));
            model.CurrentPiece.Coordinates.Add((14, 2));

            model.CurrentPiece.Type = PieceType.Z;

            model.MovePieceDown();

            Assert.AreEqual(model.Table[12, 1], (int)PieceType.Z + 1);
            Assert.AreEqual(model.Table[13, 1], (int)PieceType.Z + 1);
            Assert.AreEqual(model.Table[13, 2], (int)PieceType.Z + 1);
            Assert.AreEqual(model.Table[14, 2], (int)PieceType.Z + 1);

            Assert.AreNotEqual(model.CurrentPiece.Coordinates[0], (12, 1));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[1], (13, 1));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[2], (13, 2));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[3], (14, 2));
        }
        [TestMethod]
        public void Test_InsertPiece_OneFullLine()
        {
            model.Size = 4;
            model.Table = new int[16, 4];

            model.Table[15, 0] = (int)PieceType.TeeWee + 1;
            model.Table[15, 1] = (int)PieceType.TeeWee + 1;
            model.Table[14, 1] = (int)PieceType.TeeWee + 1;
            model.Table[15, 2] = (int)PieceType.TeeWee + 1;

            model.CurrentPiece.Coordinates.Add((12, 3));
            model.CurrentPiece.Coordinates.Add((13, 3));
            model.CurrentPiece.Coordinates.Add((14, 3));
            model.CurrentPiece.Coordinates.Add((15, 3));

            model.CurrentPiece.Type = PieceType.Hero;

            model.MovePieceDown();

            Assert.AreEqual(model.Table[12, 3], 0);
            Assert.AreEqual(model.Table[14, 1], 0);
            Assert.AreEqual(model.Table[15, 0], 0);
            Assert.AreEqual(model.Table[15, 2], 0);
            Assert.AreEqual(model.Table[15, 1], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[13, 3], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[14, 3], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[15, 3], (int)PieceType.Hero + 1);

            Assert.AreNotEqual(model.CurrentPiece.Coordinates[0], (12, 3));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[1], (13, 3));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[2], (14, 3));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[3], (15, 3));
        }
        [TestMethod]
        public void Test_InsertPiece_MultipleFullLines()
        {
            model.Size = 4;
            model.Table = new int[16, 4];

            model.Table[13, 0] = (int)PieceType.Hero + 1;
            model.Table[14, 0] = (int)PieceType.Hero + 1;
            model.Table[15, 0] = (int)PieceType.Hero + 1;

            model.Table[13, 2] = (int)PieceType.Hero + 1;
            model.Table[14, 2] = (int)PieceType.Hero + 1;
            model.Table[15, 2] = (int)PieceType.Hero + 1;

            model.Table[14, 3] = (int)PieceType.Hero + 1;
            model.Table[15, 3] = (int)PieceType.Hero + 1;

            model.Table[11, 3] = (int)PieceType.TeeWee + 1;
            model.Table[12, 3] = (int)PieceType.TeeWee + 1;
            model.Table[12, 2] = (int)PieceType.TeeWee + 1;
            model.Table[13, 3] = (int)PieceType.TeeWee + 1;

            model.CurrentPiece.Coordinates.Add((12, 1));
            model.CurrentPiece.Coordinates.Add((13, 1));
            model.CurrentPiece.Coordinates.Add((14, 1));
            model.CurrentPiece.Coordinates.Add((15, 1));

            model.CurrentPiece.Type = PieceType.Hero;

            model.MovePieceDown();

            Assert.AreEqual(model.Table[11, 3], 0);
            Assert.AreEqual(model.Table[12, 1], 0);
            Assert.AreEqual(model.Table[12, 2], 0);
            Assert.AreEqual(model.Table[12, 3], 0);
            Assert.AreEqual(model.Table[13, 0], 0);
            Assert.AreEqual(model.Table[13, 1], 0);
            Assert.AreEqual(model.Table[13, 2], 0);
            Assert.AreEqual(model.Table[13, 3], 0);
            Assert.AreEqual(model.Table[14, 0], 0);
            Assert.AreEqual(model.Table[14, 1], 0);
            Assert.AreEqual(model.Table[14, 2], 0);
            Assert.AreEqual(model.Table[15, 0], 0);

            Assert.AreEqual(model.Table[15, 1], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[15, 2], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[15, 3], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[14, 3], (int)PieceType.TeeWee + 1);

            Assert.AreNotEqual(model.CurrentPiece.Coordinates[0], (12, 2));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[1], (13, 2));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[2], (14, 2));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[3], (15, 2));
        }
        [TestMethod]
        public void Test_NoInsertion_NoRemoval_MultipleFullLines()
        {
            model.Size = 4;
            model.Table = new int[16, 4];

            model.Table[13, 0] = (int)PieceType.Hero + 1;
            model.Table[14, 0] = (int)PieceType.Hero + 1;
            model.Table[15, 0] = (int)PieceType.Hero + 1;

            model.Table[13, 2] = (int)PieceType.Hero + 1;
            model.Table[14, 2] = (int)PieceType.Hero + 1;
            model.Table[15, 2] = (int)PieceType.Hero + 1;

            model.Table[14, 3] = (int)PieceType.Hero + 1;
            model.Table[15, 3] = (int)PieceType.Hero + 1;

            model.Table[11, 3] = (int)PieceType.TeeWee + 1;
            model.Table[12, 3] = (int)PieceType.TeeWee + 1;
            model.Table[12, 2] = (int)PieceType.TeeWee + 1;
            model.Table[13, 3] = (int)PieceType.TeeWee + 1;

            model.CurrentPiece.Coordinates.Add((11, 1));
            model.CurrentPiece.Coordinates.Add((12, 1));
            model.CurrentPiece.Coordinates.Add((13, 1));
            model.CurrentPiece.Coordinates.Add((14, 1));

            model.CurrentPiece.Type = PieceType.Hero;

            model.MovePieceDown();

            Assert.AreEqual(model.Table[13, 0], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[14, 0], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[15, 0], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[12, 1], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[13, 1], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[14, 1], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[15, 1], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[13, 2], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[14, 2], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[15, 2], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[14, 3], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[15, 3], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[13, 3], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[12, 3], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[12, 2], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[11, 3], (int)PieceType.TeeWee + 1);
        }
        #endregion
        #region Persistence
        private void InitializePersistanceTests()
        {
            mockedSaveFile = "4\n";
            mockedSaveFile += $"{(int)PieceType.TeeWee} {(int)PieceDirection.Left} 14 3 13 3 13 2 12 3 \n";
            for (int i = 0; i < 11; ++i)
            {
                mockedSaveFile += "0 0 0 0 \n";
            }
            mockedSaveFile += "2 0 0 0 \n";
            mockedSaveFile += "2 0 0 5 \n";
            mockedSaveFile += "2 0 5 5 \n";
            mockedSaveFile += "2 5 0 5 \n";
            mockedSaveFile += "5 5 5 0 \n";
            
            model.GameActive = false;
        }
        [TestMethod]
        public async Task Test_LoadGameAsync()
        {
            InitializePersistanceTests();
            await model.LoadGameAsync(mockedSaveFile);
            Assert.AreEqual(model.Size, 4);
            Assert.AreEqual(model.CurrentPiece.Type, PieceType.TeeWee);
            Assert.AreEqual(model.CurrentPiece.Direction, PieceDirection.Left);
            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (14, 3));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (13, 3));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (13, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (12, 3));
            for (int line = 0; line < 11; ++line)
            {
                for (int row = 0; row < model.Size; ++row)
                {
                    Assert.AreEqual(model.Table[line, row], 0);
                }
            }
            Assert.AreEqual(model.Table[11, 0], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[12, 0], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[13, 0], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[14, 0], (int)PieceType.Hero + 1);

            Assert.AreEqual(model.Table[15, 0], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[15, 1], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[14, 1], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[15, 2], (int)PieceType.TeeWee + 1);

            Assert.AreEqual(model.Table[12, 3], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[13, 3], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[13, 2], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[14, 3], (int)PieceType.TeeWee + 1);

            Assert.AreEqual(model.Table[11, 1], 0);
            Assert.AreEqual(model.Table[11, 2], 0);
            Assert.AreEqual(model.Table[11, 3], 0);
            Assert.AreEqual(model.Table[12, 1], 0);
            Assert.AreEqual(model.Table[12, 2], 0);
            Assert.AreEqual(model.Table[13, 1], 0);
            Assert.AreEqual(model.Table[14, 2], 0);
            Assert.AreEqual(model.Table[15, 3], 0);
        }
        [TestMethod]
        public async Task Test_SaveGameAsync()
        {
            InitializePersistanceTests();
            model.Size = 4;
            model.Table = new int[16, model.Size];
            model.CurrentPiece.Type = PieceType.TeeWee;
            model.CurrentPiece.Direction = PieceDirection.Left;
            model.CurrentPiece.Coordinates = new List<(int, int)>();

            model.CurrentPiece.Coordinates.Add((14, 3));
            model.CurrentPiece.Coordinates.Add((13, 3));
            model.CurrentPiece.Coordinates.Add((13, 2));
            model.CurrentPiece.Coordinates.Add((12, 3));

            for (int line = 0; line < 11; ++line)
            {
                for (int row = 0; row < model.Size; ++row)
                {
                    model.Table[line, row] = 0;
                }
            }
            model.Table[11, 0] = (int)PieceType.Hero + 1;
            model.Table[12, 0] = (int)PieceType.Hero + 1;
            model.Table[13, 0] = (int)PieceType.Hero + 1;
            model.Table[14, 0] = (int)PieceType.Hero + 1;

            model.Table[15, 0] = (int)PieceType.TeeWee + 1;
            model.Table[15, 1] = (int)PieceType.TeeWee + 1;
            model.Table[14, 1] = (int)PieceType.TeeWee + 1;
            model.Table[15, 2] = (int)PieceType.TeeWee + 1;

            model.Table[12, 3] = (int)PieceType.TeeWee + 1;
            model.Table[13, 3] = (int)PieceType.TeeWee + 1;
            model.Table[13, 2] = (int)PieceType.TeeWee + 1;
            model.Table[14, 3] = (int)PieceType.TeeWee + 1;

            string savedContents = "";
            savedContents = await model.SaveGameAsync(savedContents);

            Assert.AreEqual(mockedSaveFile, savedContents);
        }
        #endregion
    }
}