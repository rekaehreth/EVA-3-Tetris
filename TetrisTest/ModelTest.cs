using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinFormsTetris.Model;
using WinFormsTetris.Persistence;
using Moq;
using System.Threading.Tasks;
using System;

namespace TetrisTest
{
    [TestClass]
    public class ModelTest
    {
        private TetrisModel model;
        private string mockedSaveFile;
        private MockTetrisPersistence persistenceMock;
        private PieceType GetTypeAtPosition(int line, int row)
        {
            return (PieceType)(model.Table[line, row] + 1);
        }
        [TestInitialize]
        public void Initialize()
        {
            persistenceMock = new MockTetrisPersistence();


            model = new TetrisModel();
            model.GameOver += GameIsOver;
            model.UpdateTable += TableUpdated;
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
        #endregion
        #region Movement
        #region Smashboy
        [TestMethod]
        public void Test_MoveSmashboy_Succesfull()
        {

        }
        [TestMethod]
        public void Test_MoveSmashboy_Unsuccesfull()
        {

        }
        [TestMethod]
        public void Test_RotateSmashboy_Succesfull()
        {

        }
        [TestMethod]
        public void Test_RotateSmashboy_Unsuccesfull()
        {

        }
        #endregion
        #region Hero
        [TestMethod]
        public void Test_MoveHero_Succesfull()
        {

        }
        [TestMethod]
        public void Test_MoveHero_Unsuccesfull()
        {

        }
        [TestMethod]
        public void Test_RotateHero_Succesfull()
        {

        }
        [TestMethod]
        public void Test_RotateHero_Unsuccesfull()
        {

        }
        #endregion
        #region Ricky
        [TestMethod]
        public void Test_MoveRicky_Succesfull()
        {

        }
        [TestMethod]
        public void Test_MoveRicky_Unsuccesfull()
        {

        }
        [TestMethod]
        public void Test_RotateRicky_Succesfull()
        {

        }
        [TestMethod]
        public void Test_RotateRicky_Unsuccesfull()
        {

        }
        #endregion
        #region TeeWee
        [TestMethod]
        public void Test_MoveTeeWee_Succesfull()
        {

        }
        [TestMethod]
        public void Test_MoveTeeWee_Unsuccesfull()
        {

        }
        [TestMethod]
        public void Test_RotateTeeWee_Succesfull()
        {

        }
        [TestMethod]
        public void Test_RotateTeeWee_Unsuccesfull()
        {

        }
        #endregion
        #region Z
        [TestMethod]
        public void Test_MoveZ_Succesfull()
        {

        }
        [TestMethod]
        public void Test_MoveZ_Unsuccesfull()
        {

        }
        [TestMethod]
        public void Test_RotateZ_Succesfull()
        {

        }
        [TestMethod]
        public void Test_RotateZ_Unsuccesfull()
        {

        }
        #endregion
        #endregion
        #region Insertion
        [TestMethod]
        public void Test_InsertPiece_NoFullLine()
        {

        }
        [TestMethod]
        public void Test_InsertPiece_OneFullLine()
        {

        }
        [TestMethod]
        public void Test_InsertPiece_MultipleFullLines()
        {

        }
        #endregion
        #region Persist Data 
        private void InitializePersistanceTests()
        {
            mockedSaveFile = "4\n";
            mockedSaveFile += $"{(int)PieceType.TeeWee} {(int)PieceDirection.Left} 12 3 13 3 13 2 14 3\n";
            for (int i = 0; i < 11; ++i)
            {
                mockedSaveFile += "0 0 0 0\n";
            }
            mockedSaveFile += "2 0 0 0\n";
            mockedSaveFile += "2 0 0 4\n";
            mockedSaveFile += "2 0 4 4\n";
            mockedSaveFile += "2 4 0 4\n";
            mockedSaveFile += "4 4 4 0\n";
        }
        [TestMethod]
        public async Task Test_LoadGameAsync()
        {
            InitializePersistanceTests();
            await model.LoadGameAsync(mockedSaveFile);
            Assert.AreEqual(model.Size, 4);
            Assert.AreEqual(model.CurrentPiece.Type, PieceType.TeeWee);
            Assert.AreEqual(model.CurrentPiece.Direction, PieceDirection.Left);
            Assert.AreEqual(model.CurrentPiece.Coordinates[0].Item1, 12);
            Assert.AreEqual(model.CurrentPiece.Coordinates[0].Item2, 3);
            Assert.AreEqual(model.CurrentPiece.Coordinates[0].Item1, 13);
            Assert.AreEqual(model.CurrentPiece.Coordinates[0].Item2, 3);
            Assert.AreEqual(model.CurrentPiece.Coordinates[0].Item1, 13);
            Assert.AreEqual(model.CurrentPiece.Coordinates[0].Item2, 2);
            Assert.AreEqual(model.CurrentPiece.Coordinates[0].Item1, 14);
            Assert.AreEqual(model.CurrentPiece.Coordinates[0].Item2, 3);
            for (int line = 0; line < 11; ++ line)
            {
                for(int row = 0; row < model.Size; ++row)
                {
                    Assert.AreEqual(model.Table[line, row], 0);
                }
            }
            Assert.AreEqual(GetTypeAtPosition(11, 0), PieceType.Hero);
            Assert.AreEqual(GetTypeAtPosition(12, 0), PieceType.Hero);
            Assert.AreEqual(GetTypeAtPosition(13, 0), PieceType.Hero);
            Assert.AreEqual(GetTypeAtPosition(14, 0), PieceType.Hero);

            Assert.AreEqual(GetTypeAtPosition(15, 0), PieceType.TeeWee);
            Assert.AreEqual(GetTypeAtPosition(15, 1), PieceType.TeeWee);
            Assert.AreEqual(GetTypeAtPosition(14, 1), PieceType.TeeWee);
            Assert.AreEqual(GetTypeAtPosition(15, 2), PieceType.TeeWee);

            Assert.AreEqual(GetTypeAtPosition(12, 3), PieceType.TeeWee);
            Assert.AreEqual(GetTypeAtPosition(13, 3), PieceType.TeeWee);
            Assert.AreEqual(GetTypeAtPosition(13, 2), PieceType.TeeWee);
            Assert.AreEqual(GetTypeAtPosition(14, 3), PieceType.TeeWee);

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
            string saveResults = "";
            model.Size = 4;
            model.CurrentPiece.Type = PieceType.TeeWee;
            model.CurrentPiece.Direction = PieceDirection.Left;
            model.CurrentPiece.Coordinates[0] = (12, 3);
            model.CurrentPiece.Coordinates[0] = (13, 3);
            model.CurrentPiece.Coordinates[0] = (13, 2);
            model.CurrentPiece.Coordinates[0] = (14, 3);
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

            model.Table[11, 1] = 0;
            model.Table[11, 2] = 0;
            model.Table[11, 3] = 0;
            model.Table[12, 1] = 0;
            model.Table[12, 2] = 0;
            model.Table[13, 1] = 0;
            model.Table[14, 2] = 0;
            model.Table[15, 3] = 0;
            
            await model.SaveGameAsync(saveResults);
            Assert.AreEqual(mockedSaveFile, saveResults);
        }
        #endregion
        #region Events handlers
        private void TableUpdated(Object sender, EventArgs e)
        {
            //Assert.IsTrue(_model.GameTime >= 0); // a játékidõ nem lehet negatív
            //Assert.AreEqual(_model.GameTime == 0, _model.IsGameOver); // a tesztben a játéknak csak akkor lehet vége, ha lejárt az idõ

            //Assert.AreEqual(e.GameStepCount, _model.GameStepCount); // a két értéknek egyeznie kell
            //Assert.AreEqual(e.GameTime, _model.GameTime); // a két értéknek egyeznie kell
            //Assert.IsFalse(e.IsWon); // még nem nyerték meg a játékot
        }

        private void GameIsOver(Object sender, EventArgs e)
        {
            //Assert.IsTrue(_model.IsGameOver); // biztosan vége van a játéknak
            //Assert.AreEqual(0, e.GameTime); // a tesztben csak akkor váltódhat ki, ha elfogy az idõ
            //Assert.IsFalse(e.IsWon);
        }
        #endregion
    }
}
