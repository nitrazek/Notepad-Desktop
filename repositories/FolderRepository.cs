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
    public class FolderRepository
    {
        private string DB_PATH = $"{AppDomain.CurrentDomain.BaseDirectory}/DB.json";
        private List<Folder> folders = new List<Folder>();
        private Task? savingTask = null;

        public FolderRepository()
        {
            ReadFromJson();
        }

        public void AddFolder(Folder folder)
        {
            folders.Add(folder);
            SaveToJson();
        }

        public Folder? GetFolderById(Guid id)
        {
            Folder? folder = folders.Find(x => x.Id == id);
            if (folder == null) return null;
            return new Folder(folder);
        }

        public Folder? GetFolderByNoteId(Guid noteId)
        {
            Folder? folder = folders.Find(x => x.Notes.Find(y => y.Id == noteId) != null);
            if (folder == null) return null;
            return folder;
        }

        public Note? GetNoteById(Guid noteId)
        {
            for (int i = 0; i < folders.Count; i++)
            {
                Folder folder = folders[i];
                for (int j = 0; j < folder.Notes.Count; j++)
                {
                    Note note = folder.Notes[j];
                    if(note.Id == noteId) return note;
                }
            }
            return null;
        }

        public List<Folder> GetAllFolders()
        {
            return folders.Select(folder => new Folder(folder)).ToList();
        }

        public void UpdateFolder(Folder editedFolder)
        {
            int index = folders.FindIndex(Folder => Folder.Id == editedFolder.Id);
            if (index == -1) return;

            folders[index] = new Folder(editedFolder);
            SaveToJson();
        }

        public void DeleteFolder(Folder deleteFolder)
        {
            Folder? folder = folders.Find(folder => folder.Id == deleteFolder.Id);
            if (folder == null) return;

            folders.Remove(folder);
            SaveToJson();
        }

        public async Task SaveToJson()
        {
            if (savingTask != null)
            {
                await savingTask;
            }

            savingTask = Task.Run(() =>
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(folders, options);
                File.WriteAllText(DB_PATH, jsonString);
            });
        }

        public void ReadFromJson()
        {
            if (!File.Exists(DB_PATH))
            {
                SaveToJson();
                return;
            }

            string jsonString = File.ReadAllText(DB_PATH);
            List<Folder>? jsonList = JsonSerializer.Deserialize<List<Folder>>(jsonString);
            if (jsonList != null)
                folders = jsonList;
        }
    }
}
