using System;
using System.Collections.Generic;

namespace MetroEventsApi.Models;

public partial class User
{
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserRole Role { get; set; } = UserRole.Regular;
    public UserStatus Status { get; set; } = UserStatus.Offline;
}

public enum UserRole
{
    Regular,
    Organizer,
    Administrator
}

public enum UserStatus
{
    Online,
    Offline
}