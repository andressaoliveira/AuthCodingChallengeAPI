using AuthCodingChallengeApi.Model;
using MySqlConnector;

namespace AuthCodingChallengeApi.Repository
{
    public class UserRepository
    {
        private readonly MySqlConnection connection = new("server=mysqlserver.cv8svfzmm14w.us-east-1.rds.amazonaws.com;user=admin;password=CW5HgxwDg4fzYATuqWDv;database=dbcodingchallenge");
        
        public async Task<User> Get(string username, string password)
        {
            var users = new List<User>();
            MySqlCommand comando = new("SELECT U.Email, U.Senha, P.Perfil FROM Usuario U INNER JOIN Perfil P ON P.IdPerfil = U.IdPerfil", connection);
            connection.Open();
            var reader = await comando.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var user = new User()
                    {
                        UserName = reader.GetString(0),
                        Password = reader.GetString(1),
                        Role = reader.GetString(2)
                    };
                    users.Add(user);
                }
            }

            return users.FirstOrDefault(x => x.UserName == username && x.Password == password);
        }
    }
}
