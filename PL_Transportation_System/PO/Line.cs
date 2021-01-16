using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL_Transportation_System.PO
{
    public class Line : DependencyObject
    {
        public int LineId { get; set; } // קוד הקו

        public int LineNumber
        {
            get { return (int)GetValue(lineNumberProperty); }
            set { SetValue(lineNumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty lineNumberProperty =
            DependencyProperty.Register("LineNumber", typeof(int), typeof(Line), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnPropChanged)));

        private static void OnPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PO.Line)d).IsUpdated = true;
        }

        public Areas Area
        {
            get { return (Areas)GetValue(AreaProperty); }
            set { SetValue(AreaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Area.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaProperty =
            DependencyProperty.Register("Area", typeof(Areas), typeof(Line), new FrameworkPropertyMetadata(default(Areas), FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnPropChanged)));


        public IEnumerable<StationOfLine> StationsList
        {
            get { return (IEnumerable<StationOfLine>)GetValue(StationsListProperty); }
            set { SetValue(StationsListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StationsList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StationsListProperty =
            DependencyProperty.Register("StationsList", typeof(IEnumerable<StationOfLine>), typeof(Line), new FrameworkPropertyMetadata(new List<StationOfLine>(), FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnPropChanged)));


        public bool IsUpdated { get; set; }
        public bool IsDeleted
        {
            get { return (bool)GetValue(IsDeletedProperty); }
            set { SetValue(IsDeletedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDeleted.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDeletedProperty =
            DependencyProperty.Register("IsDeleted", typeof(bool), typeof(Line), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnPropChanged)));



    }

}

