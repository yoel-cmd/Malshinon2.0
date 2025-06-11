using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Malshinon2._0.MalshinonDAL
{
    internal class malshinonDAL
    {

        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;
        //----------------------------------------------------------------------------------------

        public malshinonDAL(string connection)
        {
            conn = new MySqlConnection(connection);
            conn.Open();
        }
        //----------------------------------------------------------------------------------------
        public bool FindPersonByName(string firstName, string lastName)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM people WHERE FirstName = @FirstName AND LastName = @LastName";
                using ( cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);                   
                    int count = Convert.ToInt32(cmd.ExecuteScalar()); 
                    return count > 0; 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw; 
            }
        }
        //----------------------------------------------------------------------------------------
        public int returnIdByName(string firstName, string lastName)
        {
            try
            {
                string query = "SELECT Id FROM people WHERE FirstName = @FirstName AND LastName = @LastName";
                using (cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                   object id = cmd.ExecuteScalar();
                    if(id != null)
                    {
                        return Convert.ToInt32(id);
                    }
                    return -1 ; 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }

        }
        //----------------------------------------------------------------------------------------
        public void createPeople(string firstName,string lastName)
        {
            try
            {
                string SecretCode = createSecretCode();
                string creatInformers = "INSERT INTO people(FirsrName,LastName,LastName)" +
                    "VALUES (@FirsrName,@LastName,@LastName)";
                using (cmd = new MySqlCommand(creatInformers, conn))
                {
                    cmd.Parameters.AddWithValue("@FirsrName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@LastName", SecretCode);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine($" {firstName} {lastName} added to DB");
                }
            }
            catch (Exception e)
            {
                
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
        }
        //----------------------------------------------------------------------------------------
        public string createSecretCode()
        {
            Random rnd = new Random();
            string secret = "";
            for (int i = 0; i < 7; i++)
            {
                char c = (char)rnd.Next(33, 126);
                secret += c;
            }
            return secret;
        }
        //----------------------------------------------------------------------------------------
        public bool checkIfSecretCodeExists(string SecretCode)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM people WHERE LastName = @secretCode";
                using (cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@secretCode", SecretCode);
                    int num = Convert.ToInt32(cmd.ExecuteScalar());
                    return num > 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
        }
        //----------------------------------------------------------------------------------------
        // פה אני חייב להכניס בדיקה האם הוא הכניס שם 
        public string[] ParseReport(string report)
        {
            string[] reportArr = report.Split(' ');
            if (reportArr.Length < 3)
            {
                Console.WriteLine("Report must contain valid first and last names");
                return null;
            }
            return reportArr;
        }
        //----------------------------------------------------------------------------------------
        public void updateReports(int idTarget,int idInforment, string report)
        {

        }





    }
}
