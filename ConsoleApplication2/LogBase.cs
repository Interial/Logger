using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Configuration;

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
    // I tu się zaplątałem i mam problem jak to pogodzić.
    public class DBLogger : LogBase
    {

        

        public void Log(Exception exception)
        {
            lock (lockObj)
            {
             StringBuilder sbExceptionMessage = new StringBuilder();

                do
                {
                    sbExceptionMessage.Append("Exception Type" + Environment.NewLine);
                    sbExceptionMessage.Append(exception.GetType().Name);
                    sbExceptionMessage.Append(Environment.NewLine + Environment.NewLine);
                    sbExceptionMessage.Append("Message" + Environment.NewLine);
                    sbExceptionMessage.Append("Stack Trace" + Environment.NewLine);
                    sbExceptionMessage.Append(exception.StackTrace + Environment.NewLine + Environment.NewLine);

                    exception = exception.InnerException;
                } while (exception != null);

                string cs = ConfigurationManager.ConnectionStrings["CustomLoggerDB"].ConnectionString;
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("spInsertLog", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ExceptionMessage", sbExceptionMessage.ToString());
                cmd.Parameters.Add(param);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();





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
