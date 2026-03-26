using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Interfaces
{
    public interface IBookEditionRepository
    {      
        BookEdition? GetById(int id);
        void Add(BookEdition edition);
        void Update(BookEdition edition);
        void Delete(int id);
        int GetBookIdByEditionId(int editionId);
    }
}
