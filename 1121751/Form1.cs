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
            StartTimer(); // 開始計時器
            StartMarquee();
        }
        private void StartMarquee()
        {

            labelMarquee.Left = this.Width;
            timerMarquee.Interval = 100; // 設置計時器間隔為100毫秒
            timerMarquee.Start(); // 開始計時器
        }
        private void StartTimer()
        {
            timeLeft = 45; // 初始化倒數時間為30秒
            labelTimer.Text = "測驗時間剩餘" + timeLeft + "秒"; // 顯示初始時間
            Timer.Interval = 1000; // 設置計時器間隔為1000毫秒，即1秒
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--; // 每秒減少一秒
                labelTimer.Text = "測驗時間剩餘" + timeLeft + "秒"; // 更新顯示的時間
            }
            else
            {
                Timer.Stop(); // 時間到，停止計時器
                labelTimer.Text = "時間到！";
                DialogResult result = MessageBox.Show("時間到！你想要重新開始還是結束程序？", "通知", MessageBoxButtons.RetryCancel);

                if (result == DialogResult.Retry)
                {
                    ClearSelections();
                    StartTimer(); // 重新開始計時器
                }
                else if (result == DialogResult.Cancel)
                {
                    this.Close(); // 關閉程式
                }
            }
        }
        private void ClearSelections()
        {
            // 清除所有選中的RadioButton
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
                // 改變所有文字控件的顏色
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
                // 改變所有文字控件的字體
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
            labelMarquee.Left -= 15; // 每次移動5個像素

            // 當跑馬燈完全移出視窗時，重設其位置
            if (labelMarquee.Right < 0)
            {
                labelMarquee.Left = this.Width;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Timer.Stop();
            var unansweredQuestions = GetUnansweredQuestions();
            if (unansweredQuestions.Count == 0)// 所有問題都有選項被選中
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
                    MessageBox.Show("嗨~" + textBox1.Text+"你是偏向右腦人！\n具有直覺思考、感性、天馬行空等特性。", "測驗結果");
                }
                else
                {
                    MessageBox.Show("嗨~" + textBox1.Text+"你是偏向左腦人！\n具有邏輯思考、理性、現實等特性。", "測驗結果");
                }
                this.Close();
            }
            
            else
            {
                // 顯示具體哪些問題沒有選項被選中
                string message = "請回答以下問題：\n" + string.Join("\n", unansweredQuestions);
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
                    unansweredQuestions.Add(groupBox.Text); // 假設 GroupBox.Text 是問題的標題
                }
            }
            return unansweredQuestions;
        }
    }
    
}
