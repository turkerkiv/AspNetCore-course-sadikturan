namespace FormsApp.Models
{
    public class Repository
    {
        private static readonly List<Product> _products = new();
        private static readonly List<Category> _categories = new();

        public static List<Product> Products => _products;
        public static List<Category> Categories => _categories;

        static Repository()
        {
            _categories.Add(new Category { Id = 1, Name = "Phone" });
            _categories.Add(new Category { Id = 2, Name = "Personal Computer" });
        }

        public static void AddProduct(Product product)
        {
            product.Id = _products.Count+1;
            _products.Add(product);
        }

        public static Product? GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public static void UpdateProduct(Product newProduct)
        {
            int index = _products.FindIndex(p => p.Id == newProduct.Id);
            _products[index] = newProduct;
        }

        public static void DeleteProduct(int id)
        {
            _products.RemoveAll(pr => pr.Id == id);
        }
    }
}