using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_Middleware.DAO
{
    public class SalaryProtector
    {
        private readonly IDataProtector _protector;

        public SalaryProtector(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("Salary.Protection.v1");
        }

        public string Encrypt(string salary)
        {
            if (string.IsNullOrWhiteSpace(salary))
                return salary;

            return _protector.Protect(salary);
        }

        public string Decrypt(string encryptedSalary)
        {
            if (string.IsNullOrWhiteSpace(encryptedSalary))
                return encryptedSalary;

            return _protector.Unprotect(encryptedSalary);
        }
    }
}
