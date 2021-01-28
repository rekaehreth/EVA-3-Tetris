using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace WinFormsTetris
{
    class TetrisPersistence
    {
        private StreamReader loader;
        private StreamWriter saver;
        TetrisModel model;
        public TetrisPersistence()
        {
            
        }
        
        public async Task Save(string path)
        {
            try
            {
                saver = new StreamWriter(path);
            }
            catch (Exception)
            {
                throw new FileOperationException();
            }
        }
        public async Task Load(string path)
        {
            try
            {
                loader = new StreamReader(path);
            }
            catch (Exception)
            {
                throw new FileOperationException();
            }
        }

    }

    class FileOperationException : Exception {
        public string Message { get; set; }
        public FileOperationException(string message = null)
        {
            Message = message;
        }
    }
}
