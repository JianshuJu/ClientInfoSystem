using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class EmployeeRegisterRequestModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Designation { get; set; }
    }
    public class EmployeeLoginRequestModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
    public class ClientRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phones { get; set; }
        public string Address { get; set; }
        public DateTime AddedOn { get; set; }
    }

    public class InteractionRequestModel
    {
        public int ClientId { get; set; }
        public int EmpId { get; set; }
        public char IntType { get; set; }
        public DateTime IntDate { get; set; }
        public string Remarks { get; set; }
    }
}
