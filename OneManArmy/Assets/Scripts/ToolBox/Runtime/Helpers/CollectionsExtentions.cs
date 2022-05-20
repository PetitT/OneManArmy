using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LowTeeGames
{
    public static class CollectionsExtentions
    {
        public static T GetRandom<T>(this List<T> list)
        {
            int random = Random.Range(0, list.Count);
            T item = list[random];
            return item;
        }

        public static T GetRandom<T>(this T[] arr)
        {
            int random = Random.Range(0, arr.Length);
            T item = arr[random];
            return item;
        }

        public static T GetRandom<T, U>(this Dictionary<T, U> dictionnary)
        {
            List<T> tempList = new List<T>();

            foreach (var key in dictionnary.Keys)
            {
                tempList.Add(key);
            }

            int random = Random.Range(0, tempList.Count);
            T item = tempList[random];
            return item;
        }

        public static T GetRandom<T>(this IList<T> list)
        {
            int random = Random.Range(0, list.Count);
            T item = list[random];
            return item;
        }
    }
}
