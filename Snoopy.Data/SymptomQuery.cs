using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Snoopy.Data.Model;

namespace Snoopy.Data
{
    public class SymptomQuery
    {
        public static IList<user_symptom_meta> GetMetaList()
        {
            IList<user_symptom_meta> list = new List<user_symptom_meta>();
            string sql = "select symptom_id,symptom_name from user_symptom_meta order by symptom_id desc";

            using (var dr = MySqlHelper.ExecuteReader(ConnectionHelper.Connection, sql))
            {
                while (dr.Read())
                {
                    list.Add(new user_symptom_meta()
                    {
                        symptom_id = int.Parse(dr["symptom_id"].ToString()),
                        symptom_name = dr["symptom_name"].ToString()
                    });
                }
            }
            return list;
        }

        public static user_symptom_meta ReaderMetaBind(IDataReader dr)
        {
            //需保证字段不能为null
            var model = new user_symptom_meta();
            model.symptom_id = int.Parse(dr["symptom_id"].ToString());
            model.symptom_name = dr["symptom_name"].ToString();
            return model;
        }
        public static IList<user_symptom> GetList(string user_id)
        {
            IList<user_symptom> list = new List<user_symptom>();
            string sql = "select ID,user_id,symptom_id,symptom_check,symptom_desc from user_symptom where user_id=@user_id order by ID desc";
            MySqlParameter[] parameters = {
	 new MySqlParameter("@user_id", MySqlDbType.VarChar,45),
				};
            parameters[0].Value = user_id;

            using (var dr = MySqlHelper.ExecuteReader(ConnectionHelper.Connection, sql, parameters))
            {
                while (dr.Read())
                {
                    list.Add(new user_symptom()
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        user_id = dr["user_id"].ToString(),
                        symptom_id = int.Parse(dr["symptom_id"].ToString()),
                        symptom_check = dr["symptom_check"].ToString() == "1",
                        symptom_desc = dr["symptom_desc"].ToString()
                    });
                }
            }
            return list;
        }

        public static int GetUserSymptomCount()
        {
            string sql = "select count(*) from user_symptom";
            using (var dr = MySqlHelper.ExecuteReader(ConnectionHelper.Connection, sql))
            {
                while (dr.Read())
                {
                    return int.Parse(dr[0].ToString());
                }
            }
            return 0;
        }


        public static bool Delete(string user_id)
        {
            string sql = "delete from  user_symptom  where user_id=@user_id";
            MySqlParameter[] parameters = {
	 new MySqlParameter("@user_id", MySqlDbType.VarChar,45)
				};
            parameters[0].Value = user_id;
            return MySqlHelper.ExecuteNonQuery(ConnectionHelper.Connection, sql, parameters) > 0;
        }


        public static bool Insert(user_symptom model)
        {
            string sql = "insert into user_symptom(ID,user_id,symptom_id,symptom_check,symptom_desc) values (@ID,@user_id,@symptom_id,@symptom_check,@symptom_desc)";
            MySqlParameter[] parameters = {
	 new MySqlParameter("@ID", MySqlDbType.Int32),
	 new MySqlParameter("@user_id", MySqlDbType.VarChar,45),
	 new MySqlParameter("@symptom_id", MySqlDbType.Int32),
	 new MySqlParameter("@symptom_check", MySqlDbType.Bit),
	 new MySqlParameter("@symptom_desc", MySqlDbType.VarChar,200)
				};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.user_id;
            parameters[2].Value = model.symptom_id;
            parameters[3].Value = model.symptom_check;
            parameters[4].Value = model.symptom_desc;
            return MySqlHelper.ExecuteNonQuery(ConnectionHelper.Connection, sql, parameters) > 0;
        }

        public user_symptom ReaderBind(IDataReader dr)
        {
            //需保证字段不能为null
            var model = new user_symptom();
            model.ID = int.Parse(dr["ID"].ToString());
            model.user_id = dr["user_id"].ToString();
            model.symptom_id = int.Parse(dr["symptom_id"].ToString());
            model.symptom_check = bool.Parse(dr["symptom_check"].ToString());
            model.symptom_desc = dr["symptom_desc"].ToString();
            return model;
        }
    }
}
