using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L10_Struct
{
    public partial class Form1 : Form
    {
        LibraryReader[] Data = new LibraryReader[10];
        int count = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Data[count].Id = tbId.Text;
            Data[count].Name = tbName.Text;
            Data[count].Address = tbAddress.Text;
            Data[count].Phone = tbPhone.Text;
            dgvMain.Rows.Add(Data[count].Id, Data[count].Name, Data[count].Address, Data[count].Phone);
            count++;
        }

        private void btGet_Click(object sender, EventArgs e)
        {
            dgvAddress.Rows.Clear();
            var add = tbAddressRequest.Text;
            foreach (var el in Data)
            {
                if (el.Address == add)
                    dgvAddress.Rows.Add(el.Id, el.Name, el.Address, el.Phone);
            }
        }
    }
}
