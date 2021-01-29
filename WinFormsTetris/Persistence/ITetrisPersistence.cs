using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsTetris.Persistence
{
    public interface ITetrisPersistence
    {
        Task LoadAsync(String path);
        Task SaveAsync(String path);
    }
}
