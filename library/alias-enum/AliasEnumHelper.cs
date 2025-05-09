using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AliasEnum;

public static class AliasEnumHelper {
  public static List<string> GetAllValidValues<TEnum>() where TEnum : struct, Enum {
    if (TypeDescriptor.GetConverter(typeof(TEnum)) is AliasEnumTypeConverter<TEnum> converter) return converter.GetAllValidValues();
    throw new InvalidOperationException($"Type {typeof(TEnum).Name} does not have a valid AliasEnumTypeConverter.");
  }

  public static TEnum? ParseEnum<TEnum>(string value) where TEnum : struct {
    TypeConverter converter = TypeDescriptor.GetConverter(typeof(TEnum));
    if (converter != null) {
      try {
        return (TEnum?)converter.ConvertFromString(value);
      } catch {
        return null;
      }
    }
    return null;
  }
}
