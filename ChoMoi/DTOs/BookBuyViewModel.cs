using ChoMoi.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoMoi.DTOs
{
    public class BookBuyViewModel
    {
        public int Id { get; set; }
        public string BuyFrom { get; set; }

        public BookBuyViewModel(BookBuy bookBuy)
        {
            Id = bookBuy.Id;
            BuyFrom = bookBuy.BuyFrom;
        }
    }
}
