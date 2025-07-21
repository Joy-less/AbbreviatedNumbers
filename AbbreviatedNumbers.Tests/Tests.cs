namespace AbbreviatedNumbers.Tests;

public class Tests {
    [Fact]
    public void SmallDoubleTest() {
        1.2345.AbbreviateNumber().ShouldBe("1");
        1.5345.AbbreviateNumber().ShouldBe("2");
    }
    [Fact]
    public void SmallDoubleDecimalsTest() {
        1.2345.AbbreviateNumber(2).ShouldBe("1.23");
        1.5345.AbbreviateNumber(2).ShouldBe("1.53");
    }
    [Fact]
    public void SmallDoubleMidpointRoundingTest() {
        1.5345.AbbreviateNumber(MidpointRounding: MidpointRounding.AwayFromZero).ShouldBe("2");
        1.5345.AbbreviateNumber(MidpointRounding: MidpointRounding.ToZero).ShouldBe("1");
    }
    [Fact]
    public void LargeDoubleTest() {
        1234.56.AbbreviateNumber().ShouldBe("1K");
        5678.56.AbbreviateNumber().ShouldBe("6K");
    }
    [Fact]
    public void LargeDoubleDecimalsTest() {
        1234.56.AbbreviateNumber(1).ShouldBe("1.2K");
        5678.56.AbbreviateNumber(2).ShouldBe("5.68K");
    }
    [Fact]
    public void LargeDoubleMidpointRoundingTest() {
        5678.56.AbbreviateNumber(MidpointRounding: MidpointRounding.AwayFromZero).ShouldBe("6K");
        5678.56.AbbreviateNumber(MidpointRounding: MidpointRounding.ToZero).ShouldBe("5K");
    }
    [Fact]
    public void NegativeLargeDoubleDecimalsTest() {
        (-1234.56).AbbreviateNumber(1).ShouldBe("-1.2K");
        (-5678.56).AbbreviateNumber(2).ShouldBe("-5.68K");
    }
}