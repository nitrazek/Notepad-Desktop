using System;
using System.Collections.Generic;
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
        private int password;

        public Guid Id { get { return id; } }
        public string Title { get { return title; } set { title = value; } }
        public string Content { get { return content; } set { content = value; } }
        public int Password { get { return password; } set { password = value; } }

        public Note()
        {
            id = Guid.NewGuid();
            title = string.Empty;
            content = string.Empty;
            password = 0;
        }

        public Note(Note oldNote)
        {
            id = oldNote.Id;
            title = oldNote.Title;
            content = oldNote.Content;
            password = oldNote.Password;
        }

        public Note(string title, string content) {
            id = Guid.NewGuid();
            this.title = title;
            this.content = content;
            password = 0;
        }

        public Note(string title, string content, int password)
        {
            id = Guid.NewGuid();
            this.title = title;
            this.content = content;
            this.password = password;
        }
    }
}
