using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL_Transportation_System.Utils
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class DisplayAttribute : Attribute
    {
        public DisplayAttribute(string displayName)
        {
            Description = displayName;
        }

        public string Description { get; set; }
    }
    public class DisplayAttributeBasedObjectDataProvider : ObjectDataProvider
    {
        public object GetEnumValues(Enum enumObj)
        {
            var attribute = enumObj.GetType().GetRuntimeField(enumObj.ToString()).
                GetCustomAttributes(typeof(DisplayAttribute), false).
                SingleOrDefault() as DisplayAttribute;
            return attribute == null ? enumObj.ToString() : attribute.Description;
        }

        public List<object> GetShortListOfApplicationGestures(Type type)
        {
            var shortListOfApplicationGestures = Enum.GetValues(type).OfType<Enum>().Select(GetEnumValues).ToList();
            return
                shortListOfApplicationGestures;
        }
    }

}
