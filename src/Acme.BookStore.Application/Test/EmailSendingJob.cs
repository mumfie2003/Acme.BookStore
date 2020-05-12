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
        public IRepository<Book, Guid> BookRepository { 
            get { return _bookRepository; } 
            set { 
                _bookRepository = value; } }
        private IRepository<Book, Guid> _bookRepository;
        public EmailSendingJob()
        {

        }
        //public EmailSendingJob(IRepository<Book, Guid> bookRepository)
        //{
        //   // _bookRepository = bookRepository;
        //}

        //public override void Execute(EmailSendingArgs args)
        //{
        //    const string MODULE = "Execute";
        //    MyLogIt(MODULE, "BEGIN");
        //    var query = BookRepository
        //        .Where(p => p.Price > 10.0);
        //    var results = query.ToList();

        //    MyLogIt(MODULE, "END");

        //}

        private static void MyLogIt(string module, string output)
        {
            using (StreamWriter sw = File.AppendText(@"E:\abd-Projects\TvGuide\Src\BackgroundJobs\aspnet-core\src\AppsByDesign.BackgroundJobs.Web\Logs\debugit.txt"))
            {
                sw.WriteLine($"{DateTime.Now.TimeOfDay} {module}->{output}");
            }
        }

        public override async Task ExecuteAsync(EmailSendingArgs args)
        {
            const string MODULE = "ExecuteAsync";
            MyLogIt(MODULE, "BEGIN");
            var books = await BookRepository
                .Where(p => p.Price > 10.0)
                .ToListAsync();
           
            MyLogIt(MODULE, $"END Count:{books.Count()}");
        }
    }
}
