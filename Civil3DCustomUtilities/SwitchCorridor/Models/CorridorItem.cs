using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShushilC3DUtilities.Models
{
    public class CorridorItem
    {
        public string Name { get; set; }
        public ObjectId ObjectId { get; set; }
        public bool IsSwitchedOn { get; set; }
    }
}
