using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScreenWatchData;
using System.Drawing;
using System.Web.Mvc;

namespace ScreenWatchUI.Models
{
    public class ToneTriggerRepository
    {
        ScreenShotActions screenShotActions = new ScreenShotActions();

        public List<ToneTrigger> getAllToneTriggers()
        {
            List<ScreenWatchData.ToneTrigger> sToneTriggers =  screenShotActions.getAllToneTriggers();
            List<ScreenWatchUI.Models.ToneTrigger> toneTriggers = new List<ScreenWatchUI.Models.ToneTrigger>();
            ScreenWatchUI.Models.ToneTrigger toneTrigger;
            foreach (ScreenWatchData.ToneTrigger sToneTrigger in sToneTriggers)
            {
                toneTrigger = toneTriggerMapper(sToneTrigger);
                toneTriggers.Add(toneTrigger);
            }
            return toneTriggers;
        }

        public ToneTrigger getToneTrigger(String id)
        {
            return toneTriggerMapper(screenShotActions.getToneTriggerById(id));
        }

        public Guid addToneTrigger(ToneTrigger toneTrigger)
        {
            return screenShotActions.insertToneTrigger(toneTriggerMapper(toneTrigger));
        }

        public void deleteToneTrigger(ToneTrigger toneTrigger)
        {
            screenShotActions.deleteToneTrigger(toneTrigger.id);
        }

        public void save(ToneTrigger toneTrigger)
        {
            screenShotActions.updateToneTrigger(toneTriggerMapper(toneTrigger));
        }

        public IEnumerable<SelectListItem> getUserList()
        {
            return getUserList(String.Empty);
        }

        public IEnumerable<SelectListItem> getUserList(String userName){

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

        private ToneTrigger toneTriggerMapper(ScreenWatchData.ToneTrigger sToneTrigger)
        {
            ScreenWatchUI.Models.ToneTrigger toneTrigger = new ScreenWatchUI.Models.ToneTrigger();
            toneTrigger.id = sToneTrigger.id;
            toneTrigger.userName = sToneTrigger.userName;
            toneTrigger.lowerColorBound = ColorTranslator.ToHtml(sToneTrigger.lowerColorBound);
            toneTrigger.upperColorBound = ColorTranslator.ToHtml(sToneTrigger.upperColorBound);
            toneTrigger.sensitivity = sToneTrigger.sensitivity.ToString();

            return toneTrigger;
        }

        private ScreenWatchData.ToneTrigger toneTriggerMapper(ToneTrigger toneTrigger)
        {
            ScreenWatchData.ToneTrigger sToneTrigger = new ScreenWatchData.ToneTrigger();
            sToneTrigger.id = toneTrigger.id;
            sToneTrigger.userName = toneTrigger.userName;
            sToneTrigger.userEmail = toneTrigger.userEmail;
            sToneTrigger.lowerColorBound = ColorTranslator.FromHtml(toneTrigger.lowerColorBound.ToString());
            sToneTrigger.upperColorBound = ColorTranslator.FromHtml(toneTrigger.upperColorBound.ToString());
            sToneTrigger.sensitivity = int.Parse(toneTrigger.sensitivity);
            return sToneTrigger;
        }

        #endregion
    }
}