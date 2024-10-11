﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OnlineEducationPlatform.DAL.Data.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        [ForeignKey("Lecture")]
        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }
    }
}
