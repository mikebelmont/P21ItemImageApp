using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemApp.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string ImageLocation { get; set; }
        public int P21ItemID { get; set; }
        [NotMapped]
        [DisplayName("Add image for this item")]
        public IFormFile ImageFile{ get; set; }
    }
}
