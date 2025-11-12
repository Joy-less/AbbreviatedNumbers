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
    /// AbbreviateNumber(5678.0) // 6K
    /// AbbreviateNumber(5678.0, 1) // 5.7K
    /// AbbreviateNumber(5678.0, 1, MidpointRounding.ToZero) // 5.6K
    /// </code>
    /// </summary>
    public static string AbbreviateNumber<TValue, TAbbreviation>(this TValue Value, int DecimalPlaces, MidpointRounding MidpointRounding, IReadOnlyDictionary<TAbbreviation, string> Abbreviations, IFormatProvider? Provider = null)
        where TValue : IFloatingPoint<TValue>, IComparisonOperators<TValue, TValue, bool>
        where TAbbreviation : INumberBase<TAbbreviation>
    {
        // Create format string
        string Format = CreateDecimalFormatString(DecimalPlaces);
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

            // Apply rounding to abbreviated value
            AbbreviatedValue = TValue.Round(AbbreviatedValue, DecimalPlaces, MidpointRounding);

            // Stringify abbreviated value
            return AbbreviatedValue.ToString(Format, Provider) + Abbreviation;
        }

        // Apply rounding to small value
        Value = TValue.Round(Value, DecimalPlaces, MidpointRounding);

        // Stringify small value
        return Value.ToString(Format, Provider);
    }
    /// <inheritdoc cref="AbbreviateNumber{TValue, TAbbreviation}(TValue, int, MidpointRounding, IReadOnlyDictionary{TAbbreviation, string}, IFormatProvider?)"/>
    public static string AbbreviateNumber<TValue>(this TValue Value, int DecimalPlaces = 0, MidpointRounding MidpointRounding = MidpointRounding.AwayFromZero)
        where TValue : IFloatingPoint<TValue>, IComparisonOperators<TValue, TValue, bool>
    {
        return AbbreviateNumber(Value, DecimalPlaces, MidpointRounding, DefaultAbbreviations);
    }

    private static string CreateDecimalFormatString(int DecimalPlaces) {
        return DecimalPlaces switch {
            0 => "0.",
            1 => "0.#",
            2 => "0.##",
            3 => "0.###",
            4 => "0.####",
            5 => "0.#####",
            6 => "0.######",
            7 => "0.#######",
            8 => "0.########",
            9 => "0.#########",
            10 => "0.##########",
            _ => "0." + new string('#', DecimalPlaces),
        };
    }
}