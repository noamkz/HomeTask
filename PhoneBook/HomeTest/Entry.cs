using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace HomeTask
{
    partial class PhoneBook
    {
        [Serializable]
        public class Entry
        {
            #region constants
            enum EConst
            {
                Byte = 1,
                NameLengthSize = Byte,
                EntryLengthSize = Byte * 4,
                NumberOfEntrySize = Byte * 4,
                EntryDataSection = NameLengthSize + EntryLengthSize,
                EOF = 11, // => -1
                TotalLengthStep = Byte * 22,
                DataSectionSize = Byte * 28
            }
            #endregion
            #region props
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Type { get; set; }
            #endregion
            #region ctor 
            public Entry(string data, string phone, string type)
            {
                this.Name = data;
                this.Phone = phone;
                this.Type = type;
            }
            #endregion
            #region public Static Serialize Methods

            // Entry format: 4 byte bodyLength / 1 byte name length / n byte string body
            static public byte[] SerializeEntry(Entry e)
            {
                string entryBody = $"{e.Name},{e.Phone},{e.Type}";

                byte[] stream = new byte[(int)EConst.EntryDataSection + entryBody.Length];

                BinaryFormatter bin = new BinaryFormatter();

                // data section formating
                BitConverter.GetBytes(entryBody.Length).CopyTo(stream, 0);
                stream[4] = Convert.ToByte(e.Name.Length);

                // body section formating
                Encoding.ASCII.GetBytes(entryBody).CopyTo(stream, (int)EConst.EntryDataSection);

                return stream;
            }

            // Entry Array format: Entry format / Entry format / 4 byte number of Entry 
            static public void SerializeEntrysArray(string fileName, Entry[] entrys)
            {
                using (Stream file = File.Open(fileName, FileMode.Create))
                {
                    byte[] entryStream = EntryToByteArray(entrys);

                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(file, entryStream);
                }
            }

            private static byte[] EntryToByteArray(Entry[] entrys)
            {
                byte[] entryStream = new byte[0];

                for (int i = 0, fromTo = 0; i < entrys.Length; i++)
                {
                    byte[] tmpStream = SerializeEntry(entrys[i]);
                    Array.Resize<byte>(ref entryStream, entryStream.Length + tmpStream.Length);
                    tmpStream.CopyTo(entryStream, fromTo);
                    fromTo += tmpStream.Length;
                }
                Array.Resize<byte>(ref entryStream, entryStream.Length + (int)EConst.EntryLengthSize);

                BitConverter.GetBytes(entrys.Length).CopyTo(entryStream, entryStream.Length - (int)EConst.EntryLengthSize);

                return entryStream;
            }


            static public Entry[] DeSerializeEntryArray(string fileName)
            {
                byte[] SerializeEntry = GetStreamFromFile(fileName);

                int numOfEntry = BitConverter.ToInt32(SerializeEntry, SerializeEntry.Length - (int)EConst.EntryLengthSize);

                Entry[] entries = new Entry[numOfEntry];

                for (int i = 0, from = 0, nextEntry = 0; i < entries.Length; i++, from += nextEntry + (int)EConst.EntryDataSection)
                {
                    nextEntry = BitConverter.ToInt32(SerializeEntry, from);
                    byte[] entry = new byte[nextEntry + (int)EConst.EntryDataSection];

                    Array.Copy(SerializeEntry, from, entry, 0, nextEntry + (int)EConst.EntryDataSection);
                    entries[i] = DeSerializeEntry(entry);
                }

                return entries;
            }


            private static Entry DeSerializeEntry(byte[] SerializeEntry)
            {

                int to = SerializeEntry.Length - (int)EConst.EntryDataSection;
                string bodyToString = Encoding.ASCII.GetString(SerializeEntry, (int)EConst.EntryDataSection, to);

                string[] props = bodyToString.Split(',');

                return new Entry(props[0], props[1], props[2]);
            }
            #endregion
            #region public Static Main Methods
            static public void UpdateOrInsertInPlace(string Filename, Entry e)
            {
                string nameToSearch = e.Name;

                using (var file = File.Open(Filename, FileMode.Open, FileAccess.ReadWrite))
                {
                    int oneByte = 0;

                    while ((oneByte = file.ReadByte()) != (int)EConst.EOF)
                    {
                        // if the lengths is equals need to comparer the strings
                        if (oneByte == nameToSearch.Length && (file.Position + oneByte) < file.Length)
                        {
                            long entryStartPos = file.Position - (int)EConst.EntryDataSection;

                            // nameLengthToSearch == OnePropSize
                            byte[] nameFromFile = GetData(file, oneByte);

                            // is the name is same
                            if (Encoding.ASCII.GetString(nameFromFile) == nameToSearch)
                            {
                                file.Position = entryStartPos;

                                UpdateEntry(e, file);

                                WriteTotalLength(file, file.Position);

                                return;
                            }
                        }
                    }
                    // entry not found, insert the entry to end of the file
                    // end update the number of entry and the new total Length

                    InsertNewEntry(e, file);

                    WriteTotalLength(file, file.Position);
                }
            }
            #endregion
            #region Private Static Methods
            private static byte[] GetStreamFromFile(string fileName)
            {
                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    return (byte[])bin.Deserialize(stream);
                }
            }

            private static byte[] GetData(FileStream file, long numOfByteToRead)
            {
                byte[] nameFromFile = new byte[numOfByteToRead];
                file.Read(nameFromFile);

                return nameFromFile;
            }

            private static void UpdateEntry(Entry e, FileStream file)
            {
                long startPos = file.Position;

                int oldEntryLength = GetLength(file);

                // go to end of entry, +1 byte for the name length 
                file.Position += oldEntryLength + (int)EConst.NameLengthSize;

                // read the all data left after the update entry
                byte[] restData = GetData(file, file.Length - file.Position);

                // go to the entry start position
                file.Position = startPos;

                // format the new entry and write to end of file
                file.Write(Entry.SerializeEntry(e));
                file.Write(restData);

            }

            // insert to the end of file
            private static void InsertNewEntry(Entry e, FileStream file)
            {
                file.Position = file.Length - 5;

                byte[] numOfEntry = BitConverter.GetBytes(GetLength(file) + 1);

                file.Position -= (int)EConst.EntryLengthSize;
                // format the new entry and write it an the update entry number
                file.Write(Entry.SerializeEntry(e));

                file.Write(numOfEntry);

                file.WriteByte((byte)EConst.EOF);
            }

            private static int GetLength(FileStream file)
            {
                byte[] numOfEntry = new byte[(int)EConst.EntryLengthSize];
                file.Read(numOfEntry);

                return (BitConverter.ToInt32(numOfEntry, 0));
            }

            private static void WriteTotalLength(FileStream file, long newSize)
            {
                file.Position = (long)EConst.TotalLengthStep;
                file.Write(BitConverter.GetBytes(newSize - (long)EConst.DataSectionSize), 0, (int)EConst.EntryLengthSize);
            }
            #endregion
        }
    }

}

