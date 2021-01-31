using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinFormsTetris.Model;
using WinFormsTetris.Persistence;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace TetrisTest
{
    [TestClass]
    public class SmashboyMovementTest
    {
        private TetrisModel model;
        [TestInitialize]
        public void Initialize()
        {
            model = new TetrisModel();
            model.GameActive = true;
            model.CurrentPiece = new TetrisPiece();
            model.CurrentPiece.Coordinates = new List<(int, int)>();
            model.Size = 4;
            model.Table = new int[16, 4];
            model.CurrentPiece.Type = PieceType.Smashboy;
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
            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((1, 1));

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
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((0, 3));
            model.CurrentPiece.Coordinates.Add((1, 2));
            model.CurrentPiece.Coordinates.Add((1, 3));

            model.MovePieceRight();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (0, 3));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (1, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (1, 3));

            Assert.AreEqual(model.Table[0, 2], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[0, 3], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[1, 2], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[1, 3], (int)PieceType.Smashboy + 1);

        }
        [TestMethod]
        public void Test_MoveSmashboyLeft_Succesfull()
        {
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((1, 1));
            model.CurrentPiece.Coordinates.Add((1, 2));

            model.Table[0, 1] = (int)PieceType.Smashboy + 1;
            model.Table[0, 2] = (int)PieceType.Smashboy + 1;
            model.Table[1, 1] = (int)PieceType.Smashboy + 1;
            model.Table[1, 2] = (int)PieceType.Smashboy + 1;

            model.MovePieceLeft();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (1, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (1, 1));

            Assert.AreEqual(model.Table[0, 2], 0);
            Assert.AreEqual(model.Table[1, 2], 0);

            Assert.AreEqual(model.Table[0, 0], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[0, 1], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[1, 0], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[1, 1], (int)PieceType.Smashboy + 1);
        }
        [TestMethod]
        public void Test_MoveSmashboyLeft_Unsuccesfull()
        {
            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((1, 1));

            model.MovePieceLeft();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (1, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (1, 1));

            Assert.AreEqual(model.Table[0, 0], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[0, 1], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[1, 0], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[1, 1], (int)PieceType.Smashboy + 1);
        }
        [TestMethod]
        public void Test_MoveSmashboyDown_Succesfull()
        {
            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((1, 1));

            model.Table[0, 0] = (int)PieceType.Smashboy + 1;
            model.Table[0, 1] = (int)PieceType.Smashboy + 1;
            model.Table[1, 0] = (int)PieceType.Smashboy + 1;
            model.Table[1, 1] = (int)PieceType.Smashboy + 1;

            model.MovePieceDown();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (1, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (2, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (2, 1));

            Assert.AreEqual(model.Table[0, 0], 0);
            Assert.AreEqual(model.Table[0, 1], 0);

            Assert.AreEqual(model.Table[1, 0], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[2, 1], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[1, 0], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[2, 1], (int)PieceType.Smashboy + 1);
        }
        [TestMethod]
        public void Test_MoveSmashboyDown_Unsuccesfull()
        {
            model.CurrentPiece.Coordinates.Add((3, 0));
            model.CurrentPiece.Coordinates.Add((3, 1));
            model.CurrentPiece.Coordinates.Add((4, 0));
            model.CurrentPiece.Coordinates.Add((4, 1));

            model.Table[3, 0] = (int)PieceType.Smashboy + 1;
            model.Table[3, 1] = (int)PieceType.Smashboy + 1;
            model.Table[4, 0] = (int)PieceType.Smashboy + 1;
            model.Table[4, 1] = (int)PieceType.Smashboy + 1;

            model.Table[5, 0] = (int)PieceType.Hero + 1;
            model.Table[5, 1] = (int)PieceType.Hero + 1;
            model.Table[5, 2] = (int)PieceType.Hero + 1;
            model.Table[5, 3] = (int)PieceType.Hero + 1;

            model.MovePieceDown();

            // a new CurrentPiece is generated
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[0], (3, 0));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[1], (3, 1));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[2], (4, 0));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[3], (4, 1));

            // RemoveFullLines removes Hero and moves everything further down
            Assert.AreEqual(model.Table[4, 0], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[4, 1], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[5, 0], (int)PieceType.Smashboy + 1);
            Assert.AreEqual(model.Table[5, 1], (int)PieceType.Smashboy + 1);
        }
    }
}
