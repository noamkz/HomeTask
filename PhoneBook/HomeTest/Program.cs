using HomeTask;
using System;
using System.Collections.Generic;
using static HomeTask.PhoneBook;

namespace HomeTask
{
    class Program
    {
        #region Test Section
        private static void TestGetByName(PhoneBook phoneBook)
        {
            // case: exists in the file {begin, middle, end, not in}
            string[] names = { "Faye", "Dominique", "Herrera", "noam" };

            Console.WriteLine("-----------------------------------------------------------------------------------------\n");
            Console.WriteLine("--------------------------------- Begin TestGetByName -----------------------------------\n");
            Console.WriteLine("-----------------------------------------------------------------------------------------\n\n");

            for (int i = 0; i < names.Length; i++)
            {
                Entry e = phoneBook.GetByName(names[i]);

                if (e != null)
                {
                    Console.WriteLine($"{e.Name}, {e.Phone}, {e.Type}");
                }
                else
                {
                    Console.WriteLine($"{names[i]} not found in the file");
                }
            }

            Console.WriteLine("\n\t----------------------- End TestGetByName ---------------------\n\n");
        }


        private static bool TestInsertOrUpdate(PhoneBook phoneBook)
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------\n");
            Console.WriteLine("--------------------------- Begin TestInsertOrUpdate ------------------------------------\n");
            Console.WriteLine("-----------------------------------------------------------------------------------------\n\n");

            // case: exists or not ad if update in the file 
            Entry[] testEmtry = new Entry[]
            {
                new Entry("Noam", "054121212", "--///"),
                new Entry("oria", "054121212", "------"),
                new Entry("Herrera", "054121212", "--home"),
                new Entry("tomer", "054121212", "--home"),
                new Entry("Murray", "+1 (934) 538-3007", "work"),
                new Entry("Kinney", "054121212", "school")
            };

            IEnumerable<Entry> list = phoneBook.Iterate();

            Console.WriteLine("\t----------------------------Before------------------------------------\n\n");

            PrintList(list);

            for (int i = 0; i < testEmtry.Length; i++)
            {
                phoneBook.InsertOrUpdate(testEmtry[i]);
            }

            Console.WriteLine("\t----------------------------After------------------------------------\n\n");

            list = phoneBook.Iterate();

            PrintList(list);


            Console.WriteLine("\n\t--------------End TestInsertOrUpdate --------------------\n\n");

            return (true);
        }

        #endregion

        #region helper functions
        private static void PrintList(IEnumerable<Entry> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine($"{item.Name}, {item.Phone}, {item.Type}");
            }
        }
        #endregion

        static void Main(string[] args)
        {
            PhoneBook phoneBook = new PhoneBook("PhoneBookData.bin");

            // upload data from json to bin file direct
            // phoneBook.GetDataFromJson("MuckPhoneBookData.bin");

            TestGetByName(phoneBook);
            TestInsertOrUpdate(phoneBook);
        }
    }
}
