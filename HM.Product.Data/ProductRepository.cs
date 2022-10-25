using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HM.Product.Data
{
    public class ProductRepository<T> : IProductRepository<T> where T : class
    {
        private readonly HMProductContext context;
        private readonly DbSet<T> dbset;
        public ProductRepository(HMProductContext productContext)
        {
            context = productContext;
            dbset = productContext.Set<T>();
            //var tempData = new List<ProductDB> {
            //new ProductDB{ Id = "TEST1", ProductName = "TEST", ChannelId = 1, SizeScaleId = "SIZETEST1", ProductCode = "2022001", ProductYear = 2022, Articles = new List<ArticleDB> { new ArticleDB { Id = "TEST1", ArticleId = "ARTICLETEST1", ColorId = "COLORTEST123" }, new ArticleDB { Id = "TEST1", ArticleId = "ARTICLETEST2", ColorId = "COLORTEST1" }, new ArticleDB { Id = "TEST1", ArticleId = "ARTICLETEST3", ColorId = "COLORTEST3" } } }
            //};
            //context.Product.AddRange(tempData);
            //context.SaveChanges();
        }
        public async Task<bool> Save(T entity)
        {
            try
            {
                await dbset.AddAsync(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.Out.Write(ex);
                return false;
            }
        }
        public async Task<T> Get(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? orderByDesc = null)
        {
            if(orderByDesc == null)
            {
               return await dbset.Where(predicate).AsNoTracking().FirstOrDefaultAsync();
            }
            else
            {
                return await dbset.Where(predicate).OrderByDescending(orderByDesc).AsNoTracking().FirstOrDefaultAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? orderByDesc = null)
        {
            if (orderByDesc == null)
            {
                return await dbset.Where(predicate).AsNoTracking().ToListAsync();
            }
            else
            {
                return await dbset.Where(predicate).OrderByDescending(orderByDesc).AsNoTracking().ToListAsync();
            }
        }
    }
}
