using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace _1121751
{
    public partial class Form1 : Form
    {
        private int timeLeft;
        private int marqueePos = 0;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartTimer(); // �}�l�p�ɾ�
            StartMarquee();
        }
        private void StartMarquee()
        {

            labelMarquee.Left = this.Width;
            timerMarquee.Interval = 100; // �]�m�p�ɾ����j��100�@��
            timerMarquee.Start(); // �}�l�p�ɾ�
        }
        private void StartTimer()
        {
            timeLeft = 45; // ��l�ƭ˼Ʈɶ���30��
            labelTimer.Text = "����ɶ��Ѿl" + timeLeft + "��"; // ��ܪ�l�ɶ�
            Timer.Interval = 1000; // �]�m�p�ɾ����j��1000�@��A�Y1��
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--; // �C���֤@��
                labelTimer.Text = "����ɶ��Ѿl" + timeLeft + "��"; // ��s��ܪ��ɶ�
            }
            else
            {
                Timer.Stop(); // �ɶ���A����p�ɾ�
                labelTimer.Text = "�ɶ���I";
                DialogResult result = MessageBox.Show("�ɶ���I�A�Q�n���s�}�l�٬O�����{�ǡH", "�q��", MessageBoxButtons.RetryCancel);

                if (result == DialogResult.Retry)
                {
                    ClearSelections();
                    StartTimer(); // ���s�}�l�p�ɾ�
                }
                else if (result == DialogResult.Cancel)
                {
                    this.Close(); // �����{��
                }
            }
        }
        private void ClearSelections()
        {
            // �M���Ҧ��襤��RadioButton
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;
            textBox1.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ClearSelections();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // ���ܩҦ���r�����C��
                ChangeTextColor(this, colorDialog.Color);
            }
        }
        private void ChangeTextColor(Control control, Color color)
        {
            foreach (Control c in control.Controls)
            {
                if (c is Label || c is RadioButton || c is Button || c is GroupBox)
                {
                    c.ForeColor = color;
                }
                if (c.Controls.Count > 0)
                {
                    ChangeTextColor(c, color);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                // ���ܩҦ���r���󪺦r��
                ChangeTextFont(this, fontDialog.Font);
            }
        }
        private void ChangeTextFont(Control control, Font font)
        {
            foreach (Control c in control.Controls)
            {
                if (c is Label || c is RadioButton || c is Button || c is GroupBox)
                {
                    c.Font = font;
                }
                if (c.Controls.Count > 0)
                {
                    ChangeTextFont(c, font);
                }
            }
        }

        private void timerMarquee_Tick(object sender, EventArgs e)
        {
            labelMarquee.Left -= 15; // �C������5�ӹ���

            // ��]���O�������X�����ɡA���]���m
            if (labelMarquee.Right < 0)
            {
                labelMarquee.Left = this.Width;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Timer.Stop();
            var unansweredQuestions = GetUnansweredQuestions();
            if (unansweredQuestions.Count == 0)// �Ҧ����D�����ﶵ�Q�襤
            {
                int oddCount = 0, evenCount = 0;
                if(radioButton1.Checked==true)
                    oddCount++;
                if (radioButton3.Checked == true)
                    oddCount++;
                if (radioButton5.Checked == true)
                    oddCount++;
                if (radioButton2.Checked == true)
                    evenCount++;
                if (radioButton4.Checked == true)
                    evenCount++;
                if (radioButton6.Checked == true)
                    evenCount++;
                if (oddCount > evenCount)
                {
                    MessageBox.Show("��~" + textBox1.Text+"�A�O���V�k���H�I\n�㦳��ı��ҡB�P�ʡB�Ѱ���ŵ��S�ʡC", "���絲�G");
                }
                else
                {
                    MessageBox.Show("��~" + textBox1.Text+"�A�O���V�����H�I\n�㦳�޿��ҡB�z�ʡB�{�굥�S�ʡC", "���絲�G");
                }
                this.Close();
            }
            
            else
            {
                // ��ܨ�����ǰ��D�S���ﶵ�Q�襤
                string message = "�Ц^���H�U���D�G\n" + string.Join("\n", unansweredQuestions);
                MessageBox.Show(message);
            }
        }

        private List<string> GetUnansweredQuestions()
        {
            List<string> unansweredQuestions = new List<string>();
            foreach (GroupBox groupBox in Controls.OfType<GroupBox>())
            {
                if (!groupBox.Controls.OfType<RadioButton>().Any(radioButton => radioButton.Checked))
                {
                    unansweredQuestions.Add(groupBox.Text); // ���] GroupBox.Text �O���D�����D
                }
            }
            return unansweredQuestions;
        }
    }
    
}
