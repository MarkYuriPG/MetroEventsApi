using System.ComponentModel.DataAnnotations;

namespace MetroEventsApi.Models
{
    public class UserEvent
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public Status Status { get; set; } = Status.Pending;
    }

    public enum Status
    {
        Pending,
        Declined,
        Accepted,
    }

}
