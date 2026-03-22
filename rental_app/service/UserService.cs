using rental_app.model.user;
using rental_app.repository;

namespace rental_app.service;

public class UserService
{
    private readonly UserRepo _userRepo;

    public UserService(UserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public void AddEmployee(string name, string surname)
    {
        User emp = new Employee(name, surname);
        _userRepo.AddObject(emp);
    }

    public void AddStudent(string name, string surname)
    {
        User student = new Student(name, surname);
        _userRepo.AddObject(student);
    }

    public User GetUser(long id)
    {
        var user = _userRepo.GetById(id);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with {id} not found");
        }

        return user;
    }
}