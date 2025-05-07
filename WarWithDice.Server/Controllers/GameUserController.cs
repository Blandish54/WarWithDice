using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WarWithDice.Server.Models.ClientAPIs;
using WarWithDice.Server.Models.Settings;

namespace WarWithDice.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameUserController : Controller
    {
        private readonly ConnectionStrings connectionStrings;

        public GameUserController(ConnectionStrings connectionStrings)
        {
            this.connectionStrings = connectionStrings;
        }


        //Look at SQL Joins // Build this Flu Shots Dashboard with SQL and Tableau! on youtube

        //Create SQL migration

        //Expand SQL knowledge Stored Procedures

        //Make the dice be 5 of each, d4 d6 d8 d12.


        [Route("AllUsers")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            using (var connection = new SqlConnection(connectionStrings.GameDbConnectionString))
            {
                string query = "SELECT * FROM GameUsers";

                List<string> userNames = new List<string>();

                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var userName = reader["UserName"].ToString();

                                userNames.Add(userName);
                            }
                        }
                    }
                }
                return Ok(userNames);
            }
        }



        [Route("{Id}")]
        [HttpGet]
        public IActionResult GetUser(int Id)
        {
            using (var connectionUserId = new SqlConnection(connectionStrings.GameDbConnectionString))
            {
                string queryUserId = "SELECT UserName FROM GameUsers WHERE UserId = @Id ";

                string userName = "";

                connectionUserId.Open();

                using (SqlCommand commandUserId = new SqlCommand(queryUserId, connectionUserId))
                {
                    commandUserId.Parameters.AddWithValue("Id", Id);

                    using (SqlDataReader sqlReader = commandUserId.ExecuteReader())
                    {
                        if (sqlReader.HasRows)
                        {
                            while (sqlReader.Read())
                            {
                                userName = sqlReader["UserName"].ToString();
                            }
                        }
                        else
                        {
                            userName = "There is no User with that Id";
                        }
                    }

                    return Ok(userName);
                }
            }
        }

        [Route("Add")]
        [HttpPost]
        public IActionResult AddUser(GameUserModel gameUserModel)
        {
            using (var connectionToAddUser = new SqlConnection(connectionStrings.GameDbConnectionString))
            {
                string userName = gameUserModel.FirstName.First() + gameUserModel.LastName + gameUserModel.LuckyNumber;

                string addUserQuery = "INSERT INTO GameUsers(UserName, LuckyNumber, ColorSelection, FirstName, LastName, ColorCode) VALUES (@UserName, @LuckyNumber, @ColorSelection, @FirstName, @LastName, @ColorCode)";

                connectionToAddUser.Open();

                string creationOutcome = "";

                using (SqlCommand commandAddUser = new SqlCommand(addUserQuery, connectionToAddUser))
                {
                    commandAddUser.Parameters.AddWithValue("@UserName", userName);

                    commandAddUser.Parameters.AddWithValue("@LuckyNumber", gameUserModel.LuckyNumber);

                    commandAddUser.Parameters.AddWithValue("@ColorSelection", gameUserModel.ColorSelection);

                    commandAddUser.Parameters.AddWithValue("@FirstName", gameUserModel.FirstName);

                    commandAddUser.Parameters.AddWithValue("@LastName", gameUserModel.LastName);

                    commandAddUser.Parameters.AddWithValue("@ColorCode", gameUserModel.ColorCode);

                    int rowsAffected = commandAddUser.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Game User was created");
                    }
                    else
                    {
                        return Ok ("Game User was not added");
                    }
                }
            }
        }
        
        [Route("Update/{Id}")]
        [HttpPut]
        public IActionResult UpdateUser(int Id,  string userName)
        {
            string userId = Id.ToString();  

            using (var connectionToUpdateUser = new SqlConnection(connectionStrings.GameDbConnectionString))
            {
                string updateUserQuery = "UPDATE GameUsers SET UserName = @UserName WHERE UserId = @UserId";            

                connectionToUpdateUser.Open();

                string userUpdateOutcome = "";

                using(SqlCommand commandUpdateUser = new SqlCommand(updateUserQuery, connectionToUpdateUser))
                {
                    commandUpdateUser.Parameters.AddWithValue("@UserName", userName);
                    commandUpdateUser.Parameters.AddWithValue("@UserId", Id);

                    int rowsAffected = commandUpdateUser.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        userUpdateOutcome = "The User was updated!";
                    }
                    else
                    {
                        userUpdateOutcome = "The Users records were NOT updated.";
                    }
                }
                return Ok(userUpdateOutcome);
            }
        }
        
        [Route("Delete/{UserName}")]
        [HttpDelete]
        public IActionResult UpdateUser(string UserName)
        {
            using (var connectionToDeleteUser =  new SqlConnection(connectionStrings.GameDbConnectionString))
            {
                string deleteUserQuery = "DELETE FROM GameUsers WHERE UserName = @UserName;";

                connectionToDeleteUser.Open();

                using (SqlCommand commandToDeleteUser = new SqlCommand(deleteUserQuery, connectionToDeleteUser))
                {
                    commandToDeleteUser.Parameters.AddWithValue("@UserName", UserName);

                    int rowsAffected = commandToDeleteUser.ExecuteNonQuery();

                    if(rowsAffected > 0)
                    {
                        return Ok("The record was deleted!");
                    }
                    else
                    {
                        return Ok("The was NOT deleted!");
                    }
                } 
            }
        }
    }
}

