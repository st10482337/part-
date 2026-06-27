using MySql.Data.MySqlClient;
using part.Models;
using System.Collections.Generic;

namespace part.Database
{
    public class TaskRepository
    {
        public void AddTask(CyberTask task)
        {
            using (MySqlConnection conn =
                new MySqlConnection(DatabaseHelper.ConnectionString))
            {
                conn.Open();

                string sql =
                @"INSERT INTO Tasks
                (Title,Description,ReminderDate,Completed)
                VALUES
                (@title,@description,@reminder,@completed)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@title", task.Title);
                cmd.Parameters.AddWithValue("@description", task.Description);
                cmd.Parameters.AddWithValue("@reminder", task.ReminderDate);
                cmd.Parameters.AddWithValue("@completed", task.Completed);

                cmd.ExecuteNonQuery();
            }
        }
    }
}