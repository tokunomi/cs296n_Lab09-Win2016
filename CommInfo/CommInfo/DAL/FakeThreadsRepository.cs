using CommInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommInfo.DAL
{
    public class FakeThreadsRepository : IThreadsRepository
    {
        //private List<Thread> threads = new List<Thread>();
        private List<Thread> threads;
        private int trdId = 0;  // for Thread IDs

        public FakeThreadsRepository()
        {
            threads = new List<Thread>();
        }

        public FakeThreadsRepository(List<Thread> t)
        {
            threads = t;
        }

        public Thread AddThread(Thread thread)
        {
            thread.ThreadID = ++trdId;  // auto-increments the ThreadID
            threads.Add(thread);
            return thread;
        }

        // refactored to return the thread instead of an int
        public Thread DeleteThreadById(int id)
        {
            Thread thread = GetThreadById(id);
            threads.Remove(thread);
            return thread;
        }

        //public int DeleteThreadById(int id)
        //{
        //    if (threads.Remove(GetThreadById(id)))
        //    { return 1; }
        //    else
        //    { return 0; }
        //}

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public List<Thread> GetAllThreads()
        {
            return threads;
        }

        public Thread GetThreadById(int? id)
        {
            return threads.Find(t => t.ThreadID == id);
        }

        public int UpdateThread(Thread thread)
        {
            int threadUpdated = 0;
            if (DeleteThreadById(thread.ThreadID) != null)
            {
                threads.Add(thread);
                threadUpdated = 1;
            }
            return threadUpdated;
        }
    }
}