using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WinFormsTetris.Persistence;
using WinFormsTetris.Model;

namespace TetrisTest
{
    class MockTetrisPersistence : ITetrisPersistence
    {

        public async Task SaveAsync(string path)
        {
            return;
        }
        public async Task LoadAsync(string path)
        {
            return;
        }
    }
}
