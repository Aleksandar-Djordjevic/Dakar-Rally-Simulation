namespace DakarRallySimulation
{
    public class OperationResult<TResult, TError>
    {
        public bool IsSuccess { get; }
        public TResult Result { get; }
        public TError Error { get; }

        private OperationResult(TResult result, TError error, bool isSuccess)
        {
            Result = result;
            Error = error;
            IsSuccess = isSuccess;
        }

        public static OperationResult<TResult, TError> Done(TResult result) =>
            new OperationResult<TResult, TError>(result, default(TError), true);

        public static OperationResult<TResult, TError> Failed(TError error) =>
            new OperationResult<TResult, TError>(default(TResult), error, false);
    }
}