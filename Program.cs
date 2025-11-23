using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01_02
{
    internal class Program
    {
        // --- HÀM MAIN VÀ MENU (Bước 3) ---
        static void Main(string[] args)
        {
            List<Student> studentList = new List<Student>();

            // Thêm dữ liệu mẫu (Sample data) để dễ dàng kiểm tra các chức năng LINQ (Case 3-8)
            studentList.Add(new Student("101", "Nguyen Van An", 9.5f, "CNTT"));
            studentList.Add(new Student("102", "Tran Thi Binh", 7.8f, "CNTT"));
            studentList.Add(new Student("103", "Le Van Cuong", 4.9f, "QTKD"));
            studentList.Add(new Student("104", "Pham Thi Dung", 8.0f, "CNTT"));
            studentList.Add(new Student("105", "Hoang Van Em", 6.5f, "NNA"));
            studentList.Add(new Student("106", "Nguyen Thi F", 9.5f, "CNTT"));
            studentList.Add(new Student("107", "Anh Tu", 3.5f, "CNTT"));


            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n\n******************** MENU QUẢN LÝ SINH VIÊN ********************");
                Console.WriteLine("1. Thêm sinh viên");
                Console.WriteLine("2. Xuất toàn bộ danh sách sinh viên");
                Console.WriteLine("3. Xuất DS sinh viên thuộc khoa 'CNTT'");
                Console.WriteLine("4. Xuất DS sinh viên có điểm TB >= 5");
                Console.WriteLine("5. Xuất DS sinh viên được sắp xếp theo điểm TB tăng dần");
                Console.WriteLine("6. Xuất DS sinh viên có điểm TB >= 5 VÀ thuộc khoa 'CNTT'");
                Console.WriteLine("7. Xuất DS sinh viên có điểm TB CAO NHẤT thuộc khoa 'CNTT'");
                Console.WriteLine("8. Thống kê số lượng sinh viên theo XẾP LOẠI");
                Console.WriteLine("0. Thoát");
                Console.WriteLine("*****************************************************************");

                Console.Write("Chọn chức năng (0-8): ");
                string choice = Console.ReadLine();

                Console.WriteLine("-----------------------------------------------------------------");

                switch (choice)
                {
                    case "1":
                        AddStudent(studentList);
                        break;
                    case "2":
                        DisplayStudentList(studentList);
                        break;
                    case "3":
                        DisplayStudentsByFaculty(studentList, "CNTT");
                        break;
                    case "4":
                        DisplayStudentsWithHighAverageScore(studentList, 5);
                        break;
                    case "5":
                        SortStudentsByAverageScore(studentList);
                        break;
                    case "6":
                        DisplayStudentsByFacultyAndScore(studentList, "CNTT", 5);
                        break;
                    case "7":
                        DisplayStudentsWithHighestAverageScoreByFaculty(studentList, "CNTT");
                        break;
                    case "8":
                        CountStudentsByGrade(studentList);
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Kết thúc chương trình.");
                        break;
                    default:
                        Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng chọn lại.");
                        break;
                }
            }
        } // Hết hàm Main()

        // --- CÁC HÀM CƠ BẢN (Bước 4 - Case 1 & 2) ---

        static void AddStudent(List<Student> studentList)
        {
            Console.WriteLine("\n=== Nhập thông tin sinh viên ===");
            Student student = new Student();
            student.Input(); // Gọi phương thức Input() của Class Student
            studentList.Add(student);
            Console.WriteLine("Thêm sinh viên thành công!");
        }

        static void DisplayStudentList(IEnumerable<Student> students)
        {
            List<Student> studentList = students.ToList(); // Chuyển đổi IEnumerable về List để kiểm tra Count

            if (!studentList.Any())
            {
                Console.WriteLine("Danh sách rỗng hoặc không tìm thấy sinh viên nào theo yêu cầu.");
                return;
            }

            Console.WriteLine("=== Danh sách chi tiết thông tin sinh viên ===");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine($"| {"MSSV",-10} | {"HỌ TÊN",-25} | {"KHOA",-10} | {"ĐIỂM TB",5} |");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            foreach (Student student in studentList)
            {
                student.Show(); // Gọi phương thức Show() của Class Student
            }
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine($"Tổng số sinh viên được hiển thị: {studentList.Count}");
        }

        // --- CÁC HÀM LINQ (Bước 4 - Case 3 đến 8) ---

        // Case 3: DS Sinh viên khoa cụ thể
        static void DisplayStudentsByFaculty(List<Student> studentList, string faculty)
        {
            Console.WriteLine($"\n=== Danh sách sinh viên thuộc khoa {faculty} ===");

            // LINQ: Method Syntax
            var students = studentList.Where(s => s.Faculty.Equals(faculty, StringComparison.OrdinalIgnoreCase));

            DisplayStudentList(students);
        }

        // Case 4: Xuất ra thông tin sinh viên có điểm TB lớn hơn bằng 5.
        static void DisplayStudentsWithHighAverageScore(List<Student> studentList, float minDTB)
        {
            Console.WriteLine($"\n=== Danh sách sinh viên có điểm TB >= {minDTB} ===");

            // LINQ: Query Syntax
            var students = from s in studentList
                           where s.AverageScore >= minDTB
                           select s;

            DisplayStudentList(students);
        }

        // Case 5: Xuất ra danh sách sinh viên được sắp xếp theo điểm trung bình tăng dần
        static void SortStudentsByAverageScore(List<Student> studentList)
        {
            Console.WriteLine("\n=== Danh sách sinh viên được sắp xếp theo điểm trung bình tăng dần ===");

            // LINQ: Method Syntax
            var sortedStudents = studentList.OrderBy(s => s.AverageScore);

            DisplayStudentList(sortedStudents);
        }

        // Case 6: DS sinh vien co DTB >=5 va thuoc khoa CNTT
        static void DisplayStudentsByFacultyAndScore(List<Student> studentList, string faculty, float minDTB)
        {
            Console.WriteLine($"\n=== DS sinh viên có điểm TB >= {minDTB} và thuộc khoa {faculty} ===");

            // LINQ: Method Syntax kết hợp điều kiện kép
            var students = studentList.Where(s => s.AverageScore >= minDTB
                                             && s.Faculty.Equals(faculty, StringComparison.OrdinalIgnoreCase));

            DisplayStudentList(students);
        }

        // Case 7: Xuất ra danh sách sinh viên có điểm trung bình cao nhất và thuộc khoa “CNTT”
        static void DisplayStudentsWithHighestAverageScoreByFaculty(List<Student> studentList, string faculty)
        {
            Console.WriteLine($"\n=== Danh sách sinh viên có điểm TB cao nhất thuộc khoa {faculty} ===");

            // Lọc sinh viên khoa CNTT
            var studentsInFaculty = studentList.Where(s => s.Faculty.Equals(faculty, StringComparison.OrdinalIgnoreCase));

            // Tìm điểm trung bình cao nhất (sử dụng Max())
            var maxScore = studentsInFaculty.Any() ? studentsInFaculty.Max(s => s.AverageScore) : (float?)null;

            if (maxScore.HasValue)
            {
                // Lọc ra tất cả sinh viên có điểm trung bình bằng điểm cao nhất đó
                var highestStudents = studentsInFaculty.Where(s => s.AverageScore == maxScore.Value);
                DisplayStudentList(highestStudents);
            }
            else
            {
                Console.WriteLine($"Không tìm thấy sinh viên nào thuộc khoa {faculty}.");
            }
        }

        // Case 8: Thống kê số lượng theo xếp loại
        static void CountStudentsByGrade(List<Student> studentList)
        {
            Console.WriteLine("\n=== Thống kê số lượng sinh viên theo xếp loại ===");

            // LINQ: Sử dụng GroupBy để nhóm theo xếp loại
            var gradeCounts = studentList.GroupBy(s =>
            {
                if (s.AverageScore >= 9.0) return "1. Xuất sắc (9.0 - 10.0)";
                if (s.AverageScore >= 8.0) return "2. Giỏi (8.0 - <9.0)";
                if (s.AverageScore >= 7.0) return "3. Khá (7.0 - <8.0)";
                if (s.AverageScore >= 5.0) return "4. Trung bình (5.0 - <7.0)";
                if (s.AverageScore >= 4.0) return "5. Yếu (4.0 - <5.0)";
                return "6. Kém (<4.0)";
            })
            .OrderBy(g => g.Key) // Sắp xếp theo thứ tự ưu tiên (dùng số 1. 2. để đảm bảo thứ tự đúng)
            .Select(g => new { Grade = g.Key.Substring(3), Count = g.Count() }); // Loại bỏ số thứ tự để hiển thị

            foreach (var item in gradeCounts)
            {
                Console.WriteLine($"| {item.Grade,-30} | Số lượng: {item.Count,3} |");
            }
        }

    } // Hết class Program
} // Hết namespace