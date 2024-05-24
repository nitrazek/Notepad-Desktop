using NotepadDesktop.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NotepadDesktop.repositories
{
    public class NoteRepository
    {
        private string DB_PATH = $"{AppDomain.CurrentDomain.BaseDirectory}/DB.json";
        List<Note> notes = new List<Note>();

        public NoteRepository()
        {
            ReadFromJson();
        }

        public void AddNote(Note note)
        {
            notes.Add(note);
            SaveToJson();
        }

        public Note? GetNoteById(Guid Id)
        {
            Note? note = notes.Find(note => note.Id == Id);
            if (note == null) return null;
            return new Note(note);
        }

        public List<Note> GetAllNotes()
        {
            return notes.Select(note => new Note(note)).ToList();
        }

        public void UpdateNote(Note editedNote)
        {
            int index = notes.FindIndex(note => note.Id == editedNote.Id);
            if(index <= -1) return;
            
            notes[index] = new Note(editedNote);
            SaveToJson();
        }

        public void DeleteNote(Note deleteNote)
        {
            Note? note = notes.Find(note => note.Id == deleteNote.Id);
            if (note == null) return;
            
            notes.Remove(note);
            SaveToJson();
        }

        public void SaveToJson()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(notes, options);
            File.WriteAllText(DB_PATH, jsonString);
        }

        public void ReadFromJson()
        {
            if (!File.Exists(DB_PATH))
            {
                File.WriteAllText(DB_PATH, "[]");
            }
            string jsonString = File.ReadAllText(DB_PATH);
            List<Note>? jsonList = JsonSerializer.Deserialize<List<Note>>(jsonString);
            if (jsonList != null)
                notes = jsonList;
        }
    }
}
