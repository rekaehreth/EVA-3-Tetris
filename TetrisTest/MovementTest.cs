using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinFormsTetris.Model;
using WinFormsTetris.Persistence;
using System.Threading.Tasks;
using System;

namespace TetrisTest
{
    [TestClass]
    class MovementTest
    {
        private TetrisModel model;
        private PieceType GetTypeAtPosition(int line, int row)
        {
            return (PieceType)(model.Table[line, row] + 1);
        }
        [TestInitialize]
        public void Initialize()
        {
            model = new TetrisModel();
            model.GameActive = true;
        }
        [TestCleanup]
        public void CleanUp()
        {
            model.Size = 0;
            model.Table = null;
            model.CurrentPiece = null;
            model.GameActive = false;
        }
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
        #endregion
    }
}
