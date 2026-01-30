namespace PanKunik.Results.Rop;

public static class ResultOfTAsyncExtensions
{
    extension<TIn>(Result<TIn> result)
        where TIn : notnull
    {
        public async Task<Result<TOut>> BindAsync<TOut>(Func<TIn, Task<Result<TOut>>> func)
            where TOut : notnull
        {
            if (result.IsFailure) return Result<TOut>.Failure(result.Error!);
            return await func(result.Value).ConfigureAwait(false);
        }

        public async Task<Result<TIn>> TapAsync(Func<TIn, Task> action)
        {
            if (result.IsSuccess) await action(result.Value).ConfigureAwait(false);
            return result;
        }

        public async Task<Result<TIn>> TapAsync(Func<Task> action)
        {
            if (result.IsSuccess) await action().ConfigureAwait(false);
            return result;
        }

        public async Task<Result<TOut>> MapAsync<TOut>(Func<TIn, Task<TOut>> func)
            where TOut : notnull
        {
            if (result.IsFailure) return Result<TOut>.Failure(result.Error!);
            return Result<TOut>.Success(await func(result.Value).ConfigureAwait(false));
        }

        // TODO: Tests
        public async Task<TOut> MatchAsync<TOut>(Func<TIn, Task<TOut>> onSuccess, Func<Error, Task<TOut>> onFailure)
        {
            return result.IsSuccess
                ? await onSuccess(result.Value).ConfigureAwait(false)
                : await onFailure(result.Error!).ConfigureAwait(false);
        }
    }
}