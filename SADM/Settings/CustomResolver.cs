using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Reflection;

namespace SADM.Settings
{
    public class CustomResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);
            if (prop.PropertyType.IsGenericType && member.GetCustomAttribute<JsonPropertyNameBasedOnItemClassAttribute>() != null)
            {
                Type itemType = prop.PropertyType.GetGenericArguments().First();
                JsonPluralNameAttribute att = itemType.GetCustomAttribute<JsonPluralNameAttribute>();
                prop.PropertyName = att != null ? att.PluralName : Pluralize(itemType.Name);
            }
            return prop;
        }

        protected string Pluralize(string name)
        {
            if (name.EndsWith("y") && !name.EndsWith("ay") && !name.EndsWith("ey") && !name.EndsWith("oy") && !name.EndsWith("uy"))
                return name.Substring(0, name.Length - 1) + "ies";

            if (name.EndsWith("s"))
                return name + "es";

            return name + "s";
        }
    }
    public class JsonPropertyNameBasedOnItemClassAttribute : Attribute
    {
    }
    public class JsonPluralNameAttribute : Attribute
    {
        public string PluralName { get; set; }
        public JsonPluralNameAttribute(string pluralName)
        {
            PluralName = pluralName;
        }
    }
}
