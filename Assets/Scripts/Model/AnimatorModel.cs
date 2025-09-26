using System;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class AnimatorModel : Attribute
{
    public string Name { get; }
    public AnimatorModel(string name) => Name = name;
}