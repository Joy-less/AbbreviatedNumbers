# Abbreviated Numbers

[![NuGet](https://img.shields.io/nuget/v/AbbreviatedNumbers.svg)](https://www.nuget.org/packages/AbbreviatedNumbers)

Methods to abbreviate numbers (e.g. "1K").

## Usage

```cs
NumberAbbreviator.AbbreviateNumber(5678.0); // 6K
NumberAbbreviator.AbbreviateNumber(5678.0, 1); // 5.7K
NumberAbbreviator.AbbreviateNumber(5678.0, 1, MidpointRounding.ToZero); // 5.6K
```