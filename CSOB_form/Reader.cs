using System.Collections.Generic;
using System.IO;


namespace CSOB_form {
    public static class Reader {

        private static string pathCsv;
        public static string PathCsv { get => pathCsv; set => pathCsv = value; }

        private static List<Employee> employees = new List<Employee>();
        internal static List<Employee> Employees { get => employees; set => employees = value; }

        
        public static void addEmployy(string id, string fn, string ln, string dp) {
            Employee e = new Employee(id, fn, ln, dp);
            employees.Add(e);
        }

        public static Employee[] ReturnAll() {
            return employees.ToArray();
        }

        public static void ReadCvs() {
            Employees.Clear();
            using (StreamReader sr = new StreamReader(PathCsv)) {
                string s;
                bool first = true;
                while ((s = sr.ReadLine()) != null) {
                    if (first == true) {
                        first = false;
                    }
                    else {
                        string[] divided = s.Split(";");
                        string fn = divided[0];
                        string ln = divided[1];
                        string id = divided[2];
                        string dep = divided[3];
                        addEmployy(id, fn, ln, dep);
                    }
                }
            }
        }
    }
}
