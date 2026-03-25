using rental_app.entity.equipment;
using rental_app.entity.user;

namespace rental_app.entity.rental;

public class RentalDTO
{
    public long Id { get; }
    public User UserObj { get; }
    public Device DeviceObj { get; }

    public RentalDTO(long id, User userObj, Device deviceObj)
    {
        Id = id;
        UserObj = userObj;
        DeviceObj = deviceObj;
    }
}