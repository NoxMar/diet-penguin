namespace DietPenguin.Core.Tests;

public class BaseEntityTests
{
    private const string ExampleUser = "example@example.com";
    private class BaseEntityWithExposedProtected : BaseEntity
    {
        public void UpdateCreationProperties_Exposed(DateTime createdOn, string? createdBy) 
            => UpdateCreationProperties(createdOn, createdBy);
        
        public void UpdateModifiedProperties_Exposed(DateTime? lastModified, string? modifiedBy) 
            => UpdateModifiedProperties(lastModified, modifiedBy);
    }
    // ReSharper disable once NotAccessedPositionalProperty.Local
    private record TestDomainEvent(int Field = 0) : DomainEvent;
    [Fact]
    public void QueueDomainEvent_AddEvent_EventIsUnique()
    {
        // Arrange
        var sut = Substitute.For<BaseEntity>();
        
        // Act
        for (int i = 0; i < 10; i++)
        {
            sut.QueueDomainEvent(new TestDomainEvent(i));
        }
        
        // Assert
        sut.DomainEvents.Should().HaveCount(10);
    }

    [Fact]
    public void QueueDomainEvent_NotAddEvent_EventIsNotUnique()
    {
        // Arrange
        var sut = Substitute.For<BaseEntity>();
        var firstEvent = new TestDomainEvent(1);
        var duplicateEvent = new TestDomainEvent(1);
        sut.QueueDomainEvent(firstEvent);
        
        // Act
        sut.QueueDomainEvent(duplicateEvent);
        
        // Assert
        sut.DomainEvents.Should().HaveCount(1);
    }

    [Fact]
    public void UpdateCreationProperties__SetCreationProperties()
    {
        // Arrange
        var sut = new BaseEntityWithExposedProtected();
        var createdOn = new DateTime(2023, 1, 1);
        
        // Act
        sut.UpdateCreationProperties_Exposed(createdOn, ExampleUser);
        
        // Assert
        sut.CreatedOn.Should().Be(createdOn);
        sut.CreatedBy.Should().Be(ExampleUser);
    }

    [Fact]
    public void UpdateModifiedProperties__SetModifiedProperties()
    {
        // Arrange
        var sut = new BaseEntityWithExposedProtected();
        var modifiedOn = new DateTime(2023, 1, 1);
        
        // Act
        sut.UpdateModifiedProperties_Exposed(modifiedOn, ExampleUser);
        
        // Assert
        sut.LastModifiedOn.Should().Be(modifiedOn);
        sut.LastModifiedBy.Should().Be(ExampleUser);
    }

    [Fact]
    public void UpdateIsDeleted_CalledWithoutParameters_MarkEntityAsDeleted()
    {
        // Arrange
        var sut = Substitute.For<BaseEntity>();
        
        // Act
        sut.UpdateIsDeleted();
        
        // Assert
        sut.IsDeleted.Should().BeTrue();
    }

    [Fact]
    public void UpdateIsDeleted_EntityIsDeleted_MarkAsNotDeleted()
    {
        // Arrange
        var sut = Substitute.For<BaseEntity>();
        sut.UpdateIsDeleted();
        
        // Act
        sut.UpdateIsDeleted(false);
        
        // Assert
        sut.IsDeleted.Should().BeFalse();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(10)]
    public void ClearDomainEvents_CalledRegardlessOfEventCount_ClearQueuedEvents(int eventCount)
    {
        // Arrange
        var sut = Substitute.For<BaseEntity>();
        for (int i = 0; i < eventCount; i++)
        {
            sut.QueueDomainEvent(new TestDomainEvent(i));
        }
        
        // Act
        sut.ClearDomainEvents();
        
        // Assert

        sut.DomainEvents.Should().HaveCount(0);
    }
}