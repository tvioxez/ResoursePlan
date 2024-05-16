using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace РаспределениеРесурсов
{
    public class database
    {
        public string login = "";
        public string pass = "";

        public SqlConnection con = new SqlConnection($@"Data Source=DESKTOP-UCA27K4\SQLSERVER; Initial Catalog=TransportTask;Integrated Security = True");
    }
}
