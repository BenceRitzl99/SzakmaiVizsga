using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ChefDesktop
{
    public partial class Form1 : Form
    {
        private DataGridView dgv;
        private TextBox txtChefName;
        private DateTimePicker dtpDatum;
        private ComboBox cmbKategoria;
        private TextBox txtOsszeg
        private TextBox txtMegjegyzes;
        private Button btnHozzaad;

        private List<ChefCost> Chefcosts = new List<ChefCost>();
        private string filePath = "c:/Users/Diak/..rb/Vizsga/ChefDesktop/chef_koltsegek_2024.csv";

        public Form1()
        {
            InitializeComponent();
            InitializeFormElements();
            LoadCsv();
            RefreshGrid();
        }

        private void InitializeFormElements()
        {
            this.Text = "Chef Cost Manager";
            this.Width = 900;
            this.Height = 600;

            dgv = new DataGridView
            {
                Location = new System.Drawing.Point(10, 10),
                Width = 860,
                Height = 300,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dgv);

            label lblChef = new label { Text = "Chef Name:", Location = new System.Drawing.Point(10, 320) };
            txtChefName = new TextBox { Location = new System.Drawing.Point(120, 320), Width = 200 };

            Label lblDatum = new Label { Text = "Dátum:", Location = new System.Drawing.Point(10, 360) };
            dtpDate = new DateTimePicker { Location = new System.Drawing.Point(120, 360), Width = 200 };

            Label lblKategoria = new Label { Text = "Kategória:", Location = new System.Drawing.Point(10, 400) };
            cmbCategory = new ComboBox { Location = new System.Drawing.Point(120, 400), Width = 200 };
            cmbCategory.Items.AddRange(new string[] { "Maintenance", "Repairs", "Insurance", "Cleaning", "Utilities", "Other" });
            cmbCategory.SelectedIndex = 0;

            Label lblOsszeg = new Label { Text = "Összeg (EUR):", Location = new System.Drawing.Point(10, 440) };
            txtOsszeg = new NumericUpDown { Location = new System.Drawing.Point(120, 440), Width = 200, Maximum = 1000000, DecimalPlaces = 2 };

            Label lblMegjegyzes = new Label { Text = "Megjegyzés:", Location = new System.Drawing.Point(10, 480) };
            txtMegjegyzes = new TextBox { Location = new System.Drawing.Point(120, 480), Width = 200 };

            btnAdd = new Button { Text = "Hozzáadás", Location = new System.Drawing.Point(350, 440), Width = 100 };
            btnAdd.Click += BtnAdd_Click;

            this.Controls.Add(lblChef);
            this.Controls.Add(txtChefName);
            this.Controls.Add(lblDatum);
            this.Controls.Add(dtpDatum);
            this.Controls.Add(lblKategoria);
            this.Controls.Add(cmbKategoria);
            this.Controls.Add(lblOsszeg);
            this.Controls.Add(txtOsszeg);
            this.Controls.Add(lblMegjegyzes);
            this.Controls.Add(txtMegjegyzes);
            this.Controls.Add(btnHozzaadas);
        }

        private void LoadCsv()
        {
            if (!File.Exists(filePath))
                return;

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(';');
                officeCosts.Add(new ChefCost
                {
                    Id = int.Parse(parts[0]),
                    ChefName = parts[1],
                    Datum = DateTime.Parse(parts[2]),
                    Kategoria = parts[3],
                    Osszeg = decimal.Parse(parts[4]),
                    Megjegyzes = parts[5]
                });
            }
        }

        private void SaveCsv()
        {
            var lines = new List<string> { "id;chefname;datum;kategoria;osszeg;megjegyzes" };
            lines.AddRange(chefCosts.Select(x => $"{x.Id};{x.chefName};{x.Datum:yyyy-MM-dd};{x.Kategoria};{x.Osszeg};{x.Megjegyzes}"));
            File.WriteAllLines(filePath, lines);
        }

        private void RefreshGrid()
        {
            dgv.DataSource = null;
            dgv.DataSource = chefCosts;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            int nextId = chefCosts.Any() ? chefCosts.Max(x => x.Id) + 1 : 1;
            var newCost = new ChefCost
            {
                Id = nextId,
                ChefName = txtChefName.Text,
                Datum = dtpDatum.Value,
                Kategoria = cmbKategoria.SelectedItem.ToString(),
                Osszeg = txtOsszeg.Value,
                Megjegyzes = txtMegjegyzes.Text
            };

            dhefCosts.Add(newCost);
            SaveCsv();
            RefreshGrid();

            txtchefName.Clear();
            txtOsszeg.Value = 0;
            txtMegjegyzes.Clear();
            cmbKategoria.SelectedIndex = 0;
            dtpDatum.Value = DateTime.Now;
        }
    }

    public class ChefCost
    {
        public int Id { get; set; }
        public string ChefName { get; set; }
        public DateTime Datum { get; set; }
        public string Kategoria { get; set; }
        public decimal Osszeg { get; set; }
        public string Megjegyzes { get; set; }
    }
}
        

    

