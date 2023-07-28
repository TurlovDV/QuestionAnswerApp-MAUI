using Microsoft.Data.SqlClient;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Model.QuestionItemModel;
using QuestionAnswer.DTO.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionAswer.ADO
{
    public class DataBase : IDataBaseService
    {
        public string connectionString = @"Server=DESKTOP-10OAA31\SQLEXPRESS;Database=TestBD;Trusted_Connection=true;TrustServerCertificate=True";

        public async Task AddNewUser(CreateNewUser createNewUser)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                string commandString = $"INSERT INTO Users VALUES (@id, @name, default, @password, @email);";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

                sqlCommand.Parameters.Add(new SqlParameter("@id", createNewUser.Id.ToString()));
                sqlCommand.Parameters.Add(new SqlParameter("@name", createNewUser.AuthUserItem!.Email));
                sqlCommand.Parameters.Add(new SqlParameter("@email", createNewUser.AuthUserItem!.Email));
                sqlCommand.Parameters.Add(new SqlParameter("@password", createNewUser.AuthUserItem!.Password));

                await sqlCommand.ExecuteNonQueryAsync();
            }
        }

        public async Task<IList<AnswerItemDTO>> GetAnswersToQuestion(Guid userId, Guid idQuestion, int count, int countStart)
        {
            var result = new List<AnswerItemDTO>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                
                string commandString = $"EXEC GetAnswersToQuestion '{userId}', '{idQuestion}', {count}, {countStart};";
                
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                while (await sqlDataReader.ReadAsync())
                {
                    var userName = sqlDataReader["UserName"];

                    var title = sqlDataReader["QuestionId"];
                    //var userId = sqlDataReader["UserId"];
                    var id = sqlDataReader["Id"];
                    var description = sqlDataReader["Text"];
                    var rating = sqlDataReader["Rating"];
                    var countLike = sqlDataReader["CountLike"];
                    var countDizLike = sqlDataReader["CountDizLike"];
                    var countComments = sqlDataReader["CountComments"];
                    var isLike = sqlDataReader["Like"];
                    var isDizLike = sqlDataReader["DizLike"];
                    var userIdMessage = sqlDataReader["UserId"];

                    result.Add(new AnswerItemDTO()
                    {
                        Description = description.ToString(),
                        UserName = userName.ToString(),
                        CountDizLike = int.Parse(countDizLike.ToString()!),
                        Id = Guid.Parse(id.ToString()!),
                        CountLike = int.Parse(countLike.ToString()!),
                        Rating = int.Parse(rating.ToString()!),
                        CountComments = int.Parse(countComments.ToString()!),
                        IsDizLike = bool.Parse(isDizLike.ToString()!),
                        IsLike = bool.Parse(isLike.ToString()!),
                        UserId = Guid.Parse(userIdMessage.ToString()!)
                    });
                }

                await sqlDataReader.CloseAsync();
            }

            return result;
        }

        public async Task<IList<MessageItemDTO>> GetCommentsOfAnswer(Guid userId, Guid idAnswer, int count, int countStart)
        {
            var result = new List<MessageItemDTO>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                //string commandString = $"SELECT *, (SELECT [Name] FROM Users WHERE Comments.UserId = Users.Id) AS UserName FROM Comments WHERE Comments.AnswerId = '{idAnswer.ToString()}' ORDER BY Comments.Rating OFFSET {countStart} ROWS FETCH NEXT {count} ROWS ONLY";
                var commandString = $"EXEC GetCommentsOfAnswer '{userId}', '{idAnswer}', {count}, {countStart};";

                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                while (await sqlDataReader.ReadAsync())
                {
                    var userName = sqlDataReader["UserName"];

                    var title = sqlDataReader["AnswerId"];
                    //var userId = sqlDataReader["UserId"];
                    var id = sqlDataReader["Id"];
                    var description = sqlDataReader["Text"];
                    var rating = sqlDataReader["Rating"];
                    var countLike = sqlDataReader["CountLike"];
                    var countDizLike = sqlDataReader["CountDizLike"];
                    var isLike = sqlDataReader["Like"];
                    var isDizLike = sqlDataReader["DizLike"];
                    var userIdMessage = sqlDataReader["UserId"];

                    result.Add(new MessageItemDTO()
                    {
                        Description = description.ToString(),
                        UserName = userName.ToString(),
                        CountDizLike = int.Parse(countDizLike.ToString()!),
                        Id = Guid.Parse(id.ToString()!),
                        CountLike = int.Parse(countLike.ToString()!),
                        Rating = int.Parse(rating.ToString()!),
                        IsDizLike = bool.Parse(isDizLike.ToString()!),
                        IsLike = bool.Parse(isLike.ToString()!),
                        UserId = Guid.Parse(userIdMessage.ToString()!)
                    });
                }

                await sqlDataReader.CloseAsync();
            }

            return result;
        }

        public Task<IList<Notification>> GetNotificaation()
        {
            return null!;
        }

        public async Task<Guid> Authentication(string password, string login)
        {
            Guid result = Guid.Empty;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                string commandString = $"SELECT * FROM Users WHERE [Password] = '{password}' AND [Email] = '{login}'";

                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);
                //sqlCommand.Parameters.Add(new SqlParameter("@login", login));
                //sqlCommand.Parameters.Add(new SqlParameter("@password", password));
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                if (sqlDataReader.HasRows)
                    while (await sqlDataReader.ReadAsync())
                    {
                        object userId = sqlDataReader["Id"];

                        result = Guid.Parse(userId.ToString()!);
                    }

                await sqlDataReader.CloseAsync();
            }

            return result;
        }

        public async Task<User?> GetUser(Guid id)
        {
            User? result = null;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                string commandString = "SELECT *, " +
                    "(SELECT COUNT(*) FROM Questions WHERE (SELECT COUNT(*) FROM Answers WHERE Answers.UserId = @userId AND Answers.QuestionId = Questions.Id) >= 1) AS [CountAnswers], " +
                    "(SELECT COUNT(*) FROM Questions WHERE Questions.UserId = @userId) AS [CountMyQuestions] " +
                    "FROM Users WHERE Users.Id = @userId";

                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);
                sqlCommand.Parameters.Add(new SqlParameter("@userId", id.ToString()));
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                if (sqlDataReader.HasRows)
                    while (await sqlDataReader.ReadAsync())
                    {
                        object userId = sqlDataReader["Id"];
                        object name = sqlDataReader["Name"];
                        object countAnswersToQuestions = sqlDataReader["CountAnswers"];
                        object rating = sqlDataReader["Rating"];
                        object countMyQuestions = sqlDataReader["CountMyQuestions"];

                        result = new User()
                        {
                            Id = Guid.Parse(userId.ToString()!),
                            Name = name.ToString(),
                            Rating = int.Parse(rating.ToString()!),
                            AnswersToQuestions = int.Parse(countAnswersToQuestions.ToString()!),
                            CountMyQuestions = int.Parse(countMyQuestions.ToString()!)
                        };
                    }

                await sqlDataReader.CloseAsync();
            }

            return result;
        }

        public async Task<IList<QuestionItemDTO>> GetRandomQuestions(Guid userId, int count, int startCount)
        {
            var result = new List<QuestionItemDTO>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                //var connectionString = "SELECT *, (SELECT [Name] FROM Users WHERE Id = @userId) AS UserName, " +
                //    "(SELECT COUNT(*) FROM Answers WHERE Questions.Id = Answers.QuestionId) AS CountAnswers " +
                //    "FROM Questions WHERE Questions.UserId = @userId ORDER BY Rating OFFSET 0 ROWS FETCH NEXT 30 ROWS ONLY";
                //string commandString = $"SELECT *, (SELECT [Name] FROM Users WHERE Id = Questions.UserId) AS UserName, " +
                //    $"(SELECT COUNT(*) FROM Answers WHERE Questions.Id = Answers.QuestionId) AS CountAnswers " +
                //    $"FROM Questions ORDER BY Rating OFFSET {startCount} ROWS FETCH NEXT {count} ROWS ONLY";

                string commandString = $"EXEC GetRandomQuestions @userId, {count}, {startCount};";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

                sqlCommand.Parameters.Add(new SqlParameter("@userId", userId.ToString()));

                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                while (await sqlDataReader.ReadAsync())
                {
                    var userName = sqlDataReader["UserName"];

                    var title = sqlDataReader["Title"];
                    var id = sqlDataReader["Id"];
                    var description = sqlDataReader["Description"];
                    var rating = sqlDataReader["Rating"];
                    var countLike = sqlDataReader["CountLike"];
                    var countDizLike = sqlDataReader["CountDizLike"];
                    var views = sqlDataReader["Views"];
                    var countAnswers = sqlDataReader["CountAnswers"];
                    var isLike = sqlDataReader["Like"];
                    var isDizLike = sqlDataReader["DizLike"];

                    result.Add(new QuestionItemDTO()
                    {
                        Description = description.ToString(),
                        Title = title.ToString(),
                        UserName = userName.ToString(),
                        CountDizLike = int.Parse(countDizLike.ToString()!),
                        Id = Guid.Parse(id.ToString()!),                        
                        CountLike = int.Parse(countLike.ToString()!),
                        Rating = int.Parse(rating.ToString()!),
                        Views = int.Parse(views.ToString()!),
                        CountAnswers = int.Parse(countAnswers.ToString()!),
                        IsDizLike = bool.Parse(isDizLike.ToString()!),
                        IsLike = bool.Parse(isLike.ToString()!)                        
                    });
                }

                await sqlDataReader.CloseAsync();
            }

            return result;
        }
         
        public async Task SendNewAnswerToQuestion(CreateAnswerToQuestion createAnswer)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                string commandString = $"INSERT INTO Answers VALUES (@Id, @QuestionId, @UserId, @Description, 0);";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

                sqlCommand.Parameters.Add(new SqlParameter("@id", createAnswer.Id.ToString()));
                sqlCommand.Parameters.Add(new SqlParameter("@QuestionId", createAnswer.QuestionId.ToString()));
                sqlCommand.Parameters.Add(new SqlParameter("@UserId", createAnswer.UserId.ToString()));
                sqlCommand.Parameters.Add(new SqlParameter("@Description", createAnswer.Description));
                //sqlCommand.Parameters.Add(new SqlParameter("@Rating", 0));
                //sqlCommand.Parameters.Add(new SqlParameter("@CountLike", 0));
                //sqlCommand.Parameters.Add(new SqlParameter("@CountDizLike", 0));
                

                await sqlCommand.ExecuteNonQueryAsync();
            }
        }

        public async Task SendNewCommentToAnswer(CreateCommentToAnswer createComment)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                string commandString = $"INSERT INTO Comments VALUES (@Id, @AnswerId, @UserId, @Description, 0);";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

                sqlCommand.Parameters.Add(new SqlParameter("@id", createComment.Id.ToString()));
                sqlCommand.Parameters.Add(new SqlParameter("@AnswerId", createComment.AnswerId.ToString()));
                sqlCommand.Parameters.Add(new SqlParameter("@UserId", createComment.UserId.ToString()));
                sqlCommand.Parameters.Add(new SqlParameter("@Description", createComment.Description));
                //sqlCommand.Parameters.Add(new SqlParameter("@Rating", 0));
                //sqlCommand.Parameters.Add(new SqlParameter("@CountLike", 0));
                //sqlCommand.Parameters.Add(new SqlParameter("@CountDizLike", 0));
                
                await sqlCommand.ExecuteNonQueryAsync();
            }
        }

        public async Task CreateNewQuestion(CreateQuestionItem questionItem)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                string commandString = $"INSERT INTO Questions VALUES (@Id, @Title, @UserId, @Description, 0, 0);";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

                sqlCommand.Parameters.Add(new SqlParameter("@id", questionItem.Id.ToString()));
                sqlCommand.Parameters.Add(new SqlParameter("@Title", questionItem.Title));
                sqlCommand.Parameters.Add(new SqlParameter("@UserId", questionItem.UserId.ToString()));
                sqlCommand.Parameters.Add(new SqlParameter("@Description", questionItem.Description));
                //sqlCommand.Parameters.Add(new SqlParameter("@Rating", 0));
                //sqlCommand.Parameters.Add(new SqlParameter("@CountLike", 0));
                //sqlCommand.Parameters.Add(new SqlParameter("@CountDizLike", 0));
                //sqlCommand.Parameters.Add(new SqlParameter("@Views", 0));

                await sqlCommand.ExecuteNonQueryAsync();
            }
        }

        public async Task<IList<QuestionItemDTO>> GetQuestionsFromUser(Guid userId, int count, int startCount)
        {
            var result = new List<QuestionItemDTO>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                //var commandString = "SELECT *, (SELECT [Name] FROM Users WHERE Id = @userId) AS UserName, " +
                //    "(SELECT COUNT(*) FROM Answers WHERE Questions.Id = Answers.QuestionId) AS CountAnswers " +
                //    "FROM Questions WHERE Questions.UserId = @userId ORDER BY Rating OFFSET 0 ROWS FETCH NEXT 30 ROWS ONLY";
                var commandString = $"EXEC GetQuestionsFromUser @userId, {count}, {startCount};";

                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

                sqlCommand.Parameters.Add(new SqlParameter("@userId", userId.ToString()));

                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                while (await sqlDataReader.ReadAsync())
                {
                    var userName = sqlDataReader["UserName"];

                    var title = sqlDataReader["Title"];
                    var id = sqlDataReader["Id"];
                    var description = sqlDataReader["Description"];
                    var rating = sqlDataReader["Rating"];
                    var countLike = sqlDataReader["CountLike"];
                    var countDizLike = sqlDataReader["CountDizLike"];
                    var views = sqlDataReader["Views"];
                    var countAnswers = sqlDataReader["CountAnswers"];
                    var isLike = sqlDataReader["Like"];
                    var isDizLike = sqlDataReader["DizLike"];

                    result.Add(new QuestionItemDTO()
                    {
                        Description = description.ToString(),
                        Title = title.ToString(),
                        UserName = userName.ToString(),
                        CountDizLike = int.Parse(countDizLike.ToString()!),
                        Id = Guid.Parse(id.ToString()!),
                        CountLike = int.Parse(countLike.ToString()!),
                        Rating = int.Parse(rating.ToString()!),
                        Views = int.Parse(views.ToString()!),
                        CountAnswers = int.Parse(countAnswers.ToString()!),
                        IsDizLike = bool.Parse(isDizLike.ToString()!),
                        IsLike = bool.Parse(isLike.ToString()!)                        
                    });
                }

                await sqlDataReader.CloseAsync();
            }

            return result;
        }

        public async Task MessageLike(LikeItem likeItem)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                string commandString = $"INSERT INTO Likes VALUES (@id, @messageId, @userId);";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

                sqlCommand.Parameters.Add(new SqlParameter("@id", likeItem.Id.ToString()));
                sqlCommand.Parameters.Add(new SqlParameter("@messageId", likeItem.MessageId));
                sqlCommand.Parameters.Add(new SqlParameter("@userId", likeItem.UserId));

                await sqlCommand.ExecuteNonQueryAsync();
            }
        }

        public async Task MessageDizLike(LikeItem likeItem)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                string commandString = $"INSERT INTO DizLikes VALUES (@id, @messageId, @userId);";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

                sqlCommand.Parameters.Add(new SqlParameter("@id", likeItem.Id.ToString()));
                sqlCommand.Parameters.Add(new SqlParameter("@messageId", likeItem.MessageId));
                sqlCommand.Parameters.Add(new SqlParameter("@userId", likeItem.UserId));

                await sqlCommand.ExecuteNonQueryAsync();
            }
        }

        public async Task MessageCancelLike(LikeItem likeItem)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                string commandString = $"DELETE Likes WHERE Likes.UserId = @userId AND Likes.MessageId = @messageId";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

                sqlCommand.Parameters.Add(new SqlParameter("@messageId", likeItem.MessageId));
                sqlCommand.Parameters.Add(new SqlParameter("@userId", likeItem.UserId));

                await sqlCommand.ExecuteNonQueryAsync();
            }
        }

        public async Task MessageCancelDizLike(LikeItem likeItem)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                string commandString = $"DELETE DizLikes WHERE DizLikes.UserId = @userId AND DizLikes.MessageId = @messageId";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

                sqlCommand.Parameters.Add(new SqlParameter("@messageId", likeItem.MessageId));
                sqlCommand.Parameters.Add(new SqlParameter("@userId", likeItem.UserId));

                await sqlCommand.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteQuestion(Guid questionId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                string commandString = $"DELETE Questions WHERE Id = @questionId";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

                sqlCommand.Parameters.Add(new SqlParameter("@questionId", questionId.ToString()));
                
                await sqlCommand.ExecuteNonQueryAsync();
            }
        }
    }
}
