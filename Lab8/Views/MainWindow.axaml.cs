
using Avalonia.Controls;
using System;
using System.IO;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Interactivity;
using Lab8.ViewModels;
using Lab8.Models;

namespace Lab8.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.FindControl<Button>("Add0").Click += async delegate
            {
                var context = (this.DataContext as MainWindowViewModel);
                Note newnote = new Note("New Note", "");
                context.Scheduled.Add(newnote);
            };
            this.FindControl<Button>("Add1").Click += async delegate
            {
                var context = (this.DataContext as MainWindowViewModel);
                Note newnote = new Note("New Note", "");
                context.InWork.Add(newnote);
            };
            this.FindControl<Button>("Add2").Click += async delegate
            {
                var context = (this.DataContext as MainWindowViewModel);
                Note newnote = new Note("New Note", "");
                context.Completed.Add(newnote);
            };
            this.FindControl<MenuItem>("About").Click += async delegate
            {
                await new About().ShowDialog((Window)this.VisualRoot);
            };
            this.FindControl<MenuItem>("Exit").Click += async delegate
            {
                this.Close();
            };
            this.FindControl<MenuItem>("Save").Click += async delegate
            {
                string? path;
                var taskPath = new SaveFileDialog()
                {
                    Title = "Save",
                    Filters = null
                };

                path = await taskPath.ShowAsync((Window)this.VisualRoot);
                if (path is not null)
                {
                    var items = (this.DataContext as MainWindowViewModel).Scheduled;
                    var items2 = (this.DataContext as MainWindowViewModel).InWork;
                    var items3 = (this.DataContext as MainWindowViewModel).Completed;
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(items.Count.ToString());
                        foreach (var item in items)
                        {
                            sw.WriteLine(item.Head);
                            sw.WriteLine(item.Task);
                            sw.WriteLine(item.PathImage);
                        }
                        sw.WriteLine(items2.Count.ToString());
                        foreach (var item in items2)
                        {
                            sw.WriteLine(item.Head);
                            sw.WriteLine(item.Task);
                            sw.WriteLine(item.PathImage);
                        }
                        sw.WriteLine(items3.Count.ToString());
                        foreach (var item in items3)
                        {
                            sw.WriteLine(item.Head);
                            sw.WriteLine(item.Task);
                            sw.WriteLine(item.PathImage);
                        }

                    }
                }

            };

            this.FindControl<MenuItem>("Load").Click += async delegate
            {
                string? path;
                var taskPath = new OpenFileDialog()
                {
                    Title = "Open file",
                    Filters = null
                };

                string[]? pathArray = await taskPath.ShowAsync((Window)this.VisualRoot);
                path = pathArray is null ? null : string.Join(@"\", pathArray);
                if (pathArray != null) path = string.Join(@"\", pathArray);

                if (path is not null)
                {
                    var items = (this.DataContext as MainWindowViewModel).Scheduled;
                    var items2 = (this.DataContext as MainWindowViewModel).InWork;
                    var items3 = (this.DataContext as MainWindowViewModel).Completed;
                    items.Clear();
                    items2.Clear();
                    items3.Clear();
                    using (StreamReader sr = File.OpenText(path))
                    {
                        int count;
                        if (!Int32.TryParse(sr.ReadLine(), out count)) return;
                        for (int i = 0; i < count; i++)
                        {
                            items.Add
                                (new Note(sr.ReadLine(), sr.ReadLine(), sr.ReadLine()));
                        }
                        if (!Int32.TryParse(sr.ReadLine(), out count)) return;
                        for (int i = 0; i < count; i++)
                        {
                            items2.Add
                                (new Note(sr.ReadLine(), sr.ReadLine(), sr.ReadLine()));
                        }
                        if (!Int32.TryParse(sr.ReadLine(), out count)) return;
                        for (int i = 0; i < count; i++)
                        {
                            items3.Add
                                (new Note(sr.ReadLine(), sr.ReadLine(), sr.ReadLine()));
                        }

                    }

                }
            };

            this.FindControl<MenuItem>("New").Click += async delegate
            {
                var items = (this.DataContext as MainWindowViewModel).Scheduled;
                var items2 = (this.DataContext as MainWindowViewModel).InWork;
                var items3 = (this.DataContext as MainWindowViewModel).Completed;
                items.Clear();
                items2.Clear();
                items3.Clear();
            };

        }
    }
}
