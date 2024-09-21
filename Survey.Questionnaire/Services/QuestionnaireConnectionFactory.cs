using Survey.Common.Dapper.Services;
using Survey.Questionnaires.Contracts;

namespace Survey.Questionnaires.Services
{
    public class QuestionnaireConnectionFactory : BaseDapperConnectionFactory, IQuestionnaireConnectionFactory
    {
        public QuestionnaireConnectionFactory(string connectionString) : base(connectionString)
        {
            
        }
    }
}