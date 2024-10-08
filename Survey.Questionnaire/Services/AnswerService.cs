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

        public async Task<IEnumerable<MultipleChoiceQuestionAnswer>> GetMultipleChoiceAnswersByQuestionIdAsync(int questionnnaireId, int questionId)
        {
            var repoModels = await _multipleChoiceRepo.GetAllAnswersOfQuestionIdAsync(questionnnaireId, questionId);
            return repoModels.Select(repo => new MultipleChoiceQuestionAnswer
            {
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerOptionId = repo.AnswerOptionId,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task<IEnumerable<TextQuestionAnswer>> GetTextAnswersByQuestionIdAsync(int questionnnaireId, int questionId)
        {
            var repoModels = await _textRepo.GetAllAnswersOfQuestionIdAsync(questionnnaireId, questionId);
            return repoModels.Select(repo => new TextQuestionAnswer
            {
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerText = repo.AnswerText,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task<IEnumerable<RangeQuestionAnswer>> GetRangeAnswersByQuestionIdAsync(int questionnnaireId, int questionId)
        {
            var repoModels = await _rangeRepo.GetAllAnswersOfQuestionIdAsync(questionnnaireId, questionId);
            return repoModels.Select(repo => new RangeQuestionAnswer
            {
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerValue = repo.AnswerValue,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task<IEnumerable<DegreeQuestionAnswer>> GetDegreeAnswersByQuestionIdAsync(int questionnnaireId, int questionId)
        {
            var repoModels = await _degreeRepo.GetAllAnswersOfQuestionIdAsync(questionnnaireId, questionId);
            return repoModels.Select(repo => new DegreeQuestionAnswer
            {
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerValue = repo.AnswerValue,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task<bool> UpdateMultipleChoiceAnswerAsync(MultipleChoiceQuestionAnswer answer)
        {
            var repoModel = new MultipleChoiceQuestionAnswerRepoModel
            {
                QuestionnaireId = answer.QuestionnaireId,
                QuestionId = answer.QuestionId,
                AnswerOptionId = answer.AnswerOptionId,
                StudentId = answer.StudentId,
                FillDateTime = answer.FillDateTime
            };
            return await _multipleChoiceRepo.UpdateAsync(repoModel);
        }

        public async Task<bool> UpdateTextAnswerAsync(TextQuestionAnswer answer)
        {
            var repoModel = new TextQuestionAnswerRepoModel
            {
                QuestionnaireId = answer.QuestionnaireId,
                QuestionId = answer.QuestionId,
                AnswerText = answer.AnswerText,
                StudentId = answer.StudentId,
                FillDateTime = answer.FillDateTime
            };
            return await _textRepo.UpdateAsync(repoModel);
        }

        public async Task<bool> UpdateRangeAnswerAsync(RangeQuestionAnswer answer)
        {
            var repoModel = new RangeQuestionAnswerRepoModel
            {
                QuestionnaireId = answer.QuestionnaireId,
                QuestionId = answer.QuestionId,
                AnswerValue = answer.AnswerValue,
                StudentId = answer.StudentId,
                FillDateTime = answer.FillDateTime
            };
            return await _rangeRepo.UpdateAsync(repoModel);
        }

        public async Task<bool> UpdateDegreeAnswerAsync(DegreeQuestionAnswer answer)
        {
            var repoModel = new DegreeQuestionAnswerRepoModel
            {
                QuestionnaireId = answer.QuestionnaireId,
                QuestionId = answer.QuestionId,
                AnswerValue = answer.AnswerValue,
                StudentId = answer.StudentId,
                FillDateTime = answer.FillDateTime
            };
            return await _degreeRepo.UpdateAsync(repoModel);
        }

        public async Task<bool> DeleteMultipleChoiceAnswerAsync(MultipleChoiceQuestionAnswer answer)
        {
            var repoModel = new MultipleChoiceQuestionAnswerRepoModel
            {
                QuestionnaireId = answer.QuestionnaireId,
                QuestionId = answer.QuestionId,
                AnswerOptionId = answer.AnswerOptionId,
                StudentId = answer.StudentId,
                FillDateTime = answer.FillDateTime
            };
            return await _multipleChoiceRepo.DeleteAsync(repoModel);
        }

        public async Task<bool> DeleteTextAnswerAsync(TextQuestionAnswer answer)
        {
            var repoModel = new TextQuestionAnswerRepoModel
            {
                QuestionnaireId = answer.QuestionnaireId,
                QuestionId = answer.QuestionId,
                AnswerText = answer.AnswerText,
                StudentId = answer.StudentId,
                FillDateTime = answer.FillDateTime
            };
            return await _textRepo.DeleteAsync(repoModel);
        }

        public async Task<bool> DeleteRangeAnswerAsync(RangeQuestionAnswer answer)
        {
            var repoModel = new RangeQuestionAnswerRepoModel
            {
                QuestionnaireId = answer.QuestionnaireId,
                QuestionId = answer.QuestionId,
                AnswerValue = answer.AnswerValue,
                StudentId = answer.StudentId,
                FillDateTime = answer.FillDateTime
            };
            return await _rangeRepo.DeleteAsync(repoModel);
        }

        public async Task<bool> DeleteDegreeAnswerAsync(DegreeQuestionAnswer answer)
        {
            var repoModel = new DegreeQuestionAnswerRepoModel
            {
                QuestionnaireId = answer.QuestionnaireId,
                QuestionId = answer.QuestionId,
                AnswerValue = answer.AnswerValue,
                StudentId = answer.StudentId,
                FillDateTime = answer.FillDateTime
            };
            return await _degreeRepo.DeleteAsync(repoModel);
        }

        public async Task<IEnumerable<DegreeQuestionAnswer>> GetDegreeAnswersByQuestionnaireIdAsync(int questionnaireId)
        {
            var repoModels = await _degreeRepo.GetAllOfQuestionnaireIdAsync(questionnaireId);
            return repoModels.Select(repo => new DegreeQuestionAnswer
            {
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerValue = repo.AnswerValue,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task<IEnumerable<MultipleChoiceQuestionAnswer>> GetMultipleChoiceAnswersByQuestionnaireIdAsync(int questionnaireId)
        {
            var repoModels = await _multipleChoiceRepo.GetAllOfQuestionnaireIdAsync(questionnaireId);
            return repoModels.Select(repo => new MultipleChoiceQuestionAnswer
            {
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerOptionId = repo.AnswerOptionId,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task<IEnumerable<RangeQuestionAnswer>> GetRangeAnswersByQuestionnaireIdAsync(int questionnaireId)
        {
            var repoModels = await _rangeRepo.GetAllOfQuestionnaireIdAsync(questionnaireId);
            return repoModels.Select(repo => new RangeQuestionAnswer
            {
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerValue = repo.AnswerValue,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task<IEnumerable<TextQuestionAnswer>> GetTextAnswersByQuestionnaireIdAsync(int questionnaireId)
        {
            var repoModels = await _textRepo.GetAllOfQuestionnaireIdAsync(questionnaireId);
            return repoModels.Select(repo => new TextQuestionAnswer
            {
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerText = repo.AnswerText,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task<IEnumerable<DegreeQuestionAnswer>> GetDegreeAnswersAsync(int questionnaireId, string studentId)
        {
            var repoModels = await _degreeRepo.GetAllAnswersAsync(questionnaireId, studentId);
            return repoModels.Select(repo => new DegreeQuestionAnswer
            {
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerValue = repo.AnswerValue,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task<IEnumerable<MultipleChoiceQuestionAnswer>> GetMultipleChoiceAnswersAsync(int questionnaireId, string studentId)
        {
            var repoModels = await _multipleChoiceRepo.GetAllAnswersAsync(questionnaireId, studentId);
            return repoModels.Select(repo => new MultipleChoiceQuestionAnswer
            {
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerOptionId = repo.AnswerOptionId,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task<IEnumerable<RangeQuestionAnswer>> GetRangeAnswersAsync(int questionnaireId, string studentId)
        {
            var repoModels = await _rangeRepo.GetAllAnswersAsync(questionnaireId, studentId);
            return repoModels.Select(repo => new RangeQuestionAnswer
            {
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerValue = repo.AnswerValue,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }

        public async Task<IEnumerable<TextQuestionAnswer>> GetTextAnswersAsync(int questionnaireId, string studentId)
        {
            var repoModels = await _textRepo.GetAllAnswersAsync(questionnaireId, studentId);
            return repoModels.Select(repo => new TextQuestionAnswer
            {
                QuestionnaireId = repo.QuestionnaireId,
                QuestionId = repo.QuestionId,
                AnswerText = repo.AnswerText,
                StudentId = repo.StudentId,
                FillDateTime = repo.FillDateTime
            });
        }
    }
}