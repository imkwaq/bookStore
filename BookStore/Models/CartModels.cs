using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class CartModels
    {
        private List<CartLineModels> lineCollection = new List<CartLineModels>();

        public void AddItem(BookModels book, int quantity)
        {
            CartLineModels cartLine = lineCollection
                .Where(b => b.Book.Id == book.Id)
                .FirstOrDefault();

            if (cartLine == null)
            {
                lineCollection.Add(new CartLineModels
                {
                    Book = book,
                    Quantity = quantity
                });
            }
            else
            {
                cartLine.Quantity += quantity;
            }
        }

        public void RemoveLine(BookModels book)
        {
            lineCollection.RemoveAll(b => b.Book.Id == book.Id);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(t => t.Book.Price * t.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLineModels> Lines => lineCollection;
    }
}