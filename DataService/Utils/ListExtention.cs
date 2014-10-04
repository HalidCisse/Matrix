using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;

namespace DataService.Utils
{
    static class ListExtention
    {

        public static DataTable ToDataTable<T> ( this IList<T> data )
        {
            const double d = 1.0D;
            d.ToString(CultureInfo.InvariantCulture);
            var properties = TypeDescriptor.GetProperties (typeof (T));

            var table = new DataTable ();

            foreach(PropertyDescriptor prop in properties)
            {
                table.Columns.Add (prop.Name, Nullable.GetUnderlyingType (prop.PropertyType) ?? prop.PropertyType);
            }

            foreach(T item in data)
            {
                var row = table.NewRow ();

                foreach(PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue (item) ?? DBNull.Value;
                }

                table.Rows.Add (row);
            }

            return table;
        }

        public static DataRow ToDataRow<T> ( this T data )
        {
            var properties = TypeDescriptor.GetProperties (typeof (T));

            var table = new DataTable ();

            foreach(PropertyDescriptor prop in properties)
            {
                table.Columns.Add (prop.Name, Nullable.GetUnderlyingType (prop.PropertyType) ?? prop.PropertyType);
            }

            var row = table.NewRow ();

            foreach(PropertyDescriptor prop in properties)
            {
                row[prop.Name] = prop.GetValue (data) ?? DBNull.Value;
            }

            table.Rows.Add (row);

            return table.Rows[0];
        }

    }
}
