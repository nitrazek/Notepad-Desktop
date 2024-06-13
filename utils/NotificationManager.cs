using Microsoft.Toolkit.Uwp.Notifications;
using NotepadDesktop.models;
using NotepadDesktop.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadDesktop.utils
{
    public class NotificationManager
    {
        private FolderRepository folderRepository;
        private Dictionary<Guid, Timer> timers = new Dictionary<Guid, Timer>();

        public NotificationManager(FolderRepository folderRepository)
        {
            this.folderRepository = folderRepository;
        }

        public void ScheduleNotification(DateTime? scheduledTime, string title, string content, Guid noteId)
        {
            if (scheduledTime == null) return;

            var timeToWait = (int)(scheduledTime - DateTime.Now).Value.TotalMilliseconds;
            if (timeToWait <= 0)
            {
                return;
                //ShowNotification(content);
            }
            else
            {
                if (timers.ContainsKey(noteId)) timers[noteId].Dispose();

                timers[noteId] = new Timer((state) =>
                {
                    if (timers.ContainsKey(noteId)) timers.Remove(noteId);

                    Note? note = folderRepository.GetNoteById(noteId);
                    if (note != null && note.NotificationDate != null && note.NotificationDate.Value.CompareTo(scheduledTime) == 0)
                        ShowNotification(title, content);
                }, null, timeToWait, Timeout.Infinite);
            }
        }

        private void ShowNotification(string title, string content)
        {
            new ToastContentBuilder()
                .AddText("NotepadDesktop")
                .AddText(title)
                .AddText(content)
                .Show();
        }
    }
}
