using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Form1 : Form
    {
        string FilePath = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilePath = "";
            richTextBox1.Text = "";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd = new OpenFileDialog() { Filter = "TextDocument | *.txt", ValidateNames=true, Multiselect=false })
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    using(StreamReader sr = new StreamReader(ofd.FileName))
                    {
                        FilePath = ofd.FileName;
                        Task<string> text = sr.ReadToEndAsync();
                        richTextBox1.Text = text.Result;
                    }
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "TextDocument | *.txt", ValidateNames = true })
                {
                    if(sfd.ShowDialog() == DialogResult.OK)
                    {
                        using(StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            sw.WriteLineAsync(richTextBox1.Text);
                        }
                    }
                }
            }
            else
            {
                using(StreamWriter sw = new StreamWriter(FilePath))
                {
                    sw.WriteLineAsync(richTextBox1.Text);
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "TextDocument | *.txt", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        sw.WriteLineAsync(richTextBox1.Text);
                    }
                }
            }
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            if(printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(wordWrapToolStripMenuItem.Checked == true)
            {
                wordWrapToolStripMenuItem.Checked = false;
                richTextBox1.WordWrap = false;
            }
            else
            {
                wordWrapToolStripMenuItem.Checked = true;
                richTextBox1.WordWrap = true;
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            richTextBox1.SelectionFont = new Font(fontDialog1.Font.FontFamily, fontDialog1.Font.Size, fontDialog1.Font.Style);
        }

        private void highlightTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionBackColor = Color.Yellow;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, richTextBox1.Font, Brushes.Black, 12, 10);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
            }
            else
            {
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.notepad2;
        }
    }
}
