using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enlighten.Data.Models;

namespace Enlighten.Study.Core.Services
{
    public class StudyTopicTrackerService
    {
        public List<UnitTopicTrackerModel> TrackerUnits { get; set; } 

        //resume from previous
        public StudyTopicTrackerService(List<UnitTopicTrackerModel> units)
        {
            TrackerUnits = units;
        }

        //initialize new
        public StudyTopicTrackerService(Textbook textbook)
        {
            TrackerUnits = new List<UnitTopicTrackerModel>();
            foreach (var unitElement in textbook.Units)
            {
                var unit = new UnitTopicTrackerModel(unitElement);

                var topicElements = unitElement.TopicList.Split(',');

                foreach (var topicElement in topicElements)
                {
                    unit.Topics.Add(new TopicTrackerModel(unitElement, topicElement));
                }

                TrackerUnits.Add(unit);
            }
        }

        public record UnitTopicTrackerModel
        {
            public UnitTopicTrackerModel(TextbookUnit unit)
            {
                Unit = unit;
                Topics = new List<TopicTrackerModel>();
            }

            public TextbookUnit Unit { get; set; }
            public List<TopicTrackerModel> Topics { get; set; }
        }

        public record TopicTrackerModel
        {
            public TopicTrackerModel(TextbookUnit unit, string topic)
            {
                Unit = unit;
                Topic = topic;
                AttemptResults = new List<bool>();
            }

            public TextbookUnit Unit { get; }
            public string Topic { get; set; }
            public List<bool> AttemptResults { get; set; }
        }

        public void AddAttemptResult(TextbookUnit unit, string topic, bool isCorrect)
        {
            var foundUnit = TrackerUnits.First(i => i.Unit.Id == unit.Id);

            var foundTopic = foundUnit.Topics.First(i => i.Topic == topic);
            
            foundTopic.AttemptResults.Add(isCorrect);
        }

       
    }
}
