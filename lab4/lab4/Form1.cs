using System;
using System.Drawing;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        private CoordinatePlane? _coordinatePlane;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ���������� ����������� ������� ���� ������������ �����
            _coordinatePlane = new CoordinatePlane(pictureBox1);
            textBox1.Text = "0";
            textBox2.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (_coordinatePlane == null) return;

                // �������� �� ���������� ����������
                if (!double.TryParse(textBox1.Text, out double x))
                {
                    MessageBox.Show("���� �����, ������ �������� ������� �������� ��� X!", "������� ��������",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!double.TryParse(textBox2.Text, out double y))
                {
                    MessageBox.Show("���� �����, ������ �������� ������� �������� ��� Y!", "������� ��������",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ��������� ������ �� ��������� ������
                Point userPoint = new Point(x, y);
                int quadrant = QuadrantDeterminer.GetQuadrant(userPoint);
                string quadrantDescription = QuadrantDeterminer.GetQuadrantDescription(quadrant);

                // ³��������� ����� �� �������
                _coordinatePlane.DrawPoint(userPoint, quadrant);

                // �������� ��������� � listBox2
                listBox2.Items.Clear();
                listBox2.Items.Add($"�����: {userPoint}");
                listBox2.Items.Add($"���������: {quadrantDescription}");

                // ������� ���� ������ ������� �� �����
                listBox2.ForeColor = quadrant switch
                {
                    1 => Color.Red,
                    2 => Color.Blue,
                    3 => Color.Green,
                    4 => Color.Orange,
                    _ => Color.Black
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������� �������: {ex.Message}", "�������",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        // ������� ������ �������� �� �
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}