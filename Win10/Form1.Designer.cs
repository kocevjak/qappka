namespace qappka_1._0_win
{
    partial class qappka
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(qappka));
            this.start_kapka = new System.Windows.Forms.TabControl();
            this.dalkovaSpoust = new System.Windows.Forms.TabPage();
            this.vyfot_dalkova_spoust = new System.Windows.Forms.Button();
            this.kapka = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.count_pocetKapek = new System.Windows.Forms.NumericUpDown();
            this.count_meziKapkami = new System.Windows.Forms.NumericUpDown();
            this.count_predBleskem = new System.Windows.Forms.NumericUpDown();
            this.count_velikostKapek = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.casosber = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.count_pocetFotek = new System.Windows.Forms.NumericUpDown();
            this.count_mezifotkami = new System.Windows.Forms.NumericUpDown();
            this.start_casosober = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.vyska = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cas_kalkulacka = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.COMPort = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.connect = new System.Windows.Forms.Button();
            this.start_kapka.SuspendLayout();
            this.dalkovaSpoust.SuspendLayout();
            this.kapka.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.count_pocetKapek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.count_meziKapkami)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.count_predBleskem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.count_velikostKapek)).BeginInit();
            this.casosber.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.count_pocetFotek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.count_mezifotkami)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vyska)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // start_kapka
            // 
            this.start_kapka.Controls.Add(this.dalkovaSpoust);
            this.start_kapka.Controls.Add(this.kapka);
            this.start_kapka.Controls.Add(this.casosber);
            this.start_kapka.Controls.Add(this.tabPage1);
            resources.ApplyResources(this.start_kapka, "start_kapka");
            this.start_kapka.Name = "start_kapka";
            this.start_kapka.SelectedIndex = 0;
            // 
            // dalkovaSpoust
            // 
            this.dalkovaSpoust.Controls.Add(this.vyfot_dalkova_spoust);
            resources.ApplyResources(this.dalkovaSpoust, "dalkovaSpoust");
            this.dalkovaSpoust.Name = "dalkovaSpoust";
            this.dalkovaSpoust.UseVisualStyleBackColor = true;
            // 
            // vyfot_dalkova_spoust
            // 
            resources.ApplyResources(this.vyfot_dalkova_spoust, "vyfot_dalkova_spoust");
            this.vyfot_dalkova_spoust.Name = "vyfot_dalkova_spoust";
            this.vyfot_dalkova_spoust.UseVisualStyleBackColor = true;
            this.vyfot_dalkova_spoust.Click += new System.EventHandler(this.vyfot_dalkova_spoust_Click);
            // 
            // kapka
            // 
            this.kapka.Controls.Add(this.tableLayoutPanel1);
            this.kapka.Controls.Add(this.button1);
            resources.ApplyResources(this.kapka, "kapka");
            this.kapka.Name = "kapka";
            this.kapka.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.count_pocetKapek, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.count_meziKapkami, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.count_predBleskem, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.count_velikostKapek, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // count_pocetKapek
            // 
            resources.ApplyResources(this.count_pocetKapek, "count_pocetKapek");
            this.count_pocetKapek.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.count_pocetKapek.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.count_pocetKapek.Name = "count_pocetKapek";
            this.count_pocetKapek.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // count_meziKapkami
            // 
            resources.ApplyResources(this.count_meziKapkami, "count_meziKapkami");
            this.count_meziKapkami.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.count_meziKapkami.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.count_meziKapkami.Name = "count_meziKapkami";
            this.count_meziKapkami.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // count_predBleskem
            // 
            resources.ApplyResources(this.count_predBleskem, "count_predBleskem");
            this.count_predBleskem.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.count_predBleskem.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.count_predBleskem.Name = "count_predBleskem";
            this.count_predBleskem.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // count_velikostKapek
            // 
            resources.ApplyResources(this.count_velikostKapek, "count_velikostKapek");
            this.count_velikostKapek.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.count_velikostKapek.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.count_velikostKapek.Name = "count_velikostKapek";
            this.count_velikostKapek.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // casosber
            // 
            this.casosber.Controls.Add(this.tableLayoutPanel2);
            this.casosber.Controls.Add(this.start_casosober);
            resources.ApplyResources(this.casosber, "casosber");
            this.casosber.Name = "casosber";
            this.casosber.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.count_pocetFotek, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.count_mezifotkami, 1, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // count_pocetFotek
            // 
            resources.ApplyResources(this.count_pocetFotek, "count_pocetFotek");
            this.count_pocetFotek.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.count_pocetFotek.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.count_pocetFotek.Name = "count_pocetFotek";
            this.count_pocetFotek.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // count_mezifotkami
            // 
            resources.ApplyResources(this.count_mezifotkami, "count_mezifotkami");
            this.count_mezifotkami.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.count_mezifotkami.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.count_mezifotkami.Name = "count_mezifotkami";
            this.count_mezifotkami.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // start_casosober
            // 
            resources.ApplyResources(this.start_casosober, "start_casosober");
            this.start_casosober.Name = "start_casosober";
            this.start_casosober.UseVisualStyleBackColor = true;
            this.start_casosober.Click += new System.EventHandler(this.start_casosober_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel4);
            this.tabPage1.Controls.Add(this.button2);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.vyska, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.cas_kalkulacka, 1, 1);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // vyska
            // 
            resources.ApplyResources(this.vyska, "vyska");
            this.vyska.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.vyska.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.vyska.Name = "vyska";
            this.vyska.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // cas_kalkulacka
            // 
            resources.ApplyResources(this.cas_kalkulacka, "cas_kalkulacka");
            this.cas_kalkulacka.Name = "cas_kalkulacka";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // COMPort
            // 
            resources.ApplyResources(this.COMPort, "COMPort");
            this.COMPort.FormattingEnabled = true;
            this.COMPort.Name = "COMPort";
            this.COMPort.Click += new System.EventHandler(this.COMPort_Click);
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.COMPort, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.connect, 1, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // connect
            // 
            resources.ApplyResources(this.connect, "connect");
            this.connect.Name = "connect";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // qappka
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.start_kapka);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "qappka";
            this.start_kapka.ResumeLayout(false);
            this.dalkovaSpoust.ResumeLayout(false);
            this.dalkovaSpoust.PerformLayout();
            this.kapka.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.count_pocetKapek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.count_meziKapkami)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.count_predBleskem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.count_velikostKapek)).EndInit();
            this.casosber.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.count_pocetFotek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.count_mezifotkami)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vyska)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl start_kapka;
        private TabPage dalkovaSpoust;
        private TabPage kapka;
        private TabPage casosber;
        private ComboBox COMPort;
        private Button vyfot_dalkova_spoust;
        private Button button1;
        private Button start_casosober;
        private TableLayoutPanel tableLayoutPanel1;
        private NumericUpDown count_pocetKapek;
        private NumericUpDown count_meziKapkami;
        private NumericUpDown count_predBleskem;
        private NumericUpDown count_velikostKapek;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label5;
        private Label label6;
        private NumericUpDown count_pocetFotek;
        private NumericUpDown count_mezifotkami;
        private TableLayoutPanel tableLayoutPanel3;
        private Button connect;
        private TabPage tabPage1;
        private Button button2;
        private TableLayoutPanel tableLayoutPanel4;
        private NumericUpDown vyska;
        private Label label7;
        private Label label8;
        private TextBox cas_kalkulacka;
    }
}