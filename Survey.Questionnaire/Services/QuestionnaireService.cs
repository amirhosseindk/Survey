using Microsoft.Extensions.Logging;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;
using Survey.Questionnaires.Types;
using System.Transactions;

namespace Survey.Questionnaires.Services
{
    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly IQuestionnaireRepository _questionnaireRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMultipleChoiceOptionRepository _multipleChoiceOptionRepository;
        private readonly ILogger<QuestionnaireService> _logger;

        public QuestionnaireService(
            IQuestionnaireRepository questionnaireRepository,
            IQuestionRepository questionRepository,
            IMultipleChoiceOptionRepository multipleChoiceOptionRepository,
            ILogger<QuestionnaireService> logger)
        {
            _questionnaireRepository = questionnaireRepository;
            _questionRepository = questionRepository;
            _multipleChoiceOptionRepository = multipleChoiceOptionRepository;
            _logger = logger;
        }

        public async Task<int> CreateQuestionnaireAsync(Questionnaire questionnaire, List<Question> questions, List<MultipleChoiceOption>? options = null)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var questionnaireId = await _questionnaireRepository.CreateAsync(questionnaire);

                    foreach (var question in questions)
                    {
                        question.QuestionnaireId = questionnaireId;
                        await _questionRepository.CreateAsync(question);
                        if (question.Type == QuestionType.MultipleChoice && options != null)
                        {
                            foreach (var option in options)
                            {
                                await _multipleChoiceOptionRepository.CreateAsync(option);
                            }
                        }
                    }

                    transaction.Complete();
                    return questionnaireId;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while creating questionnaire - {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<(Questionnaire questionnaire, List<Question> questions, List<MultipleChoiceOption>? options)> GetQuestionnaireByIdAsync(int id)
        {
            try
            {
                var questionnaire = await _questionnaireRepository.GetByIdAsync(id);
                var questions = new List<Question>();
                var options = new List<MultipleChoiceOption>();

                if (questionnaire != null)
                {
                    var questionsRep = await _questionRepository.GetByQuestionnaireIdAsync(questionnaire.Id);
                    foreach (var question in questionsRep)
                    {
                        if (question.Type == QuestionType.MultipleChoice)
                        {
                            options.AddRange(await _multipleChoiceOptionRepository.GetByQuestionIdAsync(question.Id));
                        }
                    }
                    questions = questionsRep.ToList();
                }

                return (questionnaire, questions, options);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while fetching questionnaire by ID - {ex.Message}");
                throw;
            }
        }

        public async Task UpdateQuestionnaireAsync(Questionnaire questionnaire, List<Question> questions, List<MultipleChoiceOption>? options = null)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _questionnaireRepository.UpdateAsync(questionnaire);

                    foreach (var question in questions)
                    {
                        if (question.Id > 0)
                        {
                            await _questionRepository.UpdateAsync(question);
                            if (question.Type == QuestionType.MultipleChoice && options != null)
                            {
                                foreach (var option in options)
                                {
                                    await _multipleChoiceOptionRepository.UpdateAsync(option);
                                }
                            }
                        }
                        else
                        {
                            question.QuestionnaireId = questionnaire.Id;
                            await _questionRepository.CreateAsync(question);
                            if (question.Type == QuestionType.MultipleChoice && options != null)
                            {
                                foreach (var option in options)
                                {
                                    await _multipleChoiceOptionRepository.CreateAsync(option);
                                }
                            }
                        }
                    }

                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while updating questionnaire - {ex.Message}");
                    throw;
                }
            }
        }

        public async Task DeleteQuestionnaireAsync(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var questions = await _questionRepository.GetByQuestionnaireIdAsync(id);
                    foreach (var question in questions)
                    {
                        await _questionRepository.DeleteAsync(question.Id);
                        if (question.Type == QuestionType.MultipleChoice)
                        {
                            await _multipleChoiceOptionRepository.DeleteAsync(question.Id);
                        }
                    }

                    await _questionnaireRepository.DeleteAsync(id);

                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while deleting questionnaire - {ex.Message}");
                    throw;
                }
            }
        }
    }
}