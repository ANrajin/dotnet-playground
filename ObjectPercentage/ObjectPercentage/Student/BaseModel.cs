namespace ObjectPercentage.Student;

public class BaseModel
{
    public string Name { get; set; }
    
    public  string Email { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public IList<Education> Educations { get; set; }
    
    public IList<Skills> Skills { get; set; }
    
    public int Level { get; set; }
}