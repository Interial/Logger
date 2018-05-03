using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Xml;
using Microsoft.Win32;

namespace ConsoleApplication2
{
   public abstract class LogBase
   {
       protected readonly object lockObj = new object();
        public abstract void Log(string message);

   }

    #region FileLogger

    

    
    public class FileLogger : LogBase
    {
        public string filePath = @"C:\Intel\Logs\Intel.txt";
        public override void Log(string message)
        {
            lock (lockObj)
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath))
                {
                    streamWriter.WriteLine(message);
                    streamWriter.Close();
                }
            }
        }
    }
    #endregion
    #region DBLogger
    public class DBLogger : LogBase
    {
        string connectionString = string.Empty;
        public override void Log(string message)
        {
            lock (lockObj)
            {
                


            }
            
        }
    }
    #endregion
    #region EventLogger
    public class EventLogger : LogBase
    {
        public override void Log(string message)
        {
            lock (lockObj)
            {
                EventLog eventLog = new EventLog("");
                eventLog.Source = "IDGEventLog";
                eventLog.WriteEntry(message);
            }
        }
    }
    #endregion

}
