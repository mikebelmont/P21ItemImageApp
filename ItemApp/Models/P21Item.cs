using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemApp.Models
{
    public class P21Item
    {
        [Key]
        public int inv_mast_uid { get; set; }
        public string ItemID { get; set; }
        public string Description { get; set; }
        public string ExtendedDescription { get; set; }
        public string PrimaryBin { get; set; }
        public List<Image> Images { get; set; }
    }
}
