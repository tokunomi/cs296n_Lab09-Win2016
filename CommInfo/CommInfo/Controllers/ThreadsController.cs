using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CommInfo.Models;
using System.Collections;
using CommInfo.DAL;

namespace CommInfo.Controllers
{
    public class ThreadsController : Controller
    {
        //private CommInfoContext db = new CommInfoContext();
        private IThreadsRepository repo;  // for Unit Testing
        
        public ThreadsController()
        {
            repo = new ThreadsRepository();
        }

        public ThreadsController(IThreadsRepository t)
        {
            repo = t;
        }

        // GET: Threads
        public ActionResult Index()
        {
            //return View(db.Threads.ToList());
            return View(repo.GetAllThreads());  // for Unit Testing
        }

        // GET: Threads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Thread thread = db.Threads.Find(id);
            Thread DbThread = repo.GetThreadById(id);

            /* Comment out while Unit Testing
            //List(IEnumerable(MessageViewModel)) messageVM = new List<IEnumerable(MessageViewModel)>;
            //IEnumerable<MessageViewModel> messageVM;
            List<MessageViewModel> messageVM = new List<MessageViewModel>();
            // needs an IEnumerable

            // Get the messages
            var messages = from m in db.Messages
                           select m;   
            //var DbMessages = from m in repo.Messages  // trying to use this for Unit Testing, but not working
            //                 select m;

            // In a loop:
            foreach (Message m in DbMessages)
            {
                // TODO: Get the thread that contains each message
                var topic = (from t in db.Threads
                             where t.ThreadID == m.ThreadID
                             select t).FirstOrDefault();
                // Create a View model for the message and put it in the list of view models
                messageVM.Add(new MessageViewModel() { Date = m.Date,
                                                       From = m.From,
                                                       //Topic = m.ThreadItem.Topic,
                                                       //Subject = m.Subject,
                                                       Body = m.Body 
                                                      });
            }
            */

            if (DbThread == null)  // Unit Testing
            //if (thread == null)
            {
                return HttpNotFound();
            }

            /* Comment out while Unit Testing
            // if there is just one message, display it
            if (messageVM.Count == 1)
            {
                return View("Details", messageVM[0]);
            }
            // if there is more than one message display the list of message
            else
            {
                return View("Index", messageVM);
            }
             */

            return View(DbThread);
            //return View(thread);
        }

        // GET: Threads/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Threads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ThreadID")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                /* Comment out while Unit Testing
                db.Threads.Add(thread);
                db.SaveChanges(); */
                repo.AddThread(thread);  // Unit Testing
                return RedirectToAction("Index");
            }

            return View(thread);
        }

        // GET: Threads/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            /* comment out while Unit Testing
            Thread thread = db.Threads.Find(id); */
            Thread thread = repo.GetThreadById(id);  //Unit Testing
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // POST: Threads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ThreadID")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                /* Comment out while Unit Testing
                db.Entry(thread).State = EntityState.Modified;
                db.SaveChanges();  */
                repo.UpdateThread(thread);
                return RedirectToAction("Index");
            }
            return View(thread);
        }

        // GET: Threads/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Thread thread = db.Threads.Find(id);  // Comment out while Unit Testing
            Thread thread = repo.GetThreadById(id);  //Unit Testing
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // POST: Threads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /* Comment out while Unit Testing
            Thread thread = db.Threads.Find(id);
            db.Threads.Remove(thread);
            db.SaveChanges(); */
            repo.DeleteThreadById(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                repo.Dispose();  // Unit Testing
            }
            base.Dispose(disposing);
        }
    }
}
