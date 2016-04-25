using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace ReadAndWriteFile
{
    /// <summary>
    /// XMLFile.cs's unit test class
    /// </summary>
    public static class XMLFileTests
    {

        #region Test Writing methods

        [Theory]
        [InlineData("<content>Hello World 22q</content>")]
        public static void TestWriteXmlOneAsync(string contents)
        {

            for (int i = 0; i <= 1000; i++)
            {
                Assert.ThrowsAsync<IOException>(() => XMLFile.WriteXmlOneAsync(contents));
            }
        }

       
        public static IEnumerable<object[]> WriteXmlTwoAsync_TestData()
        {
            return new object[][]
            {
                new object[] {"<title1>title1</title1>" },
                new object[] {"<title2>title2</title2>" }
            };
        }

        [Theory]
        [MemberData("WriteXmlTwoAsync_TestData")]
        public static void TestWriteXmlTwoAsync(string contents)
        {
            for (int i = 0; i <= 1000; i++)
            {
                Assert.ThrowsAsync<IOException>(() => XMLFile.WriteXmlTwoAsync(contents));
            }
        }

        #endregion

    }
}
