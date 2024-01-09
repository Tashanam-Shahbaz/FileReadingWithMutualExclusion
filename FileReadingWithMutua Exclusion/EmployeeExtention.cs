using System;
using System.Collections.Generic;

namespace FileReadingWithMutual_Exclusion
{
    public static class EmployeeExtention
    {
        public static List<T> MyWhere<T>(this List<T> records, Func< T, bool> func)
        {
            List<T> filteredList = new List<T>();

            foreach (T record in records)
            {
                if (func(record))
                {
                    filteredList.Add(record);
                }
            }
            return filteredList;
        }

        public static List<TResult> MySelect<TSource, TResult>(this List<TSource> records , Func<TSource, TResult> func)
        {
            List<TResult> results = new List<TResult>();
            foreach (var record in records) 
            {
                TResult result = func(record);
                results.Add(result);
            }
            return results;

        }
    }
}

