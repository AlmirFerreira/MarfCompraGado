namespace MarfrigCompras
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnInserir = new System.Windows.Forms.Button();
            this.btnPecuarista = new System.Windows.Forms.Button();
            this.btnAnimal = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "-";
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.ForeColor = System.Drawing.Color.Blue;
            this.btnSair.Location = new System.Drawing.Point(49, 379);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(413, 41);
            this.btnSair.TabIndex = 4;
            this.btnSair.Text = "SAIR DO SISTEMA";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnInserir
            // 
            this.btnInserir.BackColor = System.Drawing.Color.Blue;
            this.btnInserir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInserir.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnInserir.Location = new System.Drawing.Point(48, 244);
            this.btnInserir.Name = "btnInserir";
            this.btnInserir.Size = new System.Drawing.Size(414, 37);
            this.btnInserir.TabIndex = 5;
            this.btnInserir.Text = "CADASTRAR COMPRAS";
            this.btnInserir.UseVisualStyleBackColor = false;
            this.btnInserir.Click += new System.EventHandler(this.btnInserir_Click);
            // 
            // btnPecuarista
            // 
            this.btnPecuarista.BackColor = System.Drawing.Color.Blue;
            this.btnPecuarista.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPecuarista.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnPecuarista.Location = new System.Drawing.Point(49, 289);
            this.btnPecuarista.Name = "btnPecuarista";
            this.btnPecuarista.Size = new System.Drawing.Size(414, 37);
            this.btnPecuarista.TabIndex = 6;
            this.btnPecuarista.Text = "CADASTRAR PECUARISTA";
            this.btnPecuarista.UseVisualStyleBackColor = false;
            this.btnPecuarista.Click += new System.EventHandler(this.btnPecuarista_Click);
            // 
            // btnAnimal
            // 
            this.btnAnimal.BackColor = System.Drawing.Color.Blue;
            this.btnAnimal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnimal.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAnimal.Location = new System.Drawing.Point(49, 334);
            this.btnAnimal.Name = "btnAnimal";
            this.btnAnimal.Size = new System.Drawing.Size(414, 37);
            this.btnAnimal.TabIndex = 7;
            this.btnAnimal.Text = "CADASTRAR ANIMAL";
            this.btnAnimal.UseVisualStyleBackColor = false;
            this.btnAnimal.Click += new System.EventHandler(this.btnAnimal_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MarfrigCompras.Properties.Resources.MARFRIG;
            this.pictureBox1.Location = new System.Drawing.Point(49, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(414, 150);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Blue;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button3.Location = new System.Drawing.Point(49, 199);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(413, 37);
            this.button3.TabIndex = 3;
            this.button3.Text = "PESQUISAR COMPRAS";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.YellowGreen;
            this.ClientSize = new System.Drawing.Size(510, 428);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnAnimal);
            this.Controls.Add(this.btnPecuarista);
            this.Controls.Add(this.btnInserir);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnInserir;
        private System.Windows.Forms.Button btnPecuarista;
        private System.Windows.Forms.Button btnAnimal;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button3;
    }
}

