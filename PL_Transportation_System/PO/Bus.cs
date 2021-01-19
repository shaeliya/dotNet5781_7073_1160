using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL_Transportation_System.PO
{

//public DateTime LastTreatmentDate { get; set; } - לא עשיתי

     public class Bus : DependencyObject
    {

        //public double Treatment
        //{
        //    get { return (double)GetValue(TreatmentProperty); }
        //    set { SetValue(TreatmentProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Treatment.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty TreatmentProperty =
        //    DependencyProperty.Register("Treatment", typeof(double), typeof(Bus), new PropertyMetadata(0.0));


        //public double FuelRemain
        //{
        //    get { return (double)GetValue(FuelRemainProperty); }
        //    set { SetValue(FuelRemainProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for FuelRemain.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty FuelRemainProperty =
        //    DependencyProperty.Register("FuelRemain", typeof(double), typeof(Bus), new PropertyMetadata(0.0));

        public int LicenseNumber
        {
            get { return (int)GetValue(LicenseNumberProperty); }
            set { SetValue(LicenseNumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LicenseNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LicenseNumberProperty =
            DependencyProperty.Register("LicenseNumber", typeof(int), typeof(Bus), new PropertyMetadata(0));


        public bool IsUpdated { get; set; }

        private static void OnPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Bus)d).IsUpdated = true;
        }

        public bool IsDeleted
        {
            get { return (bool)GetValue(IsDeletedProperty); }
            set { SetValue(IsDeletedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDeleted.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDeletedProperty =
            DependencyProperty.Register("IsDeleted", typeof(bool), typeof(Bus), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnPropChanged)));





        public DateTime FromDate
        {
            get { return (DateTime)GetValue(FromDateProperty); }
            set { SetValue(FromDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FromDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FromDateProperty =
            DependencyProperty.Register("FromDate", typeof(DateTime), typeof(Bus), new PropertyMetadata(default));




        public double TotalTrip
        {
            get { return (double)GetValue(TotalTripProperty); }
            set { SetValue(TotalTripProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TotalTrip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalTripProperty =
            DependencyProperty.Register("TotalTrip", typeof(double), typeof(Bus), new PropertyMetadata(0.0));


        //    public BusStatuses Status
        //    {
        //        get { return (BusStatuses)GetValue(StatusProperty); }
        //        set { SetValue(StatusProperty, value); }
        //    }

        //    // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
        //    public static readonly DependencyProperty StatusProperty =
        //        DependencyProperty.Register("Status", typeof(BusStatuses), typeof(Bus), new FrameworkPropertyMetadata(default(BusStatuses), FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnPropChanged)));

        //    private static void OnPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //    {
        //        throw new NotImplementedException();
        //    }
    }
}
