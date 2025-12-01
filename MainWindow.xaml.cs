using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Media; // لإضافة الألوان

namespace SchedualApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // عند تحميل النافذة، قم بتحميل واجهة المقررات كواجهة افتراضية
            LoadWinFormsControl(new CoursManagementControl(), "Courses");
        }

        // دالة مساعدة لتبديل الـ UserControl وتحديث حالة الأزرار
        private void LoadWinFormsControl(System.Windows.Forms.UserControl control, string activeTag)
        {
            // 1. تحديث حالة الأزرار (Active State)
            foreach (var item in NavigationPanel.Children)
            {
                if (item is System.Windows.Controls.Button button)
                {
                    if (button.Tag?.ToString() == activeTag)
                    {
                        // تعيين اللون الأخضر للزر النشط
                        button.Background = new SolidColorBrush(Colors.LightGreen);
                    }
                    else
                    {
                        // إعادة الأزرار الأخرى إلى اللون الافتراضي
                        button.Background = new SolidColorBrush(Colors.LightGray);
                    }
                }
            }

            // 2. تحميل الكنترول الجديد
            if (winFormsHost != null)
            {
                winFormsHost.Child = control;
            }
        }

        // ---------------------------------------------------
        // دوال أحداث النقر للأزرار
        // ---------------------------------------------------
        private void DepartmentsButton_Click(object sender, RoutedEventArgs e)
        {
            LoadWinFormsControl(new DepartmentManagementControl(), "Departments");
        }

        private void CoursesButton_Click(object sender, RoutedEventArgs e)
        {
            LoadWinFormsControl(new CoursManagementControl(), "Courses");
        }

        private void LecturersButton_Click(object sender, RoutedEventArgs e)
        {
            LoadWinFormsControl(new LecturerManagementControl(), "Lecturers");
        }

        private void RoomsButton_Click(object sender, RoutedEventArgs e)
        {
            LoadWinFormsControl(new RoomManagementControl(), "Rooms");
        }

        // دوال الأزرار المستقبلية (تمت إضافتها الآن لتجنب التعديل لاحقاً)
        private void TimeSlotsButton_Click(object sender, RoutedEventArgs e)
        {
            // سنقوم بتصميم TimeSlotDefinitionControl في المرحلة القادمة
            //MessageBox.Show("Time Slots Definition UI is not yet implemented.", "Coming Soon");
            LoadWinFormsControl(new TimeSlotDefinitionControl(), "TimeSlots");
        }

        private void GenerationButton_Click(object sender, RoutedEventArgs e)
        {
            // سنقوم بتصميم TimetableGenerationControl في المرحلة القادمة
            //MessageBox.Show("Timetable Generation UI is not yet implemented.", "Coming Soon");
             LoadWinFormsControl(new TimetableGenerationControl(), "Generation");
        }
        private void CourseLevelButton_Click(object sender, RoutedEventArgs e)
        {
            LoadWinFormsControl(new CourseLevelManagementControl(), "CourseLevel");
        }

        //private void CourseLecturerButton_Click(object sender, RoutedEventArgs e)
        //{
        //    LoadWinFormsControl(new CourseLecturerManagementControl(), "CourseLecturer");
        //}

        private void ViewerButton_Click(object sender, RoutedEventArgs e)
        {
            // سنقوم بتصميم TimetableViewerControl في المرحلة القادمة
            MessageBox.Show("Timetable Viewer UI is not yet implemented.", "Coming Soon");
            // LoadWinFormsControl(new TimetableViewerControl(), "Viewer");
        }

        private void winFormsHost_ChildChanged(object sender, ChildChangedEventArgs e)
        {

        }
    }
}
