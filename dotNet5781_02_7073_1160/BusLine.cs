using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7073_1160
{     //פרטים על קו האוטובוס
    
    class BusLine
    {
        List<BusLineStation> Stations = new List<BusLineStation>();
        public string BusLineNumber { get; set; }
        public string StartStation { get; set; }
        public string LastStation { get; set; }
        public string Area { get; set; }
        public override string ToString()
        {
            List<string> stationKeys= new List<string>();
            foreach (var station in Stations)
            {
                stationKeys.Add( station.BusStop.BusStationKey);
            }

            return $@"The bus line is: {BusLineNumber} 
The area is: {Area}
The bus station codes are: { string.Join(", ", stationKeys)}
"
;
             
        }
        public void AddStation(int index,BusLineStation busLineStation)
        {
            if (index <= Stations.Count||index==Stations.Count+1) 
            {
              
                Stations.Insert(index-1, busLineStation);
                Console.WriteLine("The station was successfully added");

            }
            else
            {
                Console.WriteLine("It is not possible to add a station in the requested location");// אולי נגיד לו שיכנס עוד פעם לוקייששן ואז נעשה וייל במיין 
            }
        }
        //לעשות פונקציה דליט
        public void DeleteStation()
        {

        }
        
    }
}
//בס סטופ מחלקה שיש בה מידע על כל תחנת אוטובוס
//בס ליין סטיישן תחנה בהקשר לקו מסויים
//בס ליין זה קו אוטובוס מוסיפים לקו תחנות