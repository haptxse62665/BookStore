using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChoMoi.DTOs
{
    public class InsertBookViewModel 
    {
        [Required]
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public int PublisherId { get; set; }
        public int? BookBuyOnlineId { get; set; }
        public int? BookBuyOffileId { get; set; }
        public List<string> AuthorIds { get; set; }
    }
}
