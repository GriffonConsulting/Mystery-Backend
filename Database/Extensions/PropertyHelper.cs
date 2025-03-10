using System.Reflection;

namespace Database.Extensions
{
    public static class PropertyHelper
    {
        public static PropertyInfo GetProperty<T>(string propertyName)
        {
            string[] propertyParts = propertyName.Split('.');
            if (propertyParts.Length == 1)
            {
                return GetProperties<T>().FirstOrDefault((p) => string.Equals(p.Name, propertyName, StringComparison.CurrentCultureIgnoreCase));
            }

            return GetProperties(GetProperties<T>().FirstOrDefault((p) => string.Equals(p.Name, propertyParts[0], StringComparison.InvariantCultureIgnoreCase)).PropertyType).FirstOrDefault((up) => string.Equals(up.Name, propertyParts[1], StringComparison.InvariantCultureIgnoreCase));
        }

        public static string GetPropertyName(PropertyInfo property)
        {
            return property?.Name;
        }

        public static IEnumerable<PropertyInfo> GetProperties<T>()
        {
            return typeof(T).GetProperties();
        }

        public static IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            return type.GetProperties();
        }

        public static IEnumerable<PropertyInfo> GetProperties<T>(T container)
        {
            return container.GetType().GetProperties();
        }
    }
}
