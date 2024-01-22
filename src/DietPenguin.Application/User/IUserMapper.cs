namespace DietPenguin.Application.User;

public interface IUserMapper
{
    public UserDto FromEntity(Domain.User.User user);
}