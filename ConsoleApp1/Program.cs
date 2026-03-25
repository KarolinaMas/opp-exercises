using ConsoleApp1.Models;

// 1. Aprašykite klasę Person. Pridėkite FirstName, LastName, Age ir City sąvybės. Sukurkite List<Person>. Surikiuokite sąrašą pagal amžių nuo didžiausios reikšmės ir išveskite į ekraną top 5;

var personList = new List<Person>();

// {
// new Person("Karolis", "Karoliauskas", 45, "Kaunas"),
// new Person("Will", "Smith", 55, "Chicago"),
// new Person("Jesica", "Shy", 25, "Birstonas"),
// new Person("Jonas", "Jonaitis", 18, "Birstonas"),
// new Person("John", "Doe", 33, "Birstonas"),
// new Person("Jane", "Doe", 25, "Birstonas"),
// };

static List<Person> GetTop5OldestPersons(List<Person> list)
{
    var oldestPersonList = list.OrderByDescending(person => person.Age).Take(5).ToList();

    return oldestPersonList;
}

static void LogPersons(List<Person> list)
{
    Console.WriteLine(
        string.Join(
            ", ",
            list.Select(person =>
                $"{person.FirstName} {person.LastName} {person.Age} {person.City}"
            )
        )
    );
}

// LogPersons(GetTop5OldestPersons());

// GetTop5OldestPersons();

// 2. Sukurkite failą, kuriame yra sąrasas Person. Pvz.:
// Vardas Pavardenis 25 Vilnius

var filePath = "personList.txt";

static void WritePersonsToFile(string path, List<Person> list)
{
    List<Person> personList = GetTop5OldestPersons(list);

    using (var writer = new StreamWriter(path, false))
    {
        foreach (var person in personList)
        {
            writer.WriteLine($"{person.FirstName} {person.LastName} {person.Age} {person.City}");
        }
    }
}

// WritePersonsToFile(filePath);

// 3. Parašykite metodą, kuris nuskaito duomenys iš failo į List<Person> sąrašą iš pirmo punkto;

static List<Person> ReadListFromFile(string path, List<Person> list)
{
    if (!File.Exists(path))
    {
        Console.WriteLine("Please enter valid file path.");
        return list;
    }

    using (var reader = new StreamReader(path))
    {
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] wordsArr = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

            if (wordsArr.Length < 4)
                continue;

            if (!int.TryParse(wordsArr[2], out int age))
                continue;

            list.Add(new Person(wordsArr[0], wordsArr[1], age, wordsArr[3]));
        }
    }

    return list;
}

personList = ReadListFromFile(filePath, personList);

// LogPersons(personList);

// 4. Padarykite galimybė filtruoti elementus pagal miestą. Miesto pavadinimas įvedamas vartotojo;

static List<Person> FilterPersonByCity(string city, List<Person> list)
{
    var filteredList = new List<Person>();

    if (string.IsNullOrWhiteSpace(city))
    {
        Console.WriteLine("Please enter city");
        return filteredList;
    }

    foreach (var person in list)
    {
        if (person.City.Equals(city, StringComparison.OrdinalIgnoreCase)) // ignoruoja Case ir palygina City savybe su city
            filteredList.Add(person);
    }

    return filteredList;
}

// var filteredPersons = FilterPersonByCity("Birstonas", personList);

// LogPersons(filteredPersons);

// 5. Padarykite galimybė vartotojui pasirinkti pagal kokią savybę (FirstName, LastName, Age arba City) jis nori filtruoti domenys;

static List<Person> FilterPersonByProp(List<Person> list, Func<Person, bool> prop)
{
    return list.Where(prop).ToList();
}

var filteredPersons2 = FilterPersonByProp(personList, p => p.Age == 25);

// LogPersons(filteredPersons2);

// 6. Padarykite galimybė vartotojui pasirinkti pagal kokią savybę (FirstName, LastName, Age arba City) jis nori rikiuoti domenys;

static List<Person> SortedPersonByProp<T>(List<Person> list, Func<Person, T> prop)
{
    return list.OrderByDescending(prop).ToList();
}

var SortedPersons = SortedPersonByProp(personList, p => p.Age);

// LogPersons(SortedPersons);

// 7. Iškelkite filtravimo logiką į FilterPerson klasė;

var filteredPersons3 = new FilterPerson(personList, p => p.Age == 55);

// LogPersons(filteredPersons3.FilteredList);

// 8. Iškelkite rikiavimo logiką į OrderPerson klasė.

var OrderPerson2 = new OrderPerson<string>(personList, p => p.FirstName);

LogPersons(OrderPerson2.SortedList);
