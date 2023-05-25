using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AltoNotepad
{
    public partial class AltoNotepad : Form
    {
        private string currentFilePath;
        private bool isSaved;
        public AltoNotepad()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isSaved)
            {
                DialogResult result = MessageBox.Show("SaveChanges?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SaveFile();
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            textBox.Text = string.Empty;
            currentFilePath = null;
            isSaved = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isSaved)
            {
                DialogResult result = MessageBox.Show("SaveChanges?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SaveFile();
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text file (*.txt)|*.txt|All file (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = openFileDialog.FileName;
                    textBox.Text = File.ReadAllText(currentFilePath);
                    isSaved = true;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFilePath != null)
            {
                SaveFile();
            }
            else
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text file (*.txt)|*.txt|All file (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                    SaveFile();
                }
            }
        }
        private void SaveFile()
        {
            File.WriteAllText(currentFilePath, textBox.Text);
            isSaved = true;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            isSaved = false;
        }

        private void AltoNotepad_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSaved)
            {
                DialogResult result = MessageBox.Show("SaveChanges?" +
                    "", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SaveFile();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true; // ウィンドウの閉じる操作をキャンセル
                }
            }
        }
    }
}
