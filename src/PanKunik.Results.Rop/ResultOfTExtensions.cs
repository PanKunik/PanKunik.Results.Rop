namespace PanKunik.Results.Rop;

public static class ResultOfTExtensions
{
    extension<TIn>(Result<TIn> result)
        where TIn : notnull
    {
        public Result<TIn> Tap(Action<TIn> action)
        {
            if (result.IsSuccess)
                action(result.Value);

            return result;
        }

        public Result<TOut> Bind<TOut>(Func<TIn, Result<TOut>> func)
            where TOut : notnull
        {
            if (result.IsFailure) return Result<TOut>.Failure(result.Error!);
            return func(result.Value);
        }

        public Result<TIn> BindIf(bool condition, Func<TIn, Result<TIn>> func)
        {
            if (result.IsFailure)
                return result;

            if (!condition)
                return result;

            return func(result.Value);
        }

        public Result<TOut> Map<TOut>(Func<TIn, TOut> func)
            where TOut : notnull
        {
            return result.IsSuccess
                ? Result<TOut>.Success(func(result.Value))
                : Result<TOut>.Failure(result.Error!);
        }
    }
}