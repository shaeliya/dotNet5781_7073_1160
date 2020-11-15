using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7073_1160
{
    class BusCollection//:IEnumerable
    {
        public List<BusLine> BusLines { get; set; }
        public void AddBus(BusLine busLineNumber, double distanceFromPreviousBusStop, TimeSpan travelTimeFromPrevioussBusStop)
        {
            bool isBusExist = IsBusExist(busLineNumber.BusLineNumber);
            string choice = "add";
            if (!isBusExist)
            {
                InputCheckAndAddingOrDeletingBus( busLineNumber, choice);
                UpdateDistanceAndTimeFromPreviousStation( distanceFromPreviousBusStop, travelTimeFromPrevioussBusStop, choice);
            }
            else
            {
                throw new Exception("The bus station exists in the system, the requested station cannot be added");
            }

        }

       public void InputCheckAndAddingOrDeletingBus(BusLine busLineNumber, string choice, int index, BusLineStation busLineStation, double distanceFromPreviousBusStop, TimeSpan travelTimeFromPrevioussBusStop)
        {
            if (choice == "add")
            {
                int count = 0;
                foreach (BusLine busLine in BusLines)
                {
                    if (busLine.BusLineNumber == busLineNumber.BusLineNumber)
                    {
                        count++;
                    }
                }
                if (count >= 2)
                {
                    throw new Exception("This line already exists in the system");
                }

                Console.WriteLine("Please enter at least two bus stops, to finish press s");
               
                string ch = Console.ReadLine();
                while (ch != "S" || ch != "s")
                {
                   
                    busLineNumber.AddStation( index, busLineStation,distanceFromPreviousBusStop, travelTimeFromPrevioussBusStop)
                }
            }
            else
            {

            }
            
        }

        public bool IsBusExist(string busLineNumber)
        {
            foreach (var busLines in BusLines)
            {
                if (busLines.BusLineNumber == busLineNumber)
                {
                    return true;
                }
            }
            return false;
        }
        ////public IEnumerator GetEnumerator()
        //{
        //    return BusLines.GetEnumerator();
        //}
    }
}
