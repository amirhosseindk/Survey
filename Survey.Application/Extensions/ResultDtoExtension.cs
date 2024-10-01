using Survey.Application.Dtos.Result;

namespace Survey.Application.Extensions
{
    public static class ResultDtoExtension
    {
        public static ResultDto<T> ToResultDto<T>(this T source)
        {
            return new ResultDto<T>
            {
                Succeeded = true,
                Result = source,
                Error = null
            };
        }
    }
}