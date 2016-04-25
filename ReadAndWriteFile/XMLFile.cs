using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndWriteFile
{
    public static class XMLFile
    {

        /// <summary>
        /// using async method to write string xml to local
        /// </summary>
        /// <param name="contents">xml content</param>
        /// <returns>Task</returns>
        public static async Task WriteXmlOneAsync(string contents)
        {
            byte[] encodedXml = Encoding.UTF8.GetBytes(contents);

            using (FileStream fileStream = new FileStream(fileFullName,
                FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite,
                bufferSize: 8192, useAsync: true))
            {
                await fileStream.WriteAsync(encodedXml, 0, encodedXml.Length);
            }
        }

        /// <summary>
        /// Read xml from local
        /// </summary>
        /// <returns>string xml content</returns>
        public static async Task<string> ReadXmlOneAsync()
        {
            using (FileStream fileStream = new FileStream(fileFullName, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 8192, useAsync: true))
            {
                StringBuilder sb = new StringBuilder();
                byte[] buffer = new byte[1024];
                int numRead;

                while ((numRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string text = Encoding.UTF8.GetString(buffer, 0, numRead);

                    sb.Append(text);
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Read xml from local
        /// </summary>
        /// <returns>byte[] xml content</returns>
        public static async Task<byte[]> ReadXmlTwoAsync()
        {
            using (FileStream fileStream = new FileStream(fileFullName, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 8192, useAsync: true))
            {
                var arrays = new Stack<byte[]>();
                byte[] buffer = new byte[1024];
                int numRead;

                while ((numRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    arrays.Push(buffer);
                }

                var arrayList = arrays.ToArray<byte[]>();

                return CombineByteArrays(arrayList);
            }
        }

        /// <summary>
        /// Read xml from local
        /// </summary>
        /// <returns>byte[] xml content</returns>
        public static async Task<byte[]> ReadXmlThreeAsync()
        {
            using (FileStream fileStream = new FileStream(fileFullName, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 8192, useAsync: true))
            {
                IEnumerable<byte> myArray = null;
                byte[] buffer = new byte[1024];
                int numRead;

                while ((numRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    myArray = myArray.Concat(buffer);
                }

                var arrayList = myArray.ToArray();

                return arrayList;
            }
        }


        /// <summary>
        /// Read xml file from local
        /// </summary>
        /// <returns>byte[] xml content</returns>
        public static async Task<byte[]> ReadXmlFourAsync()
        {
            byte[] xmlContent = null;

            using (FileStream readFileStream = File.Open(fileFullName, FileMode.Open))
            {
                xmlContent = new byte[readFileStream.Length];
                await readFileStream.ReadAsync(xmlContent, 0, (int)readFileStream.Length);
            }

            return xmlContent;
        }

        /// <summary>
        /// write xml to local
        /// </summary>
        /// <param name="contents">xml content</param>
        /// <returns>Task</returns>
        public static async Task WriteXmlTwoAsync(string contents)
        {
            byte[] fileContents = Encoding.UTF8.GetBytes(contents);

            using (FileStream writeFileStream = File.OpenWrite(fileFullName))
            {
                await writeFileStream.WriteAsync(fileContents, 0, fileContents.Length);
            }
        }

        /// <summary>
        /// Combine some byte array to one array
        /// </summary>
        /// <param name="arrays">one or more byte array's arrays</param>
        /// <returns>byte array</returns>
        public static byte[] CombineByteArrays(params byte[][] arrays)
        {
            byte[] returnArray = new byte[arrays.Sum(x => x.Length)];
            int offset = 0;

            foreach (byte[] item in arrays)
            {
                Buffer.BlockCopy(item, 0, returnArray, offset, item.Length);
                offset += item.Length;
            }

            return returnArray;
        }

        /// <summary>
        /// xml'file name that contains file path
        /// </summary>
        static string fileFullName = AppDomain.CurrentDomain.BaseDirectory + "test.xml";
    }
}
