using Autodesk.AutoCAD.Runtime;
using ShushilC3DUtilities.Functions;
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
            SwitchBoard newSwitch = new SwitchBoard();
            //Get the current states of corridor visibility
            var corridorStates = newSwitch.GetCorridorStates();

            //Update it on GUI

            //Read changes from GUI


            //Make changes in the corridor states.
            newSwitch.SwitchCorridorOnOff(corridorStates);

        }

        public void Initialize()
        {

        }

        public void Terminate()
        {

        }
    }
}
