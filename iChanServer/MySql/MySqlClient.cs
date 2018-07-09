using System;
using System.Collections.Generic;
using System.Text;
using iChanServer.Data;
using iChanServer.Utils;
using MySql.Data.MySqlClient;

namespace iChanServer.MySql
{
    class MySqlClient
    {
        private string ConnectionString;
        public MySqlClient()
        {
            ConnectionString = new MySqlConnectionStringBuilder()
            {
                Server = EnvManager.MySqlServer,
                Database = EnvManager.MySqlDatabase,
                UserID = EnvManager.MySqlUserId,
                Password = EnvManager.MySqlPassword,
                SslMode = MySqlSslMode.None
            }.ToString();
        }
        internal int GetIdeaCount()
        {
            //    MySqlCommand command = new MySqlCommand("select count(*) from idea");
            //    return Convert.ToInt32(ExecuteCommandScalar(command));
            return 0;
        }

        internal int GetTeamCount()
        {
            return 1;
        }

        internal void SaveRequestJoinTeamData(RequestJoinTeamData data)
        {

        }

        internal void SaveTeam(Team team)
        {

        }


        internal void SaveIdea(Idea idea)
        {
            //なげぇ
            //MySqlCommand command = new MySqlCommand("insert into idea (id, user_id, user_name, user_address, title, overview, detail, idea_address, time) values (@id, @user_id, @user_name, @user_address, @title, @overview, @detail, @idea_address, @time)");
            //command.Parameters.Add(new MySqlParameter("id", idea.Id));
            //command.Parameters.Add(new MySqlParameter("user_id", idea.UserId));
            //command.Parameters.Add(new MySqlParameter("user_name", idea.UserName));
            //command.Parameters.Add(new MySqlParameter("user_address", idea.UserAddress));
            //command.Parameters.Add(new MySqlParameter("title", idea.Title));
            //command.Parameters.Add(new MySqlParameter("overview", idea.Overview));
            //command.Parameters.Add(new MySqlParameter("detail", idea.Detail));
            //command.Parameters.Add(new MySqlParameter("idea_address", idea.IdeaAddress));
            //command.Parameters.Add(new MySqlParameter("time", idea.UnixTime));
            //ExecuteCommandNonQuery(command);
        }


        private object ExecuteCommandScalar(MySqlCommand command)
        {
            object result;
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                command.Connection = con;
                con.Open();
                result = command.ExecuteScalar();
            }

            return result;
        }

        private void ExecuteCommandNonQuery(MySqlCommand command)
        {
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                command.Connection = con;
                con.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
