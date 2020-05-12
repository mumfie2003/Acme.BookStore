using Acme.BookStore.BookStore;
using Acme.BookStore.Test;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore
{
    public class TestAppService : ApplicationService
    {       
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IRepository<Book, Guid> _bookRepository; 

        public TestAppService(IBackgroundJobManager backgroundJobManager,
             IRepository<Book, Guid> bookRepository)
        {
            _backgroundJobManager = backgroundJobManager;
            _bookRepository = bookRepository;
        }

        public async Task CreateBackgroundJob(string userName, string emailAddress, string password)
        {
            var args = new EmailSendingArgs
            {
                EmailAddress = emailAddress,
                Subject = "You've successfully registered!",
                Body = "..."
            };

            await _backgroundJobManager.EnqueueAsync(args);
        }
        public void ExecuteJobDirect(string userName, string emailAddress, string password)
        {
           
            var args = new EmailSendingArgs
            {
                EmailAddress = emailAddress,
                Subject = "You've successfully registered!",
                Body = "..."
            };
            EmailSendingJob job = new EmailSendingJob();
            job.BookRepository = _bookRepository;
            job.Execute(args);
        }
    }
}
