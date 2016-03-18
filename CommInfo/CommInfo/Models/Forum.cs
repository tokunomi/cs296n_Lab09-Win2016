using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommInfo.Models
{
    public class Forum
    {
        List<Thread> threads = new List<Thread>();

        List<Member> members = new List<Member>();

        // may need this
        List<Message> messages = new List<Message>();

        public int ForumID { get; set; }
        public string ForumName { get; set; }

        public List<Thread> Threads 
        { 
            get { return threads;} 
        }

        public List<Member> Members 
        {
            get { return members; } 
        }

        public List<Message> Messages 
        {
            get { return messages; }
        }
    }
}