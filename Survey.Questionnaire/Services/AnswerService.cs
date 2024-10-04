using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;
using Microsoft.Extensions.Logging;
using System.Transactions;

namespace Survey.Questionnaires.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IDegreeQuestionAnswerRepository _degreeAnswerRepository;
        private readonly IMultipleChoiceQuestionAnswerRepository _multipleChoiceAnswerRepository;
        private readonly IRangeQuestionAnswerRepository _rangeAnswerRepository;
        private readonly ITextQuestionAnswerRepository _textAnswerRepository;
        private readonly ILogger<AnswerService> _logger;

        public AnswerService(
            IDegreeQuestionAnswerRepository degreeAnswerRepository,
            IMultipleChoiceQuestionAnswerRepository multipleChoiceAnswerRepository,
            IRangeQuestionAnswerRepository rangeAnswerRepository,
            ITextQuestionAnswerRepository textAnswerRepository,
            ILogger<AnswerService> logger)
        {
            _degreeAnswerRepository = degreeAnswerRepository;
            _multipleChoiceAnswerRepository = multipleChoiceAnswerRepository;
            _rangeAnswerRepository = rangeAnswerRepository;
            _textAnswerRepository = textAnswerRepository;
            _logger = logger;
        }

        public async Task SaveAnswersAsync(
            List<DegreeQuestionAnswerRepoModel>? degreeAnswers = null,
            List<MultipleChoiceQuestionAnswerRepoModel>? multipleChoiceAnswers = null,
            List<RangeQuestionAnswerRepoModel>? rangeAnswers = null,
            List<TextQuestionAnswerRepoModel>? textAnswers = null)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (var answer in degreeAnswers)
                    {
                        if (answer != null)
                            await _degreeAnswerRepository.CreateAsync(answer);
                    }

                    foreach (var answer in multipleChoiceAnswers)
                    {
                        if (answer != null)
                            await _multipleChoiceAnswerRepository.CreateAsync(answer);
                    }

                    foreach (var answer in rangeAnswers)
                    {
                        if (answer != null)
                            await _rangeAnswerRepository.CreateAsync(answer);
                    }

                    foreach (var answer in textAnswers)
                    {
                        if (answer != null)
                            await _textAnswerRepository.CreateAsync(answer);
                    }

                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error while saving answers - {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<IEnumerable<DegreeQuestionAnswerRepoModel>> GetDegreeAnswersByQuestionnaireIdAsync(int questionnaireId)
        {
            return await _degreeAnswerRepository.GetByQuestionnaireIdAsync(questionnaireId);
        }

        public async Task<IEnumerable<MultipleChoiceQuestionAnswerRepoModel>> GetMultipleChoiceAnswersByQuestionnaireIdAsync(int questionnaireId)
        {
            return await _multipleChoiceAnswerRepository.GetByQuestionnaireIdAsync(questionnaireId);
        }

        public async Task<IEnumerable<RangeQuestionAnswerRepoModel>> GetRangeAnswersByQuestionnaireIdAsync(int questionnaireId)
        {
            return await _rangeAnswerRepository.GetByQuestionnaireIdAsync(questionnaireId);
        }

        public async Task<IEnumerable<TextQuestionAnswerRepoModel>> GetTextAnswersByQuestionnaireIdAsync(int questionnaireId)
        {
            return await _textAnswerRepository.GetByQuestionnaireIdAsync(questionnaireId);
        }
    }
}