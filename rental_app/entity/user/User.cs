namespace rental_app.model.user;

public abstract class User : IUser
{
    private static long _idCounter = 0L;

    public long Id { get; init; }
    public string Name { get; set; }
    public string Surname { get; set; }

    protected User(string name, string surname)
    {
        Id = _idCounter++;
        Name = name;
        Surname = surname;
    }
}