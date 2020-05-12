using Acme.BookStore.BookStore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Test
{
    public class EmailSendingJob : AsyncBackgroundJob<EmailSendingArgs>, ITransientDependency
    {
        private readonly IRepository<Book, Guid> _bookRepository;

        public EmailSendingJob(IRepository<Book, Guid> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public override async Task ExecuteAsync(EmailSendingArgs args)
        {
            const string MODULE = "ExecuteAsync";
            using (StreamWriter sw = File.AppendText(@"F:\LogsTest\debugit.txt"))
            {
                try
                {
                    sw.WriteLine();
                    sw.WriteLine($"{MODULE} {DateTime.Now.TimeOfDay}   body:{args.Body}");
                    var books = await _bookRepository
                                    .Where(p => p.Price > 0)
                                    .ToListAsync();
                    foreach (var b in books)
                    {
                        sw.WriteLine($"     Book:{b.Name}");
                    }
                    sw.WriteLine($"     Book Count:{books.Count()}");
                }
                catch (Exception ex)
                {
                    sw.WriteLine($"ERROR  :{ex.Message}");
                    throw;
                }
            }
        }
    }
}
