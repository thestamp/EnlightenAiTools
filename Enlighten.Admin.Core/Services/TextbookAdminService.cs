using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enlighten.Core.Services;
using Enlighten.Data.Infrastructure;
using Enlighten.Data.Models;

namespace Enlighten.Admin.Core.Services
{
    public class TextbookAdminService : TextbookService
    {
        
        private readonly DataContext _context;

        public TextbookAdminService(DataContext context) : base(context)
        {
            _context = context;
        }

        public Textbook CreateTextbook()
        {
            return new Textbook();
        }

        public void AddTextbook(Textbook textbook)
        {
            _context.Textbooks.Add(textbook);
        }

        public void DeleteTextbook(Textbook textbook)
        {
            _context.Textbooks.Remove(textbook);
        }

        public void AddTextbookChapter(Textbook textbook, TextbookChapter chapter)
        {
            chapter.Textbook = textbook;
            textbook.Chapters.Add(chapter);
        }

        public void DeleteTextbookChapter(TextbookChapter chapter)
        {
            _context.TextbookChapters.Remove(chapter);
        }
    }
}
