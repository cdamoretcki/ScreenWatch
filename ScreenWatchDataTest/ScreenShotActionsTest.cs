using ScreenWatchData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;

namespace ScreenWatchDataTest
{
    /// <summary>
    ///This is a test class for ScreenShotActionsTest and is intended
    ///to contain all ScreenShotActionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ScreenShotActionsTest
    {
        private TestContext testContextInstance;
        private static Guid insertedId = Guid.Empty;
        private String insertedUser = "TESTUSER";

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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A null test for insertScreenShot
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ScreenWatchData.dll")]
        public void insertScreenShotNullInputTest()
        {
            ScreenShotActions_Accessor target = new ScreenShotActions_Accessor(true);
            ScreenShot screenShot = null;
            try
            {
                target.insertScreenShot(screenShot);
                // Exception should be thrown - if it isn't, fail the test
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(System.ArgumentException));
            }
        }

        /// <summary>
        ///A valid data test for insertScreenShot
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ScreenWatchData.dll")]
        public void insertScreenShotValidInputTest()
        {
            ScreenShotActions_Accessor target = new ScreenShotActions_Accessor(true); // TODO: Initialize to an appropriate value
            ScreenShot screenShot = new ScreenShot();
            screenShot.timeStamp = DateTime.Now;
            screenShot.user = insertedUser;
            Bitmap bitmap = new Bitmap(800, 600);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(0, 0, 0, 0, new Size(800, 600));
            }
            screenShot.image = bitmap;
            Guid newlyInsertedId = target.insertScreenShot(screenShot);
            Assert.IsNotNull(newlyInsertedId);

            ScreenShot returnedScreenShot = target.getScreenShotById(newlyInsertedId);
            Assert.IsNotNull(returnedScreenShot);
            Assert.AreEqual(returnedScreenShot.user, insertedUser);
            Assert.AreEqual(returnedScreenShot.timeStamp.DayOfYear, DateTime.Now.DayOfYear);

            insertedId = newlyInsertedId;
        }

        /// <summary>
        ///A test for getScreenShotsByDateRange
        ///</summary>
        [TestMethod()]
        public void getScreenShotsByDateRangeTest()
        {
            ScreenShotActions target = new ScreenShotActions(true);
            DateTime fromDate = DateTime.Now.AddMonths(-1); 
            DateTime toDate = DateTime.Now; 
            List<ScreenShot> actual = new List<ScreenShot>();
            //actual = target.getScreenShotsByDateRange(fromDate, toDate); TODO: fix after we get the database working again
            //Assert.AreNotEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for getToneTriggersByUser
        ///</summary>
        [TestMethod()]
        public void getUsersTest()
        {
            ScreenShotActions target = new ScreenShotActions(true);
            string expected = "TESTUSER"; 
            List<string> users;
            users = target.getUsers();
            Assert.IsTrue(users.Contains(expected));
        }

        /// <summary>
        ///A test for getToneTriggersByUser
        ///</summary>
        [TestMethod()]
        public void getToneTriggersByUserTest()
        {
            ScreenShotActions target = new ScreenShotActions(true); // TODO: Initialize to an appropriate value
            string user = "TESTUSER"; // TODO: Initialize to an appropriate value
            List<ToneTrigger> expected = null; // TODO: Initialize to an appropriate value
            List<ToneTrigger> actual;
            actual = target.getToneTriggersByUser(user);
            Assert.IsNotNull(actual);
            //Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for insertTextTrigger
        ///</summary>
        [TestMethod()]
        public void insertTextTriggerTest()
        {
            ScreenShotActions target = new ScreenShotActions(true);
            TextTrigger textTrigger = new TextTrigger();
            textTrigger.userName = "TESTUSER";
            textTrigger.userEmail = "TEST@";
            textTrigger.triggerString = "TEST";
            Guid actual = target.insertTextTrigger(textTrigger);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for the text trigger lifecycle
        ///</summary>
        [TestMethod()]
        public void textTriggerFullTest()
        {
            //insert
            ScreenShotActions target = new ScreenShotActions(true);
            TextTrigger textTrigger = new TextTrigger();
            textTrigger.userName = "TESTUSER";
            textTrigger.userEmail = "TEST@";
            textTrigger.triggerString = "TEST";
            Guid id = target.insertTextTrigger(textTrigger);
            Assert.IsNotNull(id);            

            //update
            string newVal = "BAYAD WERDS";
            var allTriggers = target.getAllTextTriggers();
            TextTrigger outTrigger = allTriggers.First(t => t.id.Equals(id));
            outTrigger.triggerString = newVal;
            target.updateTextTrigger(outTrigger);
            allTriggers = target.getAllTextTriggers();
            outTrigger = allTriggers.First(t => t.id.Equals(id));
            Assert.AreEqual(newVal, outTrigger.triggerString);

            //delete
            target.deleteTextTrigger(outTrigger.id);
            Assert.AreEqual(target.getAllTextTriggers().Count(t => t.id.Equals(id)), 0);
        }

        /// <summary>
        ///A test for insertToneTrigger
        ///</summary>
        [TestMethod()]
        public void insertToneTriggerTest()
        {
            ScreenShotActions target = new ScreenShotActions(true);
            ToneTrigger toneTrigger = new ToneTrigger();
            toneTrigger.userName = "TESTUSER";
            toneTrigger.userEmail = "TEST@";
            toneTrigger.lowerColorBound = Color.AliceBlue;
            toneTrigger.upperColorBound = Color.Azure;
            toneTrigger.sensitivity = 80;
            Guid actual = target.insertToneTrigger(toneTrigger);
            Assert.IsNotNull(actual);
        }
        /// <summary>
        ///A test for the text trigger lifecycle
        ///</summary>
        [TestMethod()]
        public void toneTriggerFullTest()
        {
            //insert
            ScreenShotActions target = new ScreenShotActions(true);
            ToneTrigger toneTrigger = new ToneTrigger();
            toneTrigger.userName = "TESTUSER";
            toneTrigger.userEmail = "TEST@";
            toneTrigger.lowerColorBound = Color.AliceBlue;
            toneTrigger.upperColorBound = Color.Azure;
            toneTrigger.sensitivity = 80;
            Guid id = target.insertToneTrigger(toneTrigger);
            Assert.IsNotNull(id);

            //update
            int newVal = 70;
            var allTriggers = target.getAllToneTriggers();
            ToneTrigger outTrigger = allTriggers.First(t => t.id.Equals(id));
            outTrigger.sensitivity = newVal;
            target.updateToneTrigger(outTrigger);
            allTriggers = target.getAllToneTriggers();
            outTrigger = allTriggers.First(t => t.id.Equals(id));
            Assert.AreEqual(newVal, outTrigger.sensitivity);

            //delete
            target.deleteToneTrigger(outTrigger.id);
            Assert.AreEqual(target.getAllToneTriggers().Count(t => t.id.Equals(id)), 0);
        }

        /// <summary>
        ///A test for getTextTriggersByUser
        ///</summary>
        [TestMethod()]
        public void getTextTriggersByUserTest()
        {
            ScreenShotActions target = new ScreenShotActions(true); // TODO: Initialize to an appropriate value
            string user = "TESTUSER"; // TODO: Initialize to an appropriate value
            List<TextTrigger> expected = null; // TODO: Initialize to an appropriate value
            List<TextTrigger> actual;
            actual = target.getTextTriggersByUser(user);
            Assert.IsNotNull(actual);
            //Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for getAllToneTriggers
        ///</summary>
        [TestMethod()]
        public void getAllToneTriggersTest()
        {
            ScreenShotActions target = new ScreenShotActions(true); // TODO: Initialize to an appropriate value
            List<ToneTrigger> actual;
            actual = target.getAllToneTriggers();
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for getAllTextTriggers
        ///</summary>
        [TestMethod()]
        public void getAllTextTriggersTest()
        {
            ScreenShotActions target = new ScreenShotActions(true); // TODO: Initialize to an appropriate value
            List<TextTrigger> actual;
            actual = target.getAllTextTriggers();
            Assert.IsNotNull(actual);
        }
    }
}
