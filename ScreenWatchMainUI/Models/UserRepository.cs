using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScreenWatchData;

namespace ScreenWatchUI.Models
{
    public class UserRepository
    {
        ScreenShotActions screenShotActions = new ScreenShotActions();

        public List<User> getAllUsers()
        {
            List<ScreenWatchData.User> sUsers =  screenShotActions.getAllUsers();
            List<ScreenWatchUI.Models.User> users = new List<ScreenWatchUI.Models.User>();
            ScreenWatchUI.Models.User user;
            foreach (ScreenWatchData.User sUser in sUsers){
                user = ridiculousMapper(sUser);
                users.Add(user);
            }
            return users;
        }

        private User ridiculousMapper(ScreenWatchData.User sUser)
        {
            ScreenWatchUI.Models.User user = new ScreenWatchUI.Models.User();
            user.userName = sUser.userName;
            user.email = sUser.email;
            user.isAdmin = sUser.isAdmin;
            user.isMonitored = sUser.isMonitored;

            return user;
        }

        private ScreenWatchData.User ridiculousMapper(User user)
        {
            ScreenWatchData.User sUser = new ScreenWatchData.User();
            sUser.userName = user.userName;
            sUser.email = user.email;
            sUser.isAdmin = user.isAdmin;
            sUser.isMonitored = user.isMonitored;

            return sUser;
        }

        public User getUser(String user)
        {
            return ridiculousMapper(screenShotActions.getUserByUserName(user));
        }

        public void addUser(User user)
        {
            screenShotActions.insertUser(ridiculousMapper(user));
        }

        public void deleteUser(User user)
        {
            screenShotActions.deleteUser(user.userName);
        }

        public void save(User user)
        {
            screenShotActions.updateUser(ridiculousMapper(user));
        }
    }
}