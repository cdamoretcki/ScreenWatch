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
    public class TextTriggerController : Controller
    {
        TextTriggerRepository textTriggerRepository = new TextTriggerRepository();

        //
        // GET: /User/

        public ActionResult Index()
        {
            try
            {
                List<TextTrigger> textTriggers = textTriggerRepository.getAllTextTriggers();
                return View("Index", textTriggers);
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
                TextTrigger textTrigger = textTriggerRepository.getTextTrigger(id);

                if (textTrigger == null)
                {
                    return View("NotFound");
                }
                else
                {
                    return View("Details", textTrigger);
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
                TextTrigger textTrigger = textTriggerRepository.getTextTrigger(id);
                textTrigger.userList = textTriggerRepository.getUserList(textTrigger.userName);
                return View(textTrigger);
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
                TextTrigger textTrigger = textTriggerRepository.getTextTrigger(id);
                textTrigger.userList = textTriggerRepository.getUserList(textTrigger.userName);
                
                if (ModelState.IsValid)
                {
                    textTrigger.userName = Request.Form["userName"];
                    textTrigger.triggerString = Request.Form["triggerString"];

                    if (!ModelState.IsValid)
                    {
                        return View(textTrigger);
                    }

                    textTriggerRepository.save(textTrigger);
                    return RedirectToAction("Details", new { id = textTrigger.id });
                }

                return View(textTrigger);
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
                TextTrigger textTrigger = new TextTrigger();
                textTrigger.userList = textTriggerRepository.getUserList();
                return View(textTrigger);
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
        public ActionResult Create(TextTrigger textTrigger)
        {
            try
            {
                textTrigger.userList = textTriggerRepository.getUserList();
                if (ModelState.IsValid)
                {
                    Guid id = textTriggerRepository.addTextTrigger(textTrigger);
                    return RedirectToAction("Details", new { id = id.ToString() });
                }

                return View(textTrigger);
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

        public ActionResult Delete(string id){

            try
            {
                TextTrigger textTrigger = textTriggerRepository.getTextTrigger(id);

                if (textTrigger == null)
                {
                    return View("NotFound");
                }
                else
                {
                    return View(textTrigger);
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
        public ActionResult Delete(string id, string confirmButton){
            try
            {
                TextTrigger textTrigger = textTriggerRepository.getTextTrigger(id);

                if (textTrigger == null)
                {
                    return View("NotFound");
                }

                textTriggerRepository.deleteTextTrigger(textTrigger);
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