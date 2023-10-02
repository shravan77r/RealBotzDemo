using RealBotzDemo.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RealBotzDemo.Models
{
    public class Repository : IRepository
    {
        DbContext _dbContext = new();
        public string Add_Update(User obj)
        {
            string Message = "Exception Occured.";
            try
            {
                if (obj.Id > 0)
                {
                    obj.OperationType = "UPDATE";
                    var result = _dbContext.INSERT_UPDATE_DELETE(obj);
                    if (result > 0)
                    {
                        var objHobby = new HobbyModel();
                        objHobby.OperationType = "DELETE";
                        objHobby.UserId = result;
                        _dbContext.INSERT_UPDATE_DELETE_UserHobbies(objHobby);

                        if (obj.Hobby != null)
                        {
                            var splHobbies = obj.Hobby.Split(",");
                            var splHobbyNames = obj.HobbyNames.Split(",");
                            for (int i = 0; i < splHobbies.Length; i++)
                            {
                                var objHob = new HobbyModel();
                                objHob.OperationType = "INSERT";
                                objHob.UserId = result;
                                objHob.HobbyId = Convert.ToInt32(splHobbies[i]);
                                objHob.HobbyName = Convert.ToString(splHobbyNames[i]);
                                _dbContext.INSERT_UPDATE_DELETE_UserHobbies(objHob);
                            }
                        }
                        Message = "Record Updated Successfully!";
                    }
                    else
                    {
                        Message = "Record Not Updated!";
                    }
                }
                else if (obj.Id == 0)
                {
                    obj.OperationType = "INSERT";
                    var result = _dbContext.INSERT_UPDATE_DELETE(obj);
                    if (result > 0)
                    {
                        if (obj.Hobby != null)
                        {
                            var splHobbies = obj.Hobby.Split(",");
                            var splHobbyNames = obj.HobbyNames.Split(",");
                            for (int i = 0; i < splHobbies.Length; i++)
                            {
                                var objHob = new HobbyModel();
                                objHob.OperationType = "INSERT";
                                objHob.UserId = result;
                                objHob.HobbyId = Convert.ToInt32(splHobbies[i]);
                                objHob.HobbyName = Convert.ToString(splHobbyNames[i]);
                                _dbContext.INSERT_UPDATE_DELETE_UserHobbies(objHob);
                            }
                        }
                        Message = "Record Inserted Successfully!";
                    }
                    else
                    {
                        Message = "Record Not Inserted!";
                    }
                }
            }
            catch (Exception ex)
            {
                Message = "Something Went Wrong!";
            }
            return Message;
        }
        public string Delete(int Id)
        {
            string Message = "Exception Occured.";
            try
            {
                if (Convert.ToInt32(Id) > 0)
                {
                    var obj = new User();
                    obj.Id = Id;
                    obj.OperationType = "DELETE";
                    var result = _dbContext.INSERT_UPDATE_DELETE(obj);
                    if (result > 0)
                    {
                        Message = "Record Deleted Successfully!";
                    }
                    else
                    {
                        Message = "Record Not Deleted!";
                    }
                }
                else
                {
                    Message = "Invalid Id.";
                }
            }
            catch (Exception e)
            {
                Message = "Something Went Wrong!";
            }
            return Message;
        }
        public List<UserList> GetAll()
        {
            var list = new List<UserList>();
            try
            {
                #region Get List of data
                string _query = "Get_UserList";
                var data = _dbContext.Getdata(_query);

                if (data != null && data.Rows.Count > 0)
                {
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        list.Add(new UserList
                        {
                            Id = Convert.ToInt32(data.Rows[i]["Id"]),
                            Name = Convert.ToString(data.Rows[i]["Name"]),
                            Address = Convert.ToString(data.Rows[i]["Address"]),
                            Email = Convert.ToString(data.Rows[i]["Email"]),
                            CountryName = Convert.ToString(data.Rows[i]["CountryName"]),
                            DateOfBirth = Convert.ToString(data.Rows[i]["DateOfBirth"]),
                            Gender = Convert.ToString(data.Rows[i]["Gender"]),
                            Hobbies = Convert.ToString(data.Rows[i]["Hobbies"]),
                        });
                    }
                }
                #endregion
            }
            catch (Exception e)
            {
            }
            return list;
        }

        public User GetById(int Id)
        {
            var obj = new User();
            try
            {
                #region Get Data
                if (Id > 0)
                {
                    var dataSet = _dbContext.GetDataById(Id);

                    if (dataSet.Tables.Count > 0)
                    {
                        var userData = dataSet.Tables[0];
                        foreach (DataRow row in userData.Rows)
                        {
                            obj.Id = Convert.ToInt32(row["Id"]);
                            obj.Name = row["Name"].ToString();
                            obj.Address = row["Address"].ToString();
                            obj.Email = row["Email"].ToString();
                            obj.Hobby = row["Hobbies"].ToString();
                            obj.CountryId = Convert.ToInt32(row["CountryId"]);
                            obj.Gender = Convert.ToInt32(row["Gender"]);
                            obj.DOB = Convert.ToString(row["DateOfBirth"]);
                        }
                    }
                }
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
            return obj;
        }
        public List<Country> GetCountries()
        {
            List<Country> countryList = new();
            try
            {
                string _query = "select Id, CountryName from Country order by Id";
                var data = _dbContext.Getdata(_query);

                if (data != null && data.Rows.Count > 0)
                {
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        countryList.Add(new Country
                        {
                            Id = Convert.ToInt32(data.Rows[i]["Id"]),
                            CountryName = Convert.ToString(data.Rows[i]["CountryName"])
                        });
                    }
                }
            }
            catch (Exception)
            {

            }
            return countryList;
        }
    }
}
