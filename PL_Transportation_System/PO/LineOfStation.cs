using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL_Transportation_System.PO
{
   public class LineOfStation: DependencyObject
    {
        public int LineId { get; set; } // קוד הקו

        public int LineNumber
        {
            get { return (int)GetValue(lineNumberProperty); }
            set { SetValue(lineNumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty lineNumberProperty =
            DependencyProperty.Register("LineNumber", typeof(int), typeof(LineOfStation), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnPropChanged)));

        public Areas Area
        {
            get { return (Areas)GetValue(AreaProperty); }
            set { SetValue(AreaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Area.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaProperty =
            DependencyProperty.Register("Area", typeof(Areas), typeof(LineOfStation), new FrameworkPropertyMetadata(default(Areas), FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnPropChanged)));

        public bool IsDeleted
        {
            get { return (bool)GetValue(IsDeletedProperty); }
            set { SetValue(IsDeletedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDeleted.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDeletedProperty =
            DependencyProperty.Register("IsDeleted", typeof(bool), typeof(LineOfStation), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnPropChanged)));
        private static void OnPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PO.LineOfStation)d).IsUpdated = true;
        }
        public bool IsUpdated { get; set; }

    }
}
