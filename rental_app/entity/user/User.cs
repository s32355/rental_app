using System.Text.Json.Serialization;

namespace rental_app.entity.user;

[JsonDerivedType(typeof(Employee), nameof(Employee))]
[JsonDerivedType(typeof(Student), nameof(Student))]
public abstract class User : IEntity
{
    private static long _idCounter = 1L;

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