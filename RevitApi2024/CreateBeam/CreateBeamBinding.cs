using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitApi2024.CreateBeamFrom;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitApi2024.CreateBeam
{
    [Transaction(TransactionMode.Manual)]
    public class CreateBeamBinding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            //View view = doc.ActiveView;

            //FamilyInstance beam = null;
            //LocationCurve beamLocation = beam.Location as LocationCurve;
            //Line lineBeam = beamLocation.Curve as Line;
            //XYZ beamDirection = lineBeam.Direction.Normalize();

            //XYZ rightDirection = view.RightDirection;
            //XYZ upDirection = view.UpDirection;
            //XYZ viewDirection = view.ViewDirection;

            //// tim vector vuong goc goc voi 2 vector khong song song
            //XYZ perpendicularBeam = beamDirection.CrossProduct(viewDirection).Normalize();

            //XYZ vectorA = null;
            //XYZ vectorB = null;


            //vectorA = vectorA.Normalize();
            //vectorB = vectorB.Normalize();

            //double tichVoHuong = vectorA.DotProduct(vectorB); // tich vo huong cua vector a va vector b chinh la cos b

            //if (Math.Abs(tichVoHuong) < 0.00001) // vuong goc
            //{

            //}
            //if (Math.Abs(Math.Abs(tichVoHuong) - 1) < 0.00001) // la song song
            //{

            //}

            //double radian = vectorA.AngleTo(vectorB); // goc giua 2 vector

            //Wall wallMove = null;
            //XYZ vectorMove = null;
            //XYZ pointMove = null;
            //Transform trasform1 = Transform.CreateTranslation(vectorA);
            //XYZ vectorMoveByVectorA = trasform1.OfVector(vectorMove);
            //ElementTransformUtils.MoveElement(doc, wallMove.Id, vectorMoveByVectorA);
            //XYZ vectorMovePoint = trasform1.OfPoint(pointMove);


            //Curve cureOrigin = null;
            //cureOrigin.CreateTransformed(trasform1);

            //CurveLoop curveLoop = null;

            //CurveLoop newCurveloop = CurveLoop.CreateViaOffset(curveLoop, 100 / 304.8, new XYZ(0, 0, 1));

            //Curve curveA = Line.CreateBound(XYZ.Zero, XYZ.Zero.Add(new XYZ(20,0,0)));
            //Curve curveB = Line.CreateBound(XYZ.Zero, XYZ.Zero.Add(new XYZ(0, 20, 0)));
            //// Kiem tra 2 duong curve co cat nhau khong
            //SetComparisonResult intersectionResusult= curveA.Intersect(curveB,out IntersectionResultArray resultArray);
            //foreach(IntersectionResult intersectionResult in resultArray) 
            //{
            //    if (intersectionResult != null)
            //    {
            //        XYZ point = intersectionResult.XYZPoint;
            //    }
            //}

            //Line line = Line.CreateBound(XYZ.Zero, new XYZ(2, 2, 2));
            //Line line2 = Line.CreateUnbound(XYZ.Zero, XYZ.BasisX);


            //Face face = null;
            //PlanarFace plannarFace = face as PlanarFace;
            //Line lineCheck = null;
            //XYZ pointCheck = null;

            //// chieu diem xuong plannar face
            //var interectionOnPlane= plannarFace.Project(pointCheck);

            //Plane plane = Plane.CreateByNormalAndOrigin(XYZ.BasisZ, XYZ.Zero);

            //plane = Plane.CreateByOriginAndBasis(new XYZ(1,2,3), XYZ.BasisY, XYZ.BasisZ);
            //XYZ rightPlane = plane.XVec;
            //XYZ upPlane= plane.YVec;
            //XYZ normalPlane = plane.Normal;


            //// chieu diem xuong plane
            //plane.Project(pointCheck, out UV uv, out double distance);
            //XYZ projectPt1 = plane.Origin + uv.U * plane.XVec + uv.V * plane.YVec;


            //XYZ directionLine = lineCheck.Direction.Normalize();
            //XYZ sLine = lineCheck.GetEndPoint(0);


            //double dotLineNormalPlane = directionLine.DotProduct(plane.Normal);
            //if(!(Math.Abs(dotLineNormalPlane) < 0.0001))
            //{
            //    double lineParameter = (plane.Normal.DotProduct(plane.Origin) 
            //        - plane.Normal.DotProduct(sLine))
            //        / plane.Normal.DotProduct(directionLine);

            //    XYZ intersecPoint = sLine + lineParameter * directionLine;

            //}

            CreateBeamAppShow.ShowForm();




            return Result.Succeeded;
        }
    }
}
