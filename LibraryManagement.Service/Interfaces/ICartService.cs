using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Interfaces
{
    public interface ICartService
    {
        void AddToCart(int editionId, List<int> currentCart);
        void RemoveFromCart(int editionId, List<int> currentCart);
    }
}
