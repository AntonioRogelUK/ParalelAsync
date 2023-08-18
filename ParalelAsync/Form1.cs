using ParalelAsync.Database;
using ParalelAsync.Database.Commands;
using ParalelAsync.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParalelAsync
{
    public partial class Form1 : Form
    {
        private readonly string _connectionString;
        public Form1()
        {
            _connectionString = "Server=localhost;Database=AdventureWorks2022;User Id=sa;Password=MiContraseniaSegura-2023;";
            //_connectionString = $"Server=localhost;Database=AdventureWorks2022;Trusted_Connection=True;TrustServerCertificate=true;";
            InitializeComponent();
        }

        private async void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                Person person = await new PersonCommands(_connectionString).GetPersonAsync(4);

                if (person != null )
                {
                    MessageBox.Show($"Persona: {person.BusinessEntityID}, nombre: {person.FirstName} {person.MiddleName} {person.LastName}, modificado: {person.ModifiedDate:dd/MM/yyyy}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
