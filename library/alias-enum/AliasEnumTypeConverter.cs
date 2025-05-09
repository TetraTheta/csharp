using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace AliasEnum;

public abstract class AliasEnumTypeConverter<TEnum> : EnumConverter where TEnum : struct, Enum {
  private static readonly Dictionary<TEnum, string[]> enumToAliases = [];

  static AliasEnumTypeConverter() {
    foreach (var field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static)) {
      var aliasAttr = field.GetCustomAttributes<AliasAttribute>().FirstOrDefault();
      if (aliasAttr != null) if (Enum.TryParse(field.Name, out TEnum enumValue)) enumToAliases[enumValue] = aliasAttr.Aliases;
    }
  }

  protected AliasEnumTypeConverter() : base(typeof(TEnum)) {
  }

  public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
    if (value is string str) {
      foreach (KeyValuePair<TEnum, string[]> pair in enumToAliases) {
        TEnum enumValue = pair.Key;
        string[] aliases = pair.Value;
        if (aliases.Contains(str, StringComparer.OrdinalIgnoreCase)) return enumValue;
      }
      // throw new ArgumentException($"Invalid {typeof(TEnum).Name} name: {str}");
      return null;
    }
    return base.ConvertFrom(context, culture, value);
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

  public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
    if (destinationType == typeof(string) && value is TEnum enumValue) return enumToAliases.TryGetValue(enumValue, out var aliases) ? aliases[0] : enumValue.ToString();
    return base.ConvertTo(context, culture, value, destinationType);
  }

  public List<string> GetAllValidValues() => [.. enumToAliases.Values.SelectMany(a => a)];
}
