using System.Buffers;

namespace CollectionsExtensions;

public class PooledArray<T> : IDisposable
{
    public static PooledArray<T> RentFrom(int minimumLength)
    {
        var data = ArrayPool<T>.Shared.Rent(minimumLength);
        
        return new PooledArray<T>(data);
    }
    
    public T[] Data { get; }

    private PooledArray(T[] data)
    {  
        Data = data;
    }

    private void Return()
    {
        ArrayPool<T>.Shared.Return(Data);
    }

    ~PooledArray()
    {
        Return();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Return();
    }
}