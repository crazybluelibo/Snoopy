using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Snoopy.Data.Model;
using Snoopy.Model;
 
namespace Snoopy.Data
{
    public class UserQuery
    {
        private static string connectionString = ConnectionHelper.Connection;
        public static IList<user_profile> GetList(string user_name, int top = 10)
        {
            IList<user_profile> list = new List<user_profile>();
            string sql = "select ID,user_id,user_name,user_nickname,phone_number,Email_addr,gender,birthday,location from user_profile where user_name=@user_name order by ID desc limit @top";
            MySqlParameter[] parameters = {
	 new MySqlParameter("@user_name", MySqlDbType.VarChar,45),
	 new MySqlParameter("@top", MySqlDbType.Int32)
				};
            parameters[0].Value = user_name;
            parameters[1].Value = top;
            using (var dr = MySqlHelper.ExecuteReader(connectionString, sql, parameters))
            {
                while (dr.Read())
                {
                    list.Add(new user_profile()
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        user_id = dr["user_id"].ToString(),
                        user_name = dr["user_name"].ToString(),
                        user_nickname = dr["user_nickname"].ToString(),
                        phone_number = dr["phone_number"].ToString(),
                        Email_addr = dr["Email_addr"].ToString(),
                        gender = dr["gender"].ToString() == "1",
                        birthday = DateTime.Parse(dr["birthday"].ToString()),
                        location = dr["location"].ToString()
                    });
                }
            }
            return list;
        }



        //=============分页===============
        public static IList<user_profile> GetList(out int count, string user_name, int pageindex = 1, int pagesize = 10)
        {
            IList<user_profile> list = new List<user_profile>();
            int pagestart = (pageindex - 1) * pagesize;
            string where = " where user_name=@user_name";
            string sql = "select ID,user_id,user_name,user_nickname,phone_number,Email_addr,gender,birthday,location from user_profile" + where + " order by ID desc limit " + pagestart + ", " + pagesize;
            MySqlParameter[] parameters = {
	 new MySqlParameter("@user_name", MySqlDbType.VarChar,45)
				};
            parameters[0].Value = user_name;
            using (var dr = MySqlHelper.ExecuteReader(connectionString, sql, parameters))
            {
                while (dr.Read())
                {
                    list.Add(new user_profile()
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        user_id = dr["user_id"].ToString(),
                        user_name = dr["user_name"].ToString(),
                        user_nickname = dr["user_nickname"].ToString(),
                        phone_number = dr["phone_number"].ToString(),
                        Email_addr = dr["Email_addr"].ToString(),
                        gender = dr["gender"].ToString() == "1",
                        birthday = DateTime.Parse(dr["birthday"].ToString()),
                        location = dr["location"].ToString()
                    });
                }
            }
            count = GetCount(where, parameters);
            return list;
        }

        public static int GetCount(string where, MySqlParameter[] parameters = null)
        {
            string sql = "select count(*) from user_profile " + where;
            int rows = Convert.ToInt32(MySqlHelper.ExecuteScalar(connectionString, sql, parameters));
            return rows;
        }

        public static bool Update(int ID, string user_id, string user_name, string user_nickname, string phone_number, string Email_addr, bool gender, DateTime birthday, string location)
        {
            string sql = "update  user_profile set ID=@ID,user_id=@user_id,user_name=@user_name,user_nickname=@user_nickname,phone_number=@phone_number,Email_addr=@Email_addr,gender=@gender,birthday=@birthday,location=@location where user_name=@user_name";
            MySqlParameter[] parameters = {
	 new MySqlParameter("@ID", MySqlDbType.Int32),
	 new MySqlParameter("@user_id", MySqlDbType.VarChar,45),
	 new MySqlParameter("@user_name", MySqlDbType.VarChar,45),
	 new MySqlParameter("@user_nickname", MySqlDbType.VarChar,45),
	 new MySqlParameter("@phone_number", MySqlDbType.VarChar,45),
	 new MySqlParameter("@Email_addr", MySqlDbType.VarChar,45),
	 new MySqlParameter("@gender", MySqlDbType.Bit),
	 new MySqlParameter("@birthday", MySqlDbType.Date),
	 new MySqlParameter("@location", MySqlDbType.VarChar,100)
				};
            parameters[0].Value = ID;
            parameters[1].Value = user_id;
            parameters[2].Value = user_name;
            parameters[3].Value = user_nickname;
            parameters[4].Value = phone_number;
            parameters[5].Value = Email_addr;
            parameters[6].Value = gender;
            parameters[7].Value = birthday;
            parameters[8].Value = location;
            return MySqlHelper.ExecuteNonQuery(connectionString, sql, parameters) > 0;
        }


        public static bool Delete(string user_name)
        {
            string sql = "delete from  user_profile  where user_name=@user_name";
            MySqlParameter[] parameters = {
	 new MySqlParameter("@user_name", MySqlDbType.VarChar,45)
				};
            parameters[0].Value = user_name;
            return MySqlHelper.ExecuteNonQuery(connectionString, sql, parameters) > 0;
        }


        public static bool Insert(user_profile model)
        {
            string sql = "insert into user_profile(ID,user_id,user_name,user_nickname,phone_number,Email_addr,gender,birthday,location) values (@ID,@user_id,@user_name,@user_nickname,@phone_number,@Email_addr,@gender,@birthday,@location)";
            MySqlParameter[] parameters = {
	 new MySqlParameter("@ID", MySqlDbType.Int32),
	 new MySqlParameter("@user_id", MySqlDbType.VarChar,45),
	 new MySqlParameter("@user_name", MySqlDbType.VarChar,45),
	 new MySqlParameter("@user_nickname", MySqlDbType.VarChar,45),
	 new MySqlParameter("@phone_number", MySqlDbType.VarChar,45),
	 new MySqlParameter("@Email_addr", MySqlDbType.VarChar,45),
	 new MySqlParameter("@gender", MySqlDbType.Bit),
	 new MySqlParameter("@birthday", MySqlDbType.Date),
	 new MySqlParameter("@location", MySqlDbType.VarChar,100)
				};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.user_id;
            parameters[2].Value = model.user_name;
            parameters[3].Value = model.user_nickname;
            parameters[4].Value = model.phone_number;
            parameters[5].Value = model.Email_addr;
            parameters[6].Value = model.gender;
            parameters[7].Value = model.birthday;
            parameters[8].Value = model.location;
            return MySqlHelper.ExecuteNonQuery(connectionString, sql, parameters) > 0;
        }
        public static user_profile GetModel(string user_name)
        {
            var model = new user_profile();
            string sql = "select ID,user_id,user_name,user_nickname,phone_number,Email_addr,gender,birthday,location from user_profile where user_name=@user_name order by ID desc limit 1";
            MySqlParameter[] parameters = {
	 new MySqlParameter("@user_name", MySqlDbType.VarChar,45)
				};
            parameters[0].Value = user_name;
            using (var dr = MySqlHelper.ExecuteReader(connectionString, sql, parameters))
            {
                if (dr.Read())
                {
                    model.ID = int.Parse(dr["ID"].ToString());
                    model.user_id = dr["user_id"].ToString();
                    model.user_name = dr["user_name"].ToString();
                    model.user_nickname = dr["user_nickname"].ToString();
                    model.phone_number = dr["phone_number"].ToString();
                    model.Email_addr = dr["Email_addr"].ToString();
                    model.gender = dr["gender"].ToString() == "1";
                    model.birthday = DateTime.Parse(dr["birthday"].ToString());
                    model.location = dr["location"].ToString();
                    return model;
                }
               
            }
            return null;

        }
        public static  user_profile GetModelByuserId(string user_id)
        {
            var model = new user_profile();
            string sql = "select ID,user_id,user_name,user_nickname,phone_number,Email_addr,gender,birthday,location from user_profile where user_id=@user_id order by ID desc limit 1";
            MySqlParameter[] parameters = {
	 new MySqlParameter("@user_id", MySqlDbType.VarChar,45)
				};
            parameters[0].Value = user_id;
            using (var dr = MySqlHelper.ExecuteReader(connectionString, sql, parameters))
            {
                if (dr.Read())
                {
                    model.ID = int.Parse(dr["ID"].ToString());
                    model.user_id = dr["user_id"].ToString();
                    model.user_name = dr["user_name"].ToString();
                    model.user_nickname = dr["user_nickname"].ToString();
                    model.phone_number = dr["phone_number"].ToString();
                    model.Email_addr = dr["Email_addr"].ToString();
                    model.gender = dr["gender"].ToString() == "1";
                    model.birthday = DateTime.Parse(dr["birthday"].ToString());
                    model.location = dr["location"].ToString();
                }
            }
            return model;
        }

        public static user_profile ReaderBind(IDataReader dr)
        {
            //需保证字段不能为null
            var model = new user_profile();
            model.ID = int.Parse(dr["ID"].ToString());
            model.user_id = dr["user_id"].ToString();
            model.user_name = dr["user_name"].ToString();
            model.user_nickname = dr["user_nickname"].ToString();
            model.phone_number = dr["phone_number"].ToString();
            model.Email_addr = dr["Email_addr"].ToString();
            model.gender = bool.Parse(dr["gender"].ToString());
            model.birthday = DateTime.Parse(dr["birthday"].ToString());
            model.location = dr["location"].ToString();
            return model;
        }
    }
}
