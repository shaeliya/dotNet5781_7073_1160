using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL_Transportation_System.PO
{
    public class Station: DependencyObject
    {
        /*            return string.Format("Name: " + Name + "\t Adress: " + Adress + "\t Is deleted: " + IsDeleted);
*/

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(Station), new PropertyMetadata(""));



        public string Adress
        {
            get { return (string)GetValue(AdressProperty); }
            set { SetValue(AdressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Adress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AdressProperty =
            DependencyProperty.Register("Adress", typeof(string), typeof(Station), new PropertyMetadata(""));



        public bool IsDeleted
        {
            get { return (bool)GetValue(IsDeletedProperty); }
            set { SetValue(IsDeletedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDeleted.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDeletedProperty =
            DependencyProperty.Register("IsDeleted", typeof(bool), typeof(Station), new PropertyMetadata(false));
        public override string ToString()
        {
            return string.Format("Name: " + Name + "\t Adress: " + Adress + "\t Is deleted: " + IsDeleted);
        }
    }
}
