using Api.Models;
using ChoMoi.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAPI.Models
{
    public partial class Book : AuditableEntity<int>
    {
        public Book()
        {
        }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }


        [Required(ErrorMessage = "CategoryId is required")]
        public int CategoryId { get; set; }


        [Required(ErrorMessage = "PublisherId is required")]
        public int PublisherId { get; set; }


        [ForeignKey("BookBuy")]
        public int? BookBuyOnlineId { get; set; }

        [ForeignKey("BookBuy")]
        public int? BookBuyOffileId { get; set; }

        public virtual BookBuy BookBuyOnline { get; set; }
        public virtual BookBuy BookBuyOffile { get; set; }

        public virtual BookCategory Category { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<BookAuthors> BookAuthors { get; set; }
    }
}
