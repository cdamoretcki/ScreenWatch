using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScreenWatchUI.Models;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace ScreenWatchUI.Controllers
{
    public class UserController : Controller
    {
        UserRepository userRepository = new UserRepository();

        //
        // GET: /User/

        public ActionResult Index()
        {
            try
            {
                List<User> users = userRepository.getAllUsers();
                return View("Index", users);
            }
            catch (SqlException sqlException)
            {
                return View("../Shared/DbError");
            }
            catch (Exception e)
            {
                return View("../Shared/Error");
            }
        }

        public ActionResult Details(string id)
        {
            try
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
            catch (SqlException sqlException)
            {
                return View("../Shared/DbError");
            }
            catch (Exception e)
            {
                return View("../Shared/Error");
            }
        }


        public ActionResult Edit(string id)
        {
            try
            {
                User user = userRepository.getUser(id);
                return View(user);
            }
            catch (SqlException sqlException)
            {
                return View("../Shared/DbError");
            }
            catch (Exception e)
            {
                return View("../Shared/Error");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string id, FormCollection formValues)
        {
            try
            {
                User user = userRepository.getUser(id);

                if (ModelState.IsValid)
                {
                    user.email = Request.Form["email"];
                    string isMonitored = Request.Form["isMonitored"];
                    user.isMonitored = isMonitored == "true" ? true : false;
                    string isAdmin = Request.Form["isAdmin"];
                    user.isAdmin = isAdmin == "true" ? true : false;

                    // Validation
                    if (!Regex.IsMatch(user.email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                    {
                        ModelState.AddModelError("email", "Not a valid E-Mail Address");
                    }
                    if (!ModelState.IsValid)
                    {
                        return View(user);
                    }

                    userRepository.save(user);
                    return RedirectToAction("Details", new { id = user.userName });
                }

                return View(user);
            }
            catch (SqlException sqlException)
            {
                return View("../Shared/DbError");
            }
            catch (Exception e)
            {
                return View("../Shared/Error");
            }
        }
        
        public ActionResult Create()
        {
            try
            {
                User user = new User();

                return View(user);
            }
            catch (SqlException sqlException)
            {
                return View("../Shared/DbError");
            }
            catch (Exception e)
            {
                return View("../Shared/Error");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userRepository.addUser(user);
                    return RedirectToAction("Details", new { id = user.userName });
                }
                return View(user);
            }
            catch (SqlException sqlException)
            {
                return View("../Shared/DbError");
            }
            catch (Exception e)
            {
                return View("../Shared/Error");
            }
        }

        public ActionResult Delete(string id)
        {
            try
            {
                User user = userRepository.getUser(id);
                if (user == null)
                {
                    return View("NotFound");
                }
                else
                {
                    return View(user);
                }
            }
            catch (SqlException sqlException)
            {
                return View("../Shared/DbError");
            }
            catch (Exception e)
            {
                return View("../Shared/Error");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(string id, string confirmButton)
        {
            try
            {
                User user = userRepository.getUser(id);
                if (user == null)
                {
                    return View("NotFound");
                }

                userRepository.deleteUser(user);
                return View("Deleted");
            }
            catch (SqlException sqlException)
            {
                return View("../Shared/DbError");
            }
            catch (Exception e)
            {
                return View("../Shared/Error");
            }
        }
    }
}
