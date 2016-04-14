using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DAL
{
    class CommonFunctions
    {
        public static string GetConnectionString()
        {
                     string result =  ConfigurationManager.ConnectionStrings["LocalDBConnection"].ConnectionString;

            return result;
        }

    //    public void Populate_Value(ref object Class_Value, object Database_Value)
    //    {
    //       if (Database_Value != DBNull.Value)
    //        { Class_Value = Database_Value; }
    //       else
    //        {
    //            if (Class_Value == null)
    //                { Class_Value = null; }
    //            else
    //            {
    //                switch (Class_Value.GetType().ToString())
    //                {
    //                           Case "Integer", "Double", "Decimal":
    //                    Class_Value = 0;
    //                Case "String":
    //                    Class_Value = "";
    //                Case GetType(Date)
    //                    Class_Value = Now
    //                Case GetType(Boolean)
    //                    Class_Value = False
    //                Case Else
    //                    Class_Value = Nothing


    //            }
    //        }
    //        }
    //    }
    ////    Public Sub Populate_Value(ByRef Class_Value As Object, ByVal Database_Value As Object)
    ////    If Not IsDBNull(Database_Value) Then
    ////        Class_Value = Database_Value
    ////    Else
    ////        If Class_Value Is Nothing Then
    ////            Class_Value = Nothing
    ////        Else
    ////            Select Case Class_Value.GetType
    ////                Case GetType(Integer), GetType(Double)
    ////                    Class_Value = 0
    ////                Case GetType(String)
    ////                    Class_Value = ""
    ////                Case GetType(Date)
    ////                    Class_Value = Now
    ////                Case GetType(Boolean)
    ////                    Class_Value = False
    ////                Case Else
    ////                    Class_Value = Nothing
    ////            End Select
    ////        End If
    ////    End If
    ////End Sub
    }
}
