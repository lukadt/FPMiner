using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FrequentPatternMining.Entities;
using FrequentPatternMining.BusinessLogic;
using FrequentPatternMining.IFrequentMining;
using System.Reflection;
using System.Configuration;
using System.Xml.Serialization;
using System.Xml;
using System.Globalization;
using System.Collections;
using System.IO;
using System.Drawing.Printing;

namespace FrequentPatternMining.Presentation.WinForm
{
    public partial class FrequentPatternGUI : Form
    {
        private MiningManager _managerDataMining;
        List<Transaction> _transazioniItemset = default(List<Transaction>);
        List<AssociationRule> _rules;
        private Map _actualMap;
        DataGridViewPrinter MyDataGridViewPrinter;

        public FrequentPatternGUI()
        {
            InitializeComponent();
            _managerDataMining = new MiningManager();
            _rules = new List<AssociationRule>();
            _actualMap = new Map();
        }

        private void FrequentPatternGUI_Load(object sender, EventArgs e)
        {
            List<IFrequentPatternMining> _miningAlgorithms = _managerDataMining.AlgorithmManager.MiningAlgorithms;
            PopulateAlgorithmChooseControl(_miningAlgorithms);
            combo_algoritmo.DataSource = _miningAlgorithms;
            support_combo.SelectedIndex = 1;
            confidence_combo.SelectedIndex = 6;
            this.TopMost = false;
            this.saveAssociationRuleToolStripMenuItem.Enabled = false;
            this.printAssociationRulesToolStripMenuItem.Enabled = false;
        }

        private void PopulateAlgorithmChooseControl(List<IFrequentPatternMining> _miningAlgorithms)
        {
            foreach (IFrequentPatternMining detector in _miningAlgorithms)
            {
                AssemblyDescriptionAttribute desc;
                AssemblyTitleAttribute title;
                desc = (AssemblyDescriptionAttribute)AssemblyDescriptionAttribute.GetCustomAttribute(detector.GetType().Assembly, typeof(AssemblyDescriptionAttribute));
                title = (AssemblyTitleAttribute)AssemblyTitleAttribute.GetCustomAttribute(detector.GetType().Assembly, typeof(AssemblyTitleAttribute));
                string str = string.Format("{0}, {1}, {2}", detector.GetType().FullName, title, desc);
                combo_algoritmo.Items.Add(str);
            }
        }

        private void InitializeConfiguration()
        {
            String SelectedSupport = support_combo.Items[support_combo.SelectedIndex].ToString();
            String SelectedConfidence = confidence_combo.Items[confidence_combo.SelectedIndex].ToString();
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings["SupportoMinimo"] != null)
            {
                config.AppSettings.Settings.Remove("SupportoMinimo");
            }
            config.AppSettings.Settings.Add("SupportoMinimo", SelectedSupport);
            if (config.AppSettings.Settings["ConfidenzaMinima"] != null)
            {
                config.AppSettings.Settings.Remove("ConfidenzaMinima");
            }
            config.AppSettings.Settings.Add("ConfidenzaMinima", SelectedConfidence);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void LoadTrans_button_Click(object sender, EventArgs e)
        {
            Loading_label.Text = "Loading data...";
            Loading_label.Refresh();
            Extract_label.Text = "";
            Extract_label.Refresh();
            Rules_label.Text = "";
            Rules_label.Refresh();

            if (_rules != null)
                _rules.Clear();
            this.dataGridViewAssociationRules.DataSource = null;
            this.loadAssociationRulesToolStripMenuItem.Enabled = false;
            this.saveAssociationRuleToolStripMenuItem.Enabled = false;
            this.LoadTrans_button.Enabled = false;
            this.dataGridViewAssociationRules.Refresh();
            if (_transazioniItemset == default(List<Transaction>))
            {
                _transazioniItemset = _managerDataMining.GetAllTransactions();
                _actualMap = _managerDataMining.GetMap();
            }

            Start_button.Enabled = true;
            Loading_label.Text = "Data loaded";
            Loading_label.Refresh();
        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            Extract_label.Text = "Extracting frequent patterns...";
            Extract_label.Refresh();
            InitializeConfiguration();
            Start_button.Enabled = false;
            _managerDataMining.AlgorithmManager.MiningAlgorithms[combo_algoritmo.SelectedIndex].SetMinSup(_managerDataMining.AlgorithmManager.Minsupp);
            List<ItemSet> result = _managerDataMining.AlgorithmManager.MiningAlgorithms[combo_algoritmo.SelectedIndex].ExtractFrequentPattern(_transazioniItemset);
            
            _rules.Clear();
            _rules = _managerDataMining.GenerateAssociationRuleBase(result, this._transazioniItemset.Count);
            _managerDataMining = new MiningManager();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Show_button.Enabled = true;

            this._transazioniItemset = null;

            Extract_label.Text = "Patterns extracted";
            Extract_label.Refresh();
        }

        private void Show_button_Click(object sender, EventArgs e)
        {
            Show_button.Enabled = false;
            if (_rules.Count == 0)
            {
                MessageBox.Show("No rules to display!\nRetry selecting a smaller value for the confidence.", "Advice",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            dataGridViewAssociationRules.DataSource = null;
            associationRuleBindingSource.DataSource = null;

            FillDataGridView();


            LoadTrans_button.Enabled = true;
            loadAssociationRulesToolStripMenuItem.Enabled = true;
            saveAssociationRuleToolStripMenuItem.Enabled = true;
            printAssociationRulesToolStripMenuItem.Enabled = true;
            Rules_label.Text = "Rules displayed";
            Rules_label.Refresh();
            Loading_label.Text = "";
            Loading_label.Refresh();
            Extract_label.Text = "";
            Extract_label.Refresh();
        }

        private void FillDataGridViewAfterLoading()
        {
            this.dataGridViewAssociationRules.Rows.Clear();
            StringBuilder lsbuilder, rsbuilder;
            for (int i = 0; i < _rules.Count; i++)
            {
                lsbuilder = new StringBuilder();
                rsbuilder = new StringBuilder();
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell confidencecell, supportcell, leftcell, rightcell, numcell;
                confidencecell = new DataGridViewTextBoxCell();
                supportcell = new DataGridViewTextBoxCell();
                leftcell = new DataGridViewTextBoxCell();
                rightcell = new DataGridViewTextBoxCell();
                numcell = new DataGridViewTextBoxCell();

                numcell.Value = i + 1;
                row.Cells.Add(numcell);
                confidencecell.Value = _rules[i].Confidence;
                row.Cells.Add(confidencecell);
                supportcell.Value = _rules[i].Support;
                row.Cells.Add(supportcell);

                for (int j = 0; j < _rules[i].LeftSide.ItemsNumber - 1; j++)
                {
                    lsbuilder.Append(_actualMap.Hash[_rules[i].LeftSide.Items[j].ToString()].ToString().Trim());
                    lsbuilder.Append(new char[] { ',', ' ' });
                }

                lsbuilder.Append(_actualMap.Hash[_rules[i].LeftSide.Items[_rules[i].LeftSide.ItemsNumber - 1].ToString()].ToString().Trim());

                leftcell.Value = lsbuilder.ToString();
                row.Cells.Add(leftcell);

                for (int k = 0; k < _rules[i].RightSide.ItemsNumber - 1; k++)
                {
                    rsbuilder.Append(_actualMap.Hash[_rules[i].RightSide.Items[k].ToString()].ToString().Trim());
                    rsbuilder.Append(new char[] { ',', ' ' });
                }
                rsbuilder.Append(_actualMap.Hash[_rules[i].RightSide.Items[_rules[i].RightSide.ItemsNumber - 1].ToString()].ToString().Trim());

                rightcell.Value = rsbuilder.ToString();
                row.Cells.Add(rightcell);
                this.dataGridViewAssociationRules.Rows.Insert(i, row);
            }
        }

        private void FillDataGridView()
        {
            this.dataGridViewAssociationRules.Rows.Clear();
            StringBuilder lsbuilder, rsbuilder;
            for (int i = 0; i < _rules.Count; i++)
            {
                lsbuilder = new StringBuilder();
                rsbuilder = new StringBuilder();
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell confidencecell, supportcell, leftcell, rightcell, numcell;
                confidencecell = new DataGridViewTextBoxCell();
                supportcell = new DataGridViewTextBoxCell();
                leftcell = new DataGridViewTextBoxCell();
                rightcell = new DataGridViewTextBoxCell();
                numcell = new DataGridViewTextBoxCell();

                numcell.Value = i + 1;
                row.Cells.Add(numcell);
                confidencecell.Value = _rules[i].Confidence;
                row.Cells.Add(confidencecell);
                supportcell.Value = _rules[i].Support;
                row.Cells.Add(supportcell);

                for (int j = 0; j < _rules[i].LeftSide.ItemsNumber - 1; j++)
                {
                    lsbuilder.Append(_actualMap.Hash[_rules[i].LeftSide.Items[j]].ToString().Trim());
                    lsbuilder.Append(new char[] { ',', ' ' });
                }

                lsbuilder.Append(_actualMap.Hash[_rules[i].LeftSide.Items[_rules[i].LeftSide.ItemsNumber - 1]].ToString().Trim());

                leftcell.Value = lsbuilder.ToString();
                row.Cells.Add(leftcell);

                for (int k = 0; k < _rules[i].RightSide.ItemsNumber - 1; k++)
                {
                    rsbuilder.Append(_actualMap.Hash[_rules[i].RightSide.Items[k]].ToString().Trim());
                    rsbuilder.Append(new char[] { ',', ' ' });
                }
                rsbuilder.Append(_actualMap.Hash[_rules[i].RightSide.Items[_rules[i].RightSide.ItemsNumber - 1]].ToString().Trim());

                rightcell.Value = rsbuilder.ToString();
                row.Cells.Add(rightcell);
                this.dataGridViewAssociationRules.Rows.Insert(i, row);
            }
        }

        private void saveAssociationRuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialogAR.Filter = "Rules files (*.rules)|*.rules";
            saveFileDialogAR.Title = "Save computed Association Rules";
            saveFileDialogAR.FileName = String.Format("{0}Rules_Conf_{1}_Support_{2}.rules", combo_algoritmo.SelectedItem.ToString(), confidence_combo.SelectedItem.ToString(), support_combo.SelectedItem.ToString());
            saveFileDialogAR.ShowDialog();
            if (saveFileDialogAR.FileName != String.Empty)
            {
                //Serializza l'ogetto in xml
                XmlSerializer serializerAR = new XmlSerializer(_rules.GetType());
                //FileStream stream = new FileStream(saveFileDialogAR.FileName, FileMode.Create);
                XmlTextWriter xmlTextWriter = new XmlTextWriter(saveFileDialogAR.FileName, null);
                xmlTextWriter.Formatting = Formatting.Indented;
                xmlTextWriter.WriteStartDocument(false);
                xmlTextWriter.WriteStartElement("CustomObjects");

                //  <AssociationRule>
                //  <Confidence>0.93333333333333335</Confidence>
                //  <Support>0.063194444444444442</Support>
                //  <LeftSide>
                //    <ItemsSupport>195</ItemsSupport>
                //    <Items>
                //      <int>25</int>
                //    </Items>
                //  </LeftSide>
                //  <RightSide>
                //    <ItemsSupport>275</ItemsSupport>
                //    <Items>
                //      <int>26</int>
                //    </Items>
                //  </RightSide>
                //</AssociationRule>              

                xmlTextWriter.WriteStartElement("ArrayOfAssociationRule");
                foreach (AssociationRule rule in this._rules)
                {
                    xmlTextWriter.WriteStartElement("AssociationRule", null);
                    xmlTextWriter.WriteElementString("Confidence", null, rule.Confidence.ToString(CultureInfo.InvariantCulture));
                    xmlTextWriter.WriteElementString("Support", null, rule.Support.ToString(CultureInfo.InvariantCulture));
                    xmlTextWriter.WriteStartElement("LeftSide", null);
                    xmlTextWriter.WriteElementString("ItemsSupport", null, rule.LeftSide.ItemsSupport.ToString(CultureInfo.InvariantCulture));
                    xmlTextWriter.WriteStartElement("Items", null);
                    foreach (int item in rule.LeftSide.Items)
                    {
                        xmlTextWriter.WriteElementString("int", null, item.ToString(CultureInfo.InvariantCulture));
                    }
                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteEndElement();

                    xmlTextWriter.WriteStartElement("RightSide", null);
                    xmlTextWriter.WriteElementString("ItemsSupport", null, rule.RightSide.ItemsSupport.ToString(CultureInfo.InvariantCulture));
                    xmlTextWriter.WriteStartElement("Items", null);
                    foreach (int item in rule.RightSide.Items)
                    {
                        xmlTextWriter.WriteElementString("int", null, item.ToString(CultureInfo.InvariantCulture));
                    }
                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteEndElement();

                    xmlTextWriter.WriteEndElement();
                }
                xmlTextWriter.WriteEndElement();

                // <HashTableXmlSerializer>
                //<Map>
                // <Item>
                // <Key>2723</Key>
                // <Value>Under the Volcano </Value>
                //  </Item>               

                xmlTextWriter.WriteStartElement("HashTableXmlSerializer");
                xmlTextWriter.WriteStartElement("Map");
                foreach (DictionaryEntry entry in _actualMap.Hash)
                {
                    xmlTextWriter.WriteStartElement("Item");
                    xmlTextWriter.WriteElementString("Key", null, entry.Key.ToString());
                    xmlTextWriter.WriteElementString("Value", null, entry.Value.ToString());
                    xmlTextWriter.WriteEndElement();
                }
                xmlTextWriter.WriteEndElement();


                xmlTextWriter.WriteEndElement();

                //Svuota il writer scrivendone il contenuto sul file xml
                xmlTextWriter.Flush();
                xmlTextWriter.Close();
            }
        }

        private void loadAssociationRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialogAR.Title = "Frequent Pattern Mining ";
            openFileDialogAR.InitialDirectory = @"c:\";
            openFileDialogAR.Filter = "Rules files (*.rules)|*.rules";
            openFileDialogAR.RestoreDirectory = true;
            if (openFileDialogAR.ShowDialog() == DialogResult.OK)
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(openFileDialogAR.FileName);
                XmlNode rulesNode = xmldoc.DocumentElement.ChildNodes[0];
                XmlNode hashNode = xmldoc.DocumentElement.ChildNodes[1];

                XmlSerializer serializer = new XmlSerializer(typeof(List<AssociationRule>));
                _rules = (List<AssociationRule>)serializer.Deserialize(new StringReader(rulesNode.OuterXml));
                _actualMap.Hash = (Hashtable)HashTableXmlSerializer.Deserialize(new StringReader(hashNode.OuterXml));

            }
            dataGridViewAssociationRules.DataSource = null;
            associationRuleBindingSource.DataSource = null;
            FillDataGridViewAfterLoading();
            saveAssociationRuleToolStripMenuItem.Enabled = true;
            printAssociationRulesToolStripMenuItem.Enabled = true;
        }

        private void printAssociationRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SetupThePrinting())
            {
                PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                MyPrintPreviewDialog.Document = MyPrintDocument;
                MyPrintPreviewDialog.ShowDialog();
            }
        }

        private bool SetupThePrinting()
        {
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;

            if (MyPrintDialog.ShowDialog() != DialogResult.OK)
                return false;

            MyPrintDocument.DocumentName = "Association Rule Report";
            MyPrintDocument.PrinterSettings = MyPrintDialog.PrinterSettings;
            MyPrintDocument.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            MyPrintDocument.DefaultPageSettings.Margins = new Margins(5, 5, 40, 40);
            if (MessageBox.Show("Do you want the report to be centered on the page", "Association Manager - Center on Page", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                MyDataGridViewPrinter = new DataGridViewPrinter(dataGridViewAssociationRules, MyPrintDocument, true, true, "Association Rules", new Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
            else
                MyDataGridViewPrinter = new DataGridViewPrinter(dataGridViewAssociationRules, MyPrintDocument, false, true, "Association Rules", new Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
            return true;
        }

        private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUs abus = new AboutUs();            
            abus.ShowDialog(this);
        }
    }
}