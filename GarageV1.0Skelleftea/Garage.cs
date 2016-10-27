using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageV1._0Skelleftea
{
    //class Garage<T> where T : Vehicle
   
    class Garage<T> : IEnumerable<T> where T : Vehicle
    {
        List<T> vehicles = new List<T>();

        public IEnumerator<T> GetEnumerator()
        {
            //for (int i = 0; i < vehicles.Count; i++)
            foreach (T v in vehicles) yield return v; 
            //yield return vehicles[i];
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool removeVehicle(string regNr)
        {
            T vehicle = null;
            try
            {
                vehicle = vehicles.Where(x => x.RegNr == regNr).Single();
            }
            catch (Exception ex) { }
            if (null != vehicle)
            {
                vehicles.Remove(vehicle);
                return true;
            }
            return false;

        }

        public void AddVehicle(T obj)
        {
            vehicles.Add(obj);
        }

        public IEnumerable<T> searchVehicle(string regNr)
        {
            IEnumerable<T> result = null;
            try
            {
                result = vehicles.Where(x => x.RegNr.Contains(regNr));
            }
            catch (Exception ex) { }
            return result;
        }


    }
    /*
    public class GarageSpot{

        public GarageSpot(Vehicle vs)
        {
            this.v = vs;
        }
        public Vehicle v { get; set; }
        public DateTime parkedTime {get; set;}

    }
    
    class Garage
    {
        GarageSpot[] spots;

        public Garage()
        {
            spots = new GarageSpot[10];
            for (int i = 0; i < spots.Length; i++)
                spots[i] = new GarageSpot(null);
       }

        public void AddVehicle(Vehicle vehicle)
        {
            int empty = EmptySlot();
            if (empty != -1)
            {
                spots[empty].v = vehicle;
                spots[empty].parkedTime = DateTime.Now;
            }    
        }

        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return from s in spots
                      where s.v != null
                      select s.v;

        }

        private int EmptySlot()
        {
            int ret = -1;
            for (int i = 0; i < spots.Length; i++)
                if (spots[i].v == null)
                {
                    ret = i;
                    break;
                }
                    
            return ret;

        }

        private bool IsFull()
        {
            bool isFull = true;
            for (int i=0; i<spots.Length; i++)
                if (spots[i].v ==null)
                    isFull = false;
            return isFull;    
        }
    }*/
}
