namespace Util.Helper
{
    public static class PropertiesHelper
    {
        public static string getProperties<T>(T t)
        {
            string tStr = string.Empty;
            if (t == null)
            {
                return tStr;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return tStr;
            }

            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    tStr += string.Format("{0}:{1},", name, value);
                }
                else
                {
                    getProperties(value);
                }
            }
            return tStr;
        }
    }
}