using System;
using System.ComponentModel.DataAnnotations;

namespace Entities;
/// <summary>
/// Person Domain Model Class
/// </summary>
public class Person
{
    [Key]
    public Guid PersonID{get;set;}
    [StringLength(40)] // nVarChar(40)
    public String? Email{get;set;} 
    [StringLength(40)] // nVarChar(40)
    public String? PersonName{get;set;} 
    public DateTime? BirthOfDate{get;set;} 
    [StringLength(10)] // nVarChar(10)
    public String? Gender{get;set;} 
    public Guid? CountryID{get;set;} 
    public String? CountryName{get;set;} 

    [StringLength(200)] // nVarChar(200)
    public String? Address{get;set;} 
    public bool? RecieveNewsLetter{get;set;} 
}
