using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetroEventsApi.Models;

public partial class Event
{
    public int EventId { get; set; }
    [Required]
    public string EventName { get; set; }
    [Required]
    public string EventDescription { get; set; }
    [Required]
    public string Organizer { get; set; }
    [Required]
    public string Date { get; set; }
    [Required]
    public string Location { get; set; } = string.Empty;
    public int Likes { get; set; } = 0;
    public Approval Approval { get; set; } = Approval.Pending;
}

public enum Approval
{
    Pending,
    Rejected,
    Approved
}