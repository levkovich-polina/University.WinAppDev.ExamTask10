using Timer = System.Threading.Timer;

namespace Task10
{
    public partial class Form1 : Form
    {
        private class Person
        {
            public string Name { get; set; }
            public int Height { get; set; }
            public int Age { get; set; }
            public BloodType Type { get; set; }
        }
        private enum BloodType
        {
            A,
            B,
            AB,
        }

        Timer _timer;
        string _name;
        int _height;
        int _age;
        BloodType _bloodType;
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StartFillingButton_Click(object sender, EventArgs e)
        {
            TimerCallback tm = new TimerCallback(OnTimerTicked);
            _timer = new Timer(tm, 0, 0, 500);
        }
        private void OnTimerTicked(object obj)
        {
            Random rnd = new Random();
            int length = rnd.Next(0, 11);
            Char[] letters = new Char[12] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l' };
            for (int i = 0; i < length; i++)
            {
                _name += letters[rnd.Next(0, 11)];
            }
            int height = rnd.Next(120, 190);
            _height = height;
            int age = rnd.Next(5, 80);
            _age = age;
            int bloodType = rnd.Next(0, 2);
            if(bloodType == 0)
            {
                _bloodType = BloodType.A;
            }
            else if (bloodType == 1)
            {
                _bloodType = BloodType.B;
            }
            else
            {
                _bloodType = BloodType.AB;
            }
        }
    }
}