using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using Template.Data;

namespace Template
{
    public class QuestionsService
    {
        private readonly IServiceProvider services;
        private readonly DiscordSocketClient client;
        private readonly EFContext _db;
        private readonly UsersService usersService;
        public QuestionsService(IServiceProvider services)
        {
            this.services = services;
            client = services.GetRequiredService<DiscordSocketClient>();
            _db = services.GetRequiredService<EFContext>();
            usersService = services.GetRequiredService<UsersService>();
        }
        public async Task<Question> TryGetNewQuestionForUserAsync(User user)
        {
            if (user.CurrentQuestion is null)
            {
                var question = _db.Questions.FirstOrDefault(a => !a.AnsweredUsers.Contains(user));
                if (question is not null)
                {
                    var userStatus = new QuestionStatus()
                    {
                        UserId = user.Id,
                        Status = QuestionStatusType.Answering
                    };

                    question.UsersStatus.Add(userStatus);
                    user.AnsweredQuestion.Add(question);

                    user.CurrentQuestion = question;

                    _db.SaveChanges();
                }
            }
            return user.CurrentQuestion;
        }
        public async Task<QuestionItem> TryGetNewQuestionItemForUserAsync(User user)
        {
            if (user.CurrentQuestionItem is null)
            {
                var question = await TryGetNewQuestionForUserAsync(user);
                if (question is null) return null;

                user.CurrentQuestionItem = question.Childern.FirstOrDefault(a => !user.HasAnswered(a.Id));
            }
            return user.CurrentQuestionItem;
        }
        public async Task<Question> QuestionByChilderAsync(QuestionItem item)
        {
            var question = item.Parent;
            return await Task.FromResult(question);
        }
        public async Task<QuestionItem> GetLatestQuestionItemAsync(User user)
        {
            var answer = await GetLatestAnswerAsync(user);
            return answer.QuestionItem;
        }
        public async Task<Answer> GetLatestAnswerAsync(User user)
        {
            var result = user.Answers.OrderByDescending(a => a.CreatedAt).LastOrDefault();
            return await Task.FromResult(result);
        }
        public async Task AskUserToFillQuestionAsync(ulong userId = 0, IUser user = null)
        {
            var component = new ComponentBuilder()
              .WithButton("Yes, i am ready", "ping.test.ready")
              .WithButton("Snooze", "ping.test.snooze")
              .WithButton("No, Skip", "ping.test.skip")
              .Build();

            user = user ?? await client.Rest.GetUserAsync(userId);

            await user.SendMessageAsync("Hey! Are you ready to fill out the Test report now? You can also do that on the web.", components: component);
        }
        public async Task<bool> TrySendUserQuestionAsync(User botUser)
        {
            botUser.State = State.OnAnsweringQuestions;

            var questionItem = await TryGetNewQuestionItemForUserAsync(botUser);
            if (questionItem is null)
            {
                await usersService.SendMessageToUserAsync(botUser, "Well done! This is all, you can continue with your work 💪");
                return true;
            }

            return await usersService.SendMessageToUserAsync(botUser, questionItem.Value);
        }
        public async Task SubmitQuestionAnswer(User user, string answer)
        {
            var answer_ = new Answer()
            {
                CreatedAt = DateTime.UtcNow,
                Value = answer
            };
            user.CurrentQuestionItem.Answers.Add(answer_);
            await _db.SaveChangesAsync();
        }
        private async Task Back(User user)
        {
            var questionItem = await GetLatestQuestionItemAsync(user);
            user.CurrentQuestionItem = questionItem;

            var question = await QuestionByChilderAsync(questionItem);
            user.CurrentQuestion = question;

            _db.SaveChanges();

            await TrySendUserQuestionAsync(user);
        }
        private async Task Cancel(User user)
        {
            user.State = State.None;
            _db.SaveChanges();

            await usersService.SendMessageToUserAsync(user, "task canceled.");
        }
        public async Task<bool> OnUserResponse(SocketUserMessage message, User user)
        {
            if (user.State != State.OnAnsweringQuestions)
                return false;

            switch (message.Content)
            {
                case ".cancel":
                    await Cancel(user);
                    break;

                case ".back":
                    await Back(user);
                    break;

                default:
                    await SubmitQuestionAnswer(user, message.Content);
                    break;
            };
            return true;
        }


    }
}
