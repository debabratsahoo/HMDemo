namespace HM.Product.API
{
    public interface IHttpServiceProvider
    {
        Task<T?> InvokeService<T>(string url);
    }
}