namespace Survey.Common.Exception
{
    public interface IBusinessException
    {
        int GetCode();
        string Message { get; }
        bool ReturnDetail();
    }

    public class BusinessException : System.Exception, IBusinessException
    {
        private readonly int _code;

        public BusinessException(string message, int code, bool returnDetail = true) : base(message)
        {
            _code = code;
            ReturnDetail = returnDetail;
        }

        public int GetCode() => _code;

        bool IBusinessException.ReturnDetail()
        {
            return ReturnDetail;
        }

        public bool ReturnDetail { get; }
    }
}