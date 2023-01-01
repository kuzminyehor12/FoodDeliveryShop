namespace FoodDeliveryShop.MVC.Models
{
    public class Cart
    {
        private readonly List<CartLine> _lineCollection;
        public virtual IEnumerable<CartLine> Lines
        {
            get
            {
                return _lineCollection;
            }
        }
        public Cart()
        {
            _lineCollection = new List<CartLine>();
        }
        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = Lines.FirstOrDefault(p => p.Product.ProductID == product.ProductID);

            if (line == null)
            {
                _lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product)
        {
            _lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        public virtual decimal ComputeTotalValue()
        {
            return Lines.Sum(e => e.Product.Price * e.Quantity);
        }

        public virtual void Clear()
        {
            _lineCollection.Clear();
        }
    }
}
