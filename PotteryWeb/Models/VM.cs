using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PotteryWeb.Models
{
    public class VM
    {
        public tbProduct tbProduct{ get; set; }

        public List<tbCategory> CategoryList { get; set; }
    }
}