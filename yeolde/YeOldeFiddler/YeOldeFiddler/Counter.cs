using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace YeOldeFiddler
{
    public class Counter
    {
        //Tardy guests
        public static bool Tardy = false;

        //Bartender controll
        public static bool BartenderGoesHome = true;

        //WaitressControll
        public static bool Waitressgoeshome = true;
        
        //Fast waiter
        public static bool FastWaiter = false;

        //Test 20g 3c
        public static bool moreGlasses = false;
        
        //Partybus
        public static bool partyBus;

        Random ran = new Random();

        //Amount of items (chairs, glasses)
        public static int Glass = 8;
        public static int Chairs = 9;

        //AmounOfGuestsGenerated
        public static int GuestPerLetIn = 1;

        //Washingtimes
        public static int SetTimeForCleaning = 10000;
        public static int SetTimeForWashing = 15000;

        //Times
        public static int SetMinTimeForBouncer = 3000;
        public static int SetMaxTimeForBouncer = 5000;

        //Guestcounters
        public static int AmountofGuests;

        //AddGuest
        public static void AddGuest()
        {
            AmountofGuests++;
        }

        //RemoveGuest
        public static void RemoveGuest()
        {
            AmountofGuests--;
        }

        //Glasscounter
        public static int AmountOfGlasses;

        //AddGlass
        public static void AddGlass()
        {
            AmountOfGlasses++;
        }

        //RemoveGlass
        public static void RemoveGlass()
        {
            AmountOfGlasses--;
        }
        //ChairCounter
        public static int AmountOfChairs;

        //AddGlass
        public static void AddChair()
        {
            AmountOfChairs++;
        }

        //RemoveGlass
        public static void RemoveChair()
        {
            AmountOfChairs--;
        }
        
        //Bar open
        public static bool BarOpen;

       
    }
}
