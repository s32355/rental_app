using rental_app.model;
using rental_app.repository;
using rental_app.service;

var deviceRepo = new DeviceRepo();
var userRepo = new UserRepo();
var rentalRepo = new RentalRepo();

var deviceService = new DeviceService(deviceRepo);
var userService = new UserService(userRepo);
var rentalService = new RentalService(rentalRepo, userRepo, deviceRepo);

deviceService.AddCamera("Canon EOS R50", new DateOnly(2022, 3, 15), new DateOnly(2025, 3, 15), Status.Available, 24, 64);
deviceService.AddCamera("Sony ZV-E10", new DateOnly(2023, 1, 10), new DateOnly(2026, 1, 10), Status.Available, 24, 128);
deviceService.AddLaptop("Dell XPS 15", new DateOnly(2021, 6, 20), new DateOnly(2024, 6, 20), Status.Available, 15.6, 512);
deviceService.AddLaptop("MacBook Pro 14", new DateOnly(2023, 9, 5), new DateOnly(2026, 9, 5), Status.Available, 14.2, 1024);
deviceService.AddProjector("Epson EB-W51", new DateOnly(2020, 11, 1), new DateOnly(2023, 11, 1), Status.Available, 1280, 800);
deviceService.AddProjector("BenQ MH560", new DateOnly(2022, 7, 18), new DateOnly(2025, 7, 18), Status.Available, 1920, 1080);

userService.AddEmployee("Jan", "Kowalski");
userService.AddEmployee("Anna", "Nowak");
userService.AddEmployee("Piotr", "Wiśniewski");
userService.AddStudent("Marek", "Zielinski");
userService.AddStudent("Karolina", "Wójcik");
userService.AddStudent("Tomasz", "Lewandowski");

rentalService.AddNewRental(3, 0, DateTime.Now, DateTime.Now.AddDays(2));

rentalService.AddNewRental(3, 1, DateTime.Now, DateTime.Now.AddDays(3));

rentalService.AddNewRental(1, 2, new DateTime(2026,3,20,15,30,0), new DateTime(2026,3,23,20,0,0));

double penalty = rentalService.ReturnRentalDevice(2);

Console.WriteLine(penalty);

rentalService.AddNewRental(1,2,new DateTime(2026,2,20,15,30,0), new DateTime(2026,3,20,20,0,0));

penalty = rentalService.ReturnRentalDevice(3);

Console.WriteLine(penalty);