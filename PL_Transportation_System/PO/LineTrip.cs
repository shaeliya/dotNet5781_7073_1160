using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL_Transportation_System.PO
{
   public class LineTrip: DependencyObject
    {
        /*
        
        public TimeSpan StartAt { get; set; }
         /// <summary>
        /// סימון שהישות נמחקה בכדי שלא נמחק אותה בפועל
        /// </summary>
        */

        public int LineTripId { get; set; }
        public Line Line { get; set; }
        public bool IsDeleted { get; set; }


        public TimeSpan StartAt
        {
            get { return (TimeSpan)GetValue(StartAtProperty); }
            set { SetValue(StartAtProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartAt.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartAtProperty =
            DependencyProperty.Register("StartAt", typeof(TimeSpan), typeof(LineTrip), new PropertyMetadata(default(TimeSpan)));




    }
}
