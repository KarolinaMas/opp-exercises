namespace ConsoleApp1.Models
{
    internal class Person
    {
        private int age;

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age
        {
            get { return age; }
            set
            {
                if (value >= 0)
                {
                    age = value;
                }
                else
                {
                    Console.WriteLine("Age cannot be negative.");
                }
            }
        }
        public string City { get; set; }

        internal Person(string firstName, string lastName, int age, string city)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            City = city;
        }
    }
}
