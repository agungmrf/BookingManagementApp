using System.ComponentModel.DataAnnotations;

namespace API.Utilities.Enums;

public enum StatusLevel
{
    Requested,
    Approved,
    Rejected,
    Canceled,
    Completed,
    [Display(Name = "On Going")] OnGoing
}