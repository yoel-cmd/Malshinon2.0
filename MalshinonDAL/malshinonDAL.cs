using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon2._0.Models;
using MySql.Data.MySqlClient;

namespace Malshinon2._0.MalshinonDAL
{
    internal class malshinonDAL
    {

        Random rnd = new Random();
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;
        //----------------------------------------------------------------------------------------

        public malshinonDAL(string connection)
        {
            conn = new MySqlConnection(connection);
            conn.Open();
            readToLog("connection to DB");
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
        public void deleteByName(string firstName, string lastName)
        {
            try
            {
                string query = "DELETE FROM people WHERE FirstName = @FirstName AND LastName = @LastName";
                using (cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.ExecuteNonQuery();
                    
                    readToLog($"{firstName}{lastName} delete DB");
                    
                  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }

        }
        //----------------------------------------------------------------------------------------
        public void createPeople(string firstName,string lastName,string status)
        {
            try
            {
                string SecretCode = createSecretCode();
                string creatInformers = "INSERT INTO people(FirstName,LastName,Status,SecretCode)" +
                    "VALUES (@FirsrName,@LastName,@Status,@SecretCode)";
                using (cmd = new MySqlCommand(creatInformers, conn))
                {
                    cmd.Parameters.AddWithValue("@FirsrName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@SecretCode", SecretCode);

                    cmd.ExecuteNonQuery();
                  
                    readToLog($" {firstName} {lastName} with status {status} added to DB");
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
            string secret = "";
            for (int i = 0; i < 6; i++)
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
                string query = "SELECT COUNT(*) FROM people WHERE SecretCode = @secretCode";
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
                readToLog($"Error: {e.Message}");
                throw;
            }
        }
        //----------------------------------------------------------------------------------------
        // פה אני חייב להכניס בדיקה האם הוא הכניס שם 
        public string[] parseReport(string report)
        {
            if (string.IsNullOrEmpty(report))
            {
                throw new ArgumentException("Report text cannot be empty.");
            }
            string[] reportArr = report.Split(' ');
            if (reportArr.Length < 3)
            {
                Console.WriteLine("Report must contain valid first and last names");
                readToLog("Report must contain valid first and last names");
                return null;
            }
            return reportArr;
        }
        //----------------------------------------------------------------------------------------
        public bool UpdateReport(int idInformer, int idTarget, string report)
        {
            try
            {
                
                if (string.IsNullOrEmpty(report))
                {
                    throw new ArgumentException("Report text cannot be empty.");
                }
                if (idInformer <= 0 || idTarget <= 0)
                {
                    throw new ArgumentException("InformerId and TargetId must be positive integers.");
                }

               
                string query = "INSERT INTO reports (InformerId, ReportedId, ReportText, ReportLength) VALUES (@InformerId, @ReportedId, @ReportText, @ReportLength); " +
                              "UPDATE people SET ReportCount = ReportCount + 1 WHERE Id = @InformerId; " +
                              "UPDATE people SET MentionCount = MentionCount + 1 WHERE Id = @ReportedId;";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@InformerId", idInformer);
                    cmd.Parameters.AddWithValue("@ReportedId", idTarget);
                    cmd.Parameters.AddWithValue("@ReportText", report);
                    cmd.Parameters.AddWithValue("@ReportLength", report.Length);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected >= 3) 
                    {
                        Console.WriteLine($"The comment was successfully posted ");
                        readToLog($" id ({idInformer}) Reported To TargetId id ({idTarget})");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Failed to add report: InformerId {idInformer} or TargetId {idTarget} may not exist.");
                        readToLog($"Failed to add report: InformerId {idInformer} or TargetId {idTarget} may not exist.");
                        return false;
                    }
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
        public string returnStatus(string firstName, string lastName)
        {
            try
            {
                
                string query = "SELECT Status FROM people WHERE FirstName = @FirstName AND LastName = @LastName";
                using (cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    string status = cmd.ExecuteScalar().ToString();
                    return status;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                
                throw;
            }
        }
        //----------------------------------------------------------------------------------------
        public void UpdateStatus(string firstName, string lastName, string status)
        {
            try
            {

                string query = "UPDATE people SET Status = @status WHERE FirstName = @firstName AND LastName =@LastName";
                using (cmd = new MySqlCommand(query, conn))
                {
                    
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.ExecuteNonQuery();

                    int num = Convert.ToInt32(cmd.ExecuteScalar());
                    if( num > 0)
                    {
                        readToLog($"the status for {firstName} {lastName} updeate");
                        Console.WriteLine($"the status for {firstName} {lastName} updeate");
                    }
                    else
                    {
                        readToLog("An error occurred while updating the status..");
                        Console.WriteLine("An error occurred while updating the status..");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
        }
        //----------------------------------------------------------------------------------------
        public void potentialAgen(string firstName, string lastName, string status)
        {
            try
            {

                string query = "UPDATE people SET Status = @status WHERE FirstName = @firstName AND LastName =@LastName";
                using (cmd = new MySqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.ExecuteNonQuery();

                    int num = Convert.ToInt32(cmd.ExecuteScalar());
                    if (num > 0)
                    {
                        readToLog($"the status for {firstName} {lastName} updeate");
                        Console.WriteLine($"the status for {firstName} {lastName} updeate");
                    }
                    else
                    {
                        readToLog("An error occurred while updating the status..");
                        Console.WriteLine("An error occurred while updating the status..");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public void UpdateStatusForHighActivity()
        {
            try
            {               
                string query = @"
            UPDATE people
            JOIN (
                SELECT InformerId
                FROM reports
                GROUP BY InformerId
                HAVING COUNT(*) > 10 AND AVG(ReportLength) > 100
            ) AS active_informers ON people.Id = active_informers.InformerId
            SET people.Status = 'PotentialAgent'
            WHERE people.Status != 'PotentialAgent'";

                using ( cmd = new MySqlCommand(query, conn))
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        readToLog($"Status updated to PotentialAgent for {rowsAffected} informers");
                        Console.WriteLine($"Status updated to PotentialAgent for {rowsAffected} informers");
                        
                        
                    }
                    else
                    {
                        readToLog("No updates needed (no informers met the conditions or all are already PotentialAgent)");
                        Console.WriteLine("No updates needed (no informers met the conditions or all are already PotentialAgent)");
                       
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"err : {e.Message}");
                
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------
       
        public List<people> returnByStatus(string status)
        {
            List<people> listPeople = new List<people>();
            int count = 0;
            try
            {
                string query = "SELECT * FROM people WHERE Status = @status";
                using (cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    using (reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count++;
                            people people = new people
                            (
                                reader.GetInt32("Id"),
                                reader.GetString("FirstName"),
                                reader.GetString("LastName"),
                                reader.GetString("SecretCode"),
                                reader.GetInt32("ReportCount"),
                                reader.GetString("Status"),
                                reader.GetInt32("MentionCount")
                            );
                            listPeople.Add(people);
                        }
                    } // reader נסגר כאן
                    readToLog($"{count} people adding to DB");
                    Console.WriteLine($"{count} people adding to DB");
                }
                return listPeople;
            }
            catch (Exception e)
            {
                Console.WriteLine($"err : {e.Message}");
                return null; // מומלץ להחליף ב-new List<people>()
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------
      
        public List<people> Highisk()
        {
            List<people> listPeople = new List<people>();
            int count = 0;
            try
            {
                string query = @"
        SELECT DISTINCT p.*
        FROM people p
        LEFT JOIN (
            SELECT ReportedId
            FROM reports
            GROUP BY ReportedId
            HAVING COUNT(*) >= 20
                OR SUM(CASE WHEN ReportDate >= DATE_SUB(NOW(), INTERVAL 15 MINUTE) THEN 1
              ELSE 0 END) >= 3
        ) AS dangerous_reported ON p.Id = dangerous_reported.ReportedId
        WHERE dangerous_reported.ReportedId IS NOT NULL;";
                using (cmd = new MySqlCommand(query, conn))
                {
                    using (reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count++;
                            people people = new people
                            (
                                reader.GetInt32("Id"),
                                reader.GetString("FirstName"),
                                reader.GetString("LastName"),
                                reader.GetString("SecretCode"),
                                reader.GetInt32("ReportCount"),
                                reader.GetString("Status"),
                                reader.GetInt32("MentionCount")
                            );
                            listPeople.Add(people);
                        }
                    } // reader נסגר כאן
                    readToLog($"{count} people adding to DB");
                    Console.WriteLine($"{count} people adding to DB");
                }
                return listPeople;
            }
            catch (Exception e)
            {
                Console.WriteLine($"err : {e.Message}");
                return null; // מומלץ להחליף ב-new List<people>()
            }
        }
        //------------------------------------------------------------------------------------------------------------------
        public void readToLog(string info)
        {
            try
            {
                string query = "INSERT INTO Logs (Info) VALUES (@info)"; 

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@info", info);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            
                            
                        }
                        else
                        {
                            
                            
                        }
                    }
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"err : {e.Message}");
            }
        }
        //------------------------------------------------------------------------------------------------------------------
        public void Exit()
        {
            conn.Close();
            readToLog("close connection DB ");
        }
    }

}
