﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSoup.Nodes;
using NSoup;
using NSoup.Helper;

namespace Test.Helper
{

    [TestClass]
    public class DataUtilTest
    {
        public DataUtilTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void testCharset()
        {
            Assert.AreEqual("utf-8", DataUtil.GetCharsetFromContentType("text/html;charset=utf-8 "), true);
            Assert.AreEqual("UTF-8", DataUtil.GetCharsetFromContentType("text/html; charset=UTF-8"));
            Assert.AreEqual("ISO-8859-1", DataUtil.GetCharsetFromContentType("text/html; charset=ISO-8859-1"));
            Assert.AreEqual(null, DataUtil.GetCharsetFromContentType("text/html"));
            Assert.AreEqual(null, DataUtil.GetCharsetFromContentType(null));
            Assert.AreEqual(null, DataUtil.GetCharsetFromContentType("text/html;charset=Unknown"));
        }

        
        public void testQuotedCharset()
        {
            Assert.AreEqual("utf-8", DataUtil.GetCharsetFromContentType("text/html; charset=\"utf-8\""));
            Assert.AreEqual("UTF-8", DataUtil.GetCharsetFromContentType("text/html;charset=\"UTF-8\""));
            Assert.AreEqual("ISO-8859-1", DataUtil.GetCharsetFromContentType("text/html; charset=\"ISO-8859-1\""));
            Assert.AreEqual(null, DataUtil.GetCharsetFromContentType("text/html; charset=\"Unsupported\""));
        }

        [TestMethod]
        public void discardsSpuriousByteOrderMark()
        {
            string html = "\uFEFF<html><head><title>One</title></head><body>Two</body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(html);
            Document doc = DataUtil.ParseByteData(buffer, "UTF-8", "http://foo.com/", NSoup.Parse.Parser.HtmlParser());
            Assert.AreEqual("One", doc.Head.Text());
        }
    }
}
