using rental_app.repository;
using rental_app.service;
using rental_app.view;
using rental_app.view.deviceView;
using rental_app.view.rentalView;
using rental_app.view.reportView;
using rental_app.view.userView;

// create repositories
var deviceRepo = new DeviceRepo("../../../data/device.json");
var userRepo = new UserRepo("../../../data/user.json");
var rentalRepo = new RentalRepo("../../../data/rental.json");

// create services
var deviceService = new DeviceService(deviceRepo);
var userService = new UserService(userRepo);
var rentalService = new RentalService(rentalRepo, userRepo, deviceRepo);
var reportService = new ReportService(deviceRepo, userRepo, rentalRepo);

var userView = new UserView(userService);
var deviceView = new DeviceView(deviceService);
var rentalView = new RentalView(rentalService);
var reportView = new ReportView(reportService);

AddDataIfEmpty(deviceRepo, deviceService, userRepo, userService, rentalRepo, rentalService);

try
{
    new MainView(userView, deviceView, rentalView, reportView).Start();
}
finally
{
    deviceRepo.SaveDataToJson();
    userRepo.SaveDataToJson();
    rentalRepo.SaveDataToJson();
}

static void AddDataIfEmpty(DeviceRepo deviceRepo, DeviceService deviceService, UserRepo userRepo, UserService userService, RentalRepo rentalRepo, RentalService rentalService)
{
    if (deviceRepo.GetObjects().Count == 0)
    {
        // Add cameras, laptops and projectors
        deviceService.AddCamera("Canon EOS R50", new DateOnly(2022, 3, 15), new DateOnly(2025, 3, 15), 24, 64);
        deviceService.AddCamera("Sony ZV-E10", new DateOnly(2023, 1, 10), new DateOnly(2026, 1, 10),  24, 128);
        deviceService.AddLaptop("Dell XPS 15", new DateOnly(2021, 6, 20), new DateOnly(2024, 6, 20),  15.6, 512);
        deviceService.AddLaptop("MacBook Pro 14", new DateOnly(2023, 9, 5), new DateOnly(2026, 9, 5), 14.2, 1024);
        deviceService.AddProjector("Epson EB-W51", new DateOnly(2020, 11, 1), new DateOnly(2023, 11, 1), 1280, 800);
        deviceService.AddProjector("BenQ MH560", new DateOnly(2022, 7, 18), new DateOnly(2025, 7, 18),  1920, 1080);   
    }
    
    if (userRepo.GetObjects().Count == 0)
    {
        // Add employees and students
        userService.AddEmployee("Jan", "Kowalski");
        userService.AddEmployee("Anna", "Nowak");
        userService.AddEmployee("Piotr", "Wiśniewski");
        userService.AddStudent("Marek", "Zielinski");
        userService.AddStudent("Karolina", "Wójcik");
        userService.AddStudent("Tomasz", "Lewandowski");
    }
    
    if (rentalRepo.GetObjects().Count == 0) 
    {
        // Add two rentals for student
        rentalService.AddNewRental(4, 1, DateTime.Now, DateTime.Now.AddDays(2));
        rentalService.AddNewRental(4, 2, DateTime.Now, DateTime.Now.AddDays(3));
        
        // Try to add third rental for student
        try
        {
            rentalService.AddNewRental(4, 3, DateTime.Now, DateTime.Now.AddDays(3));
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e.Message + "\n");
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message + "\n");
        }

        // Try to add rental with already rented device
        try
        {
            rentalService.AddNewRental(2, 2, DateTime.Now, DateTime.Now.AddDays(3));
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e.Message + "\n");
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message + "\n");
        }

        // Add rental with end date in the past
        rentalService.AddNewRental(1, 6, new DateTime(2026,3,20,15,30,0), new DateTime(2026,3,23,20,0,0));

        // Calculate penalty during returning rental
        double penalty = rentalService.ReturnRentalDevice(3);

        Console.WriteLine($"Penalty {penalty}\n");
        
        // Add and return rental with no penalty 
        rentalService.AddNewRental(1,6,new DateTime(2026,2,20,15,30,0), DateTime.Now.AddDays(2));

        double noPenalty = rentalService.ReturnRentalDevice(4);

        Console.WriteLine($"No penalty: {noPenalty}\n");
    }
}