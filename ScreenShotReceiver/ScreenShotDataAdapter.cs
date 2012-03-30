using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScreenWatchData;
using System.Configuration;
using System.Drawing;

namespace ScreenShotReceiver
{
    public class ScreenShotDataAdapter
    {
        IScreenShotActions dataLayer;

        public ScreenShotDataAdapter()
        {
            string connectToDataBase = ConfigurationManager.AppSettings["connectToDataBase"];
            if (bool.TrueString.Equals(connectToDataBase, StringComparison.InvariantCultureIgnoreCase))
            {
                dataLayer = new ScreenShotActions();
            }
            else
            {
                dataLayer = new ScreenShotActionStub();
            }
        }

        /// <summary>
        /// upload the screenshot
        /// </summary>
        /// <param name="screenShot"></param>
        public void SaveImage(Image image, string user, string captureTime)
        {
            ScreenShot screenShot = new ScreenShot();
            screenShot.image = image;
            screenShot.timeStamp = DateTime.Parse(captureTime);
            screenShot.user = user;
            dataLayer.insertScreenShot(screenShot);
        }

        /// <summary>
        /// get all tone triggers for a specific user
        /// </summary>
        /// <param name="user">the user being watched</param>
        /// <returns>list of all tone triggers</returns>
        public List<ToneTrigger> GetToneTriggers(string user)
        {
            return dataLayer.getToneTriggersByUser(user);
        }

        /// <summary>
        /// get all text triggers for a specific user
        /// </summary>
        /// <param name="user">user bieng watched</param>
        /// <returns>list of all tone triggers</returns>
        public List<TextTrigger> GetTextTriggers(string user)
        {
            return dataLayer.getTextTriggersByUser(user);
        }

        #region test stub
        
        private class ScreenShotActionStub : IScreenShotActions
        {
            public Guid insertScreenShot(ScreenShot screenShot)
            {
                return Guid.Empty;
            }

            public List<string> getUsers()
            {
                throw new NotImplementedException();
            }

            public List<User> getAllUsers()
            {
                throw new NotImplementedException();
            }

            void IScreenShotActions.insertUser(User user)
            {
                throw new NotImplementedException();
            }

            void IScreenShotActions.updateUser(User user)
            {
                throw new NotImplementedException();
            }

            void IScreenShotActions.deleteUser(string userName)
            {
                throw new NotImplementedException();
            }

            User IScreenShotActions.getUserByUserName(string userName)
            {
                throw new NotImplementedException();
            }

            public Guid insertTextTrigger(TextTrigger textTrigger)
            {
                throw new NotImplementedException();
            }

            public Guid insertToneTrigger(ToneTrigger toneTrigger)
            {
                throw new NotImplementedException();
            }

            public void updateTextTrigger(TextTrigger textTrigger)
            {
                throw new NotImplementedException();
            }

            public void updateToneTrigger(ToneTrigger toneTrigger)
            {
                throw new NotImplementedException();
            }

            public void deleteTextTrigger(Guid textTrigger)
            {
                throw new NotImplementedException();
            }

            public void deleteToneTrigger(Guid toneTrigger)
            {
                throw new NotImplementedException();
            }

            public List<ScreenShot> getScreenShotsByDateRange(DateTime fromDate, DateTime toDate)
            {
                throw new NotImplementedException();
            }

            public List<TextTrigger> getTextTriggersByUser(string user)
            {
                var triggers = new List<TextTrigger>();
                triggers.Add(new TextTrigger()
                {                    
                    id = Guid.Parse("00000000-0000-0000-0000-000000000123"),
                    triggerString = "bad",
                    userEmail = "jared.tait@gmail.com",
                    userName = "Jared"
                });
                triggers.Add(new TextTrigger()
                {
                    id = Guid.Parse("00000000-0000-0000-0000-000000004567"),
                    triggerString = "test",
                    userEmail = "jared.tait@gmail.com",
                    userName = "Jared"
                });
                return triggers;
            }

            public List<ToneTrigger> getToneTriggersByUser(string user)
            {
                var triggers = new List<ToneTrigger>();
                triggers.Add(new ToneTrigger()
                {
                    id = Guid.Parse("00000000-0000-0000-0000-000000004567"), 
                    lowerColorBound = Color.FromArgb(-3637420), 
                    upperColorBound = Color.FromArgb(-1150171), 
                    userEmail = "jared.tait@gmail.com",
                    userName = "Jared",
                    sensitivity = 80
                });
                return triggers;
            }

}

#endregion
    }
}