namespace FrequentPatternMining.Presentation.WinForm
{
    partial class FrequentPatternGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label_algoritmo = new System.Windows.Forms.Label();
            this.combo_algoritmo = new System.Windows.Forms.ComboBox();
            this.label_supporto = new System.Windows.Forms.Label();
            this.label_confidenza = new System.Windows.Forms.Label();
            this.support_combo = new System.Windows.Forms.ComboBox();
            this.confidence_combo = new System.Windows.Forms.ComboBox();
            this.Start_button = new System.Windows.Forms.Button();
            this.Show_button = new System.Windows.Forms.Button();
            this.LoadTrans_button = new System.Windows.Forms.Button();
            this.dataGridViewAssociationRules = new System.Windows.Forms.DataGridView();
            this.RuleNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingNavigatorAssociationRules = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStripFPGUI = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadAssociationRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAssociationRuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printAssociationRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialogAR = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogAR = new System.Windows.Forms.SaveFileDialog();
            this.Loading_label = new System.Windows.Forms.Label();
            this.Extract_label = new System.Windows.Forms.Label();
            this.Rules_label = new System.Windows.Forms.Label();
            this.MyPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.confidenceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supportDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leftSideDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rightSideDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.associationRuleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.aboutUsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAssociationRules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorAssociationRules)).BeginInit();
            this.menuStripFPGUI.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.associationRuleBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label_algoritmo
            // 
            this.label_algoritmo.AutoSize = true;
            this.label_algoritmo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_algoritmo.Location = new System.Drawing.Point(20, 39);
            this.label_algoritmo.Name = "label_algoritmo";
            this.label_algoritmo.Size = new System.Drawing.Size(59, 15);
            this.label_algoritmo.TabIndex = 0;
            this.label_algoritmo.Text = "Algorithm";
            // 
            // combo_algoritmo
            // 
            this.combo_algoritmo.FormattingEnabled = true;
            this.combo_algoritmo.Location = new System.Drawing.Point(23, 57);
            this.combo_algoritmo.Name = "combo_algoritmo";
            this.combo_algoritmo.Size = new System.Drawing.Size(216, 21);
            this.combo_algoritmo.TabIndex = 1;
            // 
            // label_supporto
            // 
            this.label_supporto.AutoSize = true;
            this.label_supporto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_supporto.Location = new System.Drawing.Point(20, 105);
            this.label_supporto.Name = "label_supporto";
            this.label_supporto.Size = new System.Drawing.Size(126, 15);
            this.label_supporto.TabIndex = 2;
            this.label_supporto.Text = "Minimum support (%)";
            // 
            // label_confidenza
            // 
            this.label_confidenza.AutoSize = true;
            this.label_confidenza.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_confidenza.Location = new System.Drawing.Point(20, 152);
            this.label_confidenza.Name = "label_confidenza";
            this.label_confidenza.Size = new System.Drawing.Size(145, 15);
            this.label_confidenza.TabIndex = 3;
            this.label_confidenza.Text = "Minimum confidence (%)";
            // 
            // support_combo
            // 
            this.support_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.support_combo.FormattingEnabled = true;
            this.support_combo.Items.AddRange(new object[] {
            "0,2",
            "0,5",
            "1,0",
            "1,5",
            "2,0",
            "2,5",
            "3,0",
            "3,5",
            "4,0",
            "4,5",
            "5,0",
            "10,0",
            "20,0",
            "30,0",
            "40,0",
            "50,0",
            "60,0",
            "70,0",
            "80,0",
            "90,0"});
            this.support_combo.Location = new System.Drawing.Point(163, 99);
            this.support_combo.Name = "support_combo";
            this.support_combo.Size = new System.Drawing.Size(76, 21);
            this.support_combo.TabIndex = 4;
            // 
            // confidence_combo
            // 
            this.confidence_combo.FormattingEnabled = true;
            this.confidence_combo.Items.AddRange(new object[] {
            "0,0",
            "10,0",
            "20,0",
            "30,0",
            "40,0",
            "45,0",
            "50,0",
            "55,0",
            "60,0",
            "65,0",
            "70,0",
            "75,0",
            "80,0",
            "85,0",
            "90,0",
            "95,0"});
            this.confidence_combo.Location = new System.Drawing.Point(171, 146);
            this.confidence_combo.Name = "confidence_combo";
            this.confidence_combo.Size = new System.Drawing.Size(68, 21);
            this.confidence_combo.TabIndex = 5;
            // 
            // Start_button
            // 
            this.Start_button.Enabled = false;
            this.Start_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start_button.Location = new System.Drawing.Point(23, 239);
            this.Start_button.Name = "Start_button";
            this.Start_button.Size = new System.Drawing.Size(216, 23);
            this.Start_button.TabIndex = 6;
            this.Start_button.Text = "Extract Frequent Pattern ";
            this.Start_button.UseVisualStyleBackColor = true;
            this.Start_button.Click += new System.EventHandler(this.Start_button_Click);
            // 
            // Show_button
            // 
            this.Show_button.Enabled = false;
            this.Show_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Show_button.Location = new System.Drawing.Point(23, 282);
            this.Show_button.Name = "Show_button";
            this.Show_button.Size = new System.Drawing.Size(216, 23);
            this.Show_button.TabIndex = 7;
            this.Show_button.Text = "Show Association Rules";
            this.Show_button.UseVisualStyleBackColor = true;
            this.Show_button.Click += new System.EventHandler(this.Show_button_Click);
            // 
            // LoadTrans_button
            // 
            this.LoadTrans_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadTrans_button.Location = new System.Drawing.Point(23, 197);
            this.LoadTrans_button.Name = "LoadTrans_button";
            this.LoadTrans_button.Size = new System.Drawing.Size(216, 23);
            this.LoadTrans_button.TabIndex = 8;
            this.LoadTrans_button.Text = "Load Transaction";
            this.LoadTrans_button.UseVisualStyleBackColor = true;
            this.LoadTrans_button.Click += new System.EventHandler(this.LoadTrans_button_Click);
            // 
            // dataGridViewAssociationRules
            // 
            this.dataGridViewAssociationRules.AllowUserToAddRows = false;
            this.dataGridViewAssociationRules.AllowUserToDeleteRows = false;
            this.dataGridViewAssociationRules.AllowUserToOrderColumns = true;
            this.dataGridViewAssociationRules.AutoGenerateColumns = false;
            this.dataGridViewAssociationRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAssociationRules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RuleNum,
            this.confidenceDataGridViewTextBoxColumn,
            this.supportDataGridViewTextBoxColumn,
            this.leftSideDataGridViewTextBoxColumn,
            this.rightSideDataGridViewTextBoxColumn});
            this.dataGridViewAssociationRules.DataSource = this.associationRuleBindingSource;
            this.dataGridViewAssociationRules.Dock = System.Windows.Forms.DockStyle.Right;
            this.dataGridViewAssociationRules.Location = new System.Drawing.Point(6, 16);
            this.dataGridViewAssociationRules.Name = "dataGridViewAssociationRules";
            this.dataGridViewAssociationRules.ReadOnly = true;
            this.dataGridViewAssociationRules.Size = new System.Drawing.Size(546, 262);
            this.dataGridViewAssociationRules.TabIndex = 11;
            // 
            // RuleNum
            // 
            this.RuleNum.FillWeight = 50F;
            this.RuleNum.HeaderText = "RuleNum";
            this.RuleNum.Name = "RuleNum";
            this.RuleNum.ReadOnly = true;
            this.RuleNum.ToolTipText = "Number of association rule";
            this.RuleNum.Width = 75;
            // 
            // bindingNavigatorAssociationRules
            // 
            this.bindingNavigatorAssociationRules.AddNewItem = null;
            this.bindingNavigatorAssociationRules.CountItem = null;
            this.bindingNavigatorAssociationRules.DeleteItem = null;
            this.bindingNavigatorAssociationRules.Location = new System.Drawing.Point(0, 24);
            this.bindingNavigatorAssociationRules.MoveFirstItem = null;
            this.bindingNavigatorAssociationRules.MoveLastItem = null;
            this.bindingNavigatorAssociationRules.MoveNextItem = null;
            this.bindingNavigatorAssociationRules.MovePreviousItem = null;
            this.bindingNavigatorAssociationRules.Name = "bindingNavigatorAssociationRules";
            this.bindingNavigatorAssociationRules.PositionItem = null;
            this.bindingNavigatorAssociationRules.Size = new System.Drawing.Size(800, 25);
            this.bindingNavigatorAssociationRules.TabIndex = 18;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(23, 23);
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 23);
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 23);
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 6);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(100, 23);
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 6);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 23);
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 23);
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 6);
            // 
            // menuStripFPGUI
            // 
            this.menuStripFPGUI.BackColor = System.Drawing.Color.OldLace;
            this.menuStripFPGUI.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutUsToolStripMenuItem});
            this.menuStripFPGUI.Location = new System.Drawing.Point(0, 0);
            this.menuStripFPGUI.Name = "menuStripFPGUI";
            this.menuStripFPGUI.Size = new System.Drawing.Size(800, 24);
            this.menuStripFPGUI.TabIndex = 13;
            this.menuStripFPGUI.Text = "menuStripFPGUI";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadAssociationRulesToolStripMenuItem,
            this.saveAssociationRuleToolStripMenuItem,
            this.printAssociationRulesToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadAssociationRulesToolStripMenuItem
            // 
            this.loadAssociationRulesToolStripMenuItem.Name = "loadAssociationRulesToolStripMenuItem";
            this.loadAssociationRulesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.loadAssociationRulesToolStripMenuItem.Text = "&Load Association Rules";
            this.loadAssociationRulesToolStripMenuItem.Click += new System.EventHandler(this.loadAssociationRulesToolStripMenuItem_Click);
            // 
            // saveAssociationRuleToolStripMenuItem
            // 
            this.saveAssociationRuleToolStripMenuItem.Name = "saveAssociationRuleToolStripMenuItem";
            this.saveAssociationRuleToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveAssociationRuleToolStripMenuItem.Text = "&Save Association Rules";
            this.saveAssociationRuleToolStripMenuItem.Click += new System.EventHandler(this.saveAssociationRuleToolStripMenuItem_Click);
            // 
            // printAssociationRulesToolStripMenuItem
            // 
            this.printAssociationRulesToolStripMenuItem.Name = "printAssociationRulesToolStripMenuItem";
            this.printAssociationRulesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.printAssociationRulesToolStripMenuItem.Text = "&Print Association Rules";
            this.printAssociationRulesToolStripMenuItem.Click += new System.EventHandler(this.printAssociationRulesToolStripMenuItem_Click);
            // 
            // Loading_label
            // 
            this.Loading_label.AutoSize = true;
            this.Loading_label.Location = new System.Drawing.Point(20, 181);
            this.Loading_label.Name = "Loading_label";
            this.Loading_label.Size = new System.Drawing.Size(10, 13);
            this.Loading_label.TabIndex = 14;
            this.Loading_label.Text = " ";
            // 
            // Extract_label
            // 
            this.Extract_label.AutoSize = true;
            this.Extract_label.Location = new System.Drawing.Point(20, 223);
            this.Extract_label.Name = "Extract_label";
            this.Extract_label.Size = new System.Drawing.Size(10, 13);
            this.Extract_label.TabIndex = 15;
            this.Extract_label.Text = " ";
            // 
            // Rules_label
            // 
            this.Rules_label.AutoSize = true;
            this.Rules_label.Location = new System.Drawing.Point(20, 266);
            this.Rules_label.Name = "Rules_label";
            this.Rules_label.Size = new System.Drawing.Size(10, 13);
            this.Rules_label.TabIndex = 16;
            this.Rules_label.Text = " ";
            // 
            // MyPrintDocument
            // 
            this.MyPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.MyPrintDocument_PrintPage);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewAssociationRules);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(245, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(555, 281);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Association Rules";
            // 
            // confidenceDataGridViewTextBoxColumn
            // 
            this.confidenceDataGridViewTextBoxColumn.DataPropertyName = "Confidence";
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.confidenceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.confidenceDataGridViewTextBoxColumn.FillWeight = 26F;
            this.confidenceDataGridViewTextBoxColumn.HeaderText = "Conf";
            this.confidenceDataGridViewTextBoxColumn.Name = "confidenceDataGridViewTextBoxColumn";
            this.confidenceDataGridViewTextBoxColumn.ReadOnly = true;
            this.confidenceDataGridViewTextBoxColumn.ToolTipText = "Confidence";
            this.confidenceDataGridViewTextBoxColumn.Width = 50;
            // 
            // supportDataGridViewTextBoxColumn
            // 
            this.supportDataGridViewTextBoxColumn.DataPropertyName = "Support";
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.supportDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.supportDataGridViewTextBoxColumn.FillWeight = 45.79218F;
            this.supportDataGridViewTextBoxColumn.HeaderText = "Support";
            this.supportDataGridViewTextBoxColumn.Name = "supportDataGridViewTextBoxColumn";
            this.supportDataGridViewTextBoxColumn.ReadOnly = true;
            this.supportDataGridViewTextBoxColumn.ToolTipText = "Support";
            this.supportDataGridViewTextBoxColumn.Width = 50;
            // 
            // leftSideDataGridViewTextBoxColumn
            // 
            this.leftSideDataGridViewTextBoxColumn.DataPropertyName = "LeftSide";
            this.leftSideDataGridViewTextBoxColumn.FillWeight = 45.79218F;
            this.leftSideDataGridViewTextBoxColumn.HeaderText = "Left Side";
            this.leftSideDataGridViewTextBoxColumn.Name = "leftSideDataGridViewTextBoxColumn";
            this.leftSideDataGridViewTextBoxColumn.ReadOnly = true;
            this.leftSideDataGridViewTextBoxColumn.ToolTipText = "Left Side of an Association Rule";
            this.leftSideDataGridViewTextBoxColumn.Width = 150;
            // 
            // rightSideDataGridViewTextBoxColumn
            // 
            this.rightSideDataGridViewTextBoxColumn.DataPropertyName = "RightSide";
            this.rightSideDataGridViewTextBoxColumn.FillWeight = 45.79218F;
            this.rightSideDataGridViewTextBoxColumn.HeaderText = "Right Side";
            this.rightSideDataGridViewTextBoxColumn.Name = "rightSideDataGridViewTextBoxColumn";
            this.rightSideDataGridViewTextBoxColumn.ReadOnly = true;
            this.rightSideDataGridViewTextBoxColumn.ToolTipText = "Right Side of an Association Rule";
            this.rightSideDataGridViewTextBoxColumn.Width = 150;
            // 
            // associationRuleBindingSource
            // 
            this.associationRuleBindingSource.DataSource = typeof(FrequentPatternMining.Entities.AssociationRule);
            // 
            // aboutUsToolStripMenuItem
            // 
            this.aboutUsToolStripMenuItem.Name = "aboutUsToolStripMenuItem";
            this.aboutUsToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.aboutUsToolStripMenuItem.Text = "&About Us";
            this.aboutUsToolStripMenuItem.Click += new System.EventHandler(this.aboutUsToolStripMenuItem_Click);
            // 
            // FrequentPatternGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 330);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Rules_label);
            this.Controls.Add(this.Extract_label);
            this.Controls.Add(this.Loading_label);
            this.Controls.Add(this.bindingNavigatorAssociationRules);
            this.Controls.Add(this.menuStripFPGUI);
            this.Controls.Add(this.LoadTrans_button);
            this.Controls.Add(this.Show_button);
            this.Controls.Add(this.Start_button);
            this.Controls.Add(this.confidence_combo);
            this.Controls.Add(this.support_combo);
            this.Controls.Add(this.label_confidenza);
            this.Controls.Add(this.label_supporto);
            this.Controls.Add(this.combo_algoritmo);
            this.Controls.Add(this.label_algoritmo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MainMenuStrip = this.menuStripFPGUI;
            this.MaximumSize = new System.Drawing.Size(806, 768);
            this.Name = "FrequentPatternGUI";
            this.Text = "FrequentPatternGUI";
            this.Load += new System.EventHandler(this.FrequentPatternGUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAssociationRules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorAssociationRules)).EndInit();
            this.menuStripFPGUI.ResumeLayout(false);
            this.menuStripFPGUI.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.associationRuleBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_algoritmo;
        private System.Windows.Forms.ComboBox combo_algoritmo;
        private System.Windows.Forms.Label label_supporto;
        private System.Windows.Forms.Label label_confidenza;
        private System.Windows.Forms.ComboBox support_combo;
        private System.Windows.Forms.ComboBox confidence_combo;
        private System.Windows.Forms.Button Start_button;
        private System.Windows.Forms.Button Show_button;
        private System.Windows.Forms.Button LoadTrans_button;
        private System.Windows.Forms.DataGridView dataGridViewAssociationRules;
        private System.Windows.Forms.BindingSource associationRuleBindingSource;
        private System.Windows.Forms.BindingNavigator bindingNavigatorAssociationRules;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.MenuStrip menuStripFPGUI;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAssociationRuleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadAssociationRulesToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogAR;
        private System.Windows.Forms.SaveFileDialog saveFileDialogAR;
        private System.Windows.Forms.Label Loading_label;
        private System.Windows.Forms.Label Extract_label;
        private System.Windows.Forms.Label Rules_label;
        private System.Windows.Forms.ToolStripMenuItem printAssociationRulesToolStripMenuItem;
        private System.Drawing.Printing.PrintDocument MyPrintDocument;
        private System.Windows.Forms.DataGridViewTextBoxColumn RuleNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn confidenceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn supportDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn leftSideDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rightSideDataGridViewTextBoxColumn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem aboutUsToolStripMenuItem;

    }
}

