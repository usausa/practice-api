namespace Example.Mock;

using Smart.Data.Accessor;

public sealed class WrapAccessorResolver<T> : IAccessorResolver<T>
{
    public T Accessor { get; }

    public WrapAccessorResolver(T accessor)
    {
        Accessor = accessor;
    }
}
