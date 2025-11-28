using System;
using System.Windows;
using System.Windows.Threading; // تأكد من وجود هذا الـ using

namespace SchedualApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 1. معالج الأخطاء غير المعالجة في سياق الواجهة (UI Thread)
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            // 2. معالج الأخطاء غير المعالجة في سياق التطبيق (Background Threads)
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // منع التطبيق من الانهيار
            e.Handled = true;
            ShowException(e.Exception, "Dispatcher Unhandled Exception (UI Thread)");
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // يجب أن يكون هذا الاستثناء من نوع Exception
            var ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                ShowException(ex, "AppDomain Unhandled Exception (Background Thread)");
            }
            // إذا كان الخطأ حرجاً (IsTerminating)، يجب إغلاق التطبيق
            if (e.IsTerminating)
            {
                // يمكن إضافة منطق تسجيل الخطأ هنا قبل الإغلاق
            }
        }

        private void ShowException(Exception ex, string title)
        {
            // نستخدم System.Windows.MessageBox هنا
            MessageBox.Show($"An unhandled exception occurred: \n\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}", title, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
