using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YeOldeFiddler
{
    public class Guest
    {
        public string NameOfGuest { get; set; }

        public Guest(string nameOfGuest)
        {
            NameOfGuest = nameOfGuest;
        }

        public int SetMinTimeToDrink;

        public void Work(ConcurrentQueue<string> heartydrinker, Action<string> callback, BlockingCollection<Chair> freeChairStack, BlockingCollection<Glass> glassCollection, ConcurrentQueue<Guest> guestQueue)
        {
            while (true)
            {
                Thread.Sleep(5);
                Random r = new Random();
                SetMinTimeToDrink = r.Next(10000, 20000);
               
                while (!heartydrinker.IsEmpty)
                {
                    string name = heartydrinker.First();
                    
                    if (Counter.Tardy)
                    {
                        Thread.Sleep(
                            SetMinTimeToDrink * 2);
                    }
                    else
                    {
                        Thread.Sleep(
                            SetMinTimeToDrink);
                    }
                    callback?.Invoke($"{name} goes home!");
                    freeChairStack.Take();
                    Counter.RemoveChair();
                    Counter.RemoveGuest();
                    heartydrinker.TryDequeue(out string g);

                }
            }
        }
    }
}
