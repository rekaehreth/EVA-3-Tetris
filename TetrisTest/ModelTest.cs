using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinFormsTetris.Model;
using WinFormsTetris.Persistence;
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
            model.GameActive = true;
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
        public void Test_MoveSmashboyRight_Succesfull()
        {
            model.Table = new int[16, 4];

            model.Table[0, 0] = (int)PieceType.Smashboy;
            model.Table[0, 1] = (int)PieceType.Smashboy;
            model.Table[1, 0] = (int)PieceType.Smashboy;
            model.Table[1, 1] = (int)PieceType.Smashboy;

            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((1, 1));

            model.MovePieceRight();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (0, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (1, 2));

            Assert.AreEqual(model.Table[0, 0], 0);
            Assert.AreEqual(model.Table[1, 0], 0);

            Assert.AreEqual(model.Table[0, 1], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[0, 2], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[1, 1], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[1, 2], (int)PieceType.Smashboy);
        }
        [TestMethod]
        public void Test_MoveSmashboyRight_Unsuccesfull()
        {
            model.Table = new int[16, 4];

            model.Table[2, 0] = (int)PieceType.Smashboy;
            model.Table[3, 0] = (int)PieceType.Smashboy;
            model.Table[2, 1] = (int)PieceType.Smashboy;
            model.Table[3, 1] = (int)PieceType.Smashboy;

            model.CurrentPiece.Coordinates.Add((2, 0));
            model.CurrentPiece.Coordinates.Add((3, 0));
            model.CurrentPiece.Coordinates.Add((2, 1));
            model.CurrentPiece.Coordinates.Add((3, 1));

            model.MovePieceRight();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (2, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (3, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (2, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (3, 1));

            Assert.AreEqual(model.Table[2, 0], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[3, 0], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[2, 1], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[3, 1], (int)PieceType.Smashboy);

        }
        [TestMethod]
        public void Test_MoveSmashboyLeft_Succesfull()
        {
            model.Table = new int[16, 4];

            model.Table[0, 1] = (int)PieceType.Smashboy;
            model.Table[0, 2] = (int)PieceType.Smashboy;
            model.Table[1, 1] = (int)PieceType.Smashboy;
            model.Table[1, 2] = (int)PieceType.Smashboy;

            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((1, 1));
            model.CurrentPiece.Coordinates.Add((1, 2));

            model.MovePieceLeft();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (1, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (1, 1));

            Assert.AreEqual(model.Table[0, 2], 0);
            Assert.AreEqual(model.Table[1, 2], 0);

            Assert.AreEqual(model.Table[0, 0], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[0, 1], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[1, 0], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[1, 1], (int)PieceType.Smashboy);

        }
        [TestMethod]
        public void Test_MoveSmashboyLeft_Unsuccesfull()
        {
            model.Table = new int[16, 4];

            model.Table[0, 0] = (int)PieceType.Smashboy;
            model.Table[0, 1] = (int)PieceType.Smashboy;
            model.Table[1, 0] = (int)PieceType.Smashboy;
            model.Table[1, 1] = (int)PieceType.Smashboy;

            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((1, 1));

            model.MovePieceLeft();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (1, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (1, 1));

            Assert.AreEqual(model.Table[0, 0], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[0, 1], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[1, 0], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[1, 1], (int)PieceType.Smashboy);
        }
        [TestMethod]
        public void Test_MoveSmashboyDown_Succesfull()
        {
            model.Table = new int[16, 4];

            model.Table[0, 0] = (int)PieceType.Smashboy;
            model.Table[0, 1] = (int)PieceType.Smashboy;
            model.Table[1, 0] = (int)PieceType.Smashboy;
            model.Table[1, 1] = (int)PieceType.Smashboy;

            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((1, 1));

            model.MovePieceDown();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (1, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (2, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (2, 1));

            Assert.AreEqual(model.Table[0, 0], 0);
            Assert.AreEqual(model.Table[0, 1], 0);

            Assert.AreEqual(model.Table[1, 0], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[1, 1], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[2, 0], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[2, 1], (int)PieceType.Smashboy);
        }
        [TestMethod]
        public void Test_MoveSmashboyDown_Unsuccesfull()
        {
            model.Table = new int[16, 4];

            model.Table[14, 0] = (int)PieceType.Smashboy;
            model.Table[14, 1] = (int)PieceType.Smashboy;
            model.Table[15, 0] = (int)PieceType.Smashboy;
            model.Table[15, 1] = (int)PieceType.Smashboy;

            model.CurrentPiece.Coordinates.Add((14, 0));
            model.CurrentPiece.Coordinates.Add((4, 1));
            model.CurrentPiece.Coordinates.Add((15, 0));
            model.CurrentPiece.Coordinates.Add((15, 1));

            model.MovePieceDown();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (14, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (14, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (15, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (15, 1));

            Assert.AreEqual(model.Table[14, 0], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[14, 1], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[15, 0], (int)PieceType.Smashboy);
            Assert.AreEqual(model.Table[15, 1], (int)PieceType.Smashboy);
        }
        #endregion
        #region Hero
        [TestMethod]
        public void Test_MoveHeroRight_Succesfull()
        {
            model.Table = new int[16, 8];

            model.Table[0, 0] = (int)PieceType.Hero;
            model.Table[0, 1] = (int)PieceType.Hero;
            model.Table[0, 2] = (int)PieceType.Hero;
            model.Table[0, 3] = (int)PieceType.Hero;

            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((0, 3));

            model.MovePieceRight();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (0, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (0, 3));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (0, 4));

            Assert.AreEqual(model.Table[0, 0], 0);

            Assert.AreEqual(model.Table[0, 1], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[0, 2], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[0, 3], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[0, 4], (int)PieceType.Hero);

            model.Table[0, 0] = 0;
            model.Table[0, 1] = 0;
            model.Table[0, 2] = 0;
            model.Table[0, 3] = 0;

            model.Table[0, 0] = (int)PieceType.Hero;
            model.Table[1, 0] = (int)PieceType.Hero;
            model.Table[2, 0] = (int)PieceType.Hero;
            model.Table[3, 0] = (int)PieceType.Hero;

            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((2, 0));
            model.CurrentPiece.Coordinates.Add((3, 0));

            model.MovePieceRight();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (2, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (3, 1));

            Assert.AreEqual(model.Table[0, 0], 0);
            Assert.AreEqual(model.Table[1, 0], 0);
            Assert.AreEqual(model.Table[2, 0], 0);
            Assert.AreEqual(model.Table[3, 0], 0);

            Assert.AreEqual(model.Table[0, 1], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[1, 1], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[2, 1], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[3, 1], (int)PieceType.Hero);
        }
        [TestMethod]
        public void Test_MoveHeroRight_Unsuccesfull()
        {
            model.Table = new int[16, 4];

            model.Table[0, 0] = (int)PieceType.Hero;
            model.Table[0, 1] = (int)PieceType.Hero;
            model.Table[0, 2] = (int)PieceType.Hero;
            model.Table[0, 3] = (int)PieceType.Hero;

            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((0, 3));

            model.MovePieceRight();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (0, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (0, 3));

            Assert.AreEqual(model.Table[0, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[0, 1], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[0, 2], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[0, 3], (int)PieceType.Hero);

            model.Table[0, 0] = 0;
            model.Table[0, 1] = 0;
            model.Table[0, 2] = 0;
            model.Table[0, 3] = 0;

            model.Table[0, 3] = (int)PieceType.Hero;
            model.Table[1, 3] = (int)PieceType.Hero;
            model.Table[2, 3] = (int)PieceType.Hero;
            model.Table[3, 3] = (int)PieceType.Hero;

            model.CurrentPiece.Coordinates.Add((0, 3));
            model.CurrentPiece.Coordinates.Add((1, 3));
            model.CurrentPiece.Coordinates.Add((2, 3));
            model.CurrentPiece.Coordinates.Add((3, 3));

            model.MovePieceRight();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 3));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 3));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (2, 3));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (3, 3));

            Assert.AreEqual(model.Table[0, 3], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[1, 3], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[2, 3], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[3, 3], (int)PieceType.Hero);
        }
        [TestMethod]
        public void Test_MoveHeroLeft_Succesfull()
        {
            model.Table = new int[16, 8];

            model.Table[0, 1] = (int)PieceType.Hero;
            model.Table[0, 2] = (int)PieceType.Hero;
            model.Table[0, 3] = (int)PieceType.Hero;
            model.Table[0, 4] = (int)PieceType.Hero;

            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((0, 3));
            model.CurrentPiece.Coordinates.Add((0, 4));

            model.MovePieceLeft();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (0, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (0, 3));

            Assert.AreEqual(model.Table[0, 4], 0);

            Assert.AreEqual(model.Table[0, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[0, 1], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[0, 2], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[0, 3], (int)PieceType.Hero);

            model.Table[0, 0] = 0;
            model.Table[0, 1] = 0;
            model.Table[0, 2] = 0;
            model.Table[0, 3] = 0;

            model.Table[0, 1] = (int)PieceType.Hero;
            model.Table[1, 1] = (int)PieceType.Hero;
            model.Table[2, 1] = (int)PieceType.Hero;
            model.Table[3, 1] = (int)PieceType.Hero;

            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((1, 1));
            model.CurrentPiece.Coordinates.Add((2, 1));
            model.CurrentPiece.Coordinates.Add((3, 1));

            model.MovePieceLeft();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (2, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (3, 0));

            Assert.AreEqual(model.Table[0, 1], 0);
            Assert.AreEqual(model.Table[1, 1], 0);
            Assert.AreEqual(model.Table[2, 1], 0);
            Assert.AreEqual(model.Table[3, 1], 0);

            Assert.AreEqual(model.Table[0, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[1, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[2, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[3, 0], (int)PieceType.Hero);
        }
        [TestMethod]
        public void Test_MoveHeroLeft_Unsuccesfull()
        {
            model.Table = new int[16, 4];

            model.Table[0, 0] = (int)PieceType.Hero;
            model.Table[0, 1] = (int)PieceType.Hero;
            model.Table[0, 2] = (int)PieceType.Hero;
            model.Table[0, 3] = (int)PieceType.Hero;

            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((0, 3));

            model.MovePieceLeft();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (0, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (0, 3));

            Assert.AreEqual(model.Table[0, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[0, 1], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[0, 2], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[0, 3], (int)PieceType.Hero);

            model.Table[0, 0] = 0;
            model.Table[0, 1] = 0;
            model.Table[0, 2] = 0;
            model.Table[0, 3] = 0;

            model.Table[0, 0] = (int)PieceType.Hero;
            model.Table[1, 0] = (int)PieceType.Hero;
            model.Table[2, 0] = (int)PieceType.Hero;
            model.Table[3, 0] = (int)PieceType.Hero;

            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((2, 0));
            model.CurrentPiece.Coordinates.Add((3, 0));

            model.MovePieceLeft();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (2, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (3, 0));

            Assert.AreEqual(model.Table[0, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[1, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[2, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[3, 0], (int)PieceType.Hero);
        }
        [TestMethod]
        public void Test_MoveHeroDown_Succesfull()
        {
            model.Table = new int[16, 4];

            model.Table[0, 0] = (int)PieceType.Hero;
            model.Table[0, 1] = (int)PieceType.Hero;
            model.Table[0, 2] = (int)PieceType.Hero;
            model.Table[0, 3] = (int)PieceType.Hero;

            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((0, 3));

            model.MovePieceDown();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (1, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (1, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (1, 3));

            Assert.AreEqual(model.Table[0, 0], 0);
            Assert.AreEqual(model.Table[0, 1], 0);
            Assert.AreEqual(model.Table[0, 2], 0);
            Assert.AreEqual(model.Table[0, 3], 0);

            Assert.AreEqual(model.Table[1, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[1, 1], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[1, 2], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[1, 3], (int)PieceType.Hero);

            model.Table[1, 0] = 0;
            model.Table[1, 1] = 0;
            model.Table[1, 2] = 0;
            model.Table[1, 3] = 0;

            model.Table[0, 0] = (int)PieceType.Hero;
            model.Table[1, 0] = (int)PieceType.Hero;
            model.Table[2, 0] = (int)PieceType.Hero;
            model.Table[3, 0] = (int)PieceType.Hero;

            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((2, 0));
            model.CurrentPiece.Coordinates.Add((3, 0));

            model.MovePieceDown();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (1, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (2, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (3, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (4, 0));

            Assert.AreEqual(model.Table[0, 0], 0);

            Assert.AreEqual(model.Table[1, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[2, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[3, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[4, 0], (int)PieceType.Hero);
        }
        [TestMethod]
        public void Test_MoveHeroDown_Unsuccesfull()
        {
            model.Table = new int[16, 4];

            model.Table[15, 0] = (int)PieceType.Hero;
            model.Table[15, 1] = (int)PieceType.Hero;
            model.Table[15, 2] = (int)PieceType.Hero;
            model.Table[15, 3] = (int)PieceType.Hero;

            model.CurrentPiece.Coordinates.Add((15, 0));
            model.CurrentPiece.Coordinates.Add((15, 1));
            model.CurrentPiece.Coordinates.Add((15, 2));
            model.CurrentPiece.Coordinates.Add((15, 3));

            model.MovePieceDown();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (15, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (15, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (15, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (15, 3));

            Assert.AreEqual(model.Table[15, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[15, 1], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[15, 2], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[15, 3], (int)PieceType.Hero);

            model.Table[15, 0] = 0;
            model.Table[15, 1] = 0;
            model.Table[15, 2] = 0;
            model.Table[15, 3] = 0;

            model.Table[12, 0] = (int)PieceType.Hero;
            model.Table[13, 0] = (int)PieceType.Hero;
            model.Table[14, 0] = (int)PieceType.Hero;
            model.Table[15, 0] = (int)PieceType.Hero;

            model.CurrentPiece.Coordinates.Add((12, 0));
            model.CurrentPiece.Coordinates.Add((13, 0));
            model.CurrentPiece.Coordinates.Add((14, 0));
            model.CurrentPiece.Coordinates.Add((15, 0));

            model.MovePieceDown();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (12, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (13, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (14, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (15, 0));

            Assert.AreEqual(model.Table[12, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[13, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[14, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[15, 0], (int)PieceType.Hero);
        }
        [TestMethod]
        public void Test_RotateHeroRight_Succesfull()
        {
            model.Table = new int[16, 8];

            model.Table[3, 0] = (int)PieceType.Hero;
            model.Table[3, 1] = (int)PieceType.Hero;
            model.Table[3, 2] = (int)PieceType.Hero;
            model.Table[3, 3] = (int)PieceType.Hero;

            model.CurrentPiece.Type = PieceType.Hero;
            model.CurrentPiece.Direction = PieceDirection.Up;

            model.CurrentPiece.Coordinates.Add((3, 0));
            model.CurrentPiece.Coordinates.Add((3, 1));
            model.CurrentPiece.Coordinates.Add((3, 2));
            model.CurrentPiece.Coordinates.Add((3, 3));

            model.RotatePiece();

            Assert.AreEqual(model.Table[3, 0], 0);
            Assert.AreEqual(model.Table[3, 1], 0);
            Assert.AreEqual(model.Table[3, 2], 0);

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (3, 3));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (2, 3));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (1, 3));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (0, 3));

            Assert.AreEqual(model.Table[3, 3], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[2, 3], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[1, 3], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[0, 3], (int)PieceType.Hero);
        }
        [TestMethod]
        public void Test_RotateHeroRight_Unsuccesfull()
        {
            model.Table = new int[16, 4];

            model.Table[0, 0] = (int)PieceType.Hero;
            model.Table[0, 1] = (int)PieceType.Hero;
            model.Table[0, 2] = (int)PieceType.Hero;
            model.Table[0, 3] = (int)PieceType.Hero;

            model.CurrentPiece.Type = PieceType.Hero;
            model.CurrentPiece.Direction = PieceDirection.Down;

            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((0, 3));

            model.RotatePiece();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (0, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (0, 3));

            Assert.AreEqual(model.Table[0, 3], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[1, 3], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[2, 3], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[3, 3], (int)PieceType.Hero);
        }
        [TestMethod]
        public void Test_RotateHeroDown_Succesfull()
        {
            model.Size = 8;
            model.Table = new int[16, 8];

            model.Table[0, 0] = (int)PieceType.Hero;
            model.Table[1, 0] = (int)PieceType.Hero;
            model.Table[2, 0] = (int)PieceType.Hero;
            model.Table[3, 0] = (int)PieceType.Hero;

            model.CurrentPiece.Type = PieceType.Hero;
            model.CurrentPiece.Direction = PieceDirection.Right;

            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((2, 0));
            model.CurrentPiece.Coordinates.Add((3, 0));

            model.RotatePiece();

            Assert.AreEqual(model.Table[0, 0], 0);
            Assert.AreEqual(model.Table[1, 0], 0);
            Assert.AreEqual(model.Table[2, 0], 0);

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (3, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (3, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (3, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (3, 3));

            Assert.AreEqual(model.Table[3, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[3, 1], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[3, 2], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[3, 3], (int)PieceType.Hero);
        }
        [TestMethod]
        public void Test_RotateHeroDown_Unsuccesfull()
        {

        }
        public void Test_RotateHeroLeft_Succesfull()
        {
            model.Size = 8;
            model.Table = new int[16, 8];

            model.Table[0, 0] = (int)PieceType.Hero;
            model.Table[0, 1] = (int)PieceType.Hero;
            model.Table[0, 2] = (int)PieceType.Hero;
            model.Table[0, 3] = (int)PieceType.Hero;

            model.CurrentPiece.Type = PieceType.Hero;
            model.CurrentPiece.Direction = PieceDirection.Down;

            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((0, 3));

            model.RotatePiece();

            Assert.AreEqual(model.Table[0, 0], 0);
            Assert.AreEqual(model.Table[0, 1], 0);
            Assert.AreEqual(model.Table[0, 2], 0);

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 3));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 3));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (2, 3));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (3, 3));

            Assert.AreEqual(model.Table[0, 3], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[1, 3], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[2, 3], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[3, 3], (int)PieceType.Hero);
        }
        [TestMethod]
        public void Test_RotateHeroUp_Succesfull()
        {
            model.Size = 8;
            model.Table = new int[16, 8];

            model.Table[0, 0] = (int)PieceType.Hero;
            model.Table[1, 0] = (int)PieceType.Hero;
            model.Table[2, 0] = (int)PieceType.Hero;
            model.Table[3, 0] = (int)PieceType.Hero;

            model.CurrentPiece.Type = PieceType.Hero;
            model.CurrentPiece.Direction = PieceDirection.Left;

            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((2, 0));
            model.CurrentPiece.Coordinates.Add((3, 0));

            model.RotatePiece();

            Assert.AreEqual(model.Table[0, 0], 0);
            Assert.AreEqual(model.Table[1, 0], 0);
            Assert.AreEqual(model.Table[2, 0], 0);

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (3, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (3, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (3, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (3, 3));

            Assert.AreEqual(model.Table[3, 0], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[3, 1], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[3, 2], (int)PieceType.Hero);
            Assert.AreEqual(model.Table[3, 3], (int)PieceType.Hero);
        }
        [TestMethod]
        public void Test_RotateHeroUp_Unsuccesfull()
        {

        }
        #endregion
        #region Ricky

        #endregion
        #region TeeWee

        #endregion
        #region Z

        #endregion
        #endregion
        #region Insertion
        [TestMethod]
        public void Test_InsertPiece_NoOtherPiece()
        {

        }
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
            mockedSaveFile += "2 0 0 5\n";
            mockedSaveFile += "2 0 5 5\n";
            mockedSaveFile += "2 5 0 5\n";
            mockedSaveFile += "5 5 5 0\n";
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
