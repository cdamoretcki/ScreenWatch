using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScreenWatchUI.Models;
using System.Text.RegularExpressions;

namespace ScreenWatchUI.Controllers
{
    public class TextTriggerController : Controller
    {
        TextTriggerRepository textTriggerRepository = new TextTriggerRepository();

        //
        // GET: /User/

        public ActionResult Index()
        {
            var textTriggers = textTriggerRepository.getAllTextTriggers();
            return View("Index", textTriggers);
        }

        public ActionResult Details(string id)
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

        public ActionResult Edit(string id)
        {
            TextTrigger textTrigger = textTriggerRepository.getTextTrigger(id);
            return View(textTrigger);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string id, FormCollection formValues)
        {
            TextTrigger textTrigger = textTriggerRepository.getTextTrigger(id);
            try
            {
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
            catch
            {
                return View(textTrigger);
            }
        }
        
        public ActionResult Create()
        {
            TextTrigger textTrigger = new TextTrigger();
            return View(textTrigger);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(TextTrigger textTrigger)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //textTrigger.id = Guid.Empty;
                    //textTrigger.userEmail = String.Empty;
                    Guid id = textTriggerRepository.addTextTrigger(textTrigger);
                    return RedirectToAction("Details", new { id = id.ToString() });
                }
                catch
                {
                    return View("Error");
                }
            }

            return View(textTrigger);
        }

        public ActionResult Delete(string id){
            
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(string id, string confirmButton){
            
            TextTrigger textTrigger = textTriggerRepository.getTextTrigger(id);
            
            if (textTrigger == null)
            {
                return View("NotFound");
            }

            textTriggerRepository.deleteTextTrigger(textTrigger);
            return View("Deleted");
        }
    }
}