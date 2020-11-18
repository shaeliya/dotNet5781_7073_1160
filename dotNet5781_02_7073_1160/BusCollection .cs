using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7073_1160
{
     public class BusCollection:IEnumerable<BusLine>
    {
        //אוסף הקווים
        public List<BusLine> BusLinesList { get; set; }

        //אוסף התחנות הקיימות במערכת
        public List<BusStop> BusStopsList { get; set; }

        public BusCollection()
        {
            BusLinesList = new List<BusLine>();
            BusStopsList = new List<BusStop>();
            
        }
        /*
                     דז: בפועל פנו אליי סטודנטים והציגו את המציאות )שהיא ברוב המקרים המציאות(. וגם מצאתי שיש אותו קו בהלוך
        שנוסע במסלולים שונים בשעות שונות. לכן עניתי להם שיכולים לאפשר יותר משני קוים עם אותו מספר ולא לוודא     
        את העניין הלוך\חזור – רק אמרתי להם להוסיף בתיעוד את ההחלטה ושהיא אושרה והנימוקים של ההחלטה. אתה יכול
        להמליץ לסטודנטים שלך גם כפי שכתבת וגם כמוני
                     */
        #region AddBusLine
        public void AddBusLine()
        {
            // לקלוט אוסף קווי אוטובוס -> לקלוט קו אוטובס- >
            //לקלוט אוסף תחנות - > לבדוק שהתחנה קיימת בליסט ואם לא ליצור

            Console.WriteLine("Enter bus Line Number");
            string busLineNumber = Console.ReadLine();

            int areaInt = 0;
            while (areaInt < 1 && areaInt > 8)
            {
                Console.WriteLine("Please Choose area code from 1 to 8 according to the following list:");
                Console.WriteLine("1 - General");
                Console.WriteLine("2 - North");
                Console.WriteLine("3 - South");
                Console.WriteLine("4 - Center");
                Console.WriteLine("5 - Jerusalem");
                Console.WriteLine("6 - TelAviv");
                Console.WriteLine("7 - YehudaShomron");
                Console.WriteLine("8 - Haifa");
                string areaStr = Console.ReadLine();
                bool temp = int.TryParse(areaStr, out areaInt);
            }

            Enum.Area area = (Enum.Area)areaInt;
            BusLine busLine = new BusLine(busLineNumber, area);

            string ch = "c";

            Console.WriteLine("you must enter at least two stations for the bus");

            while (ch.ToLower() == "c")
            {
                busLine.AddSingleBusStopToBusLine(BusStopsList);
                Console.WriteLine("Enter 'c' to continue adding. Press any key to stop");
                ch = Console.ReadLine();
            }

            if (busLine.Stations.Count < 2)
            {
                throw new NotEnoughStationsException(busLineNumber, "you must enter at least two stations for the bus!");
            }

            BusLinesList.Add(busLine);
        }





        #endregion AddBusLine

        public void DeleteBusLine()
        {
            string busLineNumber;
            Console.WriteLine("enter busLineNumber ");
            busLineNumber = Console.ReadLine();

            // משום שיכול להיות שיש כמה קווים עם אותו המספר, נמחק את כל הקווים מאותו המספר
            int index = ReturnsIndexOfBusLine(busLineNumber);

            while (index != -1)
            {
                BusLinesList.RemoveAt(index);
                index = ReturnsIndexOfBusLine(busLineNumber);
            }
        }

        private int ReturnsIndexOfBusLine(string busLineNumber)
        {
            for (int i = 0; i < BusLinesList.Count; i++)
            {
                if (BusLinesList[i].BusLineNumber == busLineNumber)
                {
                    return i;
                }
            }
            return -1;

        }

        public List<BusLine> GetAllLinesWithStation(string busStationKey)
        {
            List<BusLine> busLines = new List<BusLine>();
            foreach (BusLine busLine in this)
            {
                foreach(var station in busLine.Stations)
                {
                    if (station.BusStop.BusStationKey == busStationKey)
                    {
                        busLines.Add(busLine);
                        break;
                    }
                }

            }
            if (busLines.Count == 0)
            {
                throw new KeyNotFoundException("No Bus Lines Found for busStationKey: " + busStationKey);
            }
            return busLines;
        }
        public IEnumerator<BusLine> GetEnumerator()
        {
            return BusLinesList.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<BusLine> SortBusCollection()
        {
            BusLinesList.Sort();
            return BusLinesList;

        }

        //דז: ניתן להחזיר רשימה של קוים במקום קו בודד. זה מה שהמלצתי למי שפנה אליי. כמובן עם הסבר בתיעוד.
        public List<BusLine> this[string busLineNumber]
        {
            get { return ReturnsAllBusLinesByLineNumber(busLineNumber); }
        }

        private List<BusLine> ReturnsAllBusLinesByLineNumber(string busLineNumber)
        {
            List<BusLine> busLinesByLineNumber = new List<BusLine>();
            foreach(var busLine in this)
            {
                if (busLine.BusLineNumber == busLineNumber)
                {
                    busLinesByLineNumber.Add(busLine);
                }
            }
            if (busLinesByLineNumber.Count == 0)
            {
                throw new KeyNotFoundException("No bus found for key busLineNumber: " + busLineNumber);
            }
            return busLinesByLineNumber;
        }
        /// <summary>
        /// הוספת תחנה לקו
        /// </summary>
        public void AddStationToSpecificBusLine()
        {
            Console.WriteLine("Enter the bus line number");
            string busLineNumber = Console.ReadLine();
            foreach (var busLine in this[busLineNumber])
            {
                busLine.AddSingleBusStopToBusLine(BusStopsList);
            }

        }

        /// <summary>
        /// מחיקת תחנה מקו
        /// </summary>
        public void DeleteStationFromSpecificBusLine()
        {
            Console.WriteLine("Enter the bus line number");
            string busLineNumber = Console.ReadLine();
            Console.WriteLine("Enter the bus station key");
            string busStationKey = Console.ReadLine();
            foreach (var busLine in this[busLineNumber])
            {
                busLine.DeleteStation(busStationKey);
            }

        }
        /// <summary>
        /// הפונקציה מחזירה את כל הקווים שעוברים בתחנה
        /// </summary>
        /// <returns></returns>
        public List<string> GetBusLinesThatStopInBusStation(string busStationKey)
        {
            List<string> allBusLinesThatStopInBusStation = new List<string>();

            foreach (var b in this)
            {
                if (b.IsBusStopExist(busStationKey))
                {
                    allBusLinesThatStopInBusStation.Add(b.BusLineNumber);
                }
            }

            // יכול להיות כמה קווים עם אותו המספר שעוברים באותה התחנה
            // אין צורך להציג את הכפילויות
            allBusLinesThatStopInBusStation = allBusLinesThatStopInBusStation.Distinct().ToList();
            return allBusLinesThatStopInBusStation;
        }

        /// <summary>
        /// הפונקציה מדפיסה את כל האוטובוסים
        /// </summary>
        public void PrintAllBusses()
        {
            if (BusLinesList == null || BusLinesList.Count == 0)
            {
                Console.WriteLine("no buses in collection:");
                return;
            }

            Console.WriteLine("Buses in collection:");

            foreach (var bus in this)
            {
                Console.WriteLine(bus.BusLineNumber);
            }


        }
    }
}
