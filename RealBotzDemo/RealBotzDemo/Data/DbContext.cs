using System.Data;
using System.Data.SqlClient;
using System;
using RealBotzDemo.Models;

namespace RealBotzDemo.Data
{
    public class DbContext
    {
        private static string constr = "Server=DELL\\SQLEXPRESS;Database=DBRealBotzDemo;Trusted_Connection=True;MultipleActiveResultSets=true";
        private SqlConnection cn = new SqlConnection(constr);

        public DataTable Getdata(string qry)
        {

            DataTable dt = new DataTable();
            try
            {
                cn.Open();
                SqlDataAdapter ad = new SqlDataAdapter(qry, cn);
                ad.Fill(dt);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cn.Close();
            }
            return dt;
        }
        public int INSERT_UPDATE_DELETE(User obj)
        {

            int i = 0;
            try
            {

                SqlCommand com = new SqlCommand("INSERT_UPDATE_DELETE", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", obj.Id);
                com.Parameters.AddWithValue("@Name", obj.Name);
                com.Parameters.AddWithValue("@DateOfBirth", obj.DateOfBirth);
                com.Parameters.AddWithValue("@Address", obj.Address);
                com.Parameters.AddWithValue("@Email", obj.Email);
                com.Parameters.AddWithValue("@CountryId", obj.CountryId);
                com.Parameters.AddWithValue("@Gender", obj.Gender);
                com.Parameters.AddWithValue("@OperationType", obj.OperationType);

                SqlParameter insertedId = new SqlParameter("@InsertedId", SqlDbType.Int);
                insertedId.Direction = ParameterDirection.Output;
                com.Parameters.Add(insertedId);

                cn.Open();
                com.ExecuteNonQuery();
                i = (int)insertedId.Value;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cn.Close();
            }
            return i;
        }
        public int INSERT_UPDATE_DELETE_UserHobbies(HobbyModel obj)
        {

            int i = 0;
            try
            {

                SqlCommand com = new SqlCommand("INSERT_UPDATE_DELETE_UserHobbies", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", obj.Id);
                com.Parameters.AddWithValue("@UserId", obj.UserId);
                com.Parameters.AddWithValue("@HobbyId", obj.HobbyId);
                com.Parameters.AddWithValue("@HobbyName", obj.HobbyName);
                com.Parameters.AddWithValue("@OperationType", obj.OperationType);

                SqlParameter insertedId = new SqlParameter("@InsertedId", SqlDbType.Int);
                insertedId.Direction = ParameterDirection.Output;
                com.Parameters.Add(insertedId);

                cn.Open();
                com.ExecuteNonQuery();
                i = (int)insertedId.Value;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cn.Close();
            }
            return i;
        }
        public DataSet GetDataById(int Id)
        {

            DataSet dt = new DataSet();
            try
            {
                cn.Open();
                SqlCommand com = new SqlCommand("Get_DataById", cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", Id);
                SqlDataAdapter ad = new SqlDataAdapter(com);
                ad.Fill(dt);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cn.Close();
            }
            return dt;
        }

    }
}