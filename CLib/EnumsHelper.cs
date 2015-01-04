using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace CLib
{
    /// <summary>
    /// Helper
    /// </summary>
    public static class EnumsHelper
    {

        /// <summary>
        /// Gets the description of a specific enum value.
        /// </summary>
        public static string GetEnumDescription(this Enum eValue)
        {
            var nAttributes = eValue.GetType().GetField(eValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (nAttributes.Any()) return ((DescriptionAttribute) nAttributes.First()).Description;

            // If no description is found, best guess is to generate it by replacing underscores with spaces
            // and title case it. You can change this to however you want to handle enums with no descriptions.

            var oTi = CultureInfo.CurrentCulture.TextInfo;
            return oTi.ToTitleCase(oTi.ToLower(eValue.ToString().Replace("_", " ")));
        }


        /// <summary>
        /// Returns an enumerable collection of all values and descriptions for an enum type.
        /// </summary>        
        public static IEnumerable<KeyValuePair<string, Enum>> GetAllValuesAndDescriptions<TEnum>() where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("TEnum must be an Enumeration type");
           
            return from e in Enum.GetValues(typeof(TEnum)).Cast<Enum>() select new KeyValuePair<string, Enum>(e.GetEnumDescription(), e);
        }

    }

}







#region COURS

///// <summary>
///// 
///// </summary>
//public class ValueDescription
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public Enum Value { get; set; }

//    /// <summary>
//    /// 
//    /// </summary>
//    public string Description { get; set; }
//}


//public static IEnumerable<ValueDescription> GetAllValuesAndDescriptions<TEnum>() where TEnum : struct, IConvertible, IComparable, IFormattable
//{
//    if (!typeof(TEnum).IsEnum)
//        throw new ArgumentException("TEnum must be an Enumeration type");

//    return Enum.GetValues(typeof(TEnum)).Cast<Enum>().Select(e => new ValueDescription { Value = e, Description = e.GetEnumDescription() }).ToList();
//}



///// <summary>
///// 
///// </summary>
///// <param name="myCoursString"></param>
///// <returns></returns>
//public static CoursTypes GetCoursType(string myCoursString)
//{
//    if (myCoursString.Equals("Cours")) return CoursTypes.Cours;

//    if (myCoursString.Equals("Cours Theorique")) return CoursTypes.CoursTheorique;

//    if (myCoursString.Equals("Cours Magistral")) return CoursTypes.CoursMagistral;

//    if (myCoursString.Equals("Travaux Pratiques")) return CoursTypes.TravauxPratiques;

//    if (myCoursString.Equals("Revision")) return CoursTypes.Revision;

//    if (myCoursString.Equals("Test")) return CoursTypes.Test;

//    if (myCoursString.Equals("Control")) return CoursTypes.Control;

//    return myCoursString.Equals("Examen") ? CoursTypes.Examen : CoursTypes.None;
//}

///// <summary>
///// 
///// </summary>
///// <param name="myCoursType"></param>
///// <returns></returns>
//public static string GetCoursString(CoursTypes myCoursType)
//{
//    if (myCoursType.Equals(CoursTypes.Cours)) return "Cours"; 

//    if (myCoursType.Equals(CoursTypes.CoursTheorique)) return "Cours Theorique";

//    if (myCoursType.Equals(CoursTypes.CoursMagistral)) return "Cours Magistral";

//    if (myCoursType.Equals(CoursTypes.TravauxPratiques)) return "Travaux Pratiques";

//    if (myCoursType.Equals(CoursTypes.Revision)) return "Revision";

//    if (myCoursType.Equals(CoursTypes.Test)) return "Test";

//    if (myCoursType.Equals(CoursTypes.Control)) return "Control";

//    return myCoursType.Equals(CoursTypes.Examen) ? "Examen" : "None";
//}

///// <summary>
///// 
///// </summary>
///// <returns></returns>
//public static IEnumerable GetAllCoursTypesString()
//{
//    return (from CoursTypes en in Enum.GetValues(typeof (CoursTypes)) select GetCoursString(en)).ToList();
//}


#endregion