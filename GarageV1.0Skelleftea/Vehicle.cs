using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageV1._0Skelleftea
{
    public class Vehicle
    {
        public string RegNr { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            return (RegNr +" : " + Brand + " : " +Color) ;
        }

    }

    public class Car : Vehicle
    {
        public int nrOfDoors { get; set; }

        public Car(string regnr, string brand, string color)
        {
            base.RegNr = regnr;
            base.Brand = brand;
            base.Color = color;

        }
    }
    public class Snowmobile : Vehicle{
        
        public Snowmobile(string regnr, string brand, string color)
        {
            base.RegNr = regnr;
            base.Brand = brand;
            base.Color = color;

        }
        
    }
    
    class Airplane : Vehicle
    {
        public Airplane(string regnr, string brand, string color)
        {
            base.RegNr = regnr;
            base.Brand = brand;
            base.Color = color;

        }
        public int wingspan { get; set; }
    }

    class Motorcycle : Vehicle
    {
        public Motorcycle(string regnr, string brand, string color)
        {
            base.RegNr = regnr;
            base.Brand = brand;
            base.Color = color;

        }
        public Boolean isRiceCooker { get; set; }

    }

    


}
