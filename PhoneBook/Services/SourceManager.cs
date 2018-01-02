using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Services
{
    public class SourceManager
    {


        protected SqlConnection SqlConnection { get; set; }

        public SourceManager()
        {
            SqlConnection = new SqlConnection();
            SqlConnection.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=PhoneBook;Integrated Security=True";


        }




        public void Add(PersonModel personModel)
        {
            SqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO PEOPLE(FirstName, LastName, Phone, Email, CreatedDate, UpdateDate)" +
            " values (@FirstName, @LastName, @Phone, @Email, @CreatedDate, @UpdateDate)", SqlConnection);

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                CreateParameter("@FirstName", System.Data.DbType.AnsiString, personModel.FirstName),
                CreateParameter("@LastName", System.Data.DbType.AnsiString, personModel.LastName),
                CreateParameter("@Phone", System.Data.DbType.AnsiString, personModel.Phone),
                CreateParameter("@Email", System.Data.DbType.AnsiString, personModel.Email),
                CreateParameter("@CreatedDate", System.Data.DbType.DateTime, DateTime.Now.ToString()),
                CreateParameter("@UpdateDate", System.Data.DbType.DateTime, DateTime.Now.ToString()),

            };
            sqlCommand.Parameters.AddRange(sqlParameters);

            sqlCommand.ExecuteNonQuery();


            SqlConnection.Close();
        }




        public PersonModel GetByID(int id)
        {
            SqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"Select * From People Where ID = @ID", SqlConnection);

            SqlParameter sqlParameters = CreateParameter("@ID", System.Data.DbType.AnsiString, id.ToString());
            sqlCommand.Parameters.Add(sqlParameters);

            var reader = sqlCommand.ExecuteReader();
            PersonModel personModel = new PersonModel();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    personModel.ID = (int)reader[0];
                    personModel.FirstName = (string)reader[1];
                    personModel.LastName = (string)reader[2];
                    personModel.Phone = (string)reader[3];
                    personModel.Email = (string)reader[4];
                    personModel.CreatedDate = DateTime.Parse(reader[5].ToString());
                    personModel.UpdateDate = DateTime.Parse(reader[6].ToString());
                }

            }
            SqlConnection.Close();
            return personModel;
        }


        public void Remove(int id)
        {
            SqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"Delete From People Where ID = @ID", SqlConnection);

            SqlParameter sqlParameters = CreateParameter("@ID", System.Data.DbType.AnsiString, id.ToString());
            sqlCommand.Parameters.Add(sqlParameters);

            sqlCommand.ExecuteNonQuery();

            SqlConnection.Close();

        }
        public void Update(int id)
        {
            SqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"Select * From People Where ID = @ID", SqlConnection);

            SqlParameter sqlParameters = CreateParameter("@ID", System.Data.DbType.AnsiString, id.ToString());
            sqlCommand.Parameters.Add(sqlParameters);

            sqlCommand.ExecuteNonQuery();

            SqlConnection.Close();

        }


        public void Update(PersonModel personModel)
        {
            SqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand($"Update People Set FirstName = @FirstName, LastName = @LastName, " +
               $"Phone = @Phone, Email = @Email, CreatedDate = @CreatedDate, UpdateDate = @UpdateDate Where ID = @ID", SqlConnection);

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                CreateParameter("@ID", System.Data.DbType.AnsiString, personModel.ID.ToString()),
                CreateParameter("@FirstName", System.Data.DbType.AnsiString, personModel.FirstName),
                CreateParameter("@LastName", System.Data.DbType.AnsiString, personModel.LastName),
                CreateParameter("@Phone", System.Data.DbType.AnsiString, personModel.Phone),
                CreateParameter("@Email", System.Data.DbType.AnsiString, personModel.Email),
                CreateParameter("@CreatedDate", System.Data.DbType.DateTime, DateTime.Now.ToString()),
                CreateParameter("@UpdateDate", System.Data.DbType.DateTime, DateTime.Now.ToString()),

            };
            sqlCommand.Parameters.AddRange(sqlParameters);

            sqlCommand.ExecuteNonQuery();


            SqlConnection.Close();

        }


        protected SqlParameter CreateParameter(string parameterName, System.Data.DbType type, string value)
        {
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = parameterName;
            sqlParameter.DbType = type;
            sqlParameter.Value = value;
            return sqlParameter;
        }

        public IEnumerable<PersonModel> GetAllPerson()
        {
            List<PersonModel> list = new List<PersonModel>();

            using (SqlConnection con = new SqlConnection(SqlConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllpERSON", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    PersonModel person = new PersonModel();

                    person.ID = Convert.ToInt32(rdr["ID"]);
                    person.FirstName = rdr["FirstName"].ToString();
                    person.LastName = rdr["LastName"].ToString();
                    person.Phone = rdr["Phone"].ToString();
                    person.Email = rdr["Email"].ToString();

                    list.Add(person);
                }
                con.Close();
            }
            return list;
        }
    }

}






