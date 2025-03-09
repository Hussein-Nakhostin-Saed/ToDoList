using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using ToDoList.Domain.Dtos;
using ToDoList.Domain.Entities;
using ToDoList.Infrastructure;
using ToDoList.Services;

namespace ToDoList;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private AppDbContext _context;

    public MainWindow()
    {
        InitializeComponent();
        _context = new AppDbContext();
        LoadTasks();
    }

    private async void LoadTasks()
    {
        //var tasks = await _context.Tasks.ToListAsync();
        var tasks = new ObservableCollection<TaskItem>() 
        { 
            new TaskItem() { Id = 1, Description= "1lkdjkfc", DueDate=DateTime.Now, IsCompleted = true, Title="test1"},
            new TaskItem() { Id = 2, Description= "225dfgdb", DueDate=DateTime.Now.AddDays(-20), IsCompleted = true, Title="test2"},
            new TaskItem() { Id = 3, Description= "sdrdredre", DueDate=DateTime.Now.AddMonths(1), IsCompleted = false, Title="test3"}
        };

        TaskListView.ItemsSource = tasks;
    }

    private async void AddTask_Click(object sender, RoutedEventArgs e)
    {
        var task = new TaskItem
        {
            Title = TitleTextBox.Text,
            Description = DescriptionTextBox.Text,
            DueDate = DueDatePicker.SelectedDate ?? DateTime.Now,
            IsCompleted = false
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        LoadTasks();

        TitleTextBox.Text = "";
        DescriptionTextBox.Text = "";
        DueDatePicker.SelectedDate = null;
    }

    private async void EditTask_Click(object sender, RoutedEventArgs e)
    {
        if (TaskListView.SelectedItem is TaskItem selectedTask)
        {
            selectedTask.Title = TitleTextBox.Text;
            selectedTask.Description = DescriptionTextBox.Text;
            selectedTask.DueDate = DueDatePicker.SelectedDate ?? DateTime.Now;

            _context.Tasks.Update(selectedTask);
            await _context.SaveChangesAsync();
            LoadTasks();
        }
    }

    private async void DeleteTask_Click(object sender, RoutedEventArgs e)
    {
        if (TaskListView.SelectedItem is TaskItem selectedTask)
        {
            _context.Tasks.Remove(selectedTask);
            await _context.SaveChangesAsync();
            LoadTasks();
        }
    }

    private void ExportToPdf_Click(object sender, RoutedEventArgs e)
    {
        var document = new PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var font = new XFont("Verdana", 12);

        var tasks = new ObservableCollection<TaskItem>()
        {
            new TaskItem() { Id = 1, Description= "1lkdjkfc", DueDate=DateTime.Now, IsCompleted = true, Title="test1"},
            new TaskItem() { Id = 2, Description= "225dfgdb", DueDate=DateTime.Now.AddDays(-20), IsCompleted = true, Title="test2"},
            new TaskItem() { Id = 3, Description= "sdrdredre", DueDate=DateTime.Now.AddMonths(1), IsCompleted = false, Title="test3"}
        };
        int yPoint = 40;

        foreach (var task in tasks)
        {
            gfx.DrawString($"{task.Title} - {task.DueDate} - {(task.IsCompleted ? "انجام شده" : "انجام نشده")}",
                font, XBrushes.Black, new XRect(40, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            yPoint += 20;
        }

        document.Save("Tasks.pdf");
        MessageBox.Show("فایل PDF با موفقیت ذخیره شد!");
    }
}

public class BoolToStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? "انجام شده" : "انجام نشده";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}

public class BoolToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? Brushes.LightGreen : Brushes.LightCoral;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}