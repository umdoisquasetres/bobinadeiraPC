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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            cboPortas = new ComboBox();
            label1 = new Label();
            btnConectar = new Button();
            btnAtualizarPortas = new Button();
            panel1 = new Panel();
            label7 = new Label();
            label2 = new Label();
            panel2 = new Panel();
            label5 = new Label();
            numRPM = new NumericUpDown();
            label4 = new Label();
            numEspiras = new NumericUpDown();
            label3 = new Label();
            numDiametro = new NumericUpDown();
            btnEnviarConfig = new Button();
            btnIniciar = new Button();
            btnParar = new Button();
            label6 = new Label();
            panel3 = new Panel();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            label8 = new Label();
            panel4 = new Panel();
            label13 = new Label();
            label11 = new Label();
            progressBar1 = new ProgressBar();
            label10 = new Label();
            label9 = new Label();
            lblVoltasAtual = new Label();
            lblAlarme = new Label();
            label12 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numRPM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numEspiras).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDiametro).BeginInit();
            panel3.SuspendLayout();
            statusStrip1.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // cboPortas
            // 
            resources.ApplyResources(cboPortas, "cboPortas");
            cboPortas.FormattingEnabled = true;
            cboPortas.Name = "cboPortas";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // btnConectar
            // 
            resources.ApplyResources(btnConectar, "btnConectar");
            btnConectar.Name = "btnConectar";
            btnConectar.UseVisualStyleBackColor = true;
            btnConectar.Click += btnConectar_Click;
            // 
            // btnAtualizarPortas
            // 
            resources.ApplyResources(btnAtualizarPortas, "btnAtualizarPortas");
            btnAtualizarPortas.Name = "btnAtualizarPortas";
            btnAtualizarPortas.UseVisualStyleBackColor = true;
            btnAtualizarPortas.Click += btnAtualizarPortas_Click;
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label7);
            panel1.Controls.Add(btnAtualizarPortas);
            panel1.Controls.Add(cboPortas);
            panel1.Controls.Add(btnConectar);
            panel1.Name = "panel1";
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // panel2
            // 
            resources.ApplyResources(panel2, "panel2");
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(label5);
            panel2.Controls.Add(numRPM);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(numEspiras);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(numDiametro);
            panel2.Name = "panel2";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // numRPM
            // 
            resources.ApplyResources(numRPM, "numRPM");
            numRPM.Maximum = new decimal(new int[] { 1500, 0, 0, 0 });
            numRPM.Name = "numRPM";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // numEspiras
            // 
            resources.ApplyResources(numEspiras, "numEspiras");
            numEspiras.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numEspiras.Name = "numEspiras";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // numDiametro
            // 
            resources.ApplyResources(numDiametro, "numDiametro");
            numDiametro.DecimalPlaces = 2;
            numDiametro.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numDiametro.Maximum = new decimal(new int[] { 50, 0, 0, 65536 });
            numDiametro.Name = "numDiametro";
            // 
            // btnEnviarConfig
            // 
            resources.ApplyResources(btnEnviarConfig, "btnEnviarConfig");
            btnEnviarConfig.Name = "btnEnviarConfig";
            btnEnviarConfig.UseVisualStyleBackColor = true;
            btnEnviarConfig.Click += btnEnviarConfig_Click;
            // 
            // btnIniciar
            // 
            resources.ApplyResources(btnIniciar, "btnIniciar");
            btnIniciar.Name = "btnIniciar";
            btnIniciar.UseVisualStyleBackColor = true;
            btnIniciar.Click += btnIniciar_Click;
            // 
            // btnParar
            // 
            resources.ApplyResources(btnParar, "btnParar");
            btnParar.Name = "btnParar";
            btnParar.UseVisualStyleBackColor = true;
            btnParar.Click += btnParar_Click;
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // panel3
            // 
            resources.ApplyResources(panel3, "panel3");
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(btnIniciar);
            panel3.Controls.Add(btnEnviarConfig);
            panel3.Controls.Add(btnParar);
            panel3.Name = "panel3";
            // 
            // statusStrip1
            // 
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Name = "statusStrip1";
            statusStrip1.SizingGrip = false;
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(toolStripStatusLabel1, "toolStripStatusLabel1");
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            // 
            // panel4
            // 
            resources.ApplyResources(panel4, "panel4");
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(label12);
            panel4.Controls.Add(lblAlarme);
            panel4.Controls.Add(lblVoltasAtual);
            panel4.Controls.Add(label13);
            panel4.Controls.Add(label11);
            panel4.Controls.Add(progressBar1);
            panel4.Controls.Add(label10);
            panel4.Controls.Add(label9);
            panel4.Name = "panel4";
            // 
            // label13
            // 
            resources.ApplyResources(label13, "label13");
            label13.Name = "label13";
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            label11.Name = "label11";
            // 
            // progressBar1
            // 
            resources.ApplyResources(progressBar1, "progressBar1");
            progressBar1.Name = "progressBar1";
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            label9.Name = "label9";
            // 
            // lblVoltasAtual
            // 
            resources.ApplyResources(lblVoltasAtual, "lblVoltasAtual");
            lblVoltasAtual.ForeColor = System.Drawing.Color.Blue;
            lblVoltasAtual.Name = "lblVoltasAtual";
            // 
            // lblAlarme
            // 
            resources.ApplyResources(lblAlarme, "lblAlarme");
            lblAlarme.ForeColor = System.Drawing.Color.Red;
            lblAlarme.Name = "lblAlarme";
            // 
            // label12
            // 
            resources.ApplyResources(label12, "label12");
            label12.Name = "label12";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel4);
            Controls.Add(label6);
            Controls.Add(label1);
            Controls.Add(statusStrip1);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(label8);
            Controls.Add(panel1);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numRPM).EndInit();
            ((System.ComponentModel.ISupportInitialize)numEspiras).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDiametro).EndInit();
            panel3.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private Label label11;
        private ProgressBar progressBar1;
        private Label label10;
        private Label label9;
        private Label label13;
        private Label lblVoltasAtual;
        private Label lblAlarme;
        private Label label12;
    }
}
