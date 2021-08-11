using System;


namespace CSOB_form {
    public class Employee {
        private string id;
        private string firstName;
        private string lastName;
        private string department;
        public string Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Department { get => department; set => department = value; }


        public Employee(string id, string firstName, string lastName, string department) {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.department = department;
        }

        public override string ToString() {
            return string.Format("{0,-18} {1,-25} {2,-10}", id, firstName + " " + lastName, department);
        }
    }
}
