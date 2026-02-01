using AvaloniaModernApp.Core.Entities;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AvaloniaModernApp.Core.Interfaces;

public interface IContactsStore
{
    ObservableCollection<Contact> Contacts { get; }
    Task InitializeAsync();
    Task AddAsync(Contact contact);
}
