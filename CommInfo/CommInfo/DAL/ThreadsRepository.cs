using CommInfo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CommInfo.DAL
{
    public class ThreadsRepository : IDisposable, IThreadsRepository
    {
        CommInfoContext db = new CommInfoContext();

        public List<Message> Messages = new List<Message>();

        public List<Thread> GetAllThreads()
        {
            return db.Threads.ToList();
        }

        public Thread GetThreadById(int? id)
        {
            return db.Threads.Find(id);
        }

        // AddThread() Vsn. 1: returns object
        public Thread AddThread(Thread thread)
        {
            Thread dbThread = db.Threads.Add(thread);
            db.SaveChanges();
            return dbThread;
        }   /* */

        // AddThread() Vsn. 2: returns int
        //public int AddThread(Thread thread)
        //{
        //    Thread dbThread = db.Threads.Add(thread);
        //    return db.SaveChanges();
        //}

        public int UpdateThread(Thread thread)
        {
            db.Entry(thread).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //public int DeleteThreadById(int id)
        //{
        //    Thread thread = GetThreadById(id);
        //    db.Threads.Remove(thread);
        //    return db.SaveChanges();
        //}

        public Thread DeleteThreadById(int id)
        {
            Thread thread = GetThreadById(id);
            db.Threads.Remove(thread);
            db.SaveChanges();
            return thread;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}