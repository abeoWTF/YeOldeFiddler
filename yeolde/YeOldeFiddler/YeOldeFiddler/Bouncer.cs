using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;

namespace YeOldeFiddler
{
    public class Bouncer
    {

        public static bool BartenderOpen { get; set; }
        
        //Gästlista(namn)
        public List<string> _listofGuests = new List<string>
        {
            "Izola",
            "Viola",
            "Fridgid",
            "Flonkerton",
            "Gronk",
            "Fatty",
            "Mr Underhill",
            "Tensty",
            "Ister",
            "Flaur"
        };

        //How did they enter?
        public List<string> _mannerOfEntrance = new List<string>
        {
            "stumbles in.",
            "pops by.",
            "rushes in.",
            "walks in.",
            "walks in.",
            "slanteres in.",
            "parades in.",
            "rambles in.",
            "strolls in.",
            "steps in.",
            "races in.",
        };

        //Random-generator(duh.)
        Random r = new Random();

        //Time
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("[HH:mm:ss]");
        }

        //Counter instansiation
        Counter c = new Counter();

        //Variabler
        private Action<Guest> Guestqueue;
        private Action<string> Callback;
        

        //Bouncer working
        public void Work(Action<string> callback, Action<Guest> guestqueue)
        {
            Guestqueue = guestqueue;
            Callback = callback;
            
            while (Counter.BarOpen)
            {
                if (!Counter.partyBus) { 
                for (int i = 0; i <= Counter.GuestPerLetIn-1; i++)
                {
                    string guestname = _listofGuests[r.Next(_listofGuests.Count)];
                    string manner = _mannerOfEntrance[r.Next(_mannerOfEntrance.Count)];
                    Guestqueue(new Guest(guestname));
                    Counter.AddGuest();
                    String timeStamp = GetTimestamp(DateTime.Now);
                    Callback?.Invoke($" {guestname} {manner}");
                    Thread.Sleep(1000);
                    Callback?.Invoke($"{guestname} goes to the bar.");
                    
                }
                    Thread.Sleep(r.Next(Counter.SetMinTimeForBouncer, Counter.SetMaxTimeForBouncer));
                }
                else
                {
                    for (int i = 0; i < 1; i++)
                    {
                        //Timer till partybuss
                        System.Timers.Timer aTimer = new System.Timers.Timer();
                        aTimer.Interval = 20000;
                        aTimer.Enabled = true;
                        aTimer.AutoReset = false;
                        aTimer.Elapsed += OnTimedEvent;


                        //Ends partybussbool
                        Counter.partyBus = false;
                        callback?.Invoke($"****Partybus-mode****");
                        string guestname = _listofGuests[r.Next(_listofGuests.Count)];
                        string manner = _mannerOfEntrance[r.Next(_mannerOfEntrance.Count)];
                        guestqueue(new Guest(guestname));
                        Counter.AddGuest();
                        String timeStamp = GetTimestamp(DateTime.Now);
                        callback?.Invoke($" {guestname} {manner}");
                        Thread.Sleep(1000);
                        callback?.Invoke($"{guestname} goes to the bar.");
                        Thread.Sleep(r.Next(Counter.SetMinTimeForBouncer * 2, Counter.SetMaxTimeForBouncer * 2));
                    }
                }
                if (!Counter.BarOpen)
                {
                    Callback($"The bouncer bounces home.");
                }
            }
        }
        //Partybus event
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Callback($"P - a - r - t - y - b - u - s starting!");
            for (int i = 0; i < 15; i++)
            {
                Counter.partyBus = false;
                string guestname = _listofGuests[r.Next(_listofGuests.Count)];
                string manner = _mannerOfEntrance[r.Next(_mannerOfEntrance.Count)];
                Guestqueue(new Guest(guestname));
                Counter.AddGuest();
                String timeStamp = GetTimestamp(DateTime.Now);
                Callback?.Invoke($" {guestname} {manner}");
                Thread.Sleep(1000);
                Callback?.Invoke($"{guestname} goes to the bar.");
            }
            Callback?.Invoke($"P - a - r - t - y - b - u - s over!");
        }
    }
}
