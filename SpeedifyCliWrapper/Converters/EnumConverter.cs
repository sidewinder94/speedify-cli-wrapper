using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpeedifyCliWrapper.Converters
{
    public class EnumConverter : JsonConverter
    {
        

        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            var value = (string)reader.Value;

            bool isEnumerable = objectType.IsEnum 
                || (objectType.IsGenericType 
                    && objectType.GetGenericTypeDefinition() == typeof(Nullable<>) 
                    && objectType.GetGenericArguments()[0].IsEnum);

            if (!isEnumerable)
            {
                throw new InvalidOperationException("Only works on enums");
            }

            bool isNullable = !objectType.IsEnum;

            Type enumType = objectType;

            if(isNullable)
            {
                enumType = objectType.GetGenericArguments()[0];
            }

            var enumNames = Enum.GetNames(enumType);

            foreach (var enumName in enumNames)
            {
                var lEnumName = enumName.ToLower();
                var lValue = value.ToLower().Replace("_", "").Trim();

                if (lEnumName == lValue)
                {
                    return Enum.Parse(enumType, enumName);
                }
            }

            if(isNullable)
            {
                return null;
            }

            throw new KeyNotFoundException($"{value} couldn't be found in {objectType.Name}");
        }

        public override void WriteJson(JsonWriter writer, object value,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}