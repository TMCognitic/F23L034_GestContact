using F23L034_GestContact.Models.Dal.DataMappers;
using F23L034_GestContact.Models.Dal.Entities;
using F23L034_GestContact.Models.Dal.Repositories;
using System.Data.SqlClient;
using Tools.Database;

namespace F23L034_GestContact.Models.Dal.Services
{
    public class AuthService : IAuthRepository
    {
        const string CONNECTION_STRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=F23L034_GestContact;Integrated Security=True;";
        public Utilisateur? Login(string email, string passwd)
        {
            using (SqlConnection dbConnection = new SqlConnection(CONNECTION_STRING))
            {
                dbConnection.Open();
                return dbConnection.ExecuteReader("CSP_Login", dr => dr.ToUtilisateur(), true, new { email, passwd }).SingleOrDefault();
            }
        }

        public void Register(Utilisateur utilisateur)
        {
            using (SqlConnection dbConnection = new SqlConnection(CONNECTION_STRING))
            {
                dbConnection.Open();
                dbConnection.ExecuteNonQuery("CSP_Register", true, new { utilisateur.Nom, utilisateur.Prenom, utilisateur.Email, utilisateur.Passwd });
            }
        }
    }
}
