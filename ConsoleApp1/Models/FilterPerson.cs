namespace ConsoleApp1.Models
{
    internal class FilterPerson
    {
        public List<Person> List { get; set; }
        public Func<Person, bool> Prop { get; set; }
        public List<Person> FilteredList { get; private set; }

        public FilterPerson(List<Person> list, Func<Person, bool> prop)
        {
            List = list;
            Prop = prop;
            FilteredList = FilterPersonByProp(List, Prop);
        }

        private List<Person> FilterPersonByProp(List<Person> list, Func<Person, bool> prop)
        {
            return list.Where(prop).ToList();
        }
    }
}
