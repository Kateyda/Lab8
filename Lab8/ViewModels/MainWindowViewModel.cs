using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Interactivity;
using System.ComponentModel;
using ReactiveUI;
using System.Reactive;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Lab8.Models;

namespace Lab8.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Scheduled = BuildAllNotesSchedelued();
            InWork = BuildAllNotesInWork();
            Completed = BuildAllNotesCompleted();
            DeleteScheduled = ReactiveCommand.Create<Note>((item) =>
            {
                Scheduled.Remove(item);
            });
            DeleteInWork = ReactiveCommand.Create<Note>((item) =>
            {
                InWork.Remove(item);
            });
            DeleteCompleted = ReactiveCommand.Create<Note>((item) =>
            {
                Completed.Remove(item);
            });
            ButtonAddImage = ReactiveCommand.Create<Note, Unit>((item) =>
            {
                OpenImage(item);
                return Unit.Default;
            });


        }
        public ObservableCollection<Note> Scheduled { get; set; }

        private ObservableCollection<Note> BuildAllNotesSchedelued()
        {
            return new ObservableCollection<Note>
            {
                new Note("Planned Task 1","Task 1"),
                new Note("Planned Task 2","Task 2"),
                new Note("Planned Task 3","Task 3"),
            };
        }

        public ObservableCollection<Note> InWork { get; set; }

        private ObservableCollection<Note> BuildAllNotesInWork()
        {
            return new ObservableCollection<Note>
            {
                new Note("InWork Task 1","Task 1"),
                new Note("InWork Task 2","Task 2"),
                new Note("InWork Task 3","Task 3"),
            };
        }

        public ObservableCollection<Note> Completed { get; set; }

        private ObservableCollection<Note> BuildAllNotesCompleted()
        {
            return new ObservableCollection<Note>
            {
                new Note("Complete Task 1","Task 1"),
                new Note("Complete Task 2","Task 2"),
                new Note("Complete Task 3","Task 3"),
                new Note("Complete Task 4","Task 4"),
                new Note("Complete Task 5","Task 5"),
            };
        }

        public ReactiveCommand<Note, Unit> DeleteScheduled { get; }
        public ReactiveCommand<Note, Unit> DeleteInWork { get; }
        public ReactiveCommand<Note, Unit> DeleteCompleted { get; }
        public ReactiveCommand<Note, Unit> ButtonAddImage { get; }

        private async void OpenImage(Note item)
        {
            var taskPathOut = new OpenFileDialog()
            {
                Title = "Choose file",
                Filters = null
            }.ShowAsync(view);

            string[]? path2 = await taskPathOut;
            if (path2 != null)
            {
                item.PathImage = string.Join(@"\", path2);
            }
        }
        public Window? view;
    }
}
