using System.Reflection;

namespace Tutoring.Common.Primitives.Enumeration;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<int, TEnum> Enumerations = CreateEnumerations();

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; protected init; }
    public string Name { get; protected init; }

    public static TEnum FromValue(int id)
    {
        return Enumerations.TryGetValue(id, out TEnum? enumeration) ? enumeration : default;
    }

    public static TEnum FromName(string name)
    {
        return Enumerations.Values.FirstOrDefault(x => x.Name == name);
    }

    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null)
        {
            return false;
        }

        return GetType() == other.GetType() && Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Enumeration<TEnum> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static IEnumerable<TEnum> GetValues()
    {
        return Enumerations.Values;
    }

    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        var enumerationsType = typeof(TEnum);

        var fieldsForType = enumerationsType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(fieldInfo => enumerationsType.IsAssignableFrom( fieldInfo.FieldType))
            .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);

        return fieldsForType.ToDictionary(x => x.Id);
    }
}