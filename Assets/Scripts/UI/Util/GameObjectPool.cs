using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.dug.UI.util
{
    public class GameObjectPool<T> where T : class
    {
        // Instance count to create.
        short count;
        public delegate T Func();
        Func createFn;

        // Instances.
        Stack<T> objects;

        public GameObjectPool(short count, Func fn)
        {
            this.count = count;
            this.createFn = fn;
            this.objects = new Stack<T>(this.count);
            Allocate();
        }

        void Allocate()
        {
            for (int i = 0; i < this.count; ++i)
            {
                this.objects.Push(this.createFn());
            }
        }

        public T Pop()
        {
            if (this.objects.Count <= 0)
            {
                Allocate();
            }
            return this.objects.Pop();
        }

        public void Push(T obj)
        {
            this.objects.Push(obj);
        }

        public int Length
        {
            get
            {
                return this.objects.Count;
            }
        }
    }

}
