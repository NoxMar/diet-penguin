namespace DietPenguin.Core.Tests;

public class ResultsTests
{
    [Fact]
    public void Value_Success_ReturnProvidedValue()
    {
        // Arrange
        var sut = Result<int>.Success(1);

        // Act
        var result = sut.Value;

        // Assert
        result.Should().Be(1);
    }
    
    [Fact]
    public void Value_Failure_ThrowException()
    {
        // Arrange
        var sut = Result<int>.Failure(new Error("TEST", "test"));
        
        // Act 
        var act = () => sut.Value;
        
        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void IsSuccess_Success_BeTrue()
    {
        // Arrange
        var sut = Result<int>.Success(1);
        
        // Act
        var result = sut.IsSuccess;
        
        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsSuccess_Failure_BeFalse()
    {
        // Arrange
        var sut = Result<int>.Failure(new Error("TEST", "test"));
        
        // Act
        var result = sut.IsSuccess;
        
        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Error_Success_BeNone()
    {
        // Arrange
        var sut = Result<int>.Success(1);
        
        // Act
        var error = sut.Error;
        
        // Assert
        error.Should().Be(Error.None);
    }

    [Fact]
    public void Error_Failure_BeTheOnePassedToConstructor()
    {
        // Arrange
        var expectedError = new Error("TEST", "test");
        var sut = Result<int>.Failure(expectedError);

        // Act
        var error = sut.Error;
        
        // Assert
        error.Should().Be(expectedError);
    }

    [Fact]
    public void Match_Success_CallOnSuccess()
    {
        // Arrange
        const int RESULT_VALUE = 1;
        var onSuccess = Substitute.For<Func<int, string>>();
        var sut = Result<int>.Success(RESULT_VALUE);
        
        // Act
        sut.Match(onSuccess, _ => "");
        
        // Assert
        onSuccess.Received(1).Invoke(Arg.Is(RESULT_VALUE));
    }

    [Fact]
    public void Match_Success_NotCallOnError()
    {
        // Arrange
        var onError = Substitute.For<Func<Error, string>>();
        var sut = Result<int>.Success(1);
        
        // Act
        sut.Match(_ => "", onError);
        
        // Assert
        onError.Received(0).Invoke(Arg.Any<Error>());
    }
    
    [Fact]
    public void Match_Failure_CallOnFailure()
    {
        // Arrange
        var onFailure = Substitute.For<Func<Error, string>>();
        var error = new Error("TEST", "test");
        var sut = Result<int>.Failure(error);

        // Act
        sut.Match(_ => "", onFailure);
        
        // Assert
        onFailure.Received(1).Invoke(Arg.Is(error));
    }

    [Fact]
    public void Match_Failure_NotCallOnSuccess()
    {
        // Arrange
        var onSuccess = Substitute.For<Func<int, string>>();
        var sut = Result<int>.Failure(new Error("TEST", "test"));
        
        // Act
        sut.Match(onSuccess, _ => "");
        
        // Assert
        onSuccess.Received(0).Invoke(Arg.Any<int>());
    }
}