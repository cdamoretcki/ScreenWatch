using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScreenWatchUI.Models;

namespace ScreenWatchUI.Controllers
{
    public class UserController : Controller
    {
        UserRepository userRepository = new UserRepository();

        //
        // GET: /User/

        public ActionResult Index()
        {
            var users = userRepository.getAllUsers();
            return View("Index", users);
        }

        public ActionResult Details(string id)
        {
            User user = userRepository.getUser(id);

            if (user == null)
            {
                return View("NotFound");
            }
            else
            {
                return View("Details", user);
            }
        }


        public ActionResult Edit(string id)
        {
            User user = userRepository.getUser(id);
            return View(user);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string id, FormCollection formValues)
        
         {   User user = userRepository.getUser(id);
            try
            {
                user.email = Request.Form["email"];
                string isMonitored = Request.Form["isMonitored"];
                user.isMonitored = isMonitored == "true" ? true : false;
                string isAdmin = Request.Form["isAdmin"];
                user.isAdmin = isAdmin == "true" ? true : false;

                //UpdateModel(user);

                userRepository.save(user);

                return RedirectToAction("Details", new { id = user.userName });
            }
            catch
            {
                /*foreach (var issue in user.GetRuleViolalations())
                {
                    ModelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
                }*/
                return View(user);
            }
        }
        
        public ActionResult Create()
        {
            User user = new User();

            return View(user);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(User user){
            if(ModelState.IsValid){
                try{
                    userRepository.addUser(user);
                    return RedirectToAction("Details", new {id=user.userName});
                } catch{
                    return View("Error");
                    //ModelState.AddRuleViolations(user.GetRuleViolations());
                }
            } 
            return View(user);
        }

        public ActionResult Delete(string id){
            User user = userRepository.getUser(id);
            if(user == null){
                return View("NotFound");
            } else{
                return View(user);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(string id, string confirmButton){
            User user = userRepository.getUser(id);
            if(user == null){
                return View("NotFound");
            }
      
            userRepository.deleteUser(user);
            return View("Deleted");
        }
    }
}
