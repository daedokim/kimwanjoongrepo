using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace com.dug.UI.Events
{
    public class Listener
    {
        public GameObject Target { get; set; }
        public string Key { get; set; }
        public string FunctionName { get; set; }

        public Listener(GameObject target, string key, string functionName)
        {
            this.Target = target;
            this.Key = key;
            this.FunctionName = functionName;
        }
    }

    public class Handler
    {
        public object Target { get; set; }
        public string Key { get; set; }
        public Action<object> Callback { get; set; }

        public Handler(object target, string key, Action<object> callback)
        {
            this.Target = target;
            this.Key = key;
            this.Callback = callback;
        }
    }

    public class GameData
    {
        public string Result { get; set; }
        public object Data { get; set; }

        public GameData(string result = "", object data = null)
        {
            Result = result;
            Data = data;
        }
    }

    public class GameEventHandler
    {
        private const string TAG = "NotificationCenter";

        private static GameEventHandler instance;

        private List<Handler> handlers = new List<Handler>();

        private System.Object thisLock = new System.Object();

        public static GameEventHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameEventHandler();
                }

                return instance;
            }
        }

        private GameEventHandler() { }

        public void AddHandler(object target, string key, Action<object> callback)
        {
            if (IsExistHandler(target, key, callback))
            {
                return;
            }

            handlers.Add(new Handler(target, key, callback));
        }

        public int RemoveHandler(object target)
        {
            int count = 0;
            if (target == null)
            {
                CheckHandlerObject();
                return count;
            }

            foreach (var f in handlers.FindAll(l => l.Target == target))
            {
                count++;
                handlers.Remove(f);
            }

            return count;
        }

        public int RemoveHandler(object target, string key)
        {
            int count = 0;
            if (target == null)
            {
                CheckHandlerObject();
                return count;
            }

            foreach (var f in handlers.FindAll(l => l.Target == target && l.Key.Equals(key)))
            {
                count++;
                handlers.Remove(f);
            }

            return count;
        }

        public int RemoveHandler(object target, string key, Action<object> callback)
        {
            int count = 0;
            if (target == null)
            {
                CheckHandlerObject();
                return count;
            }

            foreach (var f in handlers.FindAll(l => l.Target == target && l.Key.Equals(key) && l.Callback == callback))
            {
                count++;
                handlers.Remove(f);
            }

            return count;
        }

        private void CheckHandlerObject()
        {
            if (handlers != null && handlers.Count > 0)
            {
                for (int i = (handlers.Count - 1); i >= 0; i--)
                {
                    Handler item = handlers[i];
                    if (item == null)
                    {
                        handlers.RemoveAt(i);
                        continue;
                    }

                    if (item.Target == null)
                    {
                        handlers.RemoveAt(i);
                        continue;
                    }

                    if (item.Callback == null)
                    {
                        handlers.RemoveAt(i);
                    }
                }
            }
        }

        public void Invoke(string key, object args = null)
        {
            lock (thisLock)
            {
                foreach (var f in handlers.FindAll(l => l.Key.Equals(key)))
                {
                    if (f.Callback == null)
                    {
                        handlers.Remove(f);
                        continue;
                    }

                    f.Callback(args);
                }
            }
        }

        private bool IsExistHandler(object target, string key, Action<object> callback)
        {
            List<Handler> temp = handlers.FindAll(l => l.Target == target && l.Key.Equals(key) && l.Callback == callback);

            if (temp != null && temp.Count > 0)
            {
                return true;
            }

            return false;
        }

    }
}
