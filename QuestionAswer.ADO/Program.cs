using Microsoft.Data.SqlClient;
using QuestionAnswer.DTO.Model;
using QuestionAnswer.DTO.Model.QuestionItemModel;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuestionAswer.ADO
{
    internal class Program
    {        
        public static string connectionString = @"Server=DESKTOP-10OAA31\SQLEXPRESS;Database=userDB;Trusted_Connection=true;TrustServerCertificate=True";

        static async Task Main(string[] args)
        {
            /*
            //for(int i = 0; i < 1000; i++) 
            //{
            //    var id = Guid.NewGuid();
            //    await DbAddUser(id, RandomString(), Random.Shared.Next(20, 100), Random.Shared.Next(100, 10000));

            //    var rand = Random.Shared.Next(100);
            //    for (int n = 0; n < rand; n++)
            //    {
            //        await DbQuestions(Guid.NewGuid(), 
            //            id, 
            //            RandomString(), 
            //            RandomString(), 
            //            RandomString(), 
            //            Random.Shared.Next(0, 40), 
            //            Random.Shared.Next(20, 100),
            //            Random.Shared.Next(100, 200),
            //            Random.Shared.Next(200, 1000));
            //    }
            //}

            //await DbAUsersPrint();
            

            var questions = await GetQuestions(10, 10);

            questions.ForEach(x => Console.WriteLine($"{x.Title}\t{x.Description}\t{x.Like}\t{x.DizLike}\t{x.Id}"));

            Console.WriteLine(GetUser(Guid.NewGuid()));
            */

            DataBase dataBase = new DataBase();

            for (int u = 0; u < 2; u++)
            {

                var userId = Guid.NewGuid();
                await dataBase.AddNewUser(new CreateNewUser()
                {
                    UserName = RandomString(),
                    Id = userId,
                    AuthUserItem = new AuthUserItem()
                    {
                        Email = RandomString(),
                        Password = RandomString()
                    }
                });

                for (int q = 0; q < 2; q++)
                {
                    var questionId = Guid.NewGuid();
                    await dataBase.CreateNewQuestion(new CreateQuestionItem()
                    {
                        Id = questionId,
                        Description = RandomString(),
                        Title = RandomString(),
                        UserId = userId
                    });

                    for (int a = 0; a < 50; a++)
                    {
                        var answerId = Guid.NewGuid();
                        await dataBase.SendNewAnswerToQuestion(new CreateAnswerToQuestion()
                        {
                            Description = RandomString(),
                            Id = answerId,
                            QuestionId = questionId,
                            UserId = userId
                        });

                        for (int c = 0; c < 5; c++)
                        {
                            var commentId = Guid.NewGuid();
                            await dataBase.SendNewCommentToAnswer(new CreateCommentToAnswer()
                            {
                                Description = RandomString(),
                                Id = commentId,
                                AnswerId = answerId,
                                UserId = userId
                            });
                        }
                    }
                }
            }

            Console.ReadLine();
        }

        public static string RandomString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[36];
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new System.String(stringChars);
        }

        public static async Task AddUser(Guid id, string name, int countAnswersToQuestions, int rating)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                string commandString = $"INSERT INTO Users VALUES (@id, @name, @count, @rating)";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

                SqlParameter idParameter = new SqlParameter("@id", id);
                SqlParameter nameParameter = new SqlParameter("@name", name);
                SqlParameter countAnswersParameter = new SqlParameter("@count", countAnswersToQuestions);
                SqlParameter ratingParameter = new SqlParameter("@rating", rating);

                //sqlCommand.Parameters.Add(/*new SqlParameter("@id", id)*/ id.ToString(), SqlDbType.UniqueIdentifier, 16);

                sqlCommand.Parameters.Add(idParameter);
                sqlCommand.Parameters.Add(nameParameter);
                sqlCommand.Parameters.Add(countAnswersParameter);
                sqlCommand.Parameters.Add(ratingParameter);

                await sqlCommand.ExecuteNonQueryAsync();

                Console.WriteLine("Строка добавлена");
            }
        }

        public static async Task AddQuestion(Guid id, 
                Guid userId,
                string titile,
                string userName,
                string description,
                int rating,
                int countLike,
                int countDizLike,
                int Views)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();


                string commandString = $"INSERT INTO Questions VALUES (@Id, @Title, @UserId, @Description, @Rating, @CountLike, @CountDizLike, @Views);";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

                
                sqlCommand.Parameters.Add(new SqlParameter("@id", id));
                sqlCommand.Parameters.Add(new SqlParameter("@Title", titile));
                sqlCommand.Parameters.Add(new SqlParameter("@UserId", userId));
                sqlCommand.Parameters.Add(new SqlParameter("@Description", description));
                sqlCommand.Parameters.Add(new SqlParameter("@Rating", rating));
                sqlCommand.Parameters.Add(new SqlParameter("@CountLike", countLike));
                sqlCommand.Parameters.Add(new SqlParameter("@CountDizLike", countDizLike));
                sqlCommand.Parameters.Add(new SqlParameter("@Views", Views));


                await sqlCommand.ExecuteNonQueryAsync();

                Console.WriteLine("Ответ на юзера добавлен добавлена");
            }
        }

        public static async Task<List<QuestionItemDTO>> GetQuestions(int countQuestion, int countStart)
        {
            var result = new List<QuestionItemDTO>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                string commandString = $"SELECT *, (SELECT [Name] FROM Users WHERE Questions.UserId = Users.Id) AS UserName FROM Questions ORDER BY Rating OFFSET {countStart} ROWS FETCH NEXT {countQuestion} ROWS ONLY";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                while (await sqlDataReader.ReadAsync())
                {
                    var userName = sqlDataReader["UserName"];

                    var title = sqlDataReader["Title"];
                    var userId = sqlDataReader["UserId"];
                    var id = sqlDataReader["Id"];
                    var description = sqlDataReader["Description"];
                    var rating = sqlDataReader["Rating"];
                    var countLike = sqlDataReader["CountLike"];
                    var countDizLike = sqlDataReader["CountDizLike"];
                    var views = sqlDataReader["Views"];

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
                    });
                }

                await sqlDataReader.CloseAsync();
            }

            return result;
        }

        public static async Task GetUsers()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                string commandString = "SELECT * FROM Users";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                Console.WriteLine($"{sqlDataReader.GetName(0)}\t{sqlDataReader.GetName(1)}\t{sqlDataReader.GetName(2)}\t{sqlDataReader.GetName(3)}");

                while (await sqlDataReader.ReadAsync())
                {
                    object id = sqlDataReader.GetValue(0);
                    object name = sqlDataReader.GetValue(1);
                    object countAnswersToQuestions = sqlDataReader.GetValue(2);
                    object rating = sqlDataReader.GetValue(3);

                    Console.WriteLine($"{id}\t{name}\t{countAnswersToQuestions}\t{rating}");
                }

                await sqlDataReader.CloseAsync();
            }
        }
       
        public static async Task<User?> GetUser(Guid id)
        {
            User? result = null;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();

                string commandString = "SELECT * FROM Users WHERE Users.Id = @userId";
                SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);
                sqlCommand.Parameters.Add(new SqlParameter("@userId", id.ToString()));
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                if (sqlDataReader.HasRows)
                    while (await sqlDataReader.ReadAsync())
                    {
                        object userId = sqlDataReader["Id"];
                        object name = sqlDataReader["Name"];
                        object countAnswersToQuestions = sqlDataReader["CountAnswersToQuestions"];
                        object rating = sqlDataReader["Rating"];

                        result = new User()
                        {
                            Id = Guid.Parse(userId.ToString()!),
                            Name = name.ToString(),
                            Rating = int.Parse(rating.ToString()!)
                        };
                    }
                
                await sqlDataReader.CloseAsync();
            }

            return result;
        }
    }
}