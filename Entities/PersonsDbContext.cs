using System;
using Microsoft.EntityFrameworkCore; 

namespace Entities;

public class PersonsDbContext : DbContext
{
    public PersonsDbContext(DbContextOptions<PersonsDbContext>  options):base(options)
    {
        
    }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Person> Persons { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Country>().ToTable("Countries");
        modelBuilder.Entity<Person>().ToTable("Persons");


        // seed to countries
        string countriesJson =  System.IO.File.ReadAllText("countries.json");
        List<Country> countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countriesJson);

        foreach (var country in countries)
        {
            modelBuilder.Entity<Country>().HasData(country);
        }

        // seed to Persons
        string PersonsJson =  System.IO.File.ReadAllText("Persons.json");
        // Deserialize the json data
        List<Person> persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(PersonsJson);

        foreach (var person in persons)
        {
            modelBuilder.Entity<Person>().HasData(person);
        }
    }
}
