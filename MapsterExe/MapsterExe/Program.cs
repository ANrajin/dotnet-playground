using Mapster;
using MapsterExe;
using MapsterExe.DTOs;
using MapsterExe.Entities;

Student student = new Student()
{
    Id = 1,
    FirstName = "Hello",
    LastName = "World",
    Email = "hello.world@gmail.com",
    Addresses = new List<Address>() { new Address()
    {
        Id = 1,
        City = "Dhaka",
        Region = "Mirpur",
        Country = "Bangladesh"
    },
    new Address(){
        Id = 2,
        City = "Sylhet",
        Region = "Habiganj",
        Country = "Bangladesh"
    }}
};

var data = student.Adapt<StudentDto>();


Console.WriteLine(data);
