namespace PanKunik.Results.Rop;

public static class TaskResultOfTExceptions
{
    extension<T>(Task<Result<T>> taskResult)
    {
        public async Task<Result<K>> BindAsync<K>(Func<T, Task<Result<K>>> func)
        {
            var result = await taskResult.ConfigureAwait(false);
            if (result.IsFailure) return Result<K>.Failure(result.Error!);
            return await func(result.Value!);
        }
        
        public async Task<Result<T>> TapAsync(Func<T, Task> action)
        {
            var result = await taskResult.ConfigureAwait(false);
            if (result.IsSuccess) await action(result.Value!).ConfigureAwait(false);
            return result;
        }
        
        public async Task<Result<T>> TapAsync(Func<Task> action)
        {
            var result = await taskResult.ConfigureAwait(false);
            if (result.IsSuccess) await action().ConfigureAwait(false);
            return result;
        }

        public async Task<Result<K>> MapAsync<K>(Func<T, Task<K>> func)
        {
            var result = await taskResult.ConfigureAwait(false);
            if (result.IsFailure) return Result<K>.Failure(result.Error!);
            return Result<K>.Success(await func(result.Value!).ConfigureAwait(false));
        }
    }
}