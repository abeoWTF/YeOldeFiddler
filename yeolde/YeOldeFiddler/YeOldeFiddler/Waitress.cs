using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YeOldeFiddler
{
    public class Waitress
    {

        public void WaitressWork(BlockingCollection<Glass> glassCollection, Action<string> callback, ConcurrentQueue<Guest> guestQueue, ConcurrentQueue<string> heartydrinker) 
        {
            
               while (Counter.Waitressgoeshome)
                {
                    if (!glassCollection.IsCompleted)
                    {
                        callback?.Invoke($"The barmaid starts cleaning the tables.");
                        int gCount = glassCollection.Count;
                        if (Counter.FastWaiter)
                        {
                            Thread.Sleep(5000);
                        }
                        else if (!Counter.FastWaiter)
                        {
                            Thread.Sleep(10000);

                        }
                        callback?.Invoke($"The barmaid finds {gCount} dirty mugs.");
                        if (gCount > 0)
                        {
                            callback?.Invoke($"The barmaids starts washing the dishes.");

                            if (Counter.FastWaiter)
                            {
                                Thread.Sleep(7500);
                            }
                            else
                            {
                                Thread.Sleep(15000);
                            }
                            for (int i = 0; i < gCount; i++)
                            {
                                glassCollection.Take();
                                Counter.RemoveGlass();
                            }
                            callback?.Invoke($"The barmaid adds {glassCollection.Count} clean glasses to the shelf.");
                        }
                        else if (!Counter.BarOpen && heartydrinker.IsEmpty && glassCollection.Count <= 0)
                        {
                            for (int i = 0; i < 1; i++)
                            {
                                callback?.Invoke($"Waitress goes home.");
                                Counter.Waitressgoeshome = false;
                            }
                        }
                    }
                }
        }
    }
}
