using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WinFormsTetris.Model;

namespace TetrisTest
{
    [TestClass]
    public class TeeWeeRotationTest
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
        public void Test_RotateSmashboy_UpToRight_Succesfull()
        {
            model.CurrentPiece.Direction = PieceDirection.Up;
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((1, 1));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((1, 2));

            for (int i = 0; i < 4; ++i)
            {
                model.Table[model.CurrentPiece.Coordinates[i].Item1, model.CurrentPiece.Coordinates[i].Item2] = (int)PieceType.TeeWee + 1;
            }

            model.RotatePiece();

            Assert.AreEqual(model.CurrentPiece.Direction, PieceDirection.Right);

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (1, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (2, 1));

            Assert.AreEqual(model.Table[1, 0], 0);

            for (int i = 0; i < 4; ++i)
            {
                Assert.AreEqual(model.Table[model.CurrentPiece.Coordinates[i].Item1, model.CurrentPiece.Coordinates[i].Item2], (int)PieceType.TeeWee + 1);
            }
        }
        [TestMethod]
        public void Test_RotateSmashboy_UpToRight_Unsuccesfull()
        {
            model.CurrentPiece.Direction = PieceDirection.Up;
            model.CurrentPiece.Coordinates.Add((15, 0));
            model.CurrentPiece.Coordinates.Add((15, 1));
            model.CurrentPiece.Coordinates.Add((14, 1));
            model.CurrentPiece.Coordinates.Add((15, 2));

            for (int i = 0; i < 4; ++i)
            {
                model.Table[model.CurrentPiece.Coordinates[i].Item1, model.CurrentPiece.Coordinates[i].Item2] = (int)PieceType.TeeWee + 1;
            }

            model.RotatePiece();

            Assert.AreEqual(model.CurrentPiece.Direction, PieceDirection.Up);

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (15, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (15, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (14, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (15, 2));

            for (int i = 0; i < 4; ++i)
            {
                Assert.AreEqual(model.Table[model.CurrentPiece.Coordinates[i].Item1, model.CurrentPiece.Coordinates[i].Item2], (int)PieceType.TeeWee + 1);
            }
        }
        [TestMethod]
        public void Test_RotateSmashboy_RightToDown_Succesfull()
        {
            model.CurrentPiece.Direction = PieceDirection.Right;
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((1, 1));
            model.CurrentPiece.Coordinates.Add((1, 2));
            model.CurrentPiece.Coordinates.Add((2, 1));

            for (int i = 0; i < 4; ++i)
            {
                model.Table[model.CurrentPiece.Coordinates[i].Item1, model.CurrentPiece.Coordinates[i].Item2] = (int)PieceType.TeeWee + 1;
            }

            model.RotatePiece();

            Assert.AreEqual(model.CurrentPiece.Direction, PieceDirection.Down);

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (1, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (2, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (1, 0));

            Assert.AreEqual(model.Table[0, 1], 0);

            for (int i = 0; i < 4; ++i)
            {
                Assert.AreEqual(model.Table[model.CurrentPiece.Coordinates[i].Item1, model.CurrentPiece.Coordinates[i].Item2], (int)PieceType.TeeWee + 1);
            }
        }
        [TestMethod]
        public void Test_RotateSmashboy_RightToDown_Unsuccesfull()
        {
            model.CurrentPiece.Direction = PieceDirection.Right;
            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((1, 1));
            model.CurrentPiece.Coordinates.Add((2, 0));

            for (int i = 0; i < 4; ++i)
            {
                model.Table[model.CurrentPiece.Coordinates[i].Item1, model.CurrentPiece.Coordinates[i].Item2] = (int)PieceType.TeeWee + 1;
            }

            model.RotatePiece();

            Assert.AreEqual(model.CurrentPiece.Direction, PieceDirection.Right);

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (2, 0));

            for (int i = 0; i < 4; ++i)
            {
                Assert.AreEqual(model.Table[model.CurrentPiece.Coordinates[i].Item1, model.CurrentPiece.Coordinates[i].Item2], (int)PieceType.TeeWee + 1);
            }
        }
        [TestMethod]
        public void Test_RotateSmashboy_DownToLeft_Succesfull()
        {
            model.CurrentPiece.Direction = PieceDirection.Down;
            model.CurrentPiece.Coordinates.Add((1, 2));
            model.CurrentPiece.Coordinates.Add((1, 1));
            model.CurrentPiece.Coordinates.Add((2, 1));
            model.CurrentPiece.Coordinates.Add((1, 0));

            for (int i = 0; i < 4; ++i)
            {
                model.Table[model.CurrentPiece.Coordinates[i].Item1, model.CurrentPiece.Coordinates[i].Item2] = (int)PieceType.TeeWee + 1;
            }

            model.RotatePiece();

            Assert.AreEqual(model.CurrentPiece.Direction, PieceDirection.Left);

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (2, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (1, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (0, 1));

            Assert.AreEqual(model.Table[1, 2], 0);

            for (int i = 0; i < 4; ++i)
            {
                Assert.AreEqual(model.Table[model.CurrentPiece.Coordinates[i].Item1, model.CurrentPiece.Coordinates[i].Item2], (int)PieceType.TeeWee + 1);
            }
        }
        [TestMethod]
        public void Test_RotateSmashboy_DownToLeft_Unsuccesfull()
        {
            model.CurrentPiece.Direction = PieceDirection.Down;
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((1, 1));
            model.CurrentPiece.Coordinates.Add((0, 0));

            for (int i = 0; i < 4; ++i)
            {
                model.Table[model.CurrentPiece.Coordinates[i].Item1, model.CurrentPiece.Coordinates[i].Item2] = (int)PieceType.TeeWee + 1;
            }

            model.RotatePiece();

            Assert.AreEqual(model.CurrentPiece.Direction, PieceDirection.Down);

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (0, 0));

            for (int i = 0; i < 4; ++i)
            {
                Assert.AreEqual(model.Table[model.CurrentPiece.Coordinates[i].Item1, model.CurrentPiece.Coordinates[i].Item2], (int)PieceType.TeeWee + 1);
            }
        }
        [TestMethod]
        public void Test_RotateSmashboy_LeftToUp_Succesfull()
        {
            model.CurrentPiece.Direction = PieceDirection.Left;
            model.CurrentPiece.Coordinates.Add((2, 1));
            model.CurrentPiece.Coordinates.Add((1, 1));
            model.CurrentPiece.Coordinates.Add((1, 0));
            model.CurrentPiece.Coordinates.Add((0, 1));

            for (int i = 0; i < 4; ++i)
            {
                model.Table[model.CurrentPiece.Coordinates[i].Item1, model.CurrentPiece.Coordinates[i].Item2] = (int)PieceType.TeeWee + 1;
            }

            model.RotatePiece();

            Assert.AreEqual(model.CurrentPiece.Direction, PieceDirection.Up);

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (1, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (1, 2));

            Assert.AreEqual(model.Table[2, 1], 0);

            for (int i = 0; i < 4; ++i)
            {
                Assert.AreEqual(model.Table[model.CurrentPiece.Coordinates[i].Item1, model.CurrentPiece.Coordinates[i].Item2], (int)PieceType.TeeWee + 1);
            }
        }
        [TestMethod]
        public void Test_RotateSmashboy_LeftToUp_Unsuccesfull()
        {
            model.CurrentPiece.Direction = PieceDirection.Left;
            model.CurrentPiece.Coordinates.Add((2, 3));
            model.CurrentPiece.Coordinates.Add((1, 3));
            model.CurrentPiece.Coordinates.Add((1, 2));
            model.CurrentPiece.Coordinates.Add((0, 3));

            for (int i = 0; i < 4; ++i)
            {
                model.Table[model.CurrentPiece.Coordinates[i].Item1, model.CurrentPiece.Coordinates[i].Item2] = (int)PieceType.TeeWee + 1;
            }

            model.RotatePiece();

            Assert.AreEqual(model.CurrentPiece.Direction, PieceDirection.Left);

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (2, 3));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 3));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (1, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (0, 3));

            for (int i = 0; i < 4; ++i)
            {
                Assert.AreEqual(model.Table[model.CurrentPiece.Coordinates[i].Item1, model.CurrentPiece.Coordinates[i].Item2], (int)PieceType.TeeWee + 1);
            }
        }
    }
}
