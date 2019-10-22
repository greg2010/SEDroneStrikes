using Sandbox.ModAPI.Ingame;
using System.Collections.Generic;
using System.Linq;
using System;


namespace IngameScript
{
    partial class Program
    {
        class Logger
        {
            // Implementation is taken from https://codereview.stackexchange.com/a/135589
            public class ConcurrentCircularBuffer<T>
            {
                private readonly LinkedList<T> _buffer;
                private int _maxItemCount;

                public ConcurrentCircularBuffer(int maxItemCount)
                {
                    _maxItemCount = maxItemCount;
                    _buffer = new LinkedList<T>();
                }

                public void Put(T item)
                {
                    lock (_buffer)
                    {
                        _buffer.AddFirst(item);
                        if (_buffer.Count > _maxItemCount)
                        {
                            _buffer.RemoveLast();
                        }
                    }
                }

                public IEnumerable<T> Read()
                {
                    lock (_buffer) { return _buffer.ToArray(); }
                }
            }


            private readonly Action<String> Echo;
            private readonly IMyProgrammableBlock Me;

            private readonly ConcurrentCircularBuffer<string> logBuffer;

            public Logger(Action<string> Echo, IMyProgrammableBlock Me, int logSize)
            {
                this.Echo = Echo;
                this.Me = Me;

                this.Me.CustomData = "";
                this.logBuffer = new ConcurrentCircularBuffer<string>(logSize);

                Echo("Printing logs to CustomData of this programmable block...");
            }

            public void Log(string message)
            {
                logBuffer.Put(message);
                this.Me.CustomData = logBuffer.Read().Reverse().Aggregate("", (acc, msg) => acc + "\n" + msg);
            }
        }
        
    }
}
