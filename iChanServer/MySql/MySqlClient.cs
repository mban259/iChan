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

        private readonly MySqlConnection _mySqlConnection;

        public void Connect()
        {
            _mySqlConnection.Open();
        }

        public void Close()
        {
            _mySqlConnection.Close();
        }

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
            Console.WriteLine(ConnectionString);
            _mySqlConnection = new MySqlConnection(ConnectionString);

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

        internal int GetIdeaId(int teamId)
        {
            return 2;
        }

        internal int SaveRequestJoinTeamData(RequestJoinTeamData data)
        {
            var command = new MySqlCommand("insert into jointeam (user_id, user_name, user_address, team_id, time, approved) values (@user_id, @user_name, @user_address, @team_id, @time, @approved);select last_insert_id()");
            command.Parameters.Add(new MySqlParameter("user_id", data.User.UserId));
            command.Parameters.Add(new MySqlParameter("user_name", data.User.UserName));
            command.Parameters.Add(new MySqlParameter("user_address", data.User.Address));
            command.Parameters.Add(new MySqlParameter("team_id", data.TeamId));
            command.Parameters.Add(new MySqlParameter("time", data.UnixTime));
            command.Parameters.Add(new MySqlParameter("approved", false));
            return Convert.ToInt32(ExecuteCommandScalar(command));
        }

        internal bool ApprovalJoinTeam(int requestId)
        {
            return true;
        }

        internal int SaveTeam(Team team)
        {
            var command = new MySqlCommand("insert into team (user_id, user_name, user_address, idea_id, time) values (@user_id, @user_name, @user_address, @idea_id, @time);select last_insert_id()");
            command.Parameters.Add(new MySqlParameter("user_id", team.User.UserId));
            command.Parameters.Add(new MySqlParameter("user_name", team.User.UserName));
            command.Parameters.Add(new MySqlParameter("user_address", team.User.Address));
            command.Parameters.Add(new MySqlParameter("idea_id", team.IdeaId));
            command.Parameters.Add(new MySqlParameter("time", team.UnixTime));
            return Convert.ToInt32(ExecuteCommandScalar(command));
        }


        internal int SaveIdea(Idea idea)
        {
            var command = new MySqlCommand("insert into idea (user_id, user_name, user_address, title, overview, detail, time) values (@user_id, @user_name, @user_address, @title, @overview, @detail, @time);select last_insert_id()");
            command.Parameters.Add(new MySqlParameter("user_id", idea.User.UserId));
            command.Parameters.Add(new MySqlParameter("user_name", idea.User.UserName));
            command.Parameters.Add(new MySqlParameter("user_address", idea.User.Address));
            command.Parameters.Add(new MySqlParameter("title", idea.Title));
            command.Parameters.Add(new MySqlParameter("overview", idea.Overview));
            command.Parameters.Add(new MySqlParameter("detail", idea.Detail));
            command.Parameters.Add(new MySqlParameter("time", idea.UnixTime));
            Console.WriteLine("SaveIdea!!!");
            return Convert.ToInt32(ExecuteCommandScalar(command));
        }

        internal void SaveIdeaAddress(int ideaId, string address)
        {
            var command = new MySqlCommand("update idea set idea_address = @idea_address where id = @id");
            command.Parameters.Add(new MySqlParameter("idea_address", address));
            command.Parameters.Add(new MySqlParameter("id", ideaId));
            Console.WriteLine(command.ToString());
            ExecuteCommandNonQuery(command);
        }

        private void ExecuteCommandNonQuery(MySqlCommand command)
        {
            command.Connection = _mySqlConnection;
            command.ExecuteNonQuery();
        }


        private object ExecuteCommandScalar(MySqlCommand command)
        {
            command.Connection = _mySqlConnection;
            return command.ExecuteScalar();
        }
    }
}
