using QuantityMeasurementApp.Controllers;
using QuantityMeasurementApp.Business.Services;
using QuantityMeasurementApp.Repositories.Implementations;
using QuantityMeasurementApp.Application;

class Program
{
    static void Main(string[] args)
    {
        var repository = QuantityMeasurementCacheRepository.GetInstance();

        var service = new QuantityMeasurementServiceImpl(repository);

        var menu = new Menu(service);
        menu.Start();
    }
}