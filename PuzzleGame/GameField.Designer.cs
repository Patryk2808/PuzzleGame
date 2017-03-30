namespace PuzzleGame
{
    partial class GameField
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.field = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // field
            // 
            this.field.BackColor = System.Drawing.Color.RoyalBlue;
            this.field.Dock = System.Windows.Forms.DockStyle.Fill;
            this.field.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.field.Location = new System.Drawing.Point(0, 0);
            this.field.Margin = new System.Windows.Forms.Padding(10);
            this.field.Name = "field";
            this.field.Size = new System.Drawing.Size(150, 150);
            this.field.TabIndex = 0;
            this.field.Text = "?";
            this.field.UseVisualStyleBackColor = false;
            this.field.MouseDown += new System.Windows.Forms.MouseEventHandler(this.field_MouseDown);
            this.field.MouseEnter += new System.EventHandler(this.field_MouseEnter);
            this.field.MouseLeave += new System.EventHandler(this.field_MouseLeave);
            // 
            // GameField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.field);
            this.Name = "GameField";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button field;
    }
}
