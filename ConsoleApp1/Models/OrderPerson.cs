namespace ConsoleApp1.Models
{
    internal class OrderPerson<T>
        where T : IComparable<T>
    {
        public List<Person> List { get; set; }
        public Func<Person, T> Prop { get; set; }

        public List<Person> SortedList { get; private set; }

        public OrderPerson(List<Person> list, Func<Person, T> prop)
        {
            List = list;
            Prop = prop;
            SortedList = SortedPersonByProp(List, Prop);
        }

        private List<Person> SortedPersonByProp(List<Person> list, Func<Person, T> prop)
        {
            return list.OrderByDescending(prop).ToList();
        }
    }
}
