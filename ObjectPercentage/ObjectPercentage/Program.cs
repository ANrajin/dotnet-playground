using ObjectPercentage;
using ObjectPercentage.Student;
using System.Collections;

var model = new BaseModel()
{
    Name = "Rajin",
    Email = "rajin@gmail.com",
    PhoneNumber = "1782044014",
    Educations = new List<Education>()
    {
        new Education()
        {
            InstituteName = "Sample",
            FieldOfStudy = "Sample"
        },
                new Education()
        {
            InstituteName = "Sample",
            FieldOfStudy = "Sample"
        },
                        new Education()
        {
            InstituteName = "Sample",
            FieldOfStudy = "Sample"
        }
    },
    Skills = new List<Skills>()
};

var count = 0;

var type = model.GetType();
var properties = type.GetProperties();

var allowedDataType = new List<Type>()
{
    typeof(string),
    typeof(int),
    typeof(double),
    typeof(float),
    typeof(decimal),
    typeof(DateTime),
    typeof(List<>),
    typeof(IEnumerable),
    typeof(IEnumerable<>),
};

foreach (var property in properties)
{
    if (allowedDataType.Contains(property.PropertyType) && property.GetValue(model) is not null)
        count++;

    if (property.PropertyType.IsGenericType && (property.PropertyType.GetGenericTypeDefinition() == typeof(IList<>)))
    {
        var list = property.GetValue(model) as IList;

        if (list is not null && list.Count > 0)
            count++;
    }
}

Console.WriteLine(count);

