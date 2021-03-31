using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalFantasyTacticsPartyBuilderBlazor.ViewModels
{
    public class JobTreantNodeViewModel
    {
        public string RequiredGender { get; set; }

        public string ImagePath { get; set; }

        public string JobName { get; set; }

        public string IdentifierName { get; set; }

        public string RequiredJobName { get; set; }

        public string RequiredJobLevelPath { get; set; }

        public List<string> JobPrerequisiteNames { get; set; }
    }
}
