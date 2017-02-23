using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShortUrlServiceWeb.Controllers.Utils;

namespace ShortUrlServiceWeb.Tests
{
    
    [TestClass]
    public class ConverterUrlTest
    {
        public ConverterUrl converter;

        public ConverterUrlTest()
        {
            converter = new ConverterUrl(10);
        }

        [TestMethod, ExpectedException(typeof(System.ArgumentException))]
        public void Throw_Exc_When_Alphabet_Contains_Label()
        {
            //Arrange
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            string label = "z";
            ConverterUrl customConverter = new ConverterUrl(0, alphabet, label);
        }

        [TestMethod, ExpectedException(typeof(System.ArgumentException))]
        public void Throw_Exc_When_Alphabet_Less_Than_16()
        {
            //Arrange
            string alphabet = "qwerty";
            ConverterUrl customConverter = new ConverterUrl(0, alphabet);
        }

        [TestMethod]
        public void Encode_Numbers()
        {
            //Arrange            
            string expectedResult1 = "2";
            string expectedResult2 = "b3q";
            string expectedResult3 = "dltl";
            string expectedResult4 = "oiob7b";
            string expectedResult5 = "djnn9jlzff";

            //Act
            string result1 = converter.Encode(1);
            string result2 = converter.Encode(12345);
            string result3 = converter.Encode(540000);
            string result4 = converter.Encode(1234512345);
            string result5 = converter.Encode(987777775554654);


            result1 = _GetValuePartOfString(result1);
            result2 = _GetValuePartOfString(result2);
            result3 = _GetValuePartOfString(result3);
            result4 = _GetValuePartOfString(result4);
            result5 = _GetValuePartOfString(result5);


            //Assert
            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
            Assert.AreEqual(expectedResult3, result3);
            Assert.AreEqual(expectedResult4, result4);
            Assert.AreEqual(expectedResult5, result5);
        }

        [TestMethod]
        public void Decode_Strings()
        {
            //Arrange
            long expectedResult1 = 1;
            long expectedResult2 = 12345;
            long expectedResult3 = 540000;
            long expectedResult4 = 1234512345;
            long expectedResult5 = 987777775554654;

            //Act
            long result1 = converter.Decode("20q09ggd9z");
            long result2 = converter.Decode("b3q09ggd9z");
            long result3 = converter.Decode("dltl0k39fp");
            long result4 = converter.Decode("oiob7b032e");
            long result5 = converter.Decode("djnn9jlzff");

            //Assert
            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
            Assert.AreEqual(expectedResult3, result3);
            Assert.AreEqual(expectedResult4, result4);
            Assert.AreEqual(expectedResult5, result5);
        }

        private string _GetValuePartOfString(string str)
        {
            int valueLength = str.IndexOf(converter.Label);
            if (valueLength > 0)
                str = str.Substring(0, valueLength);
            return str;
        }
    }
}
