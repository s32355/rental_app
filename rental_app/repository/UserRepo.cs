using rental_app.entity.user;

namespace rental_app.repository;

public class UserRepo : AbstractRepo<User>
{
    public int GetTotalNumberOfUsers()
    {
        return _map.Count;
    }
}