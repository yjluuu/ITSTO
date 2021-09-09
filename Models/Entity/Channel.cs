using System;
using System.Collections.Generic;
using System.Text;

namespace Routine.Models.Entity
{
    public class Channel
    {
        public int Id { get; set; }
        public string ChannelName { get; set; }
        public string Brand { get; set; }
        public string ChannelType { get; set; }
        public string AppId { get; set; }
        public string Secret { get; set; }
        public bool IsDeleted { get; set; }
    }
}
