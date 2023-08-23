using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enlighten.Core.Services;
using Enlighten.Data.Infrastructure;
using Enlighten.Data.Models;
using Microsoft.EntityFrameworkCore;

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
            return new Textbook()
            {
                Units = new List<TextbookUnit>()
            };
        }

        public void AddTextbook(Textbook textbook)
        {
            _context.Textbooks.Add(textbook);
        }

        public void UpdateTextbook(Textbook textbook)
        {
            // Check if the entity is already being tracked
            var existingTextbook= _context.Textbooks.Find(textbook.Id);

            // If not, attach and set its state to Modified to flag for an update
            if (existingTextbook == null)
            {
                _context.Textbooks.Attach(textbook);
                _context.Entry(textbook).State = EntityState.Modified;
            }
            else
            {
                // If already tracked, you can update the entry with the new values
                _context.Entry(existingTextbook).CurrentValues.SetValues(textbook);
            }
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
