using DemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.ViewModels
{
    public class BookViewModel
    {
        public string Title { get; set; }
        public string Catergory { get; set; }
        public string Publisher { get; set; }
        public string BuyFromOnline { get; set; }
        public string BuyFromOffline { get; set; }
        public ICollection<String> Authors { get; set; }

        public BookViewModel(Book book)
        {
            Title = book.Title;
            Catergory = book.Category.Name;
            Publisher = book.Publisher.Name;
            BuyFromOnline = book.BookBuyOnline?.BuyFrom;
            BuyFromOffline = book.BookBuyOffile?.BuyFrom;
            Authors = book.BookAuthors.Where(x => x.Book.Id == book.Id).Select(x => x.User.UserName).ToList();
        }

        
    }


}
