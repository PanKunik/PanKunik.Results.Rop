namespace PanKunik.Results.Rop;

public static class ResultOfTExtensions
{
    extension<T>(Result<T> result)
    {
        public Result<T> Tap(Action<T> action)
        {
            if (result.IsSuccess)
                action(result.Value!);

            return result;
        }

        public Result<K> Bind<K>(Func<T, Result<K>> func)
        {
            if (result.IsFailure) return Result<K>.Failure(result.Error!);
            return func(result.Value!);
        }

        public Result<T> BindIf(bool condition, Func<T, Result<T>> func)
        {
            if (result.IsFailure)
                return result;

            if (!condition)
                return result;

            return func(result.Value!);
        }

        public Result<K> Map<K>(Func<T, K> func)
        {
            return result.IsSuccess
                ? Result<K>.Success(func(result.Value!))
                : Result<K>.Failure(result.Error!);
        }
    }
}