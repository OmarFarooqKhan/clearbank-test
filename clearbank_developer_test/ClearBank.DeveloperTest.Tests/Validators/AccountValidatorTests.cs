using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators;

public class AccountValidatorTests
{
    private readonly AccountValidator _sut;

    public AccountValidatorTests()
    {
        _sut = new AccountValidator();
    }

    [Fact]
    public void Given_AccountValidator_WhenAccountIsPopulated_Then_ReturnTrue()
    {
        var result = _sut.IsValidAccount(new Account());

        result.Should().BeTrue();
    }

    [Fact]
    public void Given_AccountValidator_WhenNoAccountIsProvided_Then_ReturnFalse()
    {
        var result = _sut.IsValidAccount(null);

        result.Should().BeFalse();
    }
}