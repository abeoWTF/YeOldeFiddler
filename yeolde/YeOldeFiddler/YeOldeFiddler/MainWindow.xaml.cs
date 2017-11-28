using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YeOldeFiddler
{
    public partial class MainWindow : Window
    {
        System.Timers.Timer aTimer = new System.Timers.Timer();
        
        //Get time
        public String GetTimestamp(DateTime value)
        {
            return value.ToString("[HH:mm:ss]");
        }

        //GuestCounter
        Counter c = new Counter();

        //Length of bar-time
        public int HowLongIsBarOpen { get; set; }= 120;


        //Update counter
        public void UpdateCounter()
        {
            int countsChairsLeft = Counter.Chairs - freeChairStack.Count;
            int countGlassLeft = GlassCollection.BoundedCapacity - Counter.AmountOfGlasses;
            Dispatcher.Invoke(() => { 
            BarCounter_lbl.Content = "The Fiddler has " + Counter.AmountofGuests + " Guests!";
            GlassCounter_lbl.Content = "There's " + countGlassLeft + " mugs available!";
            ChairCounter_lbl.Content = "There's " + countsChairsLeft + " chairs available!";
            });
        }

        //Bar open
        //public bool  { get; set; }

        //Guest queues
        ConcurrentQueue<Guest> guestQueue = new ConcurrentQueue<Guest>();

        //Glass queues
        BlockingCollection<Glass> GlassCollection = new BlockingCollection<Glass>(Counter.Glass);

        //Chair queue
        BlockingCollection<Chair> freeChairStack = new BlockingCollection<Chair>(Counter.Chairs);

        //Drinking Queue
        ConcurrentQueue<string> heartyDrinker = new ConcurrentQueue<string>();

        public MainWindow()
        {
            aTimer.Interval = HowLongIsBarOpen * 1000;
            aTimer.Enabled = true;
            aTimer.AutoReset = false;
            InitializeComponent();
        }

        //Updating listbox from bartender
        private void Guestlist(string info)
        {
            Dispatcher.Invoke(() =>
            {
                UpdateCounter();
                Guests_lstbx.Items.Insert(0, GetTimestamp(DateTime.Now) + " " + info);
                
            });
        }

        //What the bartender does
        private void UpdateBartenderList(string dowhat)
        {
            Dispatcher.Invoke(() =>
            {
                UpdateCounter();
                Bartender_lstbox.Items.Insert(0, GetTimestamp(DateTime.Now) + " " + dowhat);
            });
        }

        //What the waitress does
        private void WaitressWork(string dowhat)
        {
            Dispatcher.Invoke(() =>
            {
                UpdateCounter();
                Waiter_listbx.Items.Insert(0, GetTimestamp(DateTime.Now)+ " " + dowhat);
            });
        }

        //What the guest does
        private void GuestDrinks(string dowhat)
        {
            Dispatcher.Invoke(() =>
            {
                UpdateCounter();
                Bartender_lstbox.Items.Insert(0, GetTimestamp(DateTime.Now) + " " + dowhat);
                
            });
        }

        //Add guests to Queue
        private void AddGuestToQueue(Guest guest)
        {
            guestQueue.Enqueue(guest);
        }

        //Open bar
        private void OpenBar_btn_Click(object sender, RoutedEventArgs e)
        {
            int time;
            Task.Run(() =>
            {
                HowLongIsBarOpen = 120;
                for (time = HowLongIsBarOpen - 1; time >= 0 && Counter.BarOpen; time--)
                {
                    Dispatcher.Invoke(() => { 
                    Bar_open_lbl.Content = "Time to closing: " + time;
                    });

                    Thread.Sleep(1000);
                }
                Counter.BarOpen = false;
            });

            //Working
            Waitress waitress = new Waitress();
            Bouncer bouncer = new Bouncer();
            Bartender bartender = new Bartender();
            Guest guest = new Guest("");

            Counter.BarOpen = !Counter.BarOpen;
            if (Counter.BarOpen)
            {
                Task.Run(() =>
                {
                    bartender.Work(guestQueue, freeChairStack, GlassCollection, UpdateBartenderList, Counter.BarOpen,
                        heartyDrinker);
                });
              
                Task.Run(() =>
                {

                    bouncer.Work(Guestlist, AddGuestToQueue);
                });

                Task.Run(() =>
                {
                    waitress.WaitressWork(GlassCollection, WaitressWork, guestQueue, heartyDrinker);
                });

                Task.Run(() =>
                {
                    guest.Work(heartyDrinker, GuestDrinks, freeChairStack, GlassCollection, guestQueue);
                });
            }
        }
  
        //Check how fast the guest drinks
        private void TardyGuests_checkBTN_Checked(object sender, RoutedEventArgs e)
        {
            Counter.Tardy = !Counter.Tardy;
        }

        //Check how fast the waitress works
        private void Waitress_Working_Fast_chckbx_Checked(object sender, RoutedEventArgs e)
        {
            Counter.FastWaiter = !Counter.FastWaiter;
        }

        //Check for how fast guests come
        private void Couples_night_chcbx_Checked(object sender, RoutedEventArgs e)
        {
            Counter.GuestPerLetIn = 2;
        }


        private void Partybus_btn_Click(object sender, RoutedEventArgs e)
        {
            Counter.partyBus = true;
        }

        private void ExtendTime_btn_Click(object sender, RoutedEventArgs e)
        {
            HowLongIsBarOpen = 300;
        }
    }
}
