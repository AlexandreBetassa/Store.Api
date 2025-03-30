using Dapper;
using Store.User.Domain.Entities.v1;
using Store.User.Domain.Interfaces.v1.Repositories;
using System.Data;

namespace Store.User.Infrastructure.Data.Repositories.v1
{
    public class UserRepository(IDbConnection dbConnection) : IUserRepository
    {
        private readonly IDbConnection _dbConnection = dbConnection;

        public async Task CreateAsync(User user)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@NAME", user.Name, DbType.AnsiString, ParameterDirection.Input, user.Name.Length);
            parameters.Add("@PASSWORD", user.Password, DbType.AnsiString, ParameterDirection.Input, user.Password.Length);
            parameters.Add("@EMAIL", user.Email, DbType.AnsiString, ParameterDirection.Input, user.Email.Length);
            parameters.Add("@ROLE", user.Role, DbType.AnsiString, ParameterDirection.Input, user.Role.Length);
            parameters.Add("@STATUS", user.Status, DbType.Boolean, ParameterDirection.Input);

            var query = $"INSERT INTO AUTENTICACAO (NAME, PASSWORD, EMAIL, ROLE, STATUS)" +
                            $"VALUES (@NAME, @PASSWORD, @EMAIL, @ROLE, @STATUS)";

            await _dbConnection.ExecuteScalarAsync(query, parameters);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@NAME", username, DbType.AnsiString, ParameterDirection.Input, username.Length);

            var query =
                $"SELECT NAME, PASSWORD, EMAIL, ROLE, STATUS " +
                $"FROM AUTENTICACAO " +
                $"WHERE NAME = @NAME";

            return await _dbConnection.QueryFirstOrDefaultAsync<User>(query, parameters);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@EMAIL", email, DbType.AnsiString, ParameterDirection.Input, email.Length);

            var query =
                $"SELECT ID, NAME, PASSWORD, EMAIL, ROLE " +
                $"FROM AUTENTICACAO " +
                $"WHERE EMAIL = @EMAIL";

            return await _dbConnection.QueryFirstOrDefaultAsync<User>(query, parameters);
        }

        public async Task PatchStatusAsync(string username, bool status)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@STATUS", status, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("@NAME", username, DbType.AnsiString, ParameterDirection.Input, username.Length);

            var query =
                $"UPDATE AUTENTICACAO" +
                $" SET STATUS = @STATUS" +
                $" WHERE NAME = @NAME";

            await _dbConnection.ExecuteScalarAsync(query, parameters);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@ID", id, DbType.Int64, ParameterDirection.Input, id);

            var query =
                $"SELECT NAME, EMAIL, ROLE, STATUS " +
                $"FROM AUTENTICACAO " +
                $"WHERE ID = @ID";

            return await _dbConnection.QueryFirstOrDefaultAsync<User>(query, parameters);
        }
    }
}