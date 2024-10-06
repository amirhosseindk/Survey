using Survey.Common.Dapper.Contracts;

namespace Survey.Questionnaires.Contracts
{
    public interface IQuestionnaireReadConnectionFactory : IDapperConnectionFactory
    {
    }

    public interface IQuestionnaireWriteConnectionFactory : IDapperConnectionFactory
    {
    }
}