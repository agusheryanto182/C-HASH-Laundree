using Laundry.Model.Context;
using Laundry.Model.Entity;
using Laundry.Model.Repository;
using Laundry.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace Laundry.Controller
{
    public class LoginController
    {
        private LoginRepository _loginRepository;

        public string informationUser (string name)
        {
            var result = name;
            return result;
        }

        public bool PerformLogin(Employee emp)
        {
            bool result = false;

            if (string.IsNullOrEmpty(emp.Username) || string.IsNullOrEmpty(emp.Password))
            {
                MessageBox.Show("Username dan Password harus diisi !!!", "Peringatan",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return result;
            }
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _loginRepository = new LoginRepository(context);

                // panggil method ReadByNama yang ada di dalam class repository
                result = _loginRepository.VerifyLogin(emp);
            }

            if (result == true)
            {
                MessageBox.Show("Login Berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return result;
                
            }
            else
            {
                MessageBox.Show("Username atau Password Salah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return result;
            }
        }
    }
}
