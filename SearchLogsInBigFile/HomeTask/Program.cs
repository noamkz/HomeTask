using System;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace HomeTask
{
    class Program
    {
        #region Constant
        enum Const
        {
            EOF = -1,
            MinLength = 19
        }

        #endregion

        #region helper function
        private static long GetTheFileLength(string fileName)
        {
            try
            {
                FileStream stream = File.Open(fileName, FileMode.Open);
                long startLength = stream.Length;
                stream.Close();

                return startLength;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        private static string TrimRow(StringBuilder resultAsString)
        {
            char[] delmiters = { '\n', '\t', '\r' };
            string row = resultAsString.ToString().Trim(delmiters);

            return row;
        }

        private static DateTime GetDateFromRow(string row)
        {
            string todate = row.Remove((int)Const.MinLength);

            DateTime rowDate = new DateTime();
            DateTime.TryParse(todate, out rowDate);

            return rowDate;
        }

        #endregion

        #region Main Function
        /// <summary>
        /// run on the file from start to the first one that date is later from fromDate
        /// print the log that find, great for very big files
        /// </summary>
        /// <param name="fileName">the name or the path of the file</param>
        /// <param name="fromDate">the log to search for start</param>
        /// <param name="toDate">the log to search for end</param>
        public static void SearchReacords(string fileName, DateTime fromDate, DateTime toDate)
        {
            try
            {
                long totalLength = GetTheFileLength(fileName);

                // alternative is to do it whit thread pool, but this technique give me better preference!
                using (MemoryMappedFile memoryMappedFile = MemoryMappedFile.CreateFromFile(fileName))
                using (MemoryMappedViewStream memoryMappedViewStream = memoryMappedFile.CreateViewStream(0, totalLength))
                {
                    StringBuilder resultAsString = new StringBuilder();

                    for (long j = 0; j < totalLength; ++j)
                    {
                        int result = memoryMappedViewStream.ReadByte();

                        if (result == (int)Const.EOF)
                        {
                            break;
                        }

                        else if (result == '\n')
                        {
                            string row = TrimRow(resultAsString);

                            if (row.Length > (int)Const.MinLength)
                            {
                                DateTime rowDate = GetDateFromRow(row);

                                int fromCompare = DateTime.Compare(rowDate, fromDate);
                                int toCompare = DateTime.Compare(rowDate, toDate);

                                if (fromCompare >= 0 && toCompare <= 0)
                                {
                                    // for better result buffer the output and print later
                                    Console.WriteLine(resultAsString.ToString());
                                }
                                else if (fromCompare > 0)
                                {
                                    // first one that not much end searching
                                    return;
                                }
                            }
                            // compare to
                            resultAsString.Clear();
                        }

                        resultAsString.Append((char)result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        #endregion

        #region Testing
        static private void TestLog()
        {
            DateTime from = new DateTime(2016, 9, 29, 04, 30, 31);
            DateTime to = new DateTime(2016, 9, 30, 14, 35, 10);


            Stopwatch mywatch = new Stopwatch();
            mywatch.Start();

            SearchReacords("Windows.log", from, to);

            mywatch.Stop();

            Console.WriteLine($"\n\nrunning time: {mywatch.Elapsed}\n\n");
        }

        #endregion
        static void Main(string[] args)
        {
            TestLog();

            Console.WriteLine($"----------- Main Finish --------\n\n");
        }
    }
}
