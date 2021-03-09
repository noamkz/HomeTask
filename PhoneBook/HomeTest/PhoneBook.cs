
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace HomeTask
{
    partial class PhoneBook
    {
        private string FilePath { get; set; }

        /// <summary>filename must supply,and store in bin\debug\netcore.</summary>
        public PhoneBook(string filename)
        {
            FilePath = $@"{filename}";

            if (!File.Exists(FilePath))
            {
                // option b is: to  create new file
                throw new Exception(" file name not supply or not found");
            }
        }

        /// <summary>
        /// find and return the first entry that find
        /// </summary>
        /// <param name="name">the name to search</param>
        /// <returns> return the first entry, if not find return null</returns>
        public Entry GetByName(string name)
        {
            return Iterate().ToList<Entry>().FirstOrDefault((p) => p.Name == name);
        }

        /// <summary>
        /// will overwrite the entry if there is already 
        /// one with the same Name, will not 
        /// </summary>
        /// <param name="e">entry to update or insert</param>
        public void InsertOrUpdate(Entry e)
        {
            Entry.UpdateOrInsertInPlace(FilePath, e);
        }


        /// <summary>
        /// Get a list of all the entries from bin file, in Name order
        /// </summary>
        public IEnumerable<Entry> Iterate()
        {
            List<Entry> fromBinFile = Entry.DeSerializeEntryArray(FilePath).ToList();

            return fromBinFile.OrderBy((e) => e.Name);
        }

        /// <summary>
        /// move data from JSON File to bin file
        /// this function is an extra for convenience
        /// </summary>
        public void GetDataFromJson(string fileName)
        {
            Entry[] fromJsonFile = JsonConvert.DeserializeObject<Entry[]>(File.ReadAllText(fileName));
            Entry.SerializeEntrysArray(FilePath, fromJsonFile);
        }
    }

}

