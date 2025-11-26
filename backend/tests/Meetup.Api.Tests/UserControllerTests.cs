using Meetup.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Meetup.Api.Tests;

public class UserControllerTests
{
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _controller = new UserController();
    }

    #region Basic English Tests
    
    [Fact]
    public void ValidateUser_WithValidEnglishName_ReturnsValid()
    {
        // Arrange
        var request = new ValidateUserRequest { UserName = "John" };

        // Act
        var result = _controller.ValidateUser(request) as OkObjectResult;
        var response = result?.Value as ValidateUserResponse;

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsValid);
        Assert.Equal("John", response.UserName);
    }

    [Fact]
    public void ValidateUser_WithEmptyString_ReturnsInvalid()
    {
        // Arrange
        var request = new ValidateUserRequest { UserName = "" };

        // Act
        var result = _controller.ValidateUser(request) as OkObjectResult;
        var response = result?.Value as ValidateUserResponse;

        // Assert
        Assert.NotNull(response);
        Assert.False(response.IsValid);
        Assert.Contains("cannot be empty", response.Message);
    }

    [Fact]
    public void ValidateUser_WithNull_ReturnsInvalid()
    {
        // Arrange
        var request = new ValidateUserRequest { UserName = null! };

        // Act
        var result = _controller.ValidateUser(request) as OkObjectResult;
        var response = result?.Value as ValidateUserResponse;

        // Assert
        Assert.NotNull(response);
        Assert.False(response.IsValid);
    }

    #endregion

    #region German Character Tests

    [Theory]
    [InlineData("Müller", "German name with ü")]
    [InlineData("Schröder", "German name with ö")]
    [InlineData("Bäcker", "German name with ä")]
    [InlineData("Weiß", "German name with ß")]
    [InlineData("Jürgen", "German name with ü")]
    [InlineData("Köln", "German city name")]
    public void ValidateUser_WithGermanCharacters_ReturnsValid(string userName, string description)
    {
        // Arrange
        var request = new ValidateUserRequest { UserName = userName };

        // Act
        var result = _controller.ValidateUser(request) as OkObjectResult;
        var response = result?.Value as ValidateUserResponse;

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsValid, $"{description}: Expected '{userName}' to be valid");
        Assert.Equal(userName, response.UserName);
    }

    [Fact]
    public void ValidateUser_WithUppercaseGermanUmlaut_ReturnsValid()
    {
        // Arrange
        var request = new ValidateUserRequest { UserName = "MÜLLER" };

        // Act
        var result = _controller.ValidateUser(request) as OkObjectResult;
        var response = result?.Value as ValidateUserResponse;

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsValid);
    }

    [Fact]
    public void ValidateUser_WithMixedCaseGermanName_ReturnsValid()
    {
        // Arrange
        var request = new ValidateUserRequest { UserName = "MüLLeR" };

        // Act
        var result = _controller.ValidateUser(request) as OkObjectResult;
        var response = result?.Value as ValidateUserResponse;

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsValid);
    }

    #endregion

    #region French Character Tests

    [Theory]
    [InlineData("François", "French name with ç")]
    [InlineData("Léon", "French name with é")]
    [InlineData("André", "French name with é")]
    [InlineData("Noël", "French name with ë")]
    [InlineData("Anaïs", "French name with ï")]
    [InlineData("Jérôme", "French name with é and ô")]
    [InlineData("Hélène", "French name with é and è")]
    public void ValidateUser_WithFrenchCharacters_ReturnsValid(string userName, string description)
    {
        // Arrange
        var request = new ValidateUserRequest { UserName = userName };

        // Act
        var result = _controller.ValidateUser(request) as OkObjectResult;
        var response = result?.Value as ValidateUserResponse;

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsValid, $"{description}: Expected '{userName}' to be valid");
        Assert.Equal(userName, response.UserName);
    }

    [Theory]
    [InlineData("Agnès", "French name with è")]
    [InlineData("Benoît", "French name with î")]
    [InlineData("Gaëlle", "French name with ë")]
    [InlineData("Loïc", "French name with ï")]
    public void ValidateUser_WithFrenchAccentedCharacters_ReturnsValid(string userName, string description)
    {
        // Arrange
        var request = new ValidateUserRequest { UserName = userName };

        // Act
        var result = _controller.ValidateUser(request) as OkObjectResult;
        var response = result?.Value as ValidateUserResponse;

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsValid, $"{description}: Expected '{userName}' to be valid");
    }

    [Fact]
    public void ValidateUser_WithUppercaseFrenchAccents_ReturnsValid()
    {
        // Arrange
        var request = new ValidateUserRequest { UserName = "FRANÇOIS" };

        // Act
        var result = _controller.ValidateUser(request) as OkObjectResult;
        var response = result?.Value as ValidateUserResponse;

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsValid);
    }

    #endregion

    #region Mixed Language Tests

    [Theory]
    [InlineData("JeanMüller", "Mix of French and German")]
    [InlineData("MarieLöwe", "French name with German character")]
    [InlineData("PeterDupré", "German name with French character")]
    public void ValidateUser_WithMixedLanguageCharacters_ReturnsValid(string userName, string description)
    {
        // Arrange
        var request = new ValidateUserRequest { UserName = userName };

        // Act
        var result = _controller.ValidateUser(request) as OkObjectResult;
        var response = result?.Value as ValidateUserResponse;

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsValid, $"{description}: Expected '{userName}' to be valid");
    }

    #endregion

    #region Invalid Character Tests

    [Theory]
    [InlineData("John123", "name with numbers")]
    [InlineData("John_Doe", "name with underscore")]
    [InlineData("John-Doe", "name with hyphen")]
    [InlineData("John.Doe", "name with dot")]
    [InlineData("John Doe", "name with space")]
    [InlineData("John@Doe", "name with special character")]
    [InlineData("John#Doe", "name with hash")]
    public void ValidateUser_WithInvalidCharacters_ReturnsInvalid(string userName, string description)
    {
        // Arrange
        var request = new ValidateUserRequest { UserName = userName };

        // Act
        var result = _controller.ValidateUser(request) as OkObjectResult;
        var response = result?.Value as ValidateUserResponse;

        // Assert
        Assert.NotNull(response);
        Assert.False(response.IsValid, $"{description}: Expected '{userName}' to be invalid");
    }

    [Fact]
    public void ValidateUser_WithOnlyNumbers_ReturnsInvalid()
    {
        // Arrange
        var request = new ValidateUserRequest { UserName = "12345" };

        // Act
        var result = _controller.ValidateUser(request) as OkObjectResult;
        var response = result?.Value as ValidateUserResponse;

        // Assert
        Assert.NotNull(response);
        Assert.False(response.IsValid);
    }

    #endregion
}
