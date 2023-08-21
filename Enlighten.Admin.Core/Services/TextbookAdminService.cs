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

        public void AddTextbookUnit(Textbook textbook, TextbookUnit unit)
        {
            unit.Textbook = textbook;
            textbook.Units.Add(unit);
        }

        public void DeleteTextbookUnit(TextbookUnit unit)
        {
            _context.TextbookUnits.Remove(unit);
        }
    }
}
