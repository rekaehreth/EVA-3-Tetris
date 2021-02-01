using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WinFormsTetris.Model;

namespace TetrisTest
{
    [TestClass]
    public class RickyMovementTest
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
            model.CurrentPiece.Type = PieceType.Ricky;
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
        public void Test_MoveRickyRight_Succesfull()
        {
            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((2, 0));
            model.CurrentPiece.Coordinates.Add((2, 1));

            model.Table[0, 0] = (int)PieceType.Ricky + 1;
            model.Table[1, 0] = (int)PieceType.Ricky + 1;
            model.Table[2, 0] = (int)PieceType.Ricky + 1;
            model.Table[2, 1] = (int)PieceType.Ricky + 1;

            model.MovePieceRight();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (2, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (2, 2));

            Assert.AreEqual(model.Table[0, 0], 0);
            Assert.AreEqual(model.Table[1, 0], 0);
            Assert.AreEqual(model.Table[2, 0], 0);

            Assert.AreEqual(model.Table[0, 1], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[1, 1], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[2, 1], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[2, 2], (int)PieceType.Ricky + 1);
        }
        [TestMethod]
        public void Test_MoveRickyRight_Unsuccesfull()
        {
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((1, 2));
            model.CurrentPiece.Coordinates.Add((2, 2));
            model.CurrentPiece.Coordinates.Add((2, 3));

            model.MovePieceRight();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (2, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (2, 3));

            Assert.AreEqual(model.Table[0, 2], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[1, 2], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[2, 2], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[2, 3], (int)PieceType.Ricky + 1);

        }
        [TestMethod]
        public void Test_MoveRickyLeft_Succesfull()
        {
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((1, 2));
            model.CurrentPiece.Coordinates.Add((2, 2));
            model.CurrentPiece.Coordinates.Add((2, 3));

            model.Table[0, 2] = (int)PieceType.Ricky + 1;
            model.Table[1, 2] = (int)PieceType.Ricky + 1;
            model.Table[2, 2] = (int)PieceType.Ricky + 1;
            model.Table[2, 3] = (int)PieceType.Ricky + 1;

            model.MovePieceLeft();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (2, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (2, 2));

            Assert.AreEqual(model.Table[0, 2], 0);
            Assert.AreEqual(model.Table[1, 2], 0);
            Assert.AreEqual(model.Table[2, 3], 0);

            Assert.AreEqual(model.Table[0, 1], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[1, 1], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[2, 1], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[2, 2], (int)PieceType.Ricky + 1);
        }
        [TestMethod]
        public void Test_MoveRickyLeft_Unsuccesfull()
        {
            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((2, 0));
            model.CurrentPiece.Coordinates.Add((2, 1));

            model.MovePieceLeft();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (2, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (2, 1));

            Assert.AreEqual(model.Table[0, 0], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[1, 0], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[2, 0], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[2, 1], (int)PieceType.Ricky + 1);
        }
        [TestMethod]
        public void Test_MoveRickyDown_Succesfull()
        {
            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((2, 0));
            model.CurrentPiece.Coordinates.Add((2, 1));

            model.Table[0, 0] = (int)PieceType.Ricky + 1;
            model.Table[1, 0] = (int)PieceType.Ricky + 1;
            model.Table[2, 0] = (int)PieceType.Ricky + 1;
            model.Table[2, 1] = (int)PieceType.Ricky + 1;

            model.MovePieceDown();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (1, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (2, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (3, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (3, 1));

            Assert.AreEqual(model.Table[0, 0], 0);
            Assert.AreEqual(model.Table[2, 1], 0);

            Assert.AreEqual(model.Table[1, 0], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[2, 0], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[3, 0], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[3, 1], (int)PieceType.Ricky + 1);
        }
        [TestMethod]
        public void Test_MoveRickyDown_Unsuccesfull()
        {
            model.CurrentPiece.Coordinates.Add((5, 1));
            model.CurrentPiece.Coordinates.Add((6, 1));
            model.CurrentPiece.Coordinates.Add((7, 1));
            model.CurrentPiece.Coordinates.Add((7, 2));

            model.Table[5, 0] = (int)PieceType.Ricky + 1;
            model.Table[6, 1] = (int)PieceType.Ricky + 1;
            model.Table[7, 1] = (int)PieceType.Ricky + 1;
            model.Table[7, 2] = (int)PieceType.Ricky + 1;

            model.Table[8, 0] = (int)PieceType.Smashboy + 1;
            model.Table[8, 1] = (int)PieceType.Smashboy + 1;
            model.Table[9, 0] = (int)PieceType.Smashboy + 1;
            model.Table[9, 1] = (int)PieceType.Smashboy + 1;

            model.MovePieceDown();

            Assert.AreNotEqual(model.CurrentPiece.Coordinates[0], (5, 1));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[1], (6, 1));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[2], (7, 1));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[3], (7, 2));

            Assert.AreEqual(model.Table[5, 1], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[6, 1], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[7, 1], (int)PieceType.Ricky + 1);
            Assert.AreEqual(model.Table[7, 2], (int)PieceType.Ricky + 1);
        }
    }
}
