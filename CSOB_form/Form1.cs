using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;


namespace CSOB_form {
    public partial class Form1 : Form {
        protected DialogResult dr;

        public Form1() {
            InitializeComponent();
        }

        //__________________________ MENU - OTEVŘÍT _____________________________
        //________________ do pathCsv se uloží cesta k souboru __________________
        //___________ data se uloží do kolekce a vypíší se v listBoxu ___________

        private void menuOpenFile_Click(object sender, EventArgs e) {
            openFileDialog1.Title = "Zvolte soubor";
            openFileDialog1.Filter = "soubory csv (*.csv) |*.csv";
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            DialogResult dr = openFileDialog1.ShowDialog();

            if (dr == DialogResult.OK) {
                Reader.PathCsv = openFileDialog1.FileName;
                Reader.ReadCvs();
                lboxEmployees.Items.Clear();
                lboxEmployees.Items.AddRange(Reader.ReturnAll());
            }
        }

        //_________________________ VYČISTIT LISTBOX ____________________________

        private void btnClearListBox_Click(object sender, EventArgs e) {
            lboxEmployees.Items.Clear();
        }

        //____________________ BUTTON - ULOŽIT DO DATABÁZE ______________________
        //__________ vytvoří instanci cl. Database s cestou k souboru ___________
        //_______________________ uloží data do databáze ________________________

        private void btnSaveToDB_Click(object sender, EventArgs e) {
            saveFileDialog1.Filter = "Databáze (*.db) | *.db | Databáze (*.db3) | *.db3";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.Title = "Vyberte soubor";
            DialogResult dr = saveFileDialog1.ShowDialog();

            if (dr == DialogResult.OK) {
                Database db = new Database(saveFileDialog1.FileName);      // nová instance Database s cestou k souboru
                if (db.ImportDB()) {
                    lbSaveDB.ForeColor = Color.Lime;
                    lbSaveDB.Text = "        Data byla uložena";
                }
            }

           
        }

        //_____________________ BUTTON - NAČÍST Z DATABÁZE ______________________
        //__________ vytvoří instanci cl. Database s cestou k souboru ___________
        //______________________ vypíše data do ListBoxu ________________________

        private void btnLoadFromDB_Click(object sender, EventArgs e) {
            openFileDialog1.Title = "Zvolte soubor";
            openFileDialog1.Filter = "Databáze (*.db) | *.db | Databáze (*.db3) | *.db3";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            DialogResult dr = openFileDialog1.ShowDialog();

            if (dr == DialogResult.OK) {
                Database db = new Database(openFileDialog1.FileName);      // nová instance Database s cestou k souboru
                db.ReadDB();
                lboxEmployees.Items.Clear();
                lboxEmployees.Items.AddRange(Reader.ReturnAll());

                lbDateOfSave.Text = string.Format("Import ze dne {0:d}", File.GetLastWriteTime(openFileDialog1.FileName));
            }

        }

        //_____________________________ NÁPOVĚDA ________________________________

        private void menuHelp_Click(object sender, EventArgs e) {
            MessageBox.Show("Nejprve načtěte data z .csv souboru - nahoře z menu Soubor vedle Nápovědy. \n \n" +
                "Poté načtené záznamy uložte do databáze. \n \n" +
                "Pro kontrolu můžete data načíst z databáze. Před načtením vyčistěte textový výpis dat tlačítkem: Smazat přehled.", "Nápověda");
        }
    }

}
