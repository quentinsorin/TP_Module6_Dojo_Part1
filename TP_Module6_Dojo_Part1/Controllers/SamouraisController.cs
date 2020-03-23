using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using TP_Module6_Dojo_Part1.Models;

namespace TP_Module6_Dojo_Part1.Controllers
{
    public class SamouraisController : Controller
    {
        private Context db = new Context();

        // GET: Samourais
        public ActionResult Index()
        {
            return View(db.Samourais.ToList());
        }

        // GET: Samourais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            //On récupère la liste des armes
            var samouraiVM = new SamouraiVM();
            samouraiVM.Armes = db.Armes.ToList();
            return View(samouraiVM);
        }

        // POST: Samourais/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SamouraiVM samouraiVm)
        {
            if (ModelState.IsValid)
            {
                // Si une arme est choisie alors on affecte  au samourai l'id de l'arme
                if (samouraiVm.IdSelectedArme.HasValue)
                {
                    
                    samouraiVm.Samourai.Arme = db.Armes.FirstOrDefault(a => a.Id == samouraiVm.IdSelectedArme.Value);
                }

                //on enregistre en base
                db.Samourais.Add(samouraiVm.Samourai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            samouraiVm.Armes = db.Armes.ToList();
            return View(samouraiVm);
        }

        // GET: Samourais/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            var samouraiVm = new SamouraiVM();
            samouraiVm.Armes = db.Armes.ToList();
            samouraiVm.Samourai = samourai;

            //On verifie l'existence de l'arme pour la préselectionner
            if (samourai.Arme != null)
            {
                samouraiVm.IdSelectedArme = samourai.Arme.Id;
            }
            return View(samouraiVm);
        }

        // POST: Samourais/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SamouraiVM samouraiVm)
        {
            if (ModelState.IsValid)
            {
                var samouraiDb = db.Samourais.Find(samouraiVm.Samourai.Id);
                samouraiDb.Nom = samouraiVm.Samourai.Nom;
                samouraiDb.Arme = null;
                samouraiDb.Force = samouraiVm.Samourai.Force;
                //on vérifie si on a une arme de sélectionner
                if (samouraiVm.IdSelectedArme.HasValue)
                {
                    samouraiDb.Arme = db.Armes.FirstOrDefault(a => a.Id == samouraiVm.IdSelectedArme.Value);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            samouraiVm.Armes = db.Armes.ToList();
            return View(samouraiVm);
        }

        // GET: Samourais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Samourai samourai = db.Samourais.Find(id);
            db.Samourais.Remove(samourai);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
