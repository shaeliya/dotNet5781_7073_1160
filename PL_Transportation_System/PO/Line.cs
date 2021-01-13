using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL_Transportation_System.PO
{
    class Line: DependencyObject
    {     
            public int lineNumber
            {
                get { return (int)GetValue(lineNumberProperty); }
                set { SetValue(lineNumberProperty, value); }
            }

            // Using a DependencyProperty as the backing store for lineNumber.  This enables animation, styling, binding, etc...
            public static readonly DependencyProperty lineNumberProperty =
                DependencyProperty.Register("lineNumber", typeof(int), typeof(Line), new PropertyMetadata(0));


            public Enum Area
            {
                get { return (Enum)GetValue(AreaProperty); }
                set { SetValue(AreaProperty, value); }
            }

            // Using a DependencyProperty as the backing store for Area.  This enables animation, styling, binding, etc...
            public static readonly DependencyProperty AreaProperty =
                DependencyProperty.Register("Area", typeof(Enum), typeof(Line), new PropertyMetadata(0));


        public IEnumerable<StationOfLine> StationsList
        {
            get { return (IEnumerable<StationOfLine>)GetValue(StationsListProperty); }
            set { SetValue(StationsListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StationsList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StationsListProperty =
            DependencyProperty.Register("StationsList", typeof(IEnumerable<StationOfLine>), typeof(Line), new PropertyMetadata(0));



    }

    }

