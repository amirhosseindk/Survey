using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IMultipleChoiceQuestionAnswerRepository _multipleChoiceRepo;
        private readonly ITextQuestionAnswerRepository _textRepo;
        private readonly IRangeQuestionAnswerRepository _rangeRepo;
        private readonly IDegreeQuestionAnswerRepository _degreeRepo;

        public AnswerService(
            IMultipleChoiceQuestionAnswerRepository multipleChoiceRepo,
            ITextQuestionAnswerRepository textRepo,
            IRangeQuestionAnswerRepository rangeRepo,
            IDegreeQuestionAnswerRepository degreeRepo)
        {
            _multipleChoiceRepo = multipleChoiceRepo;
            _textRepo = textRepo;
            _rangeRepo = rangeRepo;
            _degreeRepo = degreeRepo;
        }

        public async Task<bool> CreateMultipleChoiceAnswerAsync(MultipleChoiceQuestionAnswer answer)
        {
            var repoModel = new MultipleChoiceQuestionAnswerRepoModel
            {
                QuestionnaireId = answer.QuestionnaireId,
                QuestionId = answer.QuestionId,
                AnswerOptionId = answer.AnswerOptionId,
                StudentId = answer.StudentId,
                FillDateTime = answer.FillDateTime
            };
            return await _multipleChoiceRepo.CreateAsync(repoModel) > 0;
        }

        public async Task<bool> CreateTextAnswerAsync(TextQuestionAnswer answer)
        {
            var repoModel = new TextQuestionAnswerRepoModel
            {
                QuestionnaireId = answer.QuestionnaireId,
                QuestionId = answer.QuestionId,
                AnswerText = answer.AnswerText,
                StudentId = answer.StudentId,
                FillDateTime = answer.FillDateTime
            };
            return await _textRepo.CreateAsync(repoModel) > 0;
        }

        public async Task<bool> CreateRangeAnswerAsync(RangeQuestionAnswer answer)
        {
            var repoModel = new RangeQuestionAnswerRepoModel
            {
                QuestionnaireId = answer.QuestionnaireId,
                QuestionId = answer.QuestionId,
                AnswerValue = answer.AnswerValue,
                StudentId = answer.StudentId,
                FillDateTime = answer.FillDateTime
            };
            return await _rangeRepo.CreateAsync(repoModel) > 0;
        }

        public async Task<bool> CreateDegreeAnswerAsync(DegreeQuestionAnswer answer)
        {
            var repoModel = new DegreeQuestionAnswerRepoModel
            {
                QuestionnaireId = answer.QuestionnaireId,
                QuestionId = answer.QuestionId,
                AnswerValue = answer.AnswerValue,
                StudentId = answer.StudentId,
                FillDateTime = answer.FillDateTime
            };
            return await _degreeRepo.CreateAsync(repoModel) > 0;
        }

        public async Task<IEnumerable<MultipleChoiceQuestionAnswer>> GetMultipleChoiceAnswersByQuestionIdAsync(int questionId)
        {
            var repoModels = await _multipleChoiceRepo.GetByQuestionIdAsync(questionId);
            return repoModels.Select(repo => new MultipleChoiceQuestionAnswer
            {
                Id = repo.Id,
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerOptionId = repo.AnswerOptionId,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task<IEnumerable<TextQuestionAnswer>> GetTextAnswersByQuestionIdAsync(int questionId)
        {
            var repoModels = await _textRepo.GetByQuestionIdAsync(questionId);
            return repoModels.Select(repo => new TextQuestionAnswer
            {
                Id = repo.Id,
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerText = repo.AnswerText,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task<IEnumerable<RangeQuestionAnswer>> GetRangeAnswersByQuestionIdAsync(int questionId)
        {
            var repoModels = await _rangeRepo.GetByQuestionIdAsync(questionId);
            return repoModels.Select(repo => new RangeQuestionAnswer
            {
                Id = repo.Id,
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerValue = repo.AnswerValue,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task<IEnumerable<DegreeQuestionAnswer>> GetDegreeAnswersByQuestionIdAsync(int questionId)
        {
            var repoModels = await _degreeRepo.GetByQuestionIdAsync(questionId);
            return repoModels.Select(repo => new DegreeQuestionAnswer
            {
                Id = repo.Id,
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerValue = repo.AnswerValue,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task UpdateMultipleChoiceAnswerAsync(MultipleChoiceQuestionAnswer answer)
        {
            var repoModel = new MultipleChoiceQuestionAnswerRepoModel
            {
                Id = answer.Id,
                QuestionnaireId = answer.QuestionnaireId,
                QuestionId = answer.QuestionId,
                AnswerOptionId = answer.AnswerOptionId,
                StudentId = answer.StudentId,
                FillDateTime = answer.FillDateTime
            };
            await _multipleChoiceRepo.UpdateAsync(repoModel);
        }

        public async Task UpdateTextAnswerAsync(TextQuestionAnswer answer)
        {
            var repoModel = new TextQuestionAnswerRepoModel
            {
                Id = answer.Id,
                QuestionnaireId = answer.QuestionnaireId,
                QuestionId = answer.QuestionId,
                AnswerText = answer.AnswerText,
                StudentId = answer.StudentId,
                FillDateTime = answer.FillDateTime
            };
            await _textRepo.UpdateAsync(repoModel);
        }

        public async Task UpdateRangeAnswerAsync(RangeQuestionAnswer answer)
        {
            var repoModel = new RangeQuestionAnswerRepoModel
            {
                Id = answer.Id,
                QuestionnaireId = answer.QuestionnaireId,
                QuestionId = answer.QuestionId,
                AnswerValue = answer.AnswerValue,
                StudentId = answer.StudentId,
                FillDateTime = answer.FillDateTime
            };
            await _rangeRepo.UpdateAsync(repoModel);
        }

        public async Task UpdateDegreeAnswerAsync(DegreeQuestionAnswer answer)
        {
            var repoModel = new DegreeQuestionAnswerRepoModel
            {
                Id = answer.Id,
                QuestionnaireId = answer.QuestionnaireId,
                QuestionId = answer.QuestionId,
                AnswerValue = answer.AnswerValue,
                StudentId = answer.StudentId,
                FillDateTime = answer.FillDateTime
            };
            await _degreeRepo.UpdateAsync(repoModel);
        }

        public async Task DeleteMultipleChoiceAnswerAsync(int id)
        {
            await _multipleChoiceRepo.DeleteAsync(id);
        }

        public async Task DeleteTextAnswerAsync(int id)
        {
            await _textRepo.DeleteAsync(id);
        }

        public async Task DeleteRangeAnswerAsync(int id)
        {
            await _rangeRepo.DeleteAsync(id);
        }

        public async Task DeleteDegreeAnswerAsync(int id)
        {
            await _degreeRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<DegreeQuestionAnswer>> GetDegreeAnswersByQuestionnaireIdAsync(int questionnaireId)
        {
            var repoModels = await _degreeRepo.GetByQuestionnaireIdAsync(questionnaireId);
            return repoModels.Select(repo => new DegreeQuestionAnswer
            {
                Id = repo.Id,
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerValue = repo.AnswerValue,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task<IEnumerable<MultipleChoiceQuestionAnswer>> GetMultipleChoiceAnswersByQuestionnaireIdAsync(int questionnaireId)
        {
            var repoModels = await _multipleChoiceRepo.GetByQuestionnaireIdAsync(questionnaireId);
            return repoModels.Select(repo => new MultipleChoiceQuestionAnswer
            {
                Id = repo.Id,
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerOptionId = repo.AnswerOptionId,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task<IEnumerable<RangeQuestionAnswer>> GetRangeAnswersByQuestionnaireIdAsync(int questionnaireId)
        {
            var repoModels = await _rangeRepo.GetByQuestionnaireIdAsync(questionnaireId);
            return repoModels.Select(repo => new RangeQuestionAnswer
            {
                Id = repo.Id,
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerValue = repo.AnswerValue,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task<IEnumerable<TextQuestionAnswer>> GetTextAnswersByQuestionnaireIdAsync(int questionnaireId)
        {
            var repoModels = await _textRepo.GetByQuestionnaireIdAsync(questionnaireId);
            return repoModels.Select(repo => new TextQuestionAnswer
            {
                Id = repo.Id,
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerText = repo.AnswerText,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }
    }
}