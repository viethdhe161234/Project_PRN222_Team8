using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Inplements
{
    public class CartService : ICartService
    {
        private readonly IBookEditionRepository _editionRepository;
        private readonly IBorrowPolicyRepository _policyRepository;
        public CartService(IBookEditionRepository editionRepository,
            IBorrowPolicyRepository policyRepository)
        {
            _editionRepository = editionRepository;
            _policyRepository = policyRepository;
        }
        public void AddToCart(int editionId, List<int> currentCart)
        {
            var policy = _policyRepository.Get();

            // Check trùng EditionId
            if (currentCart.Contains(editionId))
                throw new Exception("This edition is already in your cart");

            // Check số lượng tối đa
            if (currentCart.Count >= policy.MaxBooksPerRequest)
                throw new Exception($"Maximum {policy.MaxBooksPerRequest} books per request");

            // Check trùng Book
            var newBookId = _editionRepository.GetBookIdByEditionId(editionId);
            var cartBookIds = currentCart
                .Select(id => _editionRepository.GetBookIdByEditionId(id))
                .ToList();

            if (cartBookIds.Contains(newBookId))
                throw new Exception("You already have an edition of this book in your cart");

            currentCart.Add(editionId);
        }
       
        public void RemoveFromCart(int editionId, List<int> currentCart)
        {
            currentCart.Remove(editionId);
        }
    }
}
