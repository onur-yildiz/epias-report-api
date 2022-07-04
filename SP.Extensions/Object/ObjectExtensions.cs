using System.Reflection;

namespace SP.Extensions.Object
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Compares the public properties of this with the object of same type to determine equality. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="to"></param>
        /// <param name="ignore"></param>
        /// <returns></returns>
        public static bool PublicInstancePropertiesEqual<T>(this T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                var type = typeof(T);
                var ignoreList = new List<string>(ignore);
                var unequalProperties =
                    from pi in
                    type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    where !ignoreList.Contains(pi.Name)
                    let selfValue = type.GetProperty(pi.Name)!.GetValue(self, null)
                    let toValue = type.GetProperty(pi.Name)!.GetValue(to, null)
                    where selfValue != toValue &&
                    (selfValue == null || !selfValue.Equals(toValue) && !selfValue.PublicInstancePropertiesEqual(toValue))
                    select selfValue;
                return !unequalProperties.Any();
            }
            return self == to;
        }
    }
}
