namespace SalesDemo.DataAccess.Abstract
{
    public class IUnitOfWork
    {
        ICompanyRepository Company { get; }
        IProductRepository Product { get; }
        ISaleRepository Sale { get; }
        IUserRepository User { get; }
    }
}
