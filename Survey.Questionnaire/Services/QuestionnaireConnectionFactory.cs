using Survey.Common.Dapper.Services;
using Survey.Questionnaires.Contracts;

namespace Survey.Questionnaires.Services
{
    public class QuestionnaireReadConnectionFactory : BaseDapperConnectionFactory, IQuestionnaireReadConnectionFactory
    {
        public QuestionnaireReadConnectionFactory(string connectionString) : base(connectionString)
        {
        }
    }

    public class QuestionnaireWriteConnectionFactory : BaseDapperConnectionFactory, IQuestionnaireWriteConnectionFactory
    {
        public QuestionnaireWriteConnectionFactory(string connectionString) : base(connectionString)
        {
        }
    }
}