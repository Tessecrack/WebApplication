using System;
using System.Collections.Generic;
[Serializable]
public class Person
{
    public string FirstName { get; set; }  // имя
    public string LastName { get; set; }   // фамилия
    public string MiddleName { get; set; } // отчество
    public string DateOfBirth { get; set; } // дата рождения
    public string Email { get; set; }
    public List<SocialNetwork> SocialNetworks; //список социальных сетей
    public Person() { }
    public Person(string firstName, string lastName, string middleName)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }
}