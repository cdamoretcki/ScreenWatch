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
    public class ToneTriggerController : Controller
    {
        ToneTriggerRepository toneTriggerRepository = new ToneTriggerRepository();

        //
        // GET: /User/

        public ActionResult Index()
        {
            try
            {
                List<ToneTrigger> toneTriggers = toneTriggerRepository.getAllToneTriggers();
                return View("Index", toneTriggers);
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
                ToneTrigger toneTrigger = toneTriggerRepository.getToneTrigger(id);
                toneTrigger.userList = toneTriggerRepository.getUserList(toneTrigger.userName);
                return View(toneTrigger);
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
                ToneTrigger toneTrigger = toneTriggerRepository.getToneTrigger(id);
                toneTrigger.userList = toneTriggerRepository.getUserList(toneTrigger.userName);
            
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
                ToneTrigger toneTrigger = new ToneTrigger();
                toneTrigger.userList = toneTriggerRepository.getUserList();
                return View(toneTrigger);
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
        public ActionResult Create(ToneTrigger toneTrigger)
        {
            try
            {
                toneTrigger.userList = toneTriggerRepository.getUserList(toneTrigger.userName);
                if (ModelState.IsValid)
                {
                    toneTrigger.lowerColorBound = toneTrigger.lowerColorBound.Replace("#", String.Empty);
                    toneTrigger.lowerColorBound = Int32.Parse(toneTrigger.lowerColorBound, System.Globalization.NumberStyles.HexNumber).ToString();
                    toneTrigger.upperColorBound = toneTrigger.upperColorBound.Replace("#", String.Empty);
                    toneTrigger.upperColorBound = Int32.Parse(toneTrigger.upperColorBound, System.Globalization.NumberStyles.HexNumber).ToString();
                    Guid id = toneTriggerRepository.addToneTrigger(toneTrigger);
                    return RedirectToAction("Details", new { id = id.ToString() });
                }

                return View(toneTrigger);
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
                ToneTrigger toneTrigger = toneTriggerRepository.getToneTrigger(id);

                if (toneTrigger == null)
                {
                    return View("NotFound");
                }

                toneTriggerRepository.deleteToneTrigger(toneTrigger);
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