using System.IO;

namespace Helpers
{
    internal class CompareFile
    {
        public CompareFile()
        {
        }

        public async Task<List<string>> StartAsync(string fileA, string fileB)
        {
            return Compare(fileA, fileB);
        }

        private List<string> Compare(string fileA, string fileB)
        {
            List<string> result = new List<string>();

            try
            {
                var fs1 = new FileStream(fileA, FileMode.Open);
                var fs2 = new FileStream(fileB, FileMode.Open);

                // 0
                string length1 = $"> file A: {fs1.Length.ToString()} bytes";
                result.Add(length1);
                string length2 = $"> file B: {fs2.Length.ToString()} bytes";
                result.Add(length2);

                // 1
                string sizes = CheckFileLength(fs1, fs2);
                result.Add(sizes);

                // 2
                string bytes = CheckFileByte(fs1, fs2);
                result.Add(bytes);

                fs1.Close();
                fs2.Close();
            }
            catch (Exception)
            {
                var error = "> Files access error";
                result.Add(error);
            }

            return result;
        }

        private string CheckFileLength(FileStream fs1, FileStream fs2)
        {
            if (fs1.Length != fs2.Length)
            {
                return "> Files size different";
            }
            return "> Files size ok";
        }
        private string CheckFileByte(FileStream fs1, FileStream fs2)
        {
            int file1byte, file2byte;
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while (file1byte == file2byte && file1byte != -1);

            if (file1byte - file2byte != 0)
            {
                return "> Differences in byte-by-byte comparison";
            }
            return "> Byte-by-byte comparison ok";
        }
    }
}
