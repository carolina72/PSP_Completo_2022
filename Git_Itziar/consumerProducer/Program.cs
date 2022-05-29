using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace consumerProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            // A bounded collection. It can hold no more 
            // than 100 items at once.
            BlockingCollection<int> dataItems = new BlockingCollection<int>(100);


            // A simple blocking consumer with no cancellation.
            Task t1 = Task.Run(() =>
            {
                while (!dataItems.IsCompleted)
                {

                    int data = -1;
                    // Blocks if dataItems.Count == 0.
                    // IOE means that Take() was called on a completed collection.
                    // Some other thread can call CompleteAdding after we pass the
                    // IsCompleted check but before we call Take. 
                    // In this example, we can simply catch the exception since the 
                    // loop will break on the next iteration.
                    try
                    {
                        data = dataItems.Take();
                    }
                    catch (InvalidOperationException) { }

                    if (data != -1)
                    {
                        Console.WriteLine("Elemento {0}", data);
                    }
                }
                Console.WriteLine("\r\nNo more items to take.");
            });

            // A simple blocking producer with no cancellation.
            Task t2 = Task.Run(() =>
            {
                int data = 0;
                bool noMoreItemes = false;
                while (!noMoreItemes)
                {
                    
                    // Blocks if numbers.Count == dataItems.BoundedCapacity
                    dataItems.Add(data);
                    data++;
                    if (data == 1000)
                        noMoreItemes = true;
                }
                // Let consumer know we are done.
                dataItems.CompleteAdding();

            });
            t1.Wait();
            t2.Wait();
        }
    }
}
