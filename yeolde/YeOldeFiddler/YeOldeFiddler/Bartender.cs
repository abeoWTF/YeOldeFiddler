using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace YeOldeFiddler
{
    public class Bartender
    {
        
        public void Work(ConcurrentQueue<Guest> guestQueue, BlockingCollection<Chair> chairCollection,
           BlockingCollection<Glass> glassCollection, Action<string> callback,bool isBar, ConcurrentQueue<string> heartyDrinker)
        {
            Thread.Sleep(2000);
            while (Counter.BartenderGoesHome)
            {
                if (!guestQueue.IsEmpty)
                {
                    if (Counter.AmountOfChairs < Counter.Chairs)
                    {
                        if (Counter.AmountOfGlasses < Counter.Glass)
                        {
                            Thread.Sleep(3000);
                            callback?.Invoke($"The barkeep gets a mug from the shelf!");
                            Counter.AddGlass();
                            glassCollection.Add(new Glass());
                            Thread.Sleep(3000);
                            callback?.Invoke($"The barkeep pours an ale to {guestQueue.First().NameOfGuest}");
                            chairCollection.Add(new Chair());
                            callback?.Invoke($"{guestQueue.First().NameOfGuest} sits down and drinks his beer.");
                            Counter.AddChair();
                            heartyDrinker.Enqueue(guestQueue.First().NameOfGuest);
                            guestQueue.TryDequeue(out Guest guest);
                        }
                    }
                }
                
                else if (guestQueue.IsEmpty && !Counter.BarOpen && heartyDrinker.IsEmpty)
                {
                    callback?.Invoke($"Bartender goes home.");
                    Counter.BartenderGoesHome = false;
                }
            }
        }
    }
}
