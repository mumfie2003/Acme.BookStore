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

        public TestAppService(
                    IBackgroundJobManager backgroundJobManager,
                    IRepository<Book, Guid> bookRepository)
        {
            _backgroundJobManager = backgroundJobManager;
            _bookRepository = bookRepository;
        }

        public async Task CreateBackgroundJob(string body)
        {
            var args = new EmailSendingArgs
            {
                Subject = "Created by background task",
                Body = body
            };
            await _backgroundJobManager.EnqueueAsync(args);
        }
        public async Task ExecuteJobDirect(string body)
        {
            var args = new EmailSendingArgs
            {                
                Subject = "Directly executed",
                Body = body
            };
            EmailSendingJob job = new EmailSendingJob(_bookRepository);
            await job.ExecuteAsync(args);
        }
    }
}
