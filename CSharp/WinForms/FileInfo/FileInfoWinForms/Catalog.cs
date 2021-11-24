using FileInfo.Model;
using FileInfo.Service;
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

namespace FileInfoWinForms
{
    public partial class Catalog : Form
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        LinkedList<FileInformation> list = new LinkedList<FileInformation>();
        LinkedList<AudioInfo> audioList = new LinkedList<AudioInfo>();
        LinkedList<VideoInfo> videoList = new LinkedList<VideoInfo>();
        public Catalog()
        {
            InitializeComponent();
            cbFileType.DataSource = Enum.GetValues(typeof(FileType));
            SetupColumns();
            DataGridViewDataBinding();
            SetupControls();
        }
        #region Setup
        private void SetupColumns()
        {
            dgvMain.Columns.Clear();
            var type = cbFileType.SelectedItem.ToString();
            if (type == FileType.Audio.ToString())
            {
                dgvMain.Columns.Add("Id", "Id");
                dgvMain.Columns.Add("Name", "Name");
                dgvMain.Columns.Add("Author", "Author");
                dgvMain.Columns.Add("Length", "Length");
                dgvMain.Columns.Add("Extension", "Extension");
                dgvMain.Columns.Add("Year", "Year");
                dgvMain.Columns.Add("Price", "Price");
                dgvMain.Columns.Add("CreatedBy", "CreatedBy");
            }
            else 
            {
                dgvMain.Columns.Add("Id", "Id");
                dgvMain.Columns.Add("Name", "Name");
                dgvMain.Columns.Add("Producer", "Producer");
                dgvMain.Columns.Add("MainActor", "MainActor");
                dgvMain.Columns.Add("Length", "Length");
                dgvMain.Columns.Add("Extension", "Extension");
                dgvMain.Columns.Add("Year", "Year");
                dgvMain.Columns.Add("Price", "Price");
                dgvMain.Columns.Add("CreatedBy", "CreatedBy");
            }
        }
        private void DataGridViewDataBinding()
        {
            dgvMain.Rows.Clear();
            var type = cbFileType.SelectedItem.ToString();
            if (type == FileType.Audio.ToString())
            {
                foreach (var file in audioList)
                {
                    dgvMain.Rows.Add(file.Id, file.Name, file.Author, file.Length, file.Extension, file.Year, file.Price, file.CreatedBy);
                }
            }
            else
            {
                foreach (var file in videoList)
                {
                    dgvMain.Rows.Add(file.Id, file.Name, file.Producer, file.MainActor, file.Length, file.Extension, file.Year, file.Price, file.CreatedBy);
                }
            }
        }
        private void SetupControls()
        {
            var type = cbFileType.SelectedItem.ToString();
            var flag = type == FileType.Audio.ToString();
            lblAuthor.Text = flag?"Author":"Producer";
            lblMainActor.Visible = !flag;
            tbMainActor.Visible = !flag;
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Browse Text Files",
                CheckFileExists = true,
                CheckPathExists = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var path = openFileDialog1.FileName;
                foreach (var item in FileListUploadService.ReadFromFile(path))
                {
                    list.AddLast(item);
                }
                foreach (var item in list.OfType<AudioInfo>().ToList())
                    audioList.AddLast(item);
                foreach (var item in list.OfType<VideoInfo>().ToList())
                    videoList.AddLast(item);
                DataGridViewDataBinding();
            }
        }
        #endregion
        private void cbFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupColumns();
            DataGridViewDataBinding();
            SetupControls();
        }

        private void dgvMain_SelectionChanged(object sender, EventArgs e)
        {
            var rowsCount = dgvMain.SelectedRows.Count;
            if (rowsCount == 0 || rowsCount > 1) return;
            var row = dgvMain.SelectedRows[0].Cells;
            if (row == null) return;
            var type = cbFileType.SelectedItem.ToString();
            if (type == FileType.Audio.ToString())
            {
                tbId.Text = row[0].Value.ToString();
                tbName.Text = row[1].Value.ToString();
                tbLength.Text = row[3].Value.ToString();
                tbExtension.Text = row[4].Value.ToString();
                tbYear.Text = row[5].Value.ToString();
                tbPrice.Text = row[6].Value.ToString();
                tbCreatedBy.Text = row[7].Value.ToString();

                tbAuthor.Text = row[2].Value.ToString();
            }
            else 
            {
                tbId.Text = row[0].Value.ToString();
                tbName.Text = row[1].Value.ToString();
                tbLength.Text = row[4].Value.ToString();
                tbExtension.Text = row[5].Value.ToString();
                tbYear.Text = row[6].Value.ToString();
                tbPrice.Text = row[7].Value.ToString();
                tbCreatedBy.Text = row[8].Value.ToString();

                tbAuthor.Text = row[2].Value.ToString();
                tbMainActor.Text = row[3].Value.ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvMain.Rows.Clear();
            list.Clear();
            var type = cbFileType.SelectedItem.ToString();
            if (type == FileType.Audio.ToString())
                audioList.Clear();
            else
                videoList.Clear();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var rows = dgvMain.SelectedRows;
            if (rows.Count == 0) return;
            var type = cbFileType.SelectedItem.ToString();
            if (type == FileType.Audio.ToString())
            {
                foreach (DataGridViewRow row in rows)
                {
                    audioList.Remove(audioList.ToList().Find(x => x.Id == row.Cells["Id"].Value.ToString()));
                }
            }
            else {
                foreach (DataGridViewRow row in rows)
                {
                    videoList.Remove(videoList.ToList().Find(x => x.Id == row.Cells["Id"].Value.ToString()));
                }
            }
            DataGridViewDataBinding();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var type = cbFileType.SelectedItem.ToString();
            try
            {
                if (type == FileType.Audio.ToString())
                {
                    var file = new AudioInfo()
                    {
                        Id = RandomString(6),
                        Name = tbName.Text,
                        CreatedBy = tbCreatedBy.Text,
                        Length = Double.Parse(tbLength.Text),
                        Extension = tbExtension.Text,
                        Year = Int32.Parse(tbYear.Text),
                        Price = Double.Parse(tbPrice.Text),
                        Type = FileType.Audio,

                        Author = tbAuthor.Text
                    };
                    audioList.AddLast(file);
                    DataGridViewDataBinding();
                }
                else
                {
                    videoList.AddLast(new VideoInfo()
                    {
                        Id = RandomString(6),
                        Name = tbName.Text,
                        CreatedBy = tbCreatedBy.Text,
                        Length = Double.Parse(tbLength.Text),
                        Extension = tbExtension.Text,
                        Year = Int32.Parse(tbYear.Text),
                        Price = Double.Parse(tbPrice.Text),
                        Type = FileType.Video,

                        Producer = tbAuthor.Text,
                        MainActor = tbMainActor.Text
                    });
                    DataGridViewDataBinding();
                }
            }
            catch(Exception exc) {
                MessageBox.Show("Input correct data. Pay attention to numeric fields.\n"+exc.Message);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            var value = tbFilterValue.Text;
            if (value == "") return;
            var type = cbFileType.SelectedItem.ToString();
            dgvMain.Rows.Clear();
            if (type == FileType.Audio.ToString())
            {
                var list = audioList.Where(x=>x.Name.StartsWith(value)).ToList();
                foreach (var file in list)
                {
                    dgvMain.Rows.Add(file.Id, file.Name, file.Author, file.Length, file.Extension, file.Year, file.Price, file.CreatedBy);
                }
            }
            else
            {
                var list = videoList.Where(x => x.Name.StartsWith(value)).ToList();
                foreach (var file in list)
                {
                    dgvMain.Rows.Add(file.Id, file.Name, file.Producer, file.MainActor, file.Length, file.Extension, file.Year, file.Price, file.CreatedBy);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (tbId.Text == "") 
            {
                MessageBox.Show("Select a row from data grid to edit");
                return;
            }
            var type = cbFileType.SelectedItem.ToString();
            if (type == FileType.Audio.ToString())
            {
                var item = audioList.ToList().Find(x => x.Id == tbId.Text);
                if (item == null)
                {
                    MessageBox.Show("Item is not found in this list. Maybe you should choose another file type");
                    return;
                }
                try
                {
                    item.Name = tbName.Text;
                    item.CreatedBy = tbCreatedBy.Text;
                    item.Length = Double.Parse(tbLength.Text);
                    item.Extension = tbExtension.Text;
                    item.Year = Int32.Parse(tbYear.Text);
                    item.Price = Double.Parse(tbPrice.Text);
                    item.Author = tbAuthor.Text;
                    DataGridViewDataBinding();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Input correct data. Pay attention to numeric fields.\n" + exc.Message);
                }
            }
            else
            {
                var item = videoList.ToList().Find(x => x.Id == tbId.Text);
                if (item == null)
                {
                    MessageBox.Show("Item is not found in this list. Maybe you should choose another file type");
                    return;
                }
                try
                {
                    item.Name = tbName.Text;
                    item.CreatedBy = tbCreatedBy.Text;
                    item.Length = Double.Parse(tbLength.Text);
                    item.Extension = tbExtension.Text;
                    item.Year = Int32.Parse(tbYear.Text);
                    item.Price = Double.Parse(tbPrice.Text);
                    item.Producer = tbAuthor.Text;
                    item.MainActor = tbMainActor.Text;
                    DataGridViewDataBinding();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Input correct data. Pay attention to numeric fields.\n" + exc.Message);
                }
            }
}

        private void btnClearForm_Click(object sender, EventArgs e)
        {
            tbId.Text = tbName.Text = tbPrice.Text = tbYear.Text = tbExtension.Text = tbLength.Text = tbCreatedBy.Text = tbAuthor.Text = tbMainActor.Text = "";
            tbYear.Text = "2000";
            tbPrice.Text = tbLength.Text = "0";
        }

        private void tbFilterValue_TextChanged(object sender, EventArgs e)
        {
            var value = tbFilterValue.Text;
            if (value == "")
                DataGridViewDataBinding();
        }

        private void btnFilterAuthor_Click(object sender, EventArgs e)
        {
            var value = tbFilterValue.Text;
            if (value == "") return;
            var type = cbFileType.SelectedItem.ToString();
            dgvMain.Rows.Clear();
            if (type == FileType.Audio.ToString())
            {
                var list = audioList.Where(x => x.Author.StartsWith(value)).ToList();
                foreach (var file in list)
                {
                    dgvMain.Rows.Add(file.Id, file.Name, file.Author, file.Length, file.Extension, file.Year, file.Price, file.CreatedBy);
                }
            }
            else
            {
                var list = videoList.Where(x => x.Producer.StartsWith(value)).ToList();
                foreach (var file in list)
                {
                    dgvMain.Rows.Add(file.Id, file.Name, file.Producer, file.MainActor, file.Length, file.Extension, file.Year, file.Price, file.CreatedBy);
                }
            }
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            var type = cbFileType.SelectedItem.ToString();
            dgvMain.Rows.Clear();
            if (type == FileType.Audio.ToString())
            {
                var tmp = new LinkedList<AudioInfo>();
                foreach (var item in audioList.Reverse())
                    tmp.AddLast(item);
                audioList = tmp;
            }
            else
            {
                var tmp = new LinkedList<VideoInfo>();
                foreach (var item in videoList.Reverse())
                    tmp.AddLast(item);
                videoList = tmp;
            }
            DataGridViewDataBinding();
        }
    }
}
