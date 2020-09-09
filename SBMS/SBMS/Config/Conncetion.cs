using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SBMS.Config
{
    public class Conncetion
    {
      public  SqlConnection conn = new SqlConnection("Data Source=DESKTOP-932J4T4\\SQLEXPRESS;Initial Catalog=SBMS;Integrated Security=True");

       
    }
}