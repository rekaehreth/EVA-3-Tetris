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
            model.Size = 8;
            model.Table = new int[16, 8];
            model.CurrentPiece.Type = PieceType.Hero;
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
        public void Test_MoveHeroRight_Succesfull()
        {
            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((0, 3));

            model.Table[0, 0] = (int)PieceType.Hero + 1;
            model.Table[0, 1] = (int)PieceType.Hero + 1;
            model.Table[0, 2] = (int)PieceType.Hero + 1;
            model.Table[0, 3] = (int)PieceType.Hero + 1;

            model.MovePieceRight();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (0, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (0, 3));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (0, 4));

            Assert.AreEqual(model.Table[0, 0], 0);

            Assert.AreEqual(model.Table[0, 1], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[0, 2], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[0, 3], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[0, 4], (int)PieceType.Hero + 1);
        }
        [TestMethod]
        public void Test_MoveHeroRight_Unsuccesfull()
        {
            model.CurrentPiece.Coordinates.Add((0, 4));
            model.CurrentPiece.Coordinates.Add((0, 5));
            model.CurrentPiece.Coordinates.Add((0, 6));
            model.CurrentPiece.Coordinates.Add((0, 7));

            model.Table[0, 4] = (int)PieceType.Hero + 1;
            model.Table[0, 5] = (int)PieceType.Hero + 1;
            model.Table[0, 6] = (int)PieceType.Hero + 1;
            model.Table[0, 7] = (int)PieceType.Hero + 1;

            model.MovePieceRight();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 4));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (0, 5));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (0, 6));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (0, 7));

            Assert.AreEqual(model.Table[0, 4], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[0, 5], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[0, 6], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[0, 7], (int)PieceType.Hero + 1);

        }
        [TestMethod]
        public void Test_MoveHeroLeft_Succesfull()
        {
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((0, 3));
            model.CurrentPiece.Coordinates.Add((0, 4));

            model.Table[0, 1] = (int)PieceType.Hero + 1;
            model.Table[0, 2] = (int)PieceType.Hero + 1;
            model.Table[0, 3] = (int)PieceType.Hero + 1;
            model.Table[0, 4] = (int)PieceType.Hero + 1;

            model.MovePieceLeft();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (0, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (0, 3));

            Assert.AreEqual(model.Table[0, 4], 0);

            Assert.AreEqual(model.Table[0, 0], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[0, 1], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[0, 2], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[0, 3], (int)PieceType.Hero + 1);
        }
        [TestMethod]
        public void Test_MoveHeroLeft_Unsuccesfull()
        {
            model.CurrentPiece.Coordinates.Add((0, 0));
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((0, 3));

            model.Table[0, 0] = (int)PieceType.Hero + 1;
            model.Table[0, 1] = (int)PieceType.Hero + 1;
            model.Table[0, 2] = (int)PieceType.Hero + 1;
            model.Table[0, 3] = (int)PieceType.Hero + 1;

            model.MovePieceLeft();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (0, 0));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (0, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (0, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (0, 3));

            Assert.AreEqual(model.Table[0, 0], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[0, 1], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[0, 2], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[0, 3], (int)PieceType.Hero + 1);
        }
        [TestMethod]
        public void Test_MoveHeroDown_Succesfull()
        {
            model.CurrentPiece.Coordinates.Add((0, 1));
            model.CurrentPiece.Coordinates.Add((0, 2));
            model.CurrentPiece.Coordinates.Add((0, 3));
            model.CurrentPiece.Coordinates.Add((0, 4));

            model.Table[0, 1] = (int)PieceType.Hero + 1;
            model.Table[0, 2] = (int)PieceType.Hero + 1;
            model.Table[0, 3] = (int)PieceType.Hero + 1;
            model.Table[0, 4] = (int)PieceType.Hero + 1;

            model.MovePieceDown();

            Assert.AreEqual(model.CurrentPiece.Coordinates[0], (1, 1));
            Assert.AreEqual(model.CurrentPiece.Coordinates[1], (1, 2));
            Assert.AreEqual(model.CurrentPiece.Coordinates[2], (1, 3));
            Assert.AreEqual(model.CurrentPiece.Coordinates[3], (1, 4));

            Assert.AreEqual(model.Table[0, 1], 0);
            Assert.AreEqual(model.Table[0, 2], 0);
            Assert.AreEqual(model.Table[0, 3], 0);
            Assert.AreEqual(model.Table[0, 4], 0);

            Assert.AreEqual(model.Table[1, 1], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[1, 2], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[1, 3], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[1, 4], (int)PieceType.Hero + 1);
        }
        [TestMethod]
        public void Test_MoveHeroDown_Unsuccesfull()
        {
            model.CurrentPiece.Coordinates.Add((2, 1));
            model.CurrentPiece.Coordinates.Add((2, 2));
            model.CurrentPiece.Coordinates.Add((2, 3));
            model.CurrentPiece.Coordinates.Add((2, 4));

            model.Table[2, 1] = (int)PieceType.Hero + 1;
            model.Table[2, 2] = (int)PieceType.Hero + 1;
            model.Table[2, 3] = (int)PieceType.Hero + 1;
            model.Table[2, 4] = (int)PieceType.Hero + 1;

            model.Table[3, 1] = (int)PieceType.Smashboy + 1;
            model.Table[3, 2] = (int)PieceType.Smashboy + 1;
            model.Table[4, 1] = (int)PieceType.Smashboy + 1;
            model.Table[4, 1] = (int)PieceType.Smashboy + 1;

            model.MovePieceDown();

            Assert.AreNotEqual(model.CurrentPiece.Coordinates[0], (2, 1));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[1], (2, 2));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[2], (2, 3));
            Assert.AreNotEqual(model.CurrentPiece.Coordinates[3], (2, 4));

            Assert.AreEqual(model.Table[2, 1], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[2, 2], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[2, 3], (int)PieceType.Hero + 1);
            Assert.AreEqual(model.Table[2, 4], (int)PieceType.Hero + 1);
        }
    }
}
