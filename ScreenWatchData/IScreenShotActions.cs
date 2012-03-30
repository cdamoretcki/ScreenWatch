using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ScreenWatchData
{
    public class ScreenShot
    {
        public DateTime timeStamp { get; set; }
        public String user { get; set; }
        public Image image { get; set; }
        public Image thumbnail { get; set; }
        public String filePath { get; set; }
        public String thumbnailFilePath { get; set; }
    }

    public interface AnalysisTrigger
    {
        Guid id { get; set; }
        String userName { get; set; }
    }

    public class TextTrigger : AnalysisTrigger
    {
        public Guid id { get; set; }
        public String userName { get; set; }
        public String userEmail { get; set; }
        public String triggerString { get; set; }
    }

    public class ToneTrigger : AnalysisTrigger
    {
        public Guid id { get; set; }
        public String userName { get; set; }
        public String userEmail { get; set; }
        public Color lowerColorBound { get; set; }
        public Color upperColorBound { get; set; }
        public int sensitivity { get; set; }
    }

    public class User
    {
        public String userName { get; set; }
        public String email { get; set; }
        public Boolean isMonitored { get; set; }
        public Boolean isAdmin { get; set; }
    }

    public interface IScreenShotActions
    {
        Guid insertScreenShot(ScreenShot screenShot);
        List<string> getUsers();
        Guid insertTextTrigger(TextTrigger textTrigger);
        Guid insertToneTrigger(ToneTrigger toneTrigger);
        void updateTextTrigger(TextTrigger textTrigger);
        void updateToneTrigger(ToneTrigger toneTrigger);
        void deleteTextTrigger(Guid id);
        void deleteToneTrigger(Guid id);
        List<ScreenShot> getScreenShotsByDateRange(DateTime fromDate, DateTime toDate);
        List<TextTrigger> getTextTriggersByUser(String user);
        List<ToneTrigger> getToneTriggersByUser(String user);
        void insertUser(User user);
        void updateUser(User user);
        void deleteUser(String userName);
        User getUserByUserName(String userName);
        List<User> getAllUsers();
    }
}
