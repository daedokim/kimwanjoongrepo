using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.dug.common
{
    public class JsonConverter
    {
        public static string ToJson (object obj)
        {
            string jsonString = JsonFx.Json.JsonWriter.Serialize(obj);
            return jsonString;
        }

        public static T FromJson<T>(string jsonString)
        {
            T obj = JsonFx.Json.JsonReader.Deserialize<T>(jsonString);
            return obj;
        }

        public static T GetObject<T>(Dictionary<string, object> dict)
        {
            Type type = typeof(T);
            var obj = Activator.CreateInstance(type);

            foreach (var kv in dict)
            {
                System.Reflection.FieldInfo property = type.GetField(kv.Key);

                if(property != null)
                {
                    property.SetValue(obj, kv.Value);
                }
            }
            return (T)obj;
        }

        public static T[] GetObjects<T>(Dictionary<string, object>[] dicts)
        {
            T[] objs = null;

           Type type = typeof(T);
          
            if(dicts != null && dicts.Length > 0)
            {
                objs = new T[dicts.Length];
                
                for(int i = 0; i < dicts.Length; i++)
                {
                    T obj = (T)Activator.CreateInstance(type);

                    foreach (var kv in dicts[i])
                    {
                        System.Reflection.FieldInfo property = type.GetField(kv.Key);

                        if (property != null)
                        {
                            property.SetValue(obj, kv.Value);
                        }
                    }

                    objs[i] = obj;
                }
            }

            return objs;
        }

        public static List<T> GetObjectList<T>(Dictionary<string, object>[] dicts)
        {
           List<T> objs = null;

            Type type = typeof(T);

            if (dicts != null && dicts.Length > 0)
            {
                objs = new List<T>();

                for (int i = 0; i < dicts.Length; i++)
                {
                    T obj = (T)Activator.CreateInstance(type);

                    foreach (var kv in dicts[i])
                    {
                        System.Reflection.FieldInfo property = type.GetField(kv.Key);

                        if (property != null)
                        {
                            property.SetValue(obj, kv.Value);
                        }
                    }

                    objs.Add(obj);
                }
            }

            return objs;
        }


    }

}
