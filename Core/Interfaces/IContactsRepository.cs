using AvaloniaModernApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaloniaModernApp.Core.Interfaces;

public interface IContactsRepository
{
    Task<IReadOnlyList<Contact>> GetAllAsync();
    Task AddAsync(Contact contact);
    Task SaveAsync();
}
