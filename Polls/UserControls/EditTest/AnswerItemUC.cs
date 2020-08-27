using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Polls.Models;

namespace Polls.UserControls.EditTest
{
    public delegate void AnswerItemDelegate(AnswerItemUC sender);

    public partial class AnswerItemUC : UserControl
    {
        //public event AnswerItemDelegate editItemEvent;
        public event AnswerItemDelegate deleteItemEvent;
        public event AnswerItemDelegate nextSlideEvent;

        public AnswerItemUC()
        {
            InitializeComponent();
        }

        public TextBox GetTextBox1()
        {
            return textBox1;
        }

        public int getActiveVariantNumber()
        {
            if (comboBox1.SelectedIndex.Equals(comboBox1.Items.Count - 1))
                return -1;
            else
                return comboBox1.SelectedIndex;
        }

        public int getActiveVariantRealNumber()
        {
            if (!comboBox1.SelectedItem.Equals("<Конец>"))
                return (short.Parse(((string) comboBox1.SelectedItem).Split(':')[0]) - 1);
            else
                return -1;
        }

        public void setNextSlides(List<Slide> nextSlides, int realSlideNumber)
        {
            bool isNotEnd = false;
            comboBox1.Items.Clear();
            foreach (Slide slide in nextSlides)
            {
                comboBox1.Items.Add(string.Concat(slide.slideNumber + 1, ": ", slide.question));
                if (slide.slideNumber.Equals(realSlideNumber))
                {
                    setActiveVariant(nextSlides.IndexOf(slide));
                    isNotEnd = true;
                }
            }
            comboBox1.Items.Add("<Конец>");
            if (!isNotEnd)
                setActiveVariant(-1);
        }

        public void setActiveVariant(int activeVariant)
        {
            if (activeVariant.Equals(-1))
            {
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            }
            else
            {
                comboBox1.SelectedIndex = activeVariant;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            nextSlideEvent?.Invoke(this);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            deleteItemEvent?.Invoke(this);
        }

        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{
        //    editItemEvent.Invoke(this);
        //}

        public void setDeletable(bool deletable)
        {
            pictureBox1.Visible = deletable;
        }
    }
}
