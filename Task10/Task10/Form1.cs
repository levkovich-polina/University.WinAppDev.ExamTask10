using System.ComponentModel;
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
            public BloodType BloodType { get; set; }
        }
        private enum BloodType
        {
            A,
            B,
            AB,
        }

        Timer _timer;
        Random _random = new Random();
        private BindingList<Person> _persons = new BindingList<Person>();

        public Form1()
        {
            InitializeComponent();
            DataGridView.AutoGenerateColumns = true;
            DataGridView.DataSource = _persons;
        }

        private void StartFillingButton_Click(object sender, EventArgs e)
        {
            TimerCallback tm = new TimerCallback(OnTimerTicked);
            _timer = new Timer(tm, 0, 0, 500);
        }

        private void OnTimerTicked(object obj)
        {
            try
            {
                string name = "";
                int length = _random.Next(3, 11);
                Char[] letters = new Char[12] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l' };
                for (int i = 0; i < length; i++)
                {
                    name += letters[_random.Next(0, 12)];
                }
                int height = _random.Next(120, 190);

                int age = _random.Next(5, 80);
                BloodType type;
                int bloodType = _random.Next(0, 3);
                if (bloodType == 0)
                {
                    type = BloodType.A;
                }
                else if (bloodType == 1)
                {
                    type = BloodType.B;
                }
                else
                {
                    type = BloodType.AB;
                }
             
                Invoke(() =>
                {
                    _persons.Add(new Person { Name = name, Height = height, Age = age, BloodType = type });
                });
            }
            catch(Exception e)
            {
                
            }

        }
    }
}