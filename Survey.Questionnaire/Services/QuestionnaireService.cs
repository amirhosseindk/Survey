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

        public async Task<int> CreateQuestionnaireAsync(Questionnaire questionnaire)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var questionnaireRepoModel = new QuestionnaireRepoModel
                    {
                        Title = questionnaire.Title,
                        ClassId = questionnaire.ClassId,
                        ProfessorId = questionnaire.ProfessorId
                    };

                    var questionnaireId = await _questionnaireRepository.CreateAsync(questionnaireRepoModel);

                    foreach (var question in questionnaire.Questions)
                    {
                        var questionRepoModel = new QuestionRepoModel
                        {
                            Title = question.Title,
                            Type = question.Type,
                            Rank = question.Rank,
                            QuestionnaireId = questionnaireId
                        };

                        var questionId = await _questionRepository.CreateAsync(questionRepoModel);

                        if (question.Type == QuestionType.MultipleChoice && question.Options != null)
                        {
                            foreach (var option in question.Options)
                            {
                                var optionRepoModel = new MultipleChoiceOptionRepoModel
                                {
                                    OptionText = option.OptionText,
                                    MultipleChoiceQuestionId = questionId
                                };

                                await _multipleChoiceOptionRepository.CreateAsync(optionRepoModel);
                            }
                        }
                    }

                    transaction.Complete();
                    return questionnaireId;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while creating questionnaire {questionnaire.Title} - {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<(QuestionnaireRepoModel questionnaire, List<QuestionRepoModel> questions, List<MultipleChoiceOptionRepoModel>? options)> GetQuestionnaireByIdAsync(int id)
        {
            try
            {
                var questionnaire = await _questionnaireRepository.GetByIdAsync(id);
                var questions = new List<QuestionRepoModel>();
                var options = new List<MultipleChoiceOptionRepoModel>();

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

        public async Task<bool> UpdateQuestionnaireAsync(Questionnaire questionnaire)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var questionnaireRepoModel = new QuestionnaireRepoModel
                    {
                        Id = questionnaire.Id,
                        Title = questionnaire.Title,
                        ClassId = questionnaire.ClassId,
                        ProfessorId = questionnaire.ProfessorId
                    };

                    var questionnaireUpdateResult = await _questionnaireRepository.UpdateAsync(questionnaireRepoModel);
                    if(!questionnaireUpdateResult)
                        throw new Exception("Error while updating questionnaire");

                    foreach (var question in questionnaire.Questions)
                    {
                        var questionRepoModel = new QuestionRepoModel
                        {
                            Title = question.Title,
                            Type = question.Type,
                            Rank = question.Rank,
                            QuestionnaireId = question.QuestionnaireId
                        };

                        var questionsUpdateResult = await _questionRepository.UpdateAsync(questionRepoModel);
                        if (!questionsUpdateResult)
                            throw new Exception("Error while updating questions");

                        if (question.Type == QuestionType.MultipleChoice && question.Options != null)
                        {
                            foreach (var option in question.Options)
                            {
                                var optionRepoModel = new MultipleChoiceOptionRepoModel
                                {
                                    OptionText = option.OptionText,
                                    MultipleChoiceQuestionId = option.QuestionId
                                };

                                var optionsUpdateResult = await _multipleChoiceOptionRepository.UpdateAsync(optionRepoModel);
                                if (!optionsUpdateResult)
                                    throw new Exception("Error while updating options");
                            }
                        }
                    }

                    transaction.Complete();
                    return questionnaireUpdateResult;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while updating questionnaire {questionnaire.Title} - {ex.Message}");
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