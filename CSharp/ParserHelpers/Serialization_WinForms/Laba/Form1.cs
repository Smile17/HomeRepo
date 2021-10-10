using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XML;

namespace Laba
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void btInput_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.DefaultExt = cbFormat.SelectedIndex == 0 ? "xml" : "dat";
                openFileDialog.Filter = "xml files (*.xml)|*xml|dat files (*.dat)|*dat|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileName = openFileDialog.FileName;
                    var person = (Person)
                        (cbFormat.SelectedIndex == 0 ?
                            XmlSerialization.Deserialize<Person>(fileName) :
                            BinarySerialization.Deserialize<Person>(fileName));
                    rtbFileContent.Text = person.ToString();
                }
            }

        }

        private void btSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "xml files (*.xml)|*xml|dat files (*.dat)|*dat|All files (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                    return;
                string fileName = saveFileDialog.FileName;

                var person = new Person() { Name = tbFName.Text, Surname = tbLName.Text, Year = Convert.ToInt32(tbYear.Text) };
                if (cbSaveFormat.SelectedIndex == 0)
                    XmlSerialization.Serialize(person, fileName);
                else
                    BinarySerialization.Serialize(person, fileName);

                tbYear.Clear();
                tbFName.Clear();
                tbLName.Clear();

                MessageBox.Show("File is saved");
            }
        }

    }
}
