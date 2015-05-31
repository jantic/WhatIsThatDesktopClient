using System.ComponentModel;
using System.Windows.Forms;

namespace WhatIsThat
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ProcessButton = new System.Windows.Forms.Button();
            this.TagsListView = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AnswerLabel = new System.Windows.Forms.Label();
            this.answerPicture = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.latitudeInput = new System.Windows.Forms.NumericUpDown();
            this.longitudeInput = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.geoContextModeOffRB = new System.Windows.Forms.RadioButton();
            this.geoContextModeOnRB = new System.Windows.Forms.RadioButton();
            this.ImageInterface = new WhatIsThat.ImagePanel();
            ((System.ComponentModel.ISupportInitialize)(this.answerPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.latitudeInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.longitudeInput)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProcessButton
            // 
            this.ProcessButton.Location = new System.Drawing.Point(697, 630);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.Size = new System.Drawing.Size(75, 23);
            this.ProcessButton.TabIndex = 1;
            this.ProcessButton.Text = "Process";
            this.ProcessButton.UseVisualStyleBackColor = true;
            this.ProcessButton.Click += new System.EventHandler(this.ProcessButton_Click);
            // 
            // TagsListView
            // 
            this.TagsListView.Location = new System.Drawing.Point(349, 552);
            this.TagsListView.Name = "TagsListView";
            this.TagsListView.Size = new System.Drawing.Size(287, 68);
            this.TagsListView.TabIndex = 2;
            this.TagsListView.UseCompatibleStateImageBehavior = false;
            this.TagsListView.View = System.Windows.Forms.View.List;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(346, 535);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Candidates";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 535);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Top Answer:";
            // 
            // AnswerLabel
            // 
            this.AnswerLabel.AutoSize = true;
            this.AnswerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AnswerLabel.Location = new System.Drawing.Point(155, 597);
            this.AnswerLabel.Name = "AnswerLabel";
            this.AnswerLabel.Size = new System.Drawing.Size(0, 20);
            this.AnswerLabel.TabIndex = 5;
            // 
            // answerPicture
            // 
            this.answerPicture.Location = new System.Drawing.Point(6, 558);
            this.answerPicture.Name = "answerPicture";
            this.answerPicture.Size = new System.Drawing.Size(142, 95);
            this.answerPicture.TabIndex = 6;
            this.answerPicture.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(325, 635);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Latitude";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(502, 632);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Longitude";
            // 
            // latitudeInput
            // 
            this.latitudeInput.DecimalPlaces = 6;
            this.latitudeInput.Increment = new decimal(new int[] {
            1,
            0,
            0,
            393216});
            this.latitudeInput.Location = new System.Drawing.Point(376, 632);
            this.latitudeInput.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.latitudeInput.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.latitudeInput.Name = "latitudeInput";
            this.latitudeInput.Size = new System.Drawing.Size(120, 20);
            this.latitudeInput.TabIndex = 11;
            this.latitudeInput.Value = new decimal(new int[] {
            327541752,
            0,
            0,
            458752});
            // 
            // longitudeInput
            // 
            this.longitudeInput.DecimalPlaces = 6;
            this.longitudeInput.Increment = new decimal(new int[] {
            1,
            0,
            0,
            393216});
            this.longitudeInput.Location = new System.Drawing.Point(562, 630);
            this.longitudeInput.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.longitudeInput.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.longitudeInput.Name = "longitudeInput";
            this.longitudeInput.Size = new System.Drawing.Size(120, 20);
            this.longitudeInput.TabIndex = 12;
            this.longitudeInput.Value = new decimal(new int[] {
            1172382162,
            0,
            0,
            -2147024896});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.geoContextModeOffRB);
            this.groupBox1.Controls.Add(this.geoContextModeOnRB);
            this.groupBox1.Location = new System.Drawing.Point(643, 538);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(129, 79);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Geographic Context Mode";
            // 
            // geoContextModeOffRB
            // 
            this.geoContextModeOffRB.AutoSize = true;
            this.geoContextModeOffRB.Location = new System.Drawing.Point(6, 59);
            this.geoContextModeOffRB.Name = "geoContextModeOffRB";
            this.geoContextModeOffRB.Size = new System.Drawing.Size(39, 17);
            this.geoContextModeOffRB.TabIndex = 1;
            this.geoContextModeOffRB.Text = "Off";
            this.geoContextModeOffRB.UseVisualStyleBackColor = true;
            this.geoContextModeOffRB.CheckedChanged += new System.EventHandler(this.geoContextModeOffRB_CheckedChanged);
            // 
            // geoContextModeOnRB
            // 
            this.geoContextModeOnRB.AutoSize = true;
            this.geoContextModeOnRB.Checked = true;
            this.geoContextModeOnRB.Location = new System.Drawing.Point(7, 32);
            this.geoContextModeOnRB.Name = "geoContextModeOnRB";
            this.geoContextModeOnRB.Size = new System.Drawing.Size(39, 17);
            this.geoContextModeOnRB.TabIndex = 0;
            this.geoContextModeOnRB.TabStop = true;
            this.geoContextModeOnRB.Text = "On";
            this.geoContextModeOnRB.UseVisualStyleBackColor = true;
            // 
            // ImageInterface
            // 
            this.ImageInterface.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageInterface.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ImageInterface.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ImageInterface.BackgroundImage")));
            this.ImageInterface.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ImageInterface.Location = new System.Drawing.Point(6, 1);
            this.ImageInterface.Name = "ImageInterface";
            this.ImageInterface.Size = new System.Drawing.Size(774, 531);
            this.ImageInterface.SourceImage = ((System.Drawing.Image)(resources.GetObject("ImageInterface.SourceImage")));
            this.ImageInterface.TabIndex = 14;
            this.ImageInterface.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImageInterface_MouseDown);
            this.ImageInterface.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageInterface_MouseMove);
            this.ImageInterface.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImageInterface_MouseUp);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.ImageInterface);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.longitudeInput);
            this.Controls.Add(this.latitudeInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.answerPicture);
            this.Controls.Add(this.AnswerLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TagsListView);
            this.Controls.Add(this.ProcessButton);
            this.MaximumSize = new System.Drawing.Size(800, 700);
            this.MinimumSize = new System.Drawing.Size(800, 700);
            this.Name = "Form1";
            this.Text = "What Is That?";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.answerPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.latitudeInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.longitudeInput)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button ProcessButton;
        private ListView TagsListView;
        private Label label1;
        private Label label2;
        private Label AnswerLabel;
        private PictureBox answerPicture;
        private Label label3;
        private Label label4;
        private NumericUpDown latitudeInput;
        private NumericUpDown longitudeInput;
        private GroupBox groupBox1;
        private RadioButton geoContextModeOffRB;
        private RadioButton geoContextModeOnRB;
        private ImagePanel ImageInterface;

    }
}

