namespace AvaloniaModernApp.Core.Entities;

public class Contact
{
    public string Name { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string Initials => !string.IsNullOrEmpty(Name) ? Name.Substring(0, 1).ToUpper() : "?";
}
