using System;

namespace MsFakesSample
{
    public class Log
    {
        public Log(string message)
        {
            Message = message;
            CreatedDate = DateTime.UtcNow;
        }

        public DateTime CreatedDate { get; private set; }
        public string Message { get; private set; }
    }

    public interface ILogWritter
    {
        Log AddLog();
    }

    public class LogWritter : ILogWritter
    {
        public LogWritter(IDataProvider dataProvider, IFileWriter fileWriter)
        {
            _dataProvider = dataProvider;
            _fileWriter = fileWriter;
        }

        private readonly IDataProvider _dataProvider;
        private readonly IFileWriter _fileWriter;

        public Log AddLog()
        {
            var message = _dataProvider.GetData();
            var log = new Log(message);

            _fileWriter.WriteFile(log);

            return log;
        }
    }

    public interface IDataProvider
    {
        string GetData();
    }

    public interface IFileWriter
    {
        bool WriteFile(object obj);
    }
}


