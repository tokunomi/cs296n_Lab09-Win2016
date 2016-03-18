using CommInfo.Models;
using System;
namespace CommInfo.DAL
{
    public interface IThreadsRepository
    {
        CommInfo.Models.Thread AddThread(CommInfo.Models.Thread thread);
        Thread DeleteThreadById(int id);
        void Dispose();
        System.Collections.Generic.List<CommInfo.Models.Thread> GetAllThreads();
        CommInfo.Models.Thread GetThreadById(int? id);
        int UpdateThread(CommInfo.Models.Thread thread);
    }
}
