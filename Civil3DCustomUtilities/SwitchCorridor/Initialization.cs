using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCorridor
{
    public class Initialization : IExtensionApplication
    {

        [CommandMethod("SWITCHCORRIDORS")]
        public void AcadCmdSwitchCorridors()
        {

        }

        public void Initialize()
        {

        }

        public void Terminate()
        {

        }
    }
}
