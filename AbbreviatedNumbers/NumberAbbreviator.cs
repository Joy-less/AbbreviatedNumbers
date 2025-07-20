using System.Collections.Frozen;
using System.Numerics;

namespace AbbreviatedNumbers;

/// <summary>
/// Methods to abbreviate numbers.
/// </summary>
public static class NumberAbbreviator {
    /// <summary>
    /// Standard number abbreviations.
    /// </summary>
    public static IReadOnlyDictionary<BigInteger, string> DefaultAbbreviations { get; set; } = new Dictionary<BigInteger, string> {
        [1_000] = "K",
        [1_000_000] = "M",
        [1_000_000_000] = "B",
        [1_000_000_000_000] = "T",
    }.ToFrozenDictionary();

    /// <summary>
    /// Abbreviates the number to the nearest abbreviation.<br/>
    /// For example:
    /// <code>
    /// AbbreviateNumber(-5678) // -6K
    /// AbbreviateNumber(1234.56, 1) // 1.2K
    /// </code>
    /// </summary>
    public static string AbbreviateNumber<TValue, TAbbreviation>(this TValue Value, int DecimalPlaces, IReadOnlyDictionary<TAbbreviation, string> Abbreviations, IFormatProvider? Provider = null)
        where TValue : INumberBase<TValue>, IComparisonOperators<TValue, TValue, bool>
        where TAbbreviation : INumberBase<TAbbreviation>
    {
        // Create format string
        string Format = "0." + new string('#', DecimalPlaces);
        // Get positive value
        TValue AbsoluteValue = TValue.Abs(Value);

        // Find largest abbreviation
        foreach ((TAbbreviation Divisor, string Abbreviation) in Abbreviations.OrderByDescending(Pair => Pair.Key)) {
            // Convert divisor to value type
            TValue DivisorAsTValue = TValue.CreateSaturating(Divisor);

            // Ensure value is at least divisor
            if (AbsoluteValue < DivisorAsTValue) {
                continue;
            }

            // Divide value by abbreviation divisor
            TValue AbbreviatedValue = Value / DivisorAsTValue;

            // Check if integer division was performed
            if (AbbreviatedValue * DivisorAsTValue != Value) {
                AbbreviatedValue = Round(AbbreviatedValue, DivisorAsTValue);
            }

            // Stringify abbreviated value
            return AbbreviatedValue.ToString(Format, Provider) + Abbreviation;
        }

        // Stringify small value
        return Value.ToString(Format, Provider);
    }
    /// <inheritdoc cref="AbbreviateNumber{TValue, TAbbreviation}(TValue, int, IReadOnlyDictionary{TAbbreviation, string}, IFormatProvider?)"/>
    public static string AbbreviateNumber<TValue>(this TValue Value, int DecimalPlaces = 0)
        where TValue : INumberBase<TValue>, IComparisonOperators<TValue, TValue, bool>
    {
        return AbbreviateNumber(Value, DecimalPlaces, DefaultAbbreviations);
    }

    private static TValue Round<TValue>(TValue Value, TValue Divisor)
        where TValue : INumberBase<TValue>, IComparisonOperators<TValue, TValue, bool>
    {
        // Round integer division (https://stackoverflow.com/a/41078274)
        TValue Two = TValue.One + TValue.One;
        TValue PreQuotient = Value * Two / Divisor;
        TValue Offset = TValue.IsNegative(PreQuotient) ? -TValue.One : TValue.One;
        return (PreQuotient + Offset) / Two;
    }
}