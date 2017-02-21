using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS_Server.Models
{
    public class CompanyInfor
    {
        private string _number;
        private string _name;

        public CompanyInfor(string number, string name)
        {
            this._name = name;
            this._number = number;
        }

        public string getName() { return _name; }
        public string getNumber() { return _number; }
    }
}