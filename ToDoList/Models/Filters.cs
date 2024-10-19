using Microsoft.VisualBasic;

namespace ToDoList.Models
{
    public class Filters
    {
        public Filters(string filterstring) 
        {
            FilterString = filterstring ?? "all-all-all";
            string[] filters = FilterString.Split('-');
            Due = filters[0];
            StatusId = filters[1];
        }

        public string FilterString { get; }
        public string Due { get; }
        public string StatusId { get; }

        public bool HasDue => Due.ToLower() != "all";
        public bool HasStatue => StatusId.ToLower() != "all";

        public static Dictionary<string, string> DueFilterValues = new Dictionary<string, string>
        {
            {"future", "Future"},
            {"past", "Past"},
            {"today", "Today"}
        };

        public bool IsPast => Due.ToLower() == "past";
        public bool isFuture => Due.ToLower() == "future";
        public bool isToday => Due.ToLower() == "today";
    }
}
