using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingDelegate
{

    /// <summary>
    /// A class to define a person
    /// </summary>
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public enum AgeGroup
    {
        Child,
        Adult,
        Senior
    }

    class Program
    {
        static void Main(string[] args)
        {
            var people = new List<Person>
            {
                new Person() {Age = 22, Name = "Bob"},
                new Person() {Age = 52, Name = "Matt"},
                new Person() {Age = 21, Name = "Bill"},
                new Person() {Age = 65, Name = "Jared"},
                new Person() {Age = 115, Name = "Trish"},
                new Person() {Age = 5, Name = "Susan"},
                new Person() {Age = 2, Name = "Kate"},
                new Person() {Age = 0, Name = "Ken"},
                new Person() {Age = 18, Name = "Jon"},
                new Person() {Age = 75, Name = "Sean"},
                new Person() {Age = 13, Name = "Scott"}
            };

            DisplayPeople(AgeGroup.Child, people, AgeDescending);
            DisplayPeople(AgeGroup.Adult, people, AgeDescending);
            DisplayPeople(AgeGroup.Senior, people, AgeDescending);

            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();
        }

        public delegate List<Person> PeopleAgeFilter(List<Person> people);

        public delegate List<Person> PeopleOrderFilter(List<Person> people);

        public static void DisplayPeople(AgeGroup ag, List<Person> people, PeopleOrderFilter f)
        {
            var filteredPeople = f(GetAgeFilter(ag)(people));

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Displaying " + Enum.GetName(typeof(AgeGroup), ag));
            foreach (var person in filteredPeople) Console.WriteLine("Person {0} is {1} years old.", person.Name, person.Age);
        }

        public static List<Person> AgeDescending(List<Person> people)
        {
            return people.OrderBy(x => x.Age).ToList();
        }

        public static List<Person> Children(List<Person> people)
        {
            return people.FindAll(p => p.Age < 18);
        }

        public static List<Person> Adult(List<Person> people)
        {
            return people.FindAll(p => p.Age >= 18 && p.Age < 65);
        }

        public static List<Person> Senior(List<Person> people)
        {
            return people.FindAll(p => p.Age > 65);
        }

        public static PeopleAgeFilter GetAgeFilter(AgeGroup ag)
        {
            switch (ag)
            {
                    case AgeGroup.Child: return Children;
                    case AgeGroup.Adult: return Adult;
                    case AgeGroup.Senior: return Senior;
                default: throw new ArgumentException("Unrecognized AgeGroup.");
            }
        }
    }
}
