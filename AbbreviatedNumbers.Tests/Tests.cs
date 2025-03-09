namespace AbbreviatedNumbers.Tests;

public class Tests {
    [Fact]
    public void SmallIntegerTest() {
        Assert.Equal("1", 1.AbbreviateNumber());
        Assert.Equal("2", 2.AbbreviateNumber());
    }
    [Fact]
    public void SmallFloatTest() {
        Assert.Equal("1", 1.2345.AbbreviateNumber());
        Assert.Equal("2", 1.5345.AbbreviateNumber());
    }
    [Fact]
    public void SmallFloatDecimalsTest() {
        Assert.Equal("1.23", 1.2345.AbbreviateNumber(2));
        Assert.Equal("1.53", 1.5345.AbbreviateNumber(2));
    }
    [Fact]
    public void LargeIntegerTest() {
        Assert.Equal("1K", 1234.AbbreviateNumber());
        Assert.Equal("6K", 5678.AbbreviateNumber());
    }
    [Fact]
    public void NegativeLargeIntegerDecimalsTest() {
        Assert.Equal("-1K", (-1234).AbbreviateNumber());
        Assert.Equal("-6K", (-5678).AbbreviateNumber());
    }
    [Fact]
    public void LargeFloatTest() {
        Assert.Equal("1K", 1234.56.AbbreviateNumber());
        Assert.Equal("6K", 5678.56.AbbreviateNumber());
    }
    [Fact]
    public void LargeFloatDecimalsTest() {
        Assert.Equal("1.2K", 1234.56.AbbreviateNumber(1));
        Assert.Equal("5.68K", 5678.56.AbbreviateNumber(2));
    }
    [Fact]
    public void NegativeLargeFloatDecimalsTest() {
        Assert.Equal("-1.2K", (-1234.56).AbbreviateNumber(1));
        Assert.Equal("-5.68K", (-5678.56).AbbreviateNumber(2));
    }
}