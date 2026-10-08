using System;

namespace Entities;
/// <summary>
/// Person Domain Model Class
/// </summary>
public class Person
{
    public Guid PersonID{get;set;}
    public String? Email{get;set;} 
    public String? PersonName{get;set;} 
    public DateTime? BirthOfDate{get;set;} 
    public String? Gender{get;set;} 
    public Guid? CountryID{get;set;} 
    public String? CountryName{get;set;} 
    public String? Address{get;set;} 
    public bool? RecieveNewsLetter{get;set;} 
}
