using System;
using System.Collections.Generic;
using System.Text;

namespace FF.Job.Model.DTOs
{
    public class GameDto
    {
        public int ApiId { get; set; }

        public string GamePlatform { get; set; }

        public long DownloadLastVersionKeyAuto { get; set; }

        public DateTime DownloadLastDataTimeAuto { get; set; }
    }
}
