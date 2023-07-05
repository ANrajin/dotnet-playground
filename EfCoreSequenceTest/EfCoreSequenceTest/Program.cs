// See https://aka.ms/new-console-template for more information
using EfCoreSequenceTest;
using EfCoreSequenceTest.Entities;

Thread.Sleep(7500);

Console.WriteLine("Hello, World!");

var context = new ApplicationDbContext();

for (int i = 0; i < 1000;  i++)
{
    var emp = new Employee
    {
        Name = $"Rayhan{i}"
    };

    context.Employees.Add(emp);

    context.SaveChanges();

    Console.WriteLine(emp.EmployeeId);
}
