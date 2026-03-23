using rental_app.service;

namespace rental_app.view.userView;

public class UserView
{
    private readonly UserService _userService;

    public UserView(UserService userService)
    {
        _userService = userService;
    }

    public void ShowTypesOfUsersToCreate()
    {
        Console.WriteLine("1. Create new Employee");
        Console.WriteLine("2. Create new Student");
        Console.WriteLine("Type QQ for returning to main menu");
        
        Console.Write("Choose one or type 'QQ': ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1" or "2":
                GetValuesFromUser(input);
                break;
            case "QQ":
                return;
            default:
                Console.WriteLine("Wrong input!");
                Console.WriteLine("################################\n\n");
                return;
        }
    }

    private void GetValuesFromUser(string input)
    {
        Console.Write("Enter name: ");
        string name = Console.ReadLine();
        
        Console.Write("Enter surname: ");
        string surname = Console.ReadLine();

        if (input.Equals("1"))
        {
            _userService.AddEmployee(name, surname);
        }
        else
        {
            _userService.AddStudent(name, surname);
        }

        string type = input.Equals("1") ? "Employee" : "Student";
        Console.WriteLine($"Added new {type}");
    }

    public void ShowAllUsers()
    {
        var users = _userService.GetAllUsers();

        Console.WriteLine("All users:");
        foreach (var pair in users)
        {
            Console.WriteLine($"id: {pair.Key} - name: {pair.Value.Name} - surname: {pair.Value.Surname}");
        }
    }
}