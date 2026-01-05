namespace bobinadeiraPC
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;



        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboPortas = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConectar = new System.Windows.Forms.Button();
            this.btnAtualizarPortas = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.numRPM = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numEspiras = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numDiametro = new System.Windows.Forms.NumericUpDown();
            this.btnEnviarConfig = new System.Windows.Forms.Button();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnParar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblAlarms = new System.Windows.Forms.Label();
            this.lblProgressStatus = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblVoltasAtual = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRPM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEspiras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiametro)).BeginInit();
            this.panel3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboPortas
            // 
            this.cboPortas.FormattingEnabled = true;
            this.cboPortas.Location = new System.Drawing.Point(26, 40);
            this.cboPortas.Name = "cboPortas";
            this.cboPortas.Size = new System.Drawing.Size(121, 23);
            this.cboPortas.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Portas";
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(26, 136);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(75, 23);
            this.btnConectar.TabIndex = 2;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // btnAtualizarPortas
            // 
            this.btnAtualizarPortas.Location = new System.Drawing.Point(26, 80);
            this.btnAtualizarPortas.Name = "btnAtualizarPortas";
            this.btnAtualizarPortas.Size = new System.Drawing.Size(75, 38);
            this.btnAtualizarPortas.TabIndex = 3;
            this.btnAtualizarPortas.Text = "Atualizar Portas";
            this.btnAtualizarPortas.UseVisualStyleBackColor = true;
            this.btnAtualizarPortas.Click += new System.EventHandler(this.btnAtualizarPortas_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btnAtualizarPortas);
            this.panel1.Controls.Add(this.cboPortas);
            this.panel1.Controls.Add(this.btnConectar);
            this.panel1.Location = new System.Drawing.Point(12, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 197);
            this.panel1.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 15);
            this.label7.TabIndex = 4;
            this.label7.Text = "Porta";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(236, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Controles";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.numRPM);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.numEspiras);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.numDiametro);
            this.panel2.Location = new System.Drawing.Point(236, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(209, 197);
            this.panel2.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(20, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "RPM";
            // 
            // numRPM
            // 
            this.numRPM.Location = new System.Drawing.Point(20, 138);
            this.numRPM.Maximum = new decimal(new int[] { 1500, 0, 0, 0 });
            this.numRPM.Name = "numRPM";
            this.numRPM.Size = new System.Drawing.Size(120, 23);
            this.numRPM.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(20, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Espiras";
            // 
            // numEspiras
            // 
            this.numEspiras.Location = new System.Drawing.Point(20, 40);
            this.numEspiras.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.numEspiras.Name = "numEspiras";
            this.numEspiras.Size = new System.Drawing.Size(120, 23);
            this.numEspiras.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(20, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Diâmetro (mm)";
            // 
            // numDiametro
            // 
            this.numDiametro.DecimalPlaces = 2;
            this.numDiametro.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            this.numDiametro.Location = new System.Drawing.Point(20, 87);
            this.numDiametro.Maximum = new decimal(new int[] { 50, 0, 0, 65536 });
            this.numDiametro.Name = "numDiametro";
            this.numDiametro.Size = new System.Drawing.Size(120, 23);
            this.numDiametro.TabIndex = 0;
            // 
            // btnEnviarConfig
            // 
            this.btnEnviarConfig.Location = new System.Drawing.Point(15, 23);
            this.btnEnviarConfig.Name = "btnEnviarConfig";
            this.btnEnviarConfig.Size = new System.Drawing.Size(100, 40);
            this.btnEnviarConfig.TabIndex = 7;
            this.btnEnviarConfig.Text = "Enviar Config";
            this.btnEnviarConfig.UseVisualStyleBackColor = true;
            this.btnEnviarConfig.Click += new System.EventHandler(this.btnEnviarConfig_Click);
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(162, 23);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(100, 40);
            this.btnIniciar.TabIndex = 8;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnParar
            // 
            this.btnParar.Location = new System.Drawing.Point(313, 23);
            this.btnParar.Name = "btnParar";
            this.btnParar.Size = new System.Drawing.Size(100, 40);
            this.btnParar.TabIndex = 9;
            this.btnParar.Text = "Parar";
            this.btnParar.UseVisualStyleBackColor = true;
            this.btnParar.Click += new System.EventHandler(this.btnParar_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(12, 240);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Ações";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnIniciar);
            this.panel3.Controls.Add(this.btnEnviarConfig);
            this.panel3.Controls.Add(this.btnParar);
            this.panel3.Location = new System.Drawing.Point(12, 258);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(433, 85);
            this.panel3.TabIndex = 11;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 373);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(874, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(460, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 15);
            this.label8.TabIndex = 13;
            this.label8.Text = "Monitoramento";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblAlarms);
            this.panel4.Controls.Add(this.lblProgressStatus);
            this.panel4.Controls.Add(this.progressBar1);
            this.panel4.Controls.Add(this.lblStatus);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.lblVoltasAtual);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Location = new System.Drawing.Point(460, 33);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(402, 310);
            this.panel4.TabIndex = 14;
            // 
            // lblAlarms
            // 
            this.lblAlarms.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAlarms.ForeColor = System.Drawing.Color.Red;
            this.lblAlarms.Location = new System.Drawing.Point(20, 240);
            this.lblAlarms.Name = "lblAlarms";
            this.lblAlarms.Size = new System.Drawing.Size(360, 60);
            this.lblAlarms.TabIndex = 10;
            this.lblAlarms.Text = "Alarmes:";
            // 
            // lblProgressStatus
            // 
            this.lblProgressStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblProgressStatus.Location = new System.Drawing.Point(20, 160);
            this.lblProgressStatus.Name = "lblProgressStatus";
            this.lblProgressStatus.Size = new System.Drawing.Size(120, 15);
            this.lblProgressStatus.TabIndex = 9;
            this.lblProgressStatus.Text = "Progresso geral:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(20, 180);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(360, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 20.25F);
            this.lblStatus.Location = new System.Drawing.Point(20, 50);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(360, 37);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Pronto";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(20, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 15);
            this.label9.TabIndex = 2;
            this.label9.Text = "Status atual:";
            // 
            // lblVoltasAtual
            // 
            this.lblVoltasAtual.ForeColor = System.Drawing.Color.Blue;
            this.lblVoltasAtual.Location = new System.Drawing.Point(150, 120);
            this.lblVoltasAtual.Name = "lblVoltasAtual";
            this.lblVoltasAtual.Size = new System.Drawing.Size(100, 15);
            this.lblVoltasAtual.TabIndex = 7;
            this.lblVoltasAtual.Text = "0";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(20, 120);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 15);
            this.label12.TabIndex = 6;
            this.label12.Text = "Voltas atuais:";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(874, 395);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Comando bobinadeira - M.Y. Soluções";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numRPM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEspiras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiametro)).EndInit();
            this.panel3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private ComboBox cboPortas;
        private Label label1;
        private Button btnConectar;
        private Button btnAtualizarPortas;
        private Panel panel1;
        private Label label2;
        private Panel panel2;
        private Label label4;
        private NumericUpDown numEspiras;
        private Label label3;
        private NumericUpDown numDiametro;
        private Label label5;
        private NumericUpDown numRPM;
        private Button btnEnviarConfig;
        private Button btnIniciar;
        private Button btnParar;
        private Label label6;
        private Panel panel3;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Label label7;
        private Label label8;
        private Panel panel4;
        private ProgressBar progressBar1;
        private Label lblStatus;
        private Label label9;
        private Label lblVoltasAtual;
        private Label label12;
        private Label lblProgressStatus;
        private Label lblAlarms;
    }
}