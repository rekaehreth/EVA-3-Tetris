using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WinFormsTetris.Model;

namespace TetrisTest
{
    [TestClass]
    public class TeeWeeMovementTest
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
            model.CurrentPiece.Type = PieceType.TeeWee;
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
        public void Test_MoveTeeWeeRight_Succesfull()
        {
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((1, 1));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((1, 2));

            model.Table[1, 0] = (int)PieceType.TeeWee + 1;
            model.Table[1, 1] = (int)PieceType.TeeWee + 1;
            model.Table[0, 1] = (int)PieceType.TeeWee + 1;
            model.Table[1, 2] = (int)PieceType.TeeWee + 1;

            model.MovePieceRight();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (0, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (1, 3));

            Assert.AreEqual(model.Table[1, 0], 0);
            Assert.AreEqual(model.Table[0, 1], 0);

            Assert.AreEqual(model.Table[1, 1], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[1, 2], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[0, 2], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[1, 3], (int)PieceType.TeeWee + 1);
        }
        [TestMethod]
        public void Test_MoveTeeWeeRight_Unsuccesfull()
        {
            model.CurrentPiece.Coordinates.Add((1, 1));
            model.CurrentPiece.Coordinates.Add((1, 2));
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((1, 3));

            model.MovePieceRight();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (0, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (1, 3));

            Assert.AreEqual(model.Table[1, 1], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[1, 2], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[0, 2], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[1, 3], (int)PieceType.TeeWee + 1);

        }
        [TestMethod]
        public void Test_MoveTeeWeeLeft_Succesfull()
        {
            model.CurrentPiece.Coordinates.Add((1, 1));
            model.CurrentPiece.Coordinates.Add((1, 2));
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((1, 3));

            model.Table[1, 1] = (int)PieceType.TeeWee + 1;
            model.Table[1, 2] = (int)PieceType.TeeWee + 1;
            model.Table[0, 2] = (int)PieceType.TeeWee + 1;
            model.Table[1, 3] = (int)PieceType.TeeWee + 1;

            model.MovePieceLeft();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (1, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (1, 2));

            Assert.AreEqual(model.Table[1, 3], 0);
            Assert.AreEqual(model.Table[0, 2], 0);

            Assert.AreEqual(model.Table[1, 0], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[1, 1], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[0, 1], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[1, 2], (int)PieceType.TeeWee + 1);
        }
        [TestMethod]
        public void Test_MoveTeeWeeLeft_Unsuccesfull()
        {
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((1, 1));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((1, 2));

            model.MovePieceLeft();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (1, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (1, 2));

            Assert.AreEqual(model.Table[1, 0], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[1, 1], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[0, 1], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[1, 2], (int)PieceType.TeeWee + 1);
        }
        [TestMethod]
        public void Test_MoveTeeWeeDown_Succesfull()
        {
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((1, 1));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((1, 2));

            model.Table[1, 0] = (int)PieceType.TeeWee + 1;
            model.Table[1, 1] = (int)PieceType.TeeWee + 1;
            model.Table[0, 1] = (int)PieceType.TeeWee + 1;
            model.Table[1, 2] = (int)PieceType.TeeWee + 1;

            model.MovePieceDown();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (2, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (2, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (2, 2));

            Assert.AreEqual(model.Table[1, 0], 0);
            Assert.AreEqual(model.Table[0, 1], 0);
            Assert.AreEqual(model.Table[1, 2], 0);

            Assert.AreEqual(model.Table[2, 0], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[2, 1], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[1, 1], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[2, 2], (int)PieceType.TeeWee + 1);
        }
        [TestMethod]
        public void Test_MoveTeeWeeDown_Unsuccesfull()
        {
            model.CurrentPiece.Coordinates.Add((5, 0));
            model.CurrentPiece.Coordinates.Add((5, 1));
            model.CurrentPiece.Coordinates.Add((4, 1));
            model.CurrentPiece.Coordinates.Add((5, 2));

            model.Table[5, 0] = (int)PieceType.TeeWee + 1;
            model.Table[5, 1] = (int)PieceType.TeeWee + 1;
            model.Table[4, 1] = (int)PieceType.TeeWee + 1;
            model.Table[4, 2] = (int)PieceType.TeeWee + 1;

            model.Table[6, 0] = (int)PieceType.Smashboy + 1;
            model.Table[6, 1] = (int)PieceType.Smashboy + 1;
            model.Table[7, 0] = (int)PieceType.Smashboy + 1;
            model.Table[7, 1] = (int)PieceType.Smashboy + 1;

            model.MovePieceDown();

            Assert.AreNotEqual(model.CurrentPiece.Coordinates[0], (5, 0));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[1], (5, 1));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[2], (4, 1));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[3], (5, 2));

            Assert.AreEqual(model.Table[5, 0], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[5, 1], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[4, 1], (int)PieceType.TeeWee + 1);
            Assert.AreEqual(model.Table[5, 2], (int)PieceType.TeeWee + 1);
        }
    }
}
