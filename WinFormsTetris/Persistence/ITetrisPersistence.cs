using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WinFormsTetris.Model;

namespace WinFormsTetris.Persistence
{
    public interface ITetrisPersistence
    {
        public int Size { get; set; } // oszlopok száma
        public int[,] Table { get; set; }
        public TetrisPiece CurrentPiece { get; set; }
        Task LoadAsync(String path);
        Task<string> SaveAsync(String path);
    }
}
