using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class PersonModel
    {

        public int ID { get; set; }
        [DisplayName("Imię")]
        [Required(ErrorMessage = "Wpisz {0}")]
        public string FirstName { get; set; }
        [DisplayName("Nazwisko")]
        [Required(ErrorMessage = "Wpisz {0}")]
        public string LastName { get; set; }
        [DisplayName("Telefon")]
        [Required(ErrorMessage = "Wpisz {0}")]
        public string Phone { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Wpisz {0}")]
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }


        public List<PersonModel> GetList()
        {
            SqlConnection conn = new SqlConnection();
            var MyList = new List<PersonModel>();
            SqlCommand com = new SqlCommand("SELECT * From People", conn);
            conn.Open();
            SqlDataReader rdr = com.ExecuteReader();
            while (rdr.Read())
            {
                var ast = new PersonModel()
                {
                    ID = (int)rdr["ID"],
                    FirstName = (string)rdr["FirstName"],
                    LastName = (string)rdr["LastName"],
                    Phone = (string)rdr["Phone"],
                    Email = (string)rdr["Email"],


                };
                MyList.Add(ast);
            }

            return MyList;
        }
     



        }
    }



