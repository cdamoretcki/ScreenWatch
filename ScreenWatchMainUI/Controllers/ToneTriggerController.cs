using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScreenWatchUI.Models;
using System.Text.RegularExpressions;

namespace ScreenWatchUI.Controllers
{
    public class ToneTriggerController : Controller
    {
        ToneTriggerRepository toneTriggerRepository = new ToneTriggerRepository();

        //
        // GET: /User/

        public ActionResult Index()
        {
            var toneTriggers = toneTriggerRepository.getAllToneTriggers();
            return View("Index", toneTriggers);
        }

        public ActionResult Details(string id)
        {
            ToneTrigger toneTrigger = toneTriggerRepository.getToneTrigger(id);

            if (toneTrigger == null)
            {
                return View("NotFound");
            }
            else
            {
                return View("Details", toneTrigger);
            }
        }

        public ActionResult Edit(string id)
        {
            ToneTrigger toneTrigger = toneTriggerRepository.getToneTrigger(id);
            toneTrigger.userList = toneTriggerRepository.getUserList(toneTrigger.userName);
            return View(toneTrigger);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string id, FormCollection formValues)
        {
            ToneTrigger toneTrigger = toneTriggerRepository.getToneTrigger(id);
            try
            {
                if (ModelState.IsValid)
                {
                    toneTrigger.userName = Request.Form["userName"];
                    toneTrigger.lowerColorBound = Request.Form["lowerColorBound"];
                    toneTrigger.upperColorBound = Request.Form["upperColorBound"];
                    toneTrigger.sensitivity = Request.Form["sensitivity"];

                    if (!ModelState.IsValid)
                    {
                        return View(toneTrigger);
                    }

                    toneTriggerRepository.save(toneTrigger);
                    return RedirectToAction("Details", new { id = toneTrigger.id });
                }

                return View(toneTrigger);
            }
            catch
            {
                return View(toneTrigger);
            }
        }
        
        public ActionResult Create()
        {
            ToneTrigger toneTrigger = new ToneTrigger();
            toneTrigger.userList = toneTriggerRepository.getUserList();
            return View(toneTrigger);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(ToneTrigger toneTrigger)
        {
            toneTrigger.lowerColorBound = toneTrigger.lowerColorBound.Replace("#", String.Empty);
            toneTrigger.lowerColorBound = Int32.Parse(toneTrigger.lowerColorBound, System.Globalization.NumberStyles.HexNumber).ToString();
            toneTrigger.upperColorBound = toneTrigger.upperColorBound.Replace("#", String.Empty);
            toneTrigger.upperColorBound = Int32.Parse(toneTrigger.upperColorBound, System.Globalization.NumberStyles.HexNumber).ToString();
            if (ModelState.IsValid)
            {
                try
                {
                    Guid id = toneTriggerRepository.addToneTrigger(toneTrigger);
                    return RedirectToAction("Details", new { id = id.ToString() });
                }
                catch
                {
                    return View("Error");
                }
            }

            return View(toneTrigger);
        }

        public ActionResult Delete(string id){
            
            ToneTrigger toneTrigger = toneTriggerRepository.getToneTrigger(id);
            
            if (toneTrigger == null)
            {
                return View("NotFound");
            } 
            else
            {
                return View(toneTrigger);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(string id, string confirmButton){
            
            ToneTrigger toneTrigger = toneTriggerRepository.getToneTrigger(id);
            
            if (toneTrigger == null)
            {
                return View("NotFound");
            }

            toneTriggerRepository.deleteToneTrigger(toneTrigger);
            return View("Deleted");
        }
    }
}