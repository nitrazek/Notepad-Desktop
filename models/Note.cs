﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadDesktop.models
{
    public class Note
    {
        private Guid id;
        private string title;
        private string content;
        private int? password;
        private DateTime creatingDate;
        private DateTime? notificationDate;

        public Guid Id { get { return id; } }
        public string Title { get { return title; } set { title = value; } }
        public string Content { get { return content; } set { content = value; } }
        public int? Password { get { return password; } set { password = value; } }
        public DateTime CreatingDate { get { return creatingDate; } set { creatingDate = value; } }
        public DateTime? NotificationDate { get { return notificationDate; } set { notificationDate = value; } }

        public Note()
        { 
            id = Guid.NewGuid();
            title = string.Empty;
            content = string.Empty;
            password = null;
            notificationDate = null;
            creatingDate = DateTime.Now;
        }

        public Note(string title, string content = "", int? password = null, DateTime? notificationDate = null)
        {
            id = Guid.NewGuid();
            this.title = title;
            this.content = content;
            this.password = password;
            this.notificationDate = notificationDate;
            creatingDate = DateTime.Now;
        }

        public Note(Note oldNote)
        {
            id = oldNote.Id;
            title = oldNote.Title;
            content = oldNote.Content;
            password = oldNote.Password;
            creatingDate = oldNote.creatingDate;
            notificationDate = oldNote.NotificationDate;
        }
    }
}
