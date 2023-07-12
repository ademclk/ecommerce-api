using System.Globalization;

namespace ECommerceAPI.Application.Abstractions.Storage;

public interface IStorageService : IStorage
{
    public string Storage { get; }
}


