namespace AbbreviatedNumbers.Tests;

public class Tests {
    [Fact]
    public void SmallIntegerTest() {
        1.AbbreviateNumber().ShouldBe("1");
        2.AbbreviateNumber().ShouldBe("2");
    }
    [Fact]
    public void SmallFloatTest() {
        1.2345.AbbreviateNumber().ShouldBe("1");
        1.5345.AbbreviateNumber().ShouldBe("2");
    }
    [Fact]
    public void SmallFloatDecimalsTest() {
        1.2345.AbbreviateNumber(2).ShouldBe("1.23");
        1.5345.AbbreviateNumber(2).ShouldBe("1.53");
    }
    [Fact]
    public void LargeIntegerTest() {
        1234.AbbreviateNumber().ShouldBe("1K");
        5678.AbbreviateNumber().ShouldBe("6K");
    }
    [Fact]
    public void NegativeLargeIntegerDecimalsTest() {
        (-1234).AbbreviateNumber().ShouldBe("-1K");
        (-5678).AbbreviateNumber().ShouldBe("-6K");
    }
    [Fact]
    public void LargeFloatTest() {
        1234.56.AbbreviateNumber().ShouldBe("1K");
        5678.56.AbbreviateNumber().ShouldBe("6K");
    }
    [Fact]
    public void LargeFloatDecimalsTest() {
        1234.56.AbbreviateNumber(1).ShouldBe("1.2K");
        5678.56.AbbreviateNumber(2).ShouldBe("5.68K");
    }
    [Fact]
    public void NegativeLargeFloatDecimalsTest() {
        (-1234.56).AbbreviateNumber(1).ShouldBe("-1.2K");
        (-5678.56).AbbreviateNumber(2).ShouldBe("-5.68K");
    }
}