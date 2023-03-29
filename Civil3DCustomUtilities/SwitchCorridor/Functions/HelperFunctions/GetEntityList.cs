using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShushilC3DUtilities.Functions.HelperFunctions
{
    public enum HelperEntityClass { Alignment, Surface, Assembly, Corridor }

    public static partial class HelperFunctions
    {
        //Get list of Selected Entities from the Active Document.
        public static List<string> GetEntityListFromDocument(HelperEntityClass EntityClass)
        {
            ObjectIdCollection entityIdCollection = new ObjectIdCollection();
            List<string> EntityNameList = new List<string>();

            //Get the list of selected entities from the C3D Document.
            try
            {
                using (CivilDocument civDoc = CivilApplication.ActiveDocument)
                using (Transaction trans = Application.DocumentManager.MdiActiveDocument.TransactionManager.StartTransaction())
                {
                    //Get ObjectIds of the entity
                    switch (true)
                    {
                        case Boolean flag when flag = (EntityClass == HelperEntityClass.Alignment): entityIdCollection = civDoc.GetAlignmentIds(); break;
                        case Boolean flag when flag = (EntityClass == HelperEntityClass.Surface): entityIdCollection = civDoc.GetSurfaceIds(); break;
                        case Boolean flag when flag = (EntityClass == HelperEntityClass.Assembly):
                            foreach (ObjectId assemblyId in civDoc.AssemblyCollection) entityIdCollection.Add(assemblyId); break;
                        case Boolean flag when flag = (EntityClass == HelperEntityClass.Corridor):
                            foreach (ObjectId corridorId in civDoc.CorridorCollection) entityIdCollection.Add(corridorId); break;
                        default: break;
                    }

                    //Open up entity and get its name to add to the combobox.
                    foreach (ObjectId ObjId in entityIdCollection)
                    {
                        try
                        {
                            CivDb.Entity entity = (CivDb.Entity)trans.GetObject(ObjId, OpenMode.ForRead);
                            EntityNameList.Add(entity.Name);
                        }
                        catch (Exception) { }
                    }
                    //Commit transaction.
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                Application.ShowAlertDialog($"{ex.Message}");
            }
            //Return the list of names of the entity.
            return EntityNameList;
        }
    }
}
