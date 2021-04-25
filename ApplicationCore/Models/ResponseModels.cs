using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class InteractionDetailModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int EmpId { get; set; }
        public char IntType { get; set; }
        public DateTime IntDate { get; set; }
        public string Remarks { get; set; }
        public string EmpName  { get; set; }
        public string EmpDesignation { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPhones { get; set; }
        public string ClientAddress { get; set; }
        public DateTime ClientAddedOn { get; set; }

    }

    public class ClientResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phones { get; set; }
        public string Address { get; set; }
        public DateTime AddedOn { get; set; }
    }

    public class EmployeeResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
    }

    public class EmployeeRegisterResponseModel
    {
        public string Name { get; set; }
        public string Designation { get; set; }
    }
    public class LoginResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
    }

    //public class LoginResponseModel
    //{
    //    public int Id { get; set; }
    //}
}
