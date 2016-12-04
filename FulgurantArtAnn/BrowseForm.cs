﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FulgurantArtAnn
{
    public partial class BrowseForm : Form
    {
        private readonly Form _parentForm;

        public BrowseForm(Form parent)
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            _parentForm = parent;
        }

        private void BrowseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parentForm.Show();
        }

        private void labelLinkBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void BrowseForm_Load(object sender, EventArgs e)
        {
            var data = NeuralEngine.GetImages();
            if (data.Count == 0)
            {
                MessageBox.Show("Add some art first!");
                _parentForm.Show();
                Close();
            }
            else
            {
                var imageIndex = 0;
                for (int i = 0; i < data.Count(); i++)
                {
                    ListViewGroup viewGroup;
                    var category = data.Keys.ElementAt(i);
                    if (listView1.Groups[category] == null)
                    {
                        viewGroup = new ListViewGroup(category, category);
                        listView1.Groups.Add(viewGroup);
                    }
                    else viewGroup = listView1.Groups[category];


                    var images = data.Values.ElementAt(i);
                    var filenames = Directory.GetFiles("pictures/" + category);
                    for (int j = 0; j < images.Count; j++)
                    {
                        var image = images[j];
                        imageList1.Images.Add(image);
                        ListViewItem item = new ListViewItem(new DirectoryInfo(filenames[j]).Name, imageIndex, viewGroup);
                        listView1.Items.Add(item);
                        imageIndex++;
                    }
                }
            }
        }
    }
}