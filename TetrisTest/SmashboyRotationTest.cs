using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinFormsTetris.Model;
using WinFormsTetris.Persistence;
using System.Threading.Tasks;
using System;

namespace TetrisTest
{
    [TestClass]
    public class SmashboyRotationTest
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
        #region Hero
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
    }
}
