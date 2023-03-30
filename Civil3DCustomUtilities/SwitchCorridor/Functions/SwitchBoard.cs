using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.DatabaseServices;
using ShushilC3DUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShushilC3DUtilities.Functions
{
    public class SwitchBoard
    {
        public void SwitchCorridorOnOff(List<CorridorItem> CorridorStates)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            CivilDocument cDoc = CivilApplication.ActiveDocument;

            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                //Get the list of corridors
                //var corridors = cDoc.CorridorCollection;

                foreach (var corridorState in CorridorStates)
                {
                    try
                    {
                        //Open up corridors
                        var corridor = (Corridor)trans.GetObject(corridorState.ObjectId, OpenMode.ForWrite);

                        //Turn On/Off Corridors.
                        corridor.Visible = corridorState.VisibleState;

                        corridor.Rebuild();
                    }
                    catch (Exception ex)
                    {
                        Application.ShowAlertDialog("Error message: " + ex.Message + Environment.NewLine
                            + "Stack Trace" + ex.StackTrace);
                    }

                    //End transaction
                    trans.Commit();
                }
            }
        }

        public List<CorridorItem> GetCorridorStates()
        {
            //Get current C3D Documents
            Document doc = Application.DocumentManager.MdiActiveDocument;
            CivilDocument cDoc = CivilApplication.ActiveDocument;

            //Create a list of CorridorItems to hold its states.
            List<CorridorItem> CorridorStates = new List<CorridorItem>();

            using (Transaction trans = doc.TransactionManager.StartTransaction())
            {
                //Get the list of corridors
                var corridors = cDoc.CorridorCollection;

                foreach (var corridorId in corridors)
                {
                    try
                    {
                        //Open up corridors
                        var corridor = (Corridor)trans.GetObject(corridorId, OpenMode.ForWrite);

                        //Collect this corridor state.
                        CorridorItem CorridorState = new CorridorItem()
                        {
                            ObjectId = corridorId,
                            Name = corridor.Name,
                            VisibleState = corridor.Visible,
                        };

                        //Add it to the list.
                        CorridorStates.Add(CorridorState);
                    }
                    catch (Exception ex)
                    {
                        Application.ShowAlertDialog("Error message: " + ex.Message + Environment.NewLine
                            + "Stack Trace" + ex.StackTrace);
                    }

                    //End transaction
                    trans.Commit();
                }
            }

            return CorridorStates;
        }
    }
}
