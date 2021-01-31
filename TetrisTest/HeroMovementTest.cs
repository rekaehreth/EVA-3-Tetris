using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinFormsTetris.Model;
using WinFormsTetris.Persistence;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace TetrisTest
{
    [TestClass]
    public class HeroMovementTest
    {
        private TetrisModel model;
        [TestInitialize]
        public void Initialize()
        {
            model = new TetrisModel();
            model.GameActive = true;
            model.CurrentPiece = new TetrisPiece();
            model.CurrentPiece.Coordinates = new List<(int, int)>();
        }
        [TestCleanup]
        public void CleanUp()
        {
            model.Size = 0;
            model.Table = null;
            model.CurrentPiece = null;
            model.GameActive = false;
        }
        [TestMethod]
        public void Test_MoveSmashboyRight_Succesfull()
        {
            model.Size = 4;
            model.Table = new int[16, 4];

            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((1, 1));

            model.CurrentPiece.Type = PieceType.Smashboy;

            model.Table[0, 0] = (int)PieceType.Smashboy + 1;
            model.Table[0, 1] = (int)PieceType.Smashboy + 1;
            model.Table[1, 0] = (int)PieceType.Smashboy + 1;
            model.Table[1, 1] = (int)PieceType.Smashboy + 1;

            model.MovePieceRight();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (0, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (1, 2));

            Assert.AreEqual(model.Table[0, 0], 0);
            Assert.AreEqual(model.Table[1, 0], 0);

            Assert.AreEqual(model.Table[0, 1], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[0, 2], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[1, 1], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[1, 2], (int)PieceType.Smashboy + 1);
        }
        [TestMethod]
        public void Test_MoveSmashboyRight_Unsuccesfull()
        {
            model.Table = new int[16, 4];

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
    }
}
