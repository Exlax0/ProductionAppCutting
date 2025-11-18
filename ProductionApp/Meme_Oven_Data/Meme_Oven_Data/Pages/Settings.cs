using Meme_Oven_Data.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace Meme_Oven_Data.Pages
{
    public partial class Settings : UserControl
    {
        private readonly MicrOvenContext _dbContext;
        private ComboBox cmbMachine;
        private MaskedTextBox dtAStart, dtAEnd, dtBStart, dtBEnd, dtCStart, dtCEnd;
        private Button btnSave,btStoreName, btnSaveStopReasons;
        private TextBox txtUserName;
        private DataGridView gridNames;
        private Label lblUserName;
        private DataGridView dgvOperators, dgvStopReasons;
        private BindingSource operatorsBindingSource = new BindingSource();


        public Settings(MicrOvenContext dbContext)
        {
            _dbContext = dbContext;
            InitializeComponent();
            InitControls();
            LoadMachines();
            LoadOperatorsGrid();
            LoadStopReasonsGrid();
        }

        private void LoadOperatorsGrid()
        {
            // Φόρτωσε από τη βάση
            _dbContext.Operators.Load();

            // Δέσε τα EF Local entities στο BindingSource
            operatorsBindingSource.DataSource = _dbContext.Operators.Local.ToBindingList();

            // Το AutoGenerateColumns είναι false, γιατί φτιάξαμε custom στήλες
            // Αν θες, μπορείς να βεβαιωθείς ότι Id είναι read-only:
            if (dgvOperators.Columns["Id"] != null)
                dgvOperators.Columns["Id"].ReadOnly = true;
        }



        private void LoadMachines()
        {
            // Hard-coded list
            cmbMachine.Items.Clear();
            cmbMachine.Items.Add("Κοπτικό Μηχάνημα 01");
            cmbMachine.Items.Add("Κοπτικό Μηχάνημα 02");
            cmbMachine.Items.Add("Κοπτικό Μηχάνημα 03");
            // Add more if needed

            if (cmbMachine.Items.Count > 0)
                cmbMachine.SelectedIndex = 0;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cmbMachine.SelectedItem == null)
            {
                MessageBox.Show("Παρακαλώ επιλέξτε κοπτικό");
                return;
            }

            string machine = cmbMachine.SelectedItem.ToString();

            // Helper: upsert one shift row
            void SaveShift(string shiftCode, string shiftName,
                           MaskedTextBox txtStart, MaskedTextBox txtEnd)
            {
                if (!TimeSpan.TryParse(txtStart.Text, out TimeSpan start))
                {
                    MessageBox.Show($"Invalid start time for {shiftName}");
                    return;
                }

                if (!TimeSpan.TryParse(txtEnd.Text, out TimeSpan end))
                {
                    MessageBox.Show($"Invalid end time for {shiftName}");
                    return;
                }

                var plan = _dbContext.MachineShiftPlan
                    .SingleOrDefault(x => x.Machine == machine && x.ShiftCode == shiftCode);

                if (plan == null)
                {
                    plan = new ShiftPlan
                    {
                        Machine = machine,
                        ShiftCode = shiftCode
                    };
                    _dbContext.MachineShiftPlan.Add(plan);
                }

                plan.ShiftName = shiftName;
                plan.StartTime = start;
                plan.EndTime = end;
                plan.LastUpdated = DateTime.Now;
            }

            SaveShift("A", "Shift A", dtAStart, dtAEnd);
            SaveShift("B", "Shift B", dtBStart, dtBEnd);
            SaveShift("C", "Shift C", dtCStart, dtCEnd);

            _dbContext.SaveChanges();
            _dbContext.ChangeTracker.Clear();
            _dbContext.Operators.Load();

            MessageBox.Show("Βάρδια αποθηκεύτηκα για  " + machine);
        }

        

        private void CmbMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMachine.SelectedItem == null)
                return;

            string machine = cmbMachine.SelectedItem.ToString();

            var plans = _dbContext.MachineShiftPlan
                .Where(x => x.Machine == machine)
                .ToList();

            void FillShift(string shiftCode, MaskedTextBox txtStart, MaskedTextBox txtEnd)
            {
                var p = plans.FirstOrDefault(x => x.ShiftCode == shiftCode);
                if (p != null)
                {
                    txtStart.Text = p.StartTime.ToString(@"hh\:mm");
                    txtEnd.Text = p.EndTime.ToString(@"hh\:mm");
                }
                else
                {
                    // Defaults: empty shift or some default times
                    txtStart.Text = "06:00";
                    txtEnd.Text = "14:00";
                }
            }

            FillShift("A", dtAStart, dtAEnd);
            FillShift("B", dtBStart, dtBEnd);
            FillShift("C", dtCStart, dtCEnd);
        }



        private void btStoreName_Click(object sender, EventArgs e)
        {
            string name = txtUserName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Παρακαλώ εισάγετε ένα όνομα χρήστη.",
                                "Κενό όνομα",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            var storeName = new Operators
            {
                FullName = name
            };

            _dbContext.Operators.Add(storeName);
            _dbContext.SaveChanges();

            MessageBox.Show("Χειριστής αποθηκεύτηκε επιτυχώς");

            LoadOperatorsGrid();
        }


        private void InitControls()
        {
            // Machine selector
            cmbMachine = new ComboBox
            {
                Location = new Point(80, 310),
                Size = new Size(250, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 12)
            };
            cmbMachine.SelectedIndexChanged += CmbMachine_SelectedIndexChanged;
            this.Controls.Add(cmbMachine);

            // Labels
            var lblA = new Label { Text = "Βάρδια A", Location = new Point(80, 380), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold),ForeColor = Color.GreenYellow };
            var lblB = new Label { Text = "Βάρδια B", Location = new Point(80, 430), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.GreenYellow };
            var lblC = new Label { Text = "Βάρδια C", Location = new Point(80, 480), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.GreenYellow };

            this.Controls.Add(lblA);
            this.Controls.Add(lblB);
            this.Controls.Add(lblC);

            MaskedTextBox MakeMaskedTextBox(Point p)
            {
                return new MaskedTextBox("00:00")
                {
                    Location = p,
                    Size = new Size(70, 25),
                    Font = new Font("Segoe UI", 10),
                    TextAlign = HorizontalAlignment.Center,
                    PromptChar = '_'    // δείχνει κενό ως _
                };
            }
            


            // Helper to create time-only DateTimePicker
            DateTimePicker MakeTimePicker(Point p)
            {
                return new DateTimePicker
                {
                    Format = DateTimePickerFormat.Time,
                    ShowUpDown = true,
                    Location = p,
                    Size = new Size(90, 25),
                    Font = new Font("Segoe UI", 10)
                    
                };
            }

            // Shift A time pickers
            dtAStart = MakeMaskedTextBox(new Point(170, 380));
            dtAEnd = MakeMaskedTextBox(new Point(250, 380));
            
            // Shift B
            dtBStart = MakeMaskedTextBox(new Point(170, 430));
            dtBEnd = MakeMaskedTextBox(new Point(250, 430));
            
            // Shift C
            dtCStart = MakeMaskedTextBox(new Point(170, 480));
            dtCEnd = MakeMaskedTextBox(new Point(250, 480));
            
            this.Controls.AddRange(new Control[] { dtAStart, dtAEnd, dtBStart, dtBEnd, dtCStart, dtCEnd });
                        

            // Save button
            btnSave = new Button
            {
                Text = "Αποθήκευση Ωραρίου Βάρδιας",
                Location = new Point(80, 550),
                Size = new Size(250, 50),
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            txtUserName = new TextBox()
            {
                Location = new Point(610,190),
                Size =  new Size(120,35),

            };

            lblUserName = new Label { Text = "Όνομα Χρήστη", Location = new Point(480, 190), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.BlueViolet };
            this.Controls.Add(lblUserName);
            this.Controls.Add(txtUserName);

            btStoreName = new Button
            {
                Text = "Αποθήκευση Νέου Χειριστή",
                Location = new Point(480, 240),
                Size = new Size(250, 50),
                BackColor = Color.LightBlue,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            btStoreName.Click += btStoreName_Click;
            this.Controls.Add(btStoreName);

            btnSaveStopReasons = new Button
            {
                Text = "Αποθήκευση Διακοπής Λειτουργίας",
                Location = new Point(930, 550),
                Size = new Size(250, 50),
                BackColor = Color.OrangeRed,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            btnSaveStopReasons.Click += btnSaveStopReasons_Click;
            this.Controls.Add(btnSaveStopReasons);

            dgvOperators = new DataGridView
            {
                Location = new Point(480, 310),
                Size = new Size(250, 200),
                AllowUserToAddRows = true,
                AllowUserToDeleteRows = true,
                ReadOnly = false,
                AutoGenerateColumns = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            dgvStopReasons = new DataGridView
            {
                Location = new Point(880, 310),
                Size = new Size(350, 200),
                AllowUserToAddRows = true,
                AllowUserToDeleteRows = true,
                ReadOnly = false,
                AutoGenerateColumns = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            // στήλες
            var colStopId = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "Id",
                Name = "Id",
                ReadOnly = true,
                Width = 20
            };

            var colStopOperator = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FullName",
                HeaderText = "Χειριστής",
                Name = "FullName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            var colStopDesc = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Description",
                HeaderText = "Περιγραφή",
                Name = "Description",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };


            dgvStopReasons.RowHeadersVisible = false;
            dgvStopReasons.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStopReasons.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvStopReasons.EnableHeadersVisualStyles = false;
            dgvStopReasons.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230);
            dgvStopReasons.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            dgvStopReasons.Columns.Add(colStopId);
            //dgvStopReasons.Columns.Add(colStopOperator);
            dgvStopReasons.Columns.Add(colStopDesc);
            this.Controls.Add(dgvStopReasons);

            var btnSaveOperators = new Button
            {
                Text = "Αποθήκευση αλλαγών χειριστών",
                Location = new Point(480, 550),
                Size = new Size(250, 50),
                BackColor = Color.IndianRed,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnSaveOperators.Click += BtnSaveOperators_Click;
            this.Controls.Add(btnSaveOperators);

            // εμφάνιση – εναλλαγή χρωμάτων κ.λπ.
            dgvOperators.RowHeadersVisible = false;
            dgvOperators.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOperators.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvOperators.EnableHeadersVisualStyles = false;
            dgvOperators.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230);
            dgvOperators.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // στήλες
            var colId = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "Id",
                Name = "Id",
                ReadOnly = true,
                Width = 60
            };

            var colName = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FullName",
                HeaderText = "Χειριστής",
                Name = "FullName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            dgvOperators.Columns.Add(colId);
            dgvOperators.Columns.Add(colName);

            // Binding στο BindingSource
            dgvOperators.DataSource = operatorsBindingSource;

            // πρόσθεσέ το στο UserControl
            this.Controls.Add(dgvOperators);



        }
        private void BtnSaveOperators_Click(object sender, EventArgs e)
        {
            // κλείσε τυχόν edit που είναι σε εξέλιξη
            dgvOperators.EndEdit();
            operatorsBindingSource.EndEdit();

            try
            {
                _dbContext.SaveChanges();
                MessageBox.Show("Οι αλλαγές στους χειριστές αποθηκεύτηκαν.",
                                "Αποθήκευση",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                // προαιρετικά: ξαναφορτώνεις για να είσαι 100% sync
                // LoadOperatorsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Προέκυψε σφάλμα κατά την αποθήκευση:\n" + ex.Message,
                                "Σφάλμα",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private BindingSource stopReasonsBindingSource = new BindingSource();

        private void LoadStopReasonsGrid()
        {
            _dbContext.StopReasons.Load();

            stopReasonsBindingSource.DataSource =
                _dbContext.StopReasons.Local.ToBindingList();

            dgvStopReasons.DataSource = stopReasonsBindingSource;
            // Id read-only, Description editable, IsActive editable, Code optional
        }

        private void btnSaveStopReasons_Click(object sender, EventArgs e)
        {
            dgvStopReasons.EndEdit();
            stopReasonsBindingSource.EndEdit();
            _dbContext.SaveChanges();
        }


    }
}
