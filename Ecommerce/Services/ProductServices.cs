using MongoDB.Driver;
using Ecommerce.Models;
using Microsoft.Extensions.Options;

namespace Ecommerce.Services
{
  public class ProductServices
  {
    private readonly IMongoCollection<Product> _productCollection;

    public ProductServices(IOptions<ProductDatabaseSettings> productServices)
    {
      var mongoClient = new MongoClient(productServices.Value.ConnectionString);
      var mongoDatabase = mongoClient.GetDatabase(productServices.Value.DatabaseName);

      _productCollection = mongoDatabase.GetCollection<Product>(productServices.Value.ProductCollectionName);
    }

    public async Task<List<Product>> GetAsync() => await _productCollection.Find(x => true).ToListAsync();
    public async Task<Product> GetAsync(string id) => await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Product product) => await _productCollection.InsertOneAsync(product);

    public async Task UpdateAsync(string id, Product updateProduct) => await _productCollection.ReplaceOneAsync(x => x.Id == id, updateProduct);

    public async Task RemoveAsync(string id) => await _productCollection.DeleteOneAsync(x => x.Id == id);
  }
}