using System; 

namespace Lab01_02
{
    public class Student
    {
        // 1. Field
        private string studentID;
        private string fullName;
        private float averageScore;
        private string faculty;

        // 2. Property
        public string StudentID { get => studentID; set => studentID = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public float AverageScore { get => averageScore; set => averageScore = value; }
        public string Faculty { get => faculty; set => faculty = value; }

        // 3. Constructor
        public Student()
        {
        }

        public Student(string studentID, string fullName, float averageScore, string faculty)
        {
            this.studentID = studentID;
            this.fullName = fullName;
            this.averageScore = averageScore;
            this.faculty = faculty;
        }

        // 4. Methods
        public void Input()
        {
            Console.Write("Nhập MSSV: ");
            StudentID = Console.ReadLine();
            Console.Write("Nhập Họ tên Sinh viên: ");
            FullName = Console.ReadLine();

            // Đảm bảo nhập số thực và xử lý lỗi
            bool validScore = false;
            while (!validScore)
            {
                Console.Write("Nhập Điểm TB: ");
                if (float.TryParse(Console.ReadLine(), out float score))
                {
                    AverageScore = score;
                    validScore = true;
                }
                else
                {
                    Console.WriteLine("Điểm không hợp lệ. Vui lòng nhập lại số.");
                }
            }

            Console.Write("Nhập Khoa: ");
            Faculty = Console.ReadLine();
        }

        public void Show()
        {
            Console.WriteLine("MSSV:{0,-10} | Họ Tên:{1,-25} | Khoa:{2,-10} | ĐiểmTB:{3,5:F2}",
                this.StudentID, this.fullName, this.Faculty, this.AverageScore);
        }
    }
}