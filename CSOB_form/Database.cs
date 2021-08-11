using System.Data.SQLite;


namespace CSOB_form {
     public class Database {
        private string pathDTB;
        public string PathDTB { get => pathDTB; set => pathDTB = value; }

        string createQuery = @"CREATE TABLE IF NOT EXISTS
                             [Employee] (
                             [Id] NVARCHAR(8) NOT NULL PRIMARY KEY,
                             [FirstName] NVARCHAR(64),
                             [LastName] NVARCHAR(64), 
                             [Department] NVARCHAR(64))";

        public Database(string pathDTB) {
            this.pathDTB = pathDTB;
        }

        public bool ImportDB() {
            using(SQLiteConnection conn = new SQLiteConnection("data source =" + pathDTB)) {
                using (SQLiteCommand cmd = new SQLiteCommand(conn)) {
                    conn.Open();
                    cmd.CommandText = createQuery;

                    cmd.ExecuteNonQuery();

                    foreach (Employee item in Reader.ReturnAll()) {
                        cmd.CommandText = "INSERT INTO Employee(Id,FirstName,LastName,Department) VALUES ('" + item.Id + "','" + item.FirstName + "','" + item.LastName + "','" + item.Department + "')";
                        cmd.ExecuteNonQuery();    
                    }     
                    conn.Close();  
                }
            }
            return true;
        }

        public bool ReadDB() {
            using (SQLiteConnection conn = new SQLiteConnection("data source =" + pathDTB)) {
                using (SQLiteCommand cmd = new SQLiteCommand(conn)) {
                    conn.Open();
                    cmd.CommandText = "SELECT * FROM Employee";
                    using(SQLiteDataReader reader = cmd.ExecuteReader()) {
                        Reader.Employees.Clear();
                        while (reader.Read()) {
                            Employee u = new Employee((string)reader["Id"], (string)reader["FirstName"], (string)reader["LastName"], (string)reader["Department"]);
                            Reader.Employees.Add(u);               
                        }
                    }
                    conn.Close();
                }
            }
            return true;
        }






    }
}
