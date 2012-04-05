using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScreenWatchData;
using System.Web.Mvc;

namespace ScreenWatchUI.Models
{
    public class TextTriggerRepository
    {
        ScreenShotActions screenShotActions = new ScreenShotActions();

        public List<TextTrigger> getAllTextTriggers()
        {
            List<ScreenWatchData.TextTrigger> sTextTriggers =  screenShotActions.getAllTextTriggers();
            List<ScreenWatchUI.Models.TextTrigger> textTriggers = new List<ScreenWatchUI.Models.TextTrigger>();
            ScreenWatchUI.Models.TextTrigger textTrigger;
            foreach (ScreenWatchData.TextTrigger sTextTrigger in sTextTriggers)
            {
                textTrigger = textTriggerMapper(sTextTrigger);
                textTriggers.Add(textTrigger);
            }
            return textTriggers;
        }

        public TextTrigger getTextTrigger(String id)
        {
            return textTriggerMapper(screenShotActions.getTextTriggerById(id));
        }

        public Guid addTextTrigger(TextTrigger textTrigger)
        {
            return screenShotActions.insertTextTrigger(textTriggerMapper(textTrigger));
        }

        public void deleteTextTrigger(TextTrigger textTrigger)
        {
            screenShotActions.deleteTextTrigger(textTrigger.id);
        }

        public void save(TextTrigger textTrigger)
        {
            screenShotActions.updateTextTrigger(textTriggerMapper(textTrigger));
        }

        public IEnumerable<SelectListItem> getUserList()
        {
            return getUserList(String.Empty);
        }

        public IEnumerable<SelectListItem> getUserList(String userName)
        {

            List<String> userList = new ScreenWatchData.ScreenShotActions().getUsers();
            List<SelectListItem> userSelectList = new List<SelectListItem>();
            SelectListItem selectListItem;
            foreach (String user in userList)
            {
                selectListItem = new SelectListItem();
                selectListItem.Text = user;
                selectListItem.Value = user;
                if (userName.Equals(user))
                {
                    selectListItem.Selected = true;
                }
                userSelectList.Add(selectListItem);
            }

            return userSelectList.AsEnumerable<SelectListItem>();
        }

        #region mappers

        private TextTrigger textTriggerMapper(ScreenWatchData.TextTrigger sTextTrigger)
        {
            ScreenWatchUI.Models.TextTrigger textTrigger = new ScreenWatchUI.Models.TextTrigger();
            textTrigger.id = sTextTrigger.id;
            textTrigger.userName = sTextTrigger.userName;
            textTrigger.userEmail = sTextTrigger.userEmail;
            textTrigger.triggerString = sTextTrigger.triggerString;

            return textTrigger;
        }

        private ScreenWatchData.TextTrigger textTriggerMapper(TextTrigger textTrigger)
        {
            ScreenWatchData.TextTrigger sTextTrigger = new ScreenWatchData.TextTrigger();
            sTextTrigger.id = textTrigger.id;
            sTextTrigger.userName = textTrigger.userName;
            sTextTrigger.userEmail = textTrigger.userEmail;
            sTextTrigger.triggerString = textTrigger.triggerString;

            return sTextTrigger;
        }

        #endregion

    }
}