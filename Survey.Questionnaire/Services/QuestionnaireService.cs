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
            var questionnaireName = await _questionnaireRepository.GetByTitleAsync(questionnaire.Title.ToLower());
            if (questionnaireName != null)
            {
                throw new Exception($"Questionnaire with this name {questionnaire.Title} is already available");
            }

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var questionnaireRepoModel = new QuestionnaireRepoModel
                    {
                        Title = questionnaire.Title.ToLower(),
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
                                    QuestionId = questionId
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

        public async Task<Questionnaire> GetQuestionnaireByIdAsync(int id)
        {
            try
            {
                var questionnaireRepoModel = await _questionnaireRepository.GetByIdAsync(id);
                if (questionnaireRepoModel == null)
                {
                    throw new Exception($"Questionnaire with ID {id} not found");
                }

                var questionRepoModels = await _questionRepository.GetByQuestionnaireIdAsync(questionnaireRepoModel.Id);

                var questions = new List<Question>();

                foreach (var questionRepoModel in questionRepoModels)
                {
                    var question = new Question
                    {
                        Id = questionRepoModel.Id,
                        Title = questionRepoModel.Title,
                        Type = questionRepoModel.Type,
                        Rank = questionRepoModel.Rank,
                        QuestionnaireId = questionRepoModel.QuestionnaireId
                    };

                    if (question.Type == QuestionType.MultipleChoice)
                    {
                        var optionsRepoModels = await _multipleChoiceOptionRepository.GetByQuestionIdAsync(questionRepoModel.Id);
                        question.Options = optionsRepoModels.Select(optionRepoModel => new MultipleChoiseOptions
                        {
                            OptionText = optionRepoModel.OptionText,
                            QuestionId = optionRepoModel.QuestionId
                        }).ToList();
                    }

                    questions.Add(question);
                }

                var questionnaire = new Questionnaire
                {
                    Id = questionnaireRepoModel.Id,
                    Title = questionnaireRepoModel.Title,
                    ClassId = questionnaireRepoModel.ClassId,
                    ProfessorId = questionnaireRepoModel.ProfessorId,
                    Questions = questions
                };

                return questionnaire;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while fetching questionnaire by ID - {ex.Message}");
                throw;
            }
        }

        public async Task<Questionnaire> GetQuestionnaireByTitleAsync(string title)
        {
            try
            {
                var questionnaireRepoModel = await _questionnaireRepository.GetByTitleAsync(title.ToLower());
                if (questionnaireRepoModel == null)
                {
                    throw new Exception($"Questionnaire with name {title} not found");
                }

                var questionRepoModels = await _questionRepository.GetByQuestionnaireIdAsync(questionnaireRepoModel.Id);

                var questions = new List<Question>();

                foreach (var questionRepoModel in questionRepoModels)
                {
                    var question = new Question
                    {
                        Id = questionRepoModel.Id,
                        Title = questionRepoModel.Title,
                        Type = questionRepoModel.Type,
                        Rank = questionRepoModel.Rank,
                        QuestionnaireId = questionRepoModel.QuestionnaireId
                    };

                    if (question.Type == QuestionType.MultipleChoice)
                    {
                        var optionsRepoModels = await _multipleChoiceOptionRepository.GetByQuestionIdAsync(questionRepoModel.Id);
                        question.Options = optionsRepoModels.Select(optionRepoModel => new MultipleChoiseOptions
                        {
                            OptionText = optionRepoModel.OptionText,
                            QuestionId = optionRepoModel.QuestionId
                        }).ToList();
                    }

                    questions.Add(question);
                }

                var questionnaire = new Questionnaire
                {
                    Id = questionnaireRepoModel.Id,
                    Title = questionnaireRepoModel.Title,
                    ClassId = questionnaireRepoModel.ClassId,
                    ProfessorId = questionnaireRepoModel.ProfessorId,
                    Questions = questions
                };

                return questionnaire;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while fetching questionnaire by ID - {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Questionnaire>> GetAllQuestionnairesAsync()
        {
            var questionnaires = await _questionnaireRepository.GetAllAsync();

            var result = new List<Questionnaire>();

            foreach (var questionnaire in questionnaires)
            {
                result.Add(new Questionnaire
                {
                    Id = questionnaire.Id,
                    Title = questionnaire.Title,
                    ProfessorId = questionnaire.ProfessorId,
                    ClassId = questionnaire.ClassId
                });
            }

            return result.AsEnumerable();
        }

        public async Task<IEnumerable<Questionnaire>> GetAllQuestionnairesByProfessorIdAsync(string id)
        {
            var questionnaires = await _questionnaireRepository.GetAllByProfessorIdAsync(id);

            var result = new List<Questionnaire>();

            foreach (var questionnaire in questionnaires)
            {
                result.Add(new Questionnaire
                {
                    Id = questionnaire.Id,
                    Title = questionnaire.Title,
                    ProfessorId = questionnaire.ProfessorId,
                    ClassId = questionnaire.ClassId
                });
            }

            return result.AsEnumerable();
        }

        public async Task<IEnumerable<Questionnaire>> GetAllQuestionnairesByClassIdAsync(int classId)
        {
            var questionnaires = await _questionnaireRepository.GetAllByClassIdAsync(classId);

            var result = questionnaires.Select(q => new Questionnaire
            {
                Id = q.Id,
                Title = q.Title,
                ProfessorId = q.ProfessorId,
                ClassId = q.ClassId
            }).ToList();

            return result.AsEnumerable();
        }

        public async Task<IEnumerable<Questionnaire>> GetAllQuestionnairesByStudentIdAsync(string studentId)
        {
            var questionnaires = await _questionnaireRepository.GetAllByStudentIdAsync(studentId);

            var result = questionnaires.Select(q => new Questionnaire
            {
                Id = q.Id,
                Title = q.Title,
                ProfessorId = q.ProfessorId,
                ClassId = q.ClassId
            }).ToList();

            return result;
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
                                    QuestionId = option.QuestionId
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

        public async Task<bool> DeleteQuestionnaireAsync(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var questions = await _questionRepository.GetByQuestionnaireIdAsync(id);
                    foreach (var question in questions)
                    {
                        var quesitonDeleteResult = await _questionRepository.DeleteAsync(question.Id);
                        if (!quesitonDeleteResult)
                            throw new Exception("Error while deleting question");
                        if (question.Type == QuestionType.MultipleChoice)
                        {
                            var multipleChoiseDeleteResult = await _multipleChoiceOptionRepository.DeleteAsync(question.Id);
                            if (!multipleChoiseDeleteResult)
                                throw new Exception("Error while deleting multipleChoiseOption");
                        }
                    }

                    var questionnaireDeleteResult = await _questionnaireRepository.DeleteAsync(id);
                    if (!questionnaireDeleteResult)
                        throw new Exception("Error while deleting questionnaire");

                    transaction.Complete();
                    return questionnaireDeleteResult;
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