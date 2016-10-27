using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageV1._0Skelleftea
{
    class Program
    {

        //EN ny kommentar
    #region Trap application termination
        /*
        [System.Runtime.InteropServices.DllImport("Kernel32")]§
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);
        static EventHandler _handler;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        //private static bool Handler(CtrlType sig, Garage<Vehicle> gar)

        
        private static bool Handler(CtrlType sig)
        {
            Console.WriteLine("Exiting system due to external CTRL-C, or process kill, or shutdown");

            //do your cleanup here
            //Thread.Sleep(5000); //simulate some cleanup delay

            Console.WriteLine(sig.ToString());

            //StaticGar.SaveVehicles();



            Console.WriteLine("Cleanup complete");

            //allow main to run off
            //exitSystem = true;

            //shutdown right away so there are no lingering threads
            Environment.Exit(-1);

            return true;
        }*/
        #endregion
        
        static void OnProcessExit(object sender, EventArgs e, Garage<Vehicle> garage)
        {
            Console.WriteLine("I'm out of here");
            Console.ReadKey();
        }
        
        
        
        
        
        static void Main(string[] args)
        {

            Test<Vehicle> t = new Test<Vehicle>();
            foreach (Vehicle v in t) 
                Console.WriteLine(v.Color);

            Garage<Vehicle> garage = new Garage<Vehicle>();

            //Object sender;
            //EventArgs e;
            AppDomain.CurrentDomain.ProcessExit += new EventHandler((sender, e) => OnProcessExit(sender, e, garage)); 
            
            
            // Some biolerplate to react to close window event, CTRL-C, kill, etc
            //_handler += new EventHandler(Handler);
            //SetConsoleCtrlHandler(_handler, true);
            

            seed(garage);
            new Program().mainMenu(garage);


        }

        public static void seed(Garage<Vehicle> garage)
        {
            garage.AddVehicle(new Car("A","Toyota","Red") { nrOfDoors = 2 });
            garage.AddVehicle(new Car("B","Porsche","Blue") { nrOfDoors = 4 });
            garage.AddVehicle(new Motorcycle("C","Yamaha","Black") {isRiceCooker = true });
            garage.AddVehicle(new Motorcycle("D","Harley Davidson","White") { isRiceCooker = false });
            garage.AddVehicle(new Airplane("E","Cessna","White") { wingspan = 20 });
            garage.AddVehicle(new Airplane("F","Cessna","Yellow") { wingspan = 2 });

        }

        static void WriteBlueLine(string value)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(value); //.PadRight(Console.WindowWidth - 1)); // <-- see note
            Console.ResetColor();
        }

        void RunSubMenu(Garage<Vehicle> garage, int selected)
        {
            Console.Clear();
            switch (selected)
            {
                
                    
                case 2:
                    addVehicle(garage);
                    break;
                case 3:
                    Console.Write("Enter regNr for the Vehicle to remove: ");
                    string regNr = Console.ReadLine();
                    bool result = garage.removeVehicle(regNr);
                    if (result)
                        Console.WriteLine("Vehicle was successfully removed");
                    else
                        Console.WriteLine("No vehicle matched criteria: "+regNr);
                    Console.ReadKey();
                    break;
                case 1:
                    Console.Clear();
                    Print(garage);
                    break;
                case 4:
                  /*  var query = from g in garage
                                group g by garage.GetType() into lettergroup
                                orderby lettergroup.Key ascending
                                select lettergroup;*/
                    var query = from g in garage
                                group g by g.GetType();

                    foreach (var group in query)
                    {
                        Console.WriteLine(group.Key.ToString().Substring(22) +" Antal: "+ group.Count());
                        //foreach(var vehicle in group) Console.WriteLine(vehicle.ToString());

                    }
                        
                    /*
                    string[] vehicleTypes = { "Airplane", "Motorcycle", "Car", "Bus", "Boat" };
                    int listSize = vehicleTypes.Length;
                    int[] countArray = new int[listSize];

                    foreach (Vehicle v in garage) {
                        //Console.WriteLine(v.GetType());
                        for (int i = 0; i < listSize; i++) { 
                            if (v.GetType().ToString().Equals("GarageV1._0Skelleftea." + vehicleTypes[i])) countArray[i] += 1;
                        }
                    }
                        Console.Clear();
                        for (int i = 0; i < listSize; i++)
                        Console.WriteLine(vehicleTypes[i] + "   antal: " + countArray[i]);
                    */
                    Console.ReadKey();

                    break;
                case 5:
                    Console.Clear();
                    Console.Write("Enter regNr for Vehicle to find: ");
                    regNr = Console.ReadLine();
                    IEnumerable<Vehicle> res = garage.searchVehicle(regNr);
                    if (res.Count()== 0){
                        Console.WriteLine("No vehicle was found matching regnr: {0}",regNr);
                    }
                    else
                        foreach(Vehicle v in res)
                        Console.WriteLine(v.RegNr + " : " + v.GetType());
                    Console.ReadKey();
                    break;

                case 6:
                    return;
                default:
                    break;
            }




        }
        
        public void mainMenu(Garage<Vehicle> garage)
        {
            int selected = 1;
            while (true) 
            {
                Console.Clear();
                string menu = "";
                if (selected == 1) WriteBlueLine("List all vehicles"); else Console.WriteLine("List all vehicles");
                if (selected == 2) WriteBlueLine("Add Vehicle"); else Console.WriteLine("Add Vehicle");
                if (selected == 3) WriteBlueLine("Remove Vehicle"); else Console.WriteLine("Remove Vehicle");
                if (selected == 4) WriteBlueLine("List all vehicles and how many of each is in garage"); 
                    else  Console.WriteLine("List all vehicles and how many of each is in garage");
                if (selected ==5) WriteBlueLine("Search Vehicle"); else Console.WriteLine("Search Vehicle");
                if (selected ==6) WriteBlueLine("Exit App"); else Console.WriteLine("Exit App");

                 /*Console.WriteLine("Enter menu number \n(1, 2, 3 ,4, 0) of your choice\n\n"
                 + "\n1. Add a Vehicle"
                 + "\n2. Remove a Vehicle"
                 + "\n3. List all vehicles"
                 + "\n4. List all vehicles and how many of each is in the garage"
                 + "\n5. Search for a vehicle by regnumber"
                 */
                 
                char input = ' '; //Creates the character input to be used with the switch-case below.
                try
                {

                    var kee = Console.ReadKey().Key;
                    if (kee == ConsoleKey.DownArrow)
                    {
                        selected += 1;
                        if (selected > 6) selected = 1;
                        //Console.WriteLine(selected);
                    }
                    else if (kee == ConsoleKey.UpArrow)
                    {
                        selected -= 1;
                        if (selected <1) selected = 6;
                    }
                    else if (kee == ConsoleKey.Enter)
                    {
                        if (selected == 6) return;
                        RunSubMenu(garage, selected);
                    }
                    //input = Console.ReadLine()[0]; //Tries to set input to the first char in an input line
                }
                catch (IndexOutOfRangeException) //If the input line is empty, we ask the users for some input.
                {
                    //Console.Clear();
                    //Console.WriteLine("Please enter some input!");
                }

                /*switch (input)
                {
                    case '1':
                    addVehicle(garage);
                    break;
                    case '2':
                    Console.Write("Enter regNr for the Vehicle to remove: ");
                    string regNr = Console.ReadLine();
                    garage.removeVehicle(regNr);
                    Console.ReadKey();
                    break;
                    case '3':
                        Print(garage);
                        break;
                    case '4':
                        string[] vehicleTypes = { "Airplane", "Motorcycle", "Car", "Bus", "Boat" }; 
                        int listSize = vehicleTypes.Length;
                        int[] countArray = new int[listSize];

                        foreach (Vehicle v in garage)
                            for (int i = 0; i < listSize; i++ )
                                if (v.GetType().ToString().Equals("Garage1._0."+vehicleTypes[i])) countArray[i] += 1;
                        for (int i = 0; i < listSize; i++)
                            Console.WriteLine(vehicleTypes[i] + "   antal: " + countArray[i]);

                        Console.ReadKey();

                        break;
                    case '5':
                        Console.Write("Enter regNr for the Vehicle to remove: ");
                        regNr = Console.ReadLine();
                        Vehicle vehicle = garage.searchVehicle(regNr);
                        if (null == vehicle)
                            Console.WriteLine("No vehicle matching regnr was found");
                        else
                            Console.WriteLine(vehicle.regnr + " : " + vehicle.GetType());
                        Console.ReadKey();
                        break;
                    
                    case '0':
                    return;
                    default:
                    Console.WriteLine("Please enter some valid input (0, 1, 2, 3, 4)");
                    Console.ReadKey();
                    break;
                }*/
            }


        }

        public void Print(Garage<Vehicle> garage)
        {
            foreach (Vehicle v in garage)
                Console.WriteLine(v.GetType().ToString().Substring(22) + " : " + v.ToString());
            Console.ReadKey();

        }

        public void addVehicle(Garage<Vehicle> garage)
        {
            Console.Clear();
            Console.WriteLine("input the number \n(1, 2, 3 ,4, 0) of your choice\n"
                + "\n1. Add a Car"
                + "\n2. Add a Motorcycle"
                + "\n0. Return to main menu");
            char input = ' '; //Creates the character input to be used with the switch-case below.
            try
            {
                input = Console.ReadLine()[0]; //Tries to set input to the first char in an input line
            }
            catch (IndexOutOfRangeException) //If the input line is empty, we ask the users for some input.
            {
                Console.Clear();
                Console.WriteLine("Please enter some input!");
            }

            switch (input)
            {
                case '1':
                Console.WriteLine("Ange regnr");
                string regnr = Console.ReadLine();
                Console.WriteLine("Ange antal dörrar på bilen");
                int doors = Int16.Parse(Console.ReadLine());
                Car c = new Car(regnr,"Volvo","White");
                c.nrOfDoors = doors;
                                garage.AddVehicle(c);
                break;       
                    
                case '2':
                Console.WriteLine("Ange regnr");
                regnr = Console.ReadLine();
                Console.WriteLine("Är din MC en riskokare? (J / N) ");
                bool rice = Console.ReadLine().Equals("J");
                Motorcycle mc = new Motorcycle(regnr,"Yamaha","White");
                mc.isRiceCooker = rice;
                garage.AddVehicle(mc);
                break;
                case '0':
                    return;
                default:
                    Console.WriteLine("Please enter some valid input (0, 1, 2, 3, 4)");
                    break;
            }

        }


    }
        /*
    {
        static void Main(string[] args)
        {
            Garage g = new Garage();
            g.AddVehicle(new Car("abc", "toyota", "red"));
            g.AddVehicle(new Car("bbb", "ford", "white"));

            IEnumerable<Vehicle> vehicles = g.GetAllVehicles();
            foreach (Vehicle v in vehicles)
                Console.WriteLine(v.ToString());


            Console.ReadKey();
        }
    }*/
}
