using System;

namespace AliasEnum;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class AliasAttribute(params string[] aliases) : Attribute {
  public string[] Aliases { get; } = aliases;
}
