﻿namespace MartenCqrs.Core;

using System.Collections.Generic;
using System.Linq;
using System.Reflection;


public abstract record Enumeration<T> : IComparable<T> where T : Enumeration<T>, new()
{
    private static readonly Lazy<Dictionary<int, T>> AllItems;
    private static readonly Lazy<Dictionary<string, T>> AllItemsByName;

    public static readonly T Undefined = new() {Value = -1, DisplayName = nameof(Undefined)}!;

    static Enumeration()
    {
        AllItems = new Lazy<Dictionary<int, T>>(() =>
        {
            return typeof(T)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(x => x.FieldType == typeof(T))
                .Select(x => x.GetValue(null))
                .Cast<T>()
                .ToDictionary(x => x.Value, x => x);
        });
        AllItemsByName = new Lazy<Dictionary<string, T>>(() =>
        {
            var items = new Dictionary<string, T>(AllItems.Value.Count);
            foreach (var item in AllItems.Value)
            {
                if (!items.TryAdd(item.Value.DisplayName, item.Value))
                {
                    throw new Exception(
                        $"DisplayName needs to be unique. '{item.Value.DisplayName}' already exists");
                }
            }
            return items;
        });
    }

    public Enumeration(){}
    protected Enumeration(int value, string displayName)
    {
        Value = value;
        DisplayName = displayName;
    }

    public int Value { get; private init; } = default!;
    public string DisplayName { get; private init; } = default!;
    public override string ToString() => DisplayName;

    public static IEnumerable<T> GetAll()
    {
        return AllItems.Value.Values;
    }

    public static int AbsoluteDifference(Enumeration<T> firstValue, Enumeration<T> secondValue)
    {
        return Math.Abs(firstValue.Value - secondValue.Value);
    }

    public static T FromValue(int value)
    {
        if (AllItems.Value.TryGetValue(value, out var matchingItem))
        {
            return matchingItem;
        }
        throw new InvalidOperationException($"'{value}' is not a valid value in {typeof(T)}");
    }

    public static T FromDisplayName(string displayName)
    {
        if (AllItemsByName.Value.TryGetValue(displayName, out var matchingItem))
        {
            return matchingItem;
        }
        throw new InvalidOperationException($"'{displayName}' is not a valid display name in {typeof(T)}");
    }

    public static T FromDisplayName(string displayName, Comparison<string> comparer)
    {
        foreach (var value in AllItemsByName.Value.Values)
            if(comparer(value.DisplayName, displayName) == 0)
                return value;
        throw new InvalidOperationException($"'{displayName}' is not a valid display name in {typeof(T)}");
    }

    public static bool TryParse(int value, out T parsed)
    {
        var types = GetAll();
        parsed = types.FirstOrDefault(x => x.Value.Equals(value), Undefined);
        return !parsed.Equals(Undefined);
    }

    public static bool TryParse(string value, out T parsed)
    {
        var types = GetAll();
        parsed = types.FirstOrDefault(x => x.DisplayName.Equals(value, StringComparison.InvariantCultureIgnoreCase), Undefined);
        return !parsed.Equals(Undefined);
    }

    public static bool TryParse(T value, out T parsed)
    {
        var types = GetAll();
        parsed = types.FirstOrDefault(x => x.Equals(value), Undefined);
        return !parsed.Equals(Undefined);
    }


    public int CompareTo(T? other) => Value.CompareTo(other!.Value);
}